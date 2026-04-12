using System.ComponentModel;
using AdVision.Application.Generators;
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
		private readonly ILogger<VenueForm> _logger;
		private readonly IServiceProvider _serviceProvider;
		private readonly CancellationTokenSource _cts = new();
		private readonly IVenueFakeGenerator _venueFakeGenerator;
		private readonly INotificationService _notificationService;

		public VenueForm(
			IVenueFakeGenerator venueFakeGenerator,
			INotificationService notificationService,
			IQueryHandler<IReadOnlyList<VenueTypeDto>, GetAllVenueTypesQuery> venueTypesQueryHandler,
			IServiceProvider serviceProvider,
			ILogger<VenueForm> logger)
		{
			_venueFakeGenerator = venueFakeGenerator;
			_notificationService = notificationService;
			_venueTypesQueryHandler = venueTypesQueryHandler;
			_logger = logger;
			_serviceProvider = serviceProvider;

			InitializeComponent();
		}

		protected override async void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			ReloadVenueTypes();

			if (cbVenueTypes.Items.Count > 0)
			{
				cbVenueTypes.SelectedIndex = 0;
				Generate();
			}
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

		private void BtnSave_Click(object sender, EventArgs e)
		{
		}

		private void NudRating_ValueChanged(object sender, EventArgs e)
		{
			var success = nudRating.Value > 0 && nudRating.Value <= 10;
			SetIcon(pbRatingValidation, success);
			btnSave.Enabled = success;
		}

		private void CbVenueTypes_TextChanged(object sender, EventArgs e)
		{
			var hasSelectedItem = cbVenueTypes.SelectedIndex > -1;
			SetIcon(pbVenueTypeValidation, hasSelectedItem);
			btnSave.Enabled = hasSelectedItem;
		}

		private void TxtName_TextChanged(object sender, EventArgs e)
		{
			var success = IsValidLength(txtName, VenueName.MIN_LENGTH, VenueName.MAX_LENGTH);
			SetIcon(pbNameValidation, success);
			btnSave.Enabled = success;
		}

		private void SetIcon(PictureBox box, bool success)
		{
			box.Image = success ? _success : _exception;
		}

		private void Generate()
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

		private void TxtDescription_TextChanged(object sender, EventArgs e)
		{
			var success = IsValidLength(txtDescription, VenueDescription.MIN_LENGTH, VenueDescription.MAX_LENGTH);
			SetIcon(pbDescriptionValidation, success);
			btnSave.Enabled = success;
		}

		private void TxtHouseNumber_TextChanged(object sender, EventArgs e)
		{
			var hasValue = !string.IsNullOrEmpty(txtHouseNumber.Text.Trim());
			SetIcon(pbHouserNumberValidation, hasValue);
			btnSave.Enabled = hasValue;
		}

		private void TxtRegion_TextChanged(object sender, EventArgs e)
		{
			var success = IsValidLength(txtRegion, VenueAddress.MIN_LENGTH, VenueAddress.MAX_LENGTH);
			SetIcon(pbRegionValidation, success);
			btnSave.Enabled = success;
		}

		private void TxtDistrict_TextChanged(object sender, EventArgs e)
		{
			var success = IsValidLength(txtDistrict, VenueAddress.MIN_LENGTH, VenueAddress.MAX_LENGTH);
			SetIcon(pbDistrictValidation, success);
			btnSave.Enabled = success;
		}

		private void TxtCity_TextChanged(object sender, EventArgs e)
		{
			var success = IsValidLength(txtCity, VenueAddress.MIN_LENGTH, VenueAddress.MAX_LENGTH);
			SetIcon(pbCityValidation, success);
			btnSave.Enabled = success;
		}

		private void TxtStreet_TextChanged(object sender, EventArgs e)
		{
			var success = IsValidLength(txtStreet, VenueAddress.MIN_LENGTH, VenueAddress.MAX_LENGTH);
			SetIcon(pbStreetValidation, success);
			btnSave.Enabled = success;
		}

		private void TxtLatitude_TextChanged(object sender, EventArgs e)
		{
			var success = IsDoubleValid(txtLatitude, VenueAddress.MIN_LATITUDE_VALUE, VenueAddress.MAX_LATITUDE_VALUE);
			SetIcon(pbLatitudeValidation, success);
			btnSave.Enabled = success;
		}

		private void TxtLongitude_TextChanged(object sender, EventArgs e)
		{
			var success = IsDoubleValid(txtLongitude, VenueAddress.MIN_LONGITUDE_VALUE, VenueAddress.MAX_LONGITUDE_VALUE);
			SetIcon(pbLongitudeValidation, success);
			btnSave.Enabled = success;
		}

		private void TxtWidth_TextChanged(object sender, EventArgs e)
		{
			var success = IsDoubleValid(txtWidth, VenueSize.MIN_WIDTH, VenueSize.MAX_WIDTH);
			SetIcon(pbWidthValidation, success);
			btnSave.Enabled = success;
		}

		private void TxtHeight_TextChanged(object sender, EventArgs e)
		{
			var success = IsDoubleValid(txtHeight, VenueSize.MIN_WIDTH, VenueSize.MAX_WIDTH);
			SetIcon(pbHeightValidation, success);
			btnSave.Enabled = success;
		}
	}
}