using System.ComponentModel;
using AdVision.Application.Generators;
using AdVision.Application.VenueTypes.GetAllVenueTypesQuery;
using AdVision.Contracts;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Shared.Abstractions;

namespace AdVision.Presentation
{
    public partial class VenueForm : Form
    {
        private readonly IQueryHandler<IReadOnlyList<VenueTypeDto>, GetAllVenueTypesQuery> _venueTypesQueryHandler;
        private readonly ILogger<VenueForm> _logger;
        private readonly IServiceProvider _serviceProvider;
        private readonly CancellationTokenSource _cts = new();
        private readonly IVenueFakeGenerator _venueFakeGenerator;

        public VenueForm(
            IVenueFakeGenerator venueFakeGenerator,
            IQueryHandler<IReadOnlyList<VenueTypeDto>, GetAllVenueTypesQuery> venueTypesQueryHandler,
            IServiceProvider serviceProvider,
            ILogger<VenueForm> logger)
        {
            InitializeComponent();

            _venueFakeGenerator = venueFakeGenerator;
            _venueTypesQueryHandler = venueTypesQueryHandler;
            _logger = logger;
            _serviceProvider = serviceProvider;

            txtName.Validating += TxtName_Validating;
            cbVenueTypes.Validating += CbVenueTypes_Validating;
            
            txtLatitude.Validating += TxtLatitude_Validating;
            txtLongitude.Validating += TxtLongitude_Validating;
            
            txtWidth.Validating += TxtWidth_Validating;
            txtHeight.Validating += TxtHeight_Validating;
            
            txtRegion.Validating += TxtRegion_Validating;
            txtDistrict.Validating += TxtDistrict_Validating;
            txtCity.Validating += TxtCity_Validating;
            txtStreet.Validating += TxtStreet_Validating;
            txtHouseNumber.Validating += TxtHouseNumber_Validating;
            
            txtDescription.Validating += TxtDescription_Validating;
        }

        private void TxtDescription_Validating(object? sender, CancelEventArgs e)
        {
            e.Cancel = !ValidateLength(txtRegion, 100, 2000);
        }

        private void TxtHouseNumber_Validating(object? sender, CancelEventArgs e)
        {
            var hasValue = !string.IsNullOrEmpty(txtHouseNumber.Text.Trim());
            errorProvider1.SetError(txtHouseNumber, !hasValue ? "Номер дома не указан" : "");
            e.Cancel = !hasValue;
        }

        private void TxtStreet_Validating(object? sender, CancelEventArgs e)
        {
            e.Cancel = !ValidateLength(txtStreet, 10, 300);
        }

        private void TxtCity_Validating(object? sender, CancelEventArgs e)
        {
            e.Cancel = !ValidateLength(txtCity, 10, 300);
        }

        private void TxtDistrict_Validating(object? sender, CancelEventArgs e)
        {
            e.Cancel = !ValidateLength(txtDistrict, 10, 300);
        }

        private void TxtRegion_Validating(object? sender, CancelEventArgs e)
        {
            e.Cancel = !ValidateLength(txtRegion, 10, 300);
        }

        private void TxtHeight_Validating(object? sender, CancelEventArgs e)
        {
            e.Cancel = !ValidateDouble(txtHeight, 100, 10_000);
        }

        private void TxtWidth_Validating(object? sender, CancelEventArgs e)
        {
            e.Cancel = !ValidateDouble(txtWidth, 100, 10_000);
        }

        private void TxtLongitude_Validating(object? sender, CancelEventArgs e)
        {
            e.Cancel = !ValidateDouble(txtLatitude, -180, 180);
        }

        private void TxtLatitude_Validating(object? sender, CancelEventArgs e)
        {
            e.Cancel = !ValidateDouble(txtLatitude, -90, 90);
        }

        private void CbVenueTypes_Validating(object? sender, CancelEventArgs e)
        {
            var hasSelectedItem = cbVenueTypes.SelectedIndex > -1;
            errorProvider1.SetError(cbVenueTypes, !hasSelectedItem ? "Не выбрана площадка" : "");
            e.Cancel = !hasSelectedItem;
        }

        private void TxtName_Validating(object? sender, CancelEventArgs e)
        {
            e.Cancel = !ValidateLength(txtName, 10, 500);
        }

        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            ReloadVenueTypes();
        }

        private async void ReloadVenueTypes(string? name = null)
        {
            try
            {
                cbVenueTypes.Items.Clear();
                var result = await _venueTypesQueryHandler.Handle(new GetAllVenueTypesQuery(), _cts.Token);

                if (result.IsFailure)
                {
                    _logger.LogError("Не удалось загрузить типы площадок: {Errors}", result.Error);
                    MessageBox.Show(
                        string.Join(Environment.NewLine, result.Error),
                        @"Ошибка загрузки типов площадок",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return;
                }

                foreach (var item in result.Value.OrderBy(x => x.Name))
                {
                    cbVenueTypes.Items.Add(item.Name);
                }

                cbVenueTypes.SelectedIndex = name != null ? cbVenueTypes.FindString(name) : 0;
            }
            catch (Exception ex)
            {
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
            var venueType = cbVenueTypes?.SelectedItem?.ToString() ?? "";
            var venue = _venueFakeGenerator.Generate(venueType);

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
        
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
        
        private bool ValidateLength(Control control, int min, int max)
        {
            var len = control.Text.Trim().Length;

            if (len < min || len > max)
            {
                errorProvider1.SetError(control, $"Длина строки должна быть от {min} до {max} символов.");
                return false;
            }

            errorProvider1.SetError(control, "");
            return true;
        }

        private bool ValidateDouble(Control control, double min, double max)
        {
            var result = double.TryParse(control.Text.Trim(), out var value);
            
            if (!result)
            {
                errorProvider1.SetError(control, $"Значение не заполнено");
                return false;
            }

            if (value < min || value > max)
            {
                errorProvider1.SetError(control, $"Значение должно быть от {min} до {max}.");
                return false;
            }

            errorProvider1.SetError(control, "");
            return true;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            
        }
    }
}