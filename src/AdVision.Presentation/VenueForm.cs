using AdVision.Application.Generators;
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
        private readonly Image _exception = Image.FromFile("..\\..\\..\\Resources\\exception.png");
        private readonly Image _success = Image.FromFile("..\\..\\..\\Resources\\success.png");

        private readonly IQueryHandler<IReadOnlyList<VenueTypeDto>, GetAllVenueTypesQuery> _venueTypesQueryHandler;
        private readonly ICommandHandler<Guid, CreateVenueCommand> _venueCommandHandler;
        private readonly ILogger<VenueForm> _logger;
        private readonly IServiceProvider _serviceProvider;
        private readonly CancellationTokenSource _cts = new();
        private readonly IVenueFakeGenerator _venueFakeGenerator;
        private readonly INotificationService _notificationService;

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
        }

        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            ReloadVenueTypes();

            if (cbVenueTypes.Items.Count <= 0)
            {
                pbCityValidation.Image = _exception;
                pbDescriptionValidation.Image = _exception;
                pbDistrictValidation.Image = _exception;
                pbHeightValidation.Image = _exception;
                pbHouseNumberValidation.Image = _exception;
                pbLatitudeValidation.Image = _exception;
                pbLongitudeValidation.Image = _exception;
                pbNameValidation.Image = _exception;
                pbRatingValidation.Image = _success;
                pbRegionValidation.Image = _exception;
                pbStreetValidation.Image = _exception;
                pbWidthValidation.Image = _exception;
                pbVenueTypeValidation.Image = _exception;
                btnSave.Enabled = false;
                btnGenerate.Enabled = false;
                _notificationService.ShowInfo("Как запустить генерацию",
                    "Для запуска генерации нужно создать хотя бы один тип площадки");
                return;
            }


            cbVenueTypes.SelectedIndex = 0;
            Generate();
        }

        private async void ReloadVenueTypes(string? name = null)
        {
            try
            {
                cbVenueTypes.DataSource = null;

                var result = await _venueTypesQueryHandler.Handle(
                    new GetAllVenueTypesQuery(),
                    _cts.Token);

                if (result.IsFailure)
                {
                    _logger.LogError("Не удалось загрузить типы площадок: {Error}", result.Error);
                    _notificationService.ShowError("Ошибка загрузки типов площадок",
                        string.Join(Environment.NewLine, result.Error));
                    return;
                }

                var list = result.Value
                    .OrderBy(x => x.Name)
                    .ToList();

                cbVenueTypes.DataSource = list;
                cbVenueTypes.DisplayMember = "Name";
                cbVenueTypes.ValueMember = "Id";

                if (!string.IsNullOrEmpty(name))
                {
                    cbVenueTypes.SelectedIndex = cbVenueTypes.FindString(name);
                }
                else if (list.Count > 0)
                {
                    cbVenueTypes.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                _notificationService.ShowError("Ошибка загрузки типов площадок", ex.Message);
                _logger.LogError(ex, "Ошибка загрузки типов площадок: {Message}", ex.Message);
            }
        }

        private void OpenButton_Click(object sender, EventArgs e)
        {
            var form = _serviceProvider.GetRequiredService<VenueTypeForm>();
            form.VenueTypeCreated += ReloadVenueTypes;
            form.ShowDialog();
        }

        private void BtnGenerate_Click(object sender, EventArgs e)
        {
            Generate();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private static bool IsValidLength(Control control, int min, int max)
        {
            var len = control.Text.Trim().Length;

            if (len < min || len > max)
            {
                return false;
            }

            return true;
        }

        private static bool IsDoubleValid(Control control, double min, double max)
        {
            var result = double.TryParse(control.Text.Trim(), out var value);

            if (!result)
            {
                return false;
            }

            if (value < min || value > max)
            {
                return false;
            }

            return true;
        }

        private async void BtnSave_Click(object sender, EventArgs e)
        {
            var selectedVenueType = cbVenueTypes.SelectedItem;

            if (selectedVenueType is not VenueTypeDto dto)
            {
                _logger.LogError("Не удалось преобразовать {VenueType} к типу VenueTypeDto", selectedVenueType);
                return;
            }

            if (!double.TryParse(txtLatitude.Text.Trim(), out var latitude))
            {
                _notificationService.ShowError("Не удалось преобразовать {Latitude} в тип double",
                    txtLatitude.Text.Trim());
                return;
            }

            if (!double.TryParse(txtLongitude.Text.Trim(), out var longitude))
            {
                _notificationService.ShowError("Не удалось преобразовать {Longitude} в тип double",
                    txtLongitude.Text.Trim());
                return;
            }

            if (!double.TryParse(txtWidth.Text.Trim(), out var width))
            {
                _notificationService.ShowError("Не удалось преобразовать {Width} в тип double",
                    txtWidth.Text.Trim());
                return;
            }

            if (!double.TryParse(txtHeight.Text.Trim(), out var height))
            {
                _notificationService.ShowError("Не удалось преобразовать {Height} в тип double",
                    txtHeight.Text.Trim());
                return;
            }

            var venueDto = new CreateVenueDto(
                txtName.Text.Trim(),
                dto,
                new AddressDto(
                    Region: txtRegion.Text.Trim(),
                    District: txtDistrict.Text.Trim(),
                    City: txtCity.Text.Trim(),
                    Street: txtStreet.Text.Trim(),
                    House: txtHouseNumber.Text.Trim(),
                    Latitude: latitude,
                    Longitude: longitude),
                new VenueSizeDto(width, height),
                (double)Math.Round(nudRating.Value, 0),
                txtDescription.Text
            );

            try
            {
                var result = await _venueCommandHandler.Handle(new CreateVenueCommand(venueDto), _cts.Token);

                if (result.IsFailure)
                {
                    _logger.LogError("Не удалось создать новую площадку: {Error}", result.Error);
                    _notificationService.ShowError("Ошибка создания новой площадки", string.Join("", result.Error));
                    return;
                }

                _logger.LogInformation("Создана новая площадка с id = {Id}", result.Value);
                _notificationService.ShowSuccess("Добавлена новая площадка", $"Id = {result.Value}");
            }
            catch (Exception exception)
            {
               _logger.LogError("Произошла непредвиденная ошибка в процессе создания новой площадки: {Exception}", exception);
               _notificationService.ShowError("Непредвиденная ошибка в процессе создания новой площадки", exception.Message);
            }
        }

        private void NudRating_ValueChanged(object sender, EventArgs e)
        {
            var success = nudRating.Value is > 0 and <= 10;
            SetIcon(pbRatingValidation, success);
            btnSave.Enabled = !HasErrors();
        }

        private void CbVenueTypes_TextChanged(object sender, EventArgs e)
        {
            var hasSelectedItem = cbVenueTypes.SelectedIndex > -1;
            SetIcon(pbVenueTypeValidation, hasSelectedItem);
            btnSave.Enabled = !HasErrors();
        }

        private void TxtName_TextChanged(object sender, EventArgs e)
        {
            var success = IsValidLength(txtName, VenueName.MIN_LENGTH, VenueName.MAX_LENGTH);
            SetIcon(pbNameValidation, success);
            btnSave.Enabled = !HasErrors();
        }

        private void SetIcon(PictureBox box, bool success)
        {
            box.Image = success ? _success : _exception;
        }

        private void Generate()
        {
            if (cbVenueTypes.SelectedIndex < 0)
            {
                _notificationService.ShowError("Ошибка генерации", "Сначала нужно добавить тип площадки");
                return;
            }

            var venueType = cbVenueTypes?.SelectedItem;

            if (venueType is not VenueTypeDto dto)
            {
                _logger.LogError("Не удалось преобразовать {VenueType} к типу VenueTypeDto", venueType);
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

        private void TxtDescription_TextChanged(object sender, EventArgs e)
        {
            var success = IsValidLength(txtDescription, VenueDescription.MIN_LENGTH, VenueDescription.MAX_LENGTH);
            SetIcon(pbDescriptionValidation, success);
            btnSave.Enabled = !HasErrors();
        }

        private void TxtHouseNumber_TextChanged(object sender, EventArgs e)
        {
            var hasValue = !string.IsNullOrEmpty(txtHouseNumber.Text.Trim());
            SetIcon(pbHouseNumberValidation, hasValue);
            btnSave.Enabled = !HasErrors();
        }

        private void TxtRegion_TextChanged(object sender, EventArgs e)
        {
            var success = IsValidLength(txtRegion, VenueAddress.MIN_LENGTH, VenueAddress.MAX_LENGTH);
            SetIcon(pbRegionValidation, success);
            btnSave.Enabled = !HasErrors();
        }

        private void TxtDistrict_TextChanged(object sender, EventArgs e)
        {
            var success = IsValidLength(txtDistrict, VenueAddress.MIN_LENGTH, VenueAddress.MAX_LENGTH);
            SetIcon(pbDistrictValidation, success);
            btnSave.Enabled = !HasErrors();
        }

        private void TxtCity_TextChanged(object sender, EventArgs e)
        {
            var success = IsValidLength(txtCity, VenueAddress.MIN_LENGTH, VenueAddress.MAX_LENGTH);
            SetIcon(pbCityValidation, success);
            btnSave.Enabled = !HasErrors();
        }

        private void TxtStreet_TextChanged(object sender, EventArgs e)
        {
            var success = IsValidLength(txtStreet, VenueAddress.MIN_LENGTH, VenueAddress.MAX_LENGTH);
            SetIcon(pbStreetValidation, success);
            btnSave.Enabled = !HasErrors();
        }

        private void TxtLatitude_TextChanged(object sender, EventArgs e)
        {
            var success = IsDoubleValid(txtLatitude, VenueAddress.MIN_LATITUDE_VALUE, VenueAddress.MAX_LATITUDE_VALUE);
            SetIcon(pbLatitudeValidation, success);
            btnSave.Enabled = !HasErrors();
        }

        private void TxtLongitude_TextChanged(object sender, EventArgs e)
        {
            var success = IsDoubleValid(txtLongitude, VenueAddress.MIN_LONGITUDE_VALUE,
                VenueAddress.MAX_LONGITUDE_VALUE);
            SetIcon(pbLongitudeValidation, success);
            btnSave.Enabled = !HasErrors();
        }

        private void TxtWidth_TextChanged(object sender, EventArgs e)
        {
            var success = IsDoubleValid(txtWidth, VenueSize.MIN_WIDTH, VenueSize.MAX_WIDTH);
            SetIcon(pbWidthValidation, success);
            btnSave.Enabled = !HasErrors();
        }

        private void TxtHeight_TextChanged(object sender, EventArgs e)
        {
            var success = IsDoubleValid(txtHeight, VenueSize.MIN_WIDTH, VenueSize.MAX_WIDTH);
            SetIcon(pbHeightValidation, success);
            btnSave.Enabled = !HasErrors();
        }

        private void CbVenueTypes_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cbVenueTypes.SelectedIndex >= 0)
            {
                btnGenerate.Enabled = true;
            }
        }

        private bool HasErrors()
        {
            var hasErrors =
                pbCityValidation.Image == _exception ||
                pbDescriptionValidation.Image == _exception ||
                pbDistrictValidation.Image == _exception ||
                pbHeightValidation.Image == _exception ||
                pbHouseNumberValidation.Image == _exception ||
                pbLatitudeValidation.Image == _exception ||
                pbLongitudeValidation.Image == _exception ||
                pbNameValidation.Image == _exception ||
                pbRegionValidation.Image == _exception ||
                pbStreetValidation.Image == _exception ||
                pbWidthValidation.Image == _exception ||
                pbVenueTypeValidation.Image == _exception;

            return hasErrors;
        }

        private void VenueForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            _cts.Dispose();
        }
    }
}