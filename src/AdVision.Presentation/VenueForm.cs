using AdVision.Application.Generators;
using AdVision.Application.Generators.Venues;
using AdVision.Application.Venues.CreateVenueCommand;
using AdVision.Application.VenueTypes.GetAllVenueTypesQuery;
using AdVision.Contracts;
using AdVision.Domain.Venues;
using AdVision.Presentation.Notifications;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Shared.Abstractions;

namespace AdVision.Presentation
{
    public partial class VenueForm : Form
    {
        private const string LoadVenueTypesErrorTitle = "Ошибка загрузки типов площадок";
        private const string ValidationErrorTitle = "Ошибка валидации";
        private const string SaveErrorTitle = "Ошибка создания новой площадки";
        private const string SaveSuccessTitle = "Добавлена новая площадка";
        private const string GenerateErrorTitle = "Ошибка генерации";
        private const string UnknownErrorTitle = "Непредвиденная ошибка";

        private readonly IQueryHandler<IReadOnlyList<VenueTypeDto>, GetAllVenueTypesQuery> _venueTypesQueryHandler;
        private readonly ICommandHandler<Guid, CreateVenueCommand> _venueCommandHandler;
        private readonly ILogger<VenueForm> _logger;
        private readonly IServiceProvider _serviceProvider;
        private readonly CancellationTokenSource _cts = new();
        private readonly IVenueFakeGenerator _venueFakeGenerator;
        private readonly INotificationService _notificationService;

        private bool _isSaving;
        private bool _isLoadingVenueTypes;
        
        public event Action? VenueCreated;

        public VenueForm(
            IVenueFakeGenerator venueFakeGenerator,
            INotificationService notificationService,
            IQueryHandler<IReadOnlyList<VenueTypeDto>, GetAllVenueTypesQuery> venueTypesQueryHandler,
            ICommandHandler<Guid, CreateVenueCommand> venueCommandHandler,
            IServiceProvider serviceProvider,
            ILogger<VenueForm> logger)
        {
            _venueFakeGenerator = venueFakeGenerator;
            _notificationService = notificationService;
            _venueTypesQueryHandler = venueTypesQueryHandler;
            _venueCommandHandler = venueCommandHandler;
            _logger = logger;
            _serviceProvider = serviceProvider;

            InitializeComponent();
            UpdateValidationState();
        }

        protected override async void OnLoad(EventArgs e)
        {
            try
            {
                base.OnLoad(e);
                await ReloadVenueTypesAsync();

                if (!HasVenueTypes())
                {
                    SetInitialInvalidState();
                    btnSave.Enabled = false;
                    btnGenerate.Enabled = false;

                    _notificationService.ShowInfo(
                        "Как запустить генерацию",
                        "Для запуска генерации нужно создать хотя бы один тип площадки");

                    return;
                }

                cbVenueTypes.SelectedIndex = 0;
                Generate();
                UpdateValidationState();
            }
            catch (OperationCanceledException)
            {
                _logger.LogInformation("Загрузка формы площадки была отменена");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка инициализации формы площадки");
                _notificationService.ShowError(UnknownErrorTitle, ex.Message);
            }
        }

        private async Task ReloadVenueTypesAsync(string? selectedName = null)
        {
            if (_isLoadingVenueTypes)
            {
                return;
            }

            _isLoadingVenueTypes = true;
            UseWaitCursor = true;

            try
            {
                cbVenueTypes.DataSource = null;

                var result = await _venueTypesQueryHandler.Handle(
                    new GetAllVenueTypesQuery(),
                    _cts.Token);

                if (result.IsFailure)
                {
                    var errors = string.Join(Environment.NewLine, result.Error);
                    _logger.LogError("Не удалось загрузить типы площадок: {Error}", errors);

                    _notificationService.ShowError(
                        LoadVenueTypesErrorTitle,
                        string.IsNullOrWhiteSpace(errors) ? "Не удалось загрузить типы площадок" : errors);

                    return;
                }

                var list = result.Value
                    .OrderBy(x => x.Name)
                    .ToList();

                cbVenueTypes.DataSource = list;
                cbVenueTypes.DisplayMember = nameof(VenueTypeDto.Name);
                cbVenueTypes.ValueMember = nameof(VenueTypeDto.Id);

                if (!string.IsNullOrWhiteSpace(selectedName))
                {
                    var index = cbVenueTypes.FindStringExact(selectedName);
                    cbVenueTypes.SelectedIndex = index >= 0 ? index : (list.Count > 0 ? 0 : -1);
                }
                else
                {
                    cbVenueTypes.SelectedIndex = list.Count > 0 ? 0 : -1;
                }
            }
            finally
            {
                _isLoadingVenueTypes = false;
                UseWaitCursor = false;
                UpdateValidationState();
            }
        }

        private void OpenButton_Click(object sender, EventArgs e)
        {
            var form = _serviceProvider.GetRequiredService<VenueTypeForm>();
            form.VenueTypeCreated += OnVenueTypeCreated;
            form.ShowDialog();
            form.VenueTypeCreated -= OnVenueTypeCreated;
        }

        private async void OnVenueTypeCreated(string name)
        {
            try
            {
                await ReloadVenueTypesAsync(name);
            }
            catch (OperationCanceledException)
            {
                _logger.LogInformation("Обновление списка типов площадок отменено");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка обновления списка типов площадок");
                _notificationService.ShowError(LoadVenueTypesErrorTitle, ex.Message);
            }
        }

        private void BtnGenerate_Click(object sender, EventArgs e)
        {
            Generate();
            UpdateValidationState();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private async void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                await SaveVenueAsync();
            }
            catch (OperationCanceledException)
            {
                _logger.LogInformation("Сохранение площадки отменено");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Непредвиденная ошибка при создании новой площадки");
                _notificationService.ShowError(UnknownErrorTitle, $"{SaveErrorTitle}: {ex.Message}");
            }
        }

        private async Task SaveVenueAsync()
        {
            if (_isSaving)
            {
                return;
            }

            if (!TryBuildVenueDto(out var venueDto))
            {
                ShowValidationError();
                UpdateValidationState();
                return;
            }

            _isSaving = true;
            UseWaitCursor = true;
            UpdateValidationState();

            try
            {
                var result = await _venueCommandHandler.Handle(
                    new CreateVenueCommand(venueDto),
                    _cts.Token);

                if (result.IsFailure)
                {
                    var errors = string.Join(Environment.NewLine, result.Error);
                    ShowSaveError(errors);
                    return;
                }

                _logger.LogInformation("Создана новая площадка с id = {Id}", result.Value);
                _notificationService.ShowSuccess(SaveSuccessTitle, $"Id = {result.Value}");
                
                VenueCreated?.Invoke();
            }
            finally
            {
                _isSaving = false;
                UseWaitCursor = false;

                if (!IsDisposed)
                {
                    UpdateValidationState();
                }
            }
        }

        private bool TryBuildVenueDto(out CreateVenueDto venueDto)
        {
            venueDto = default!;

            if (cbVenueTypes.SelectedItem is not VenueTypeDto venueTypeDto)
            {
                _logger.LogError("Не удалось преобразовать выбранный тип площадки к {Type}", nameof(VenueTypeDto));
                return false;
            }

            var name = txtName.Text.Trim();
            var region = txtRegion.Text.Trim();
            var district = txtDistrict.Text.Trim();
            var city = txtCity.Text.Trim();
            var street = txtStreet.Text.Trim();
            var houseNumber = txtHouseNumber.Text.Trim();
            var description = txtDescription.Text.Trim();

            if (!IsNameValid(name) ||
                !IsAddressPartValid(region) ||
                !IsAddressPartValid(district) ||
                !IsAddressPartValid(city) ||
                !IsAddressPartValid(street) ||
                string.IsNullOrWhiteSpace(houseNumber) ||
                !IsDescriptionValid(description))
            {
                return false;
            }

            if (!TryParseDouble(txtLatitude.Text, out var latitude) ||
                !IsLatitudeValid(latitude))
            {
                return false;
            }

            if (!TryParseDouble(txtLongitude.Text, out var longitude) ||
                !IsLongitudeValid(longitude))
            {
                return false;
            }

            if (!TryParseDouble(txtWidth.Text, out var width) ||
                !IsWidthValid(width))
            {
                return false;
            }

            if (!TryParseDouble(txtHeight.Text, out var height) ||
                !IsHeightValid(height))
            {
                return false;
            }

            var rating = (double)Math.Round(nudRating.Value, 0);
            if (!IsRatingValid(rating))
            {
                return false;
            }

            venueDto = new CreateVenueDto(
                name,
                venueTypeDto,
                new AddressDto(
                    Region: region,
                    District: district,
                    City: city,
                    Street: street,
                    House: houseNumber,
                    Latitude: latitude,
                    Longitude: longitude),
                new VenueSizeDto(width, height),
                rating,
                description);

            return true;
        }

        private void ShowValidationError()
        {
            _notificationService.ShowError(
                ValidationErrorTitle,
                "Проверьте корректность заполнения всех полей формы");
        }

        private void ShowSaveError(string? message)
        {
            var normalizedMessage = string.IsNullOrWhiteSpace(message)
                ? "Не удалось создать новую площадку"
                : message;

            _logger.LogError("Не удалось создать новую площадку: {Error}", normalizedMessage);
            _notificationService.ShowError(SaveErrorTitle, normalizedMessage);
        }

        private void Generate()
        {
            if (cbVenueTypes.SelectedItem is not VenueTypeDto dto)
            {
                _notificationService.ShowError(GenerateErrorTitle, "Сначала нужно добавить тип площадки");
                return;
            }

            var venue = _venueFakeGenerator.Generate(dto.Name);

            txtName.Text = venue.Name.Value;
            txtRegion.Text = venue.Address.Region;
            txtDistrict.Text = venue.Address.District;
            txtCity.Text = venue.Address.City;
            txtStreet.Text = venue.Address.Street;
            txtHouseNumber.Text = venue.Address.House;
            txtLatitude.Text = venue.Address.Latitude.ToString("N10");
            txtLongitude.Text = venue.Address.Longitude.ToString("N10");
            txtWidth.Text = venue.Size.Width.ToString("N2");
            txtHeight.Text = venue.Size.Height.ToString("N2");
            nudRating.Value = (decimal)venue.Rating.Value;
            txtDescription.Text = venue.Description.Value;
        }

        private void SetInitialInvalidState()
        {
            SetIcon(pbCityValidation, false);
            SetIcon(pbDescriptionValidation, false);
            SetIcon(pbDistrictValidation, false);
            SetIcon(pbHeightValidation, false);
            SetIcon(pbHouseNumberValidation, false);
            SetIcon(pbLatitudeValidation, false);
            SetIcon(pbLongitudeValidation, false);
            SetIcon(pbNameValidation, false);
            SetIcon(pbRatingValidation, true);
            SetIcon(pbRegionValidation, false);
            SetIcon(pbStreetValidation, false);
            SetIcon(pbWidthValidation, false);
            SetIcon(pbVenueTypeValidation, false);
        }

        private void SetIcon(PictureBox box, bool success)
        {
            box.Image = success
                ? Properties.Resources.success
                : Properties.Resources.exception;
        }

        private void UpdateValidationState()
        {
            var hasVenueTypes = HasVenueTypes();

            var nameValid = IsNameValid(txtName.Text.Trim());
            var regionValid = IsAddressPartValid(txtRegion.Text.Trim());
            var districtValid = IsAddressPartValid(txtDistrict.Text.Trim());
            var cityValid = IsAddressPartValid(txtCity.Text.Trim());
            var streetValid = IsAddressPartValid(txtStreet.Text.Trim());
            var houseValid = !string.IsNullOrWhiteSpace(txtHouseNumber.Text.Trim());
            var latitudeValid = TryParseDouble(txtLatitude.Text, out var latitude) && IsLatitudeValid(latitude);
            var longitudeValid = TryParseDouble(txtLongitude.Text, out var longitude) && IsLongitudeValid(longitude);
            var widthValid = TryParseDouble(txtWidth.Text, out var width) && IsWidthValid(width);
            var heightValid = TryParseDouble(txtHeight.Text, out var height) && IsHeightValid(height);
            var descriptionValid = IsDescriptionValid(txtDescription.Text.Trim());
            var ratingValid = IsRatingValid((double)nudRating.Value);
            var venueTypeValid = hasVenueTypes && cbVenueTypes.SelectedIndex >= 0;

            SetIcon(pbNameValidation, nameValid);
            SetIcon(pbRegionValidation, regionValid);
            SetIcon(pbDistrictValidation, districtValid);
            SetIcon(pbCityValidation, cityValid);
            SetIcon(pbStreetValidation, streetValid);
            SetIcon(pbHouseNumberValidation, houseValid);
            SetIcon(pbLatitudeValidation, latitudeValid);
            SetIcon(pbLongitudeValidation, longitudeValid);
            SetIcon(pbWidthValidation, widthValid);
            SetIcon(pbHeightValidation, heightValid);
            SetIcon(pbDescriptionValidation, descriptionValid);
            SetIcon(pbRatingValidation, ratingValid);
            SetIcon(pbVenueTypeValidation, venueTypeValid);

            var isFormValid =
                venueTypeValid &&
                nameValid &&
                regionValid &&
                districtValid &&
                cityValid &&
                streetValid &&
                houseValid &&
                latitudeValid &&
                longitudeValid &&
                widthValid &&
                heightValid &&
                descriptionValid &&
                ratingValid;

            btnGenerate.Enabled = !_isLoadingVenueTypes && hasVenueTypes;
            btnSave.Enabled = !_isSaving && !_isLoadingVenueTypes && isFormValid;
        }

        private bool HasVenueTypes()
        {
            return cbVenueTypes.Items.Count > 0;
        }

        private static bool TryParseDouble(string? text, out double value)
        {
            return double.TryParse(text?.Trim(), out value);
        }

        private static bool IsValidLength(string value, int min, int max)
        {
            var len = value.Trim().Length;
            return len >= min && len <= max;
        }

        private static bool IsNameValid(string value)
        {
            return IsValidLength(value, VenueName.MIN_LENGTH, VenueName.MAX_LENGTH);
        }

        private static bool IsDescriptionValid(string value)
        {
            return IsValidLength(value, VenueDescription.MIN_LENGTH, VenueDescription.MAX_LENGTH);
        }

        private static bool IsAddressPartValid(string value)
        {
            return IsValidLength(value, VenueAddress.MIN_LENGTH, VenueAddress.MAX_LENGTH);
        }

        private static bool IsLatitudeValid(double value)
        {
            return value is >= VenueAddress.MIN_LATITUDE_VALUE and <= VenueAddress.MAX_LATITUDE_VALUE;
        }

        private static bool IsLongitudeValid(double value)
        {
            return value is >= VenueAddress.MIN_LONGITUDE_VALUE and <= VenueAddress.MAX_LONGITUDE_VALUE;
        }

        private static bool IsWidthValid(double value)
        {
            return value is >= VenueSize.MIN_WIDTH and <= VenueSize.MAX_WIDTH;
        }

        private static bool IsHeightValid(double value)
        {
            return value is >= VenueSize.MIN_HEIGHT and <= VenueSize.MAX_HEIGHT;
        }

        private static bool IsRatingValid(double value)
        {
            return value is > 0 and <= 10;
        }

        private void NudRating_ValueChanged(object sender, EventArgs e) => UpdateValidationState();
        private void CbVenueTypes_TextChanged(object sender, EventArgs e) => UpdateValidationState();
        private void TxtName_TextChanged(object sender, EventArgs e) => UpdateValidationState();
        private void TxtDescription_TextChanged(object sender, EventArgs e) => UpdateValidationState();
        private void TxtHouseNumber_TextChanged(object sender, EventArgs e) => UpdateValidationState();
        private void TxtRegion_TextChanged(object sender, EventArgs e) => UpdateValidationState();
        private void TxtDistrict_TextChanged(object sender, EventArgs e) => UpdateValidationState();
        private void TxtCity_TextChanged(object sender, EventArgs e) => UpdateValidationState();
        private void TxtStreet_TextChanged(object sender, EventArgs e) => UpdateValidationState();
        private void TxtLatitude_TextChanged(object sender, EventArgs e) => UpdateValidationState();
        private void TxtLongitude_TextChanged(object sender, EventArgs e) => UpdateValidationState();
        private void TxtWidth_TextChanged(object sender, EventArgs e) => UpdateValidationState();
        private void TxtHeight_TextChanged(object sender, EventArgs e) => UpdateValidationState();
        private void CbVenueTypes_SelectedValueChanged(object sender, EventArgs e) => UpdateValidationState();

        private void VenueForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!_cts.IsCancellationRequested)
            {
                _cts.Cancel();
            }

            _cts.Dispose();
        }
    }
}