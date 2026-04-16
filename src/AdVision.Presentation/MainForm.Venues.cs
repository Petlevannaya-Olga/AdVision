using System.Linq.Expressions;
using AdVision.Application.Venues.GetDistinctQuery;
using AdVision.Application.Venues.GetVenuesQuery;
using AdVision.Application.VenueTypes.GetAllVenueTypesQuery;
using AdVision.Contracts;
using AdVision.Domain.Venues;
using AdVision.Domain.VenueTypes;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Shared.Extensions;

namespace AdVision.Presentation;

public partial class MainForm
{
    #region Площадки

    private void ConfigureVenuesGrid()
    {
        venuesDataGridView.AutoGenerateColumns = false;
        venuesDataGridView.Columns.Clear();

        AddVenueTextColumn(nameof(VenueDto.Name), "Название", "colName");
        AddVenueTextColumn(nameof(VenueDto.Type), "Тип", "colType");
        AddVenueTextColumn(nameof(VenueDto.Region), "Регион", "colRegion");
        AddVenueTextColumn(nameof(VenueDto.District), "Район", "colDistrict");
        AddVenueTextColumn(nameof(VenueDto.City), "Город", "colCity");
        AddVenueTextColumn(nameof(VenueDto.Street), "Улица", "colStreet");
        AddVenueTextColumn(nameof(VenueDto.House), "Дом", "colHouse");
        AddVenueTextColumn(nameof(VenueDto.Latitude), "Широта", "colLatitude");
        AddVenueTextColumn(nameof(VenueDto.Longitude), "Долгота", "colLongitude");
        AddVenueTextColumn(nameof(VenueDto.Width), "Ширина", "colWidth");
        AddVenueTextColumn(nameof(VenueDto.Height), "Высота", "colHeight");
        AddVenueTextColumn(nameof(VenueDto.Rating), "Рейтинг", "colRating");
    }

    private void AddVenueTextColumn(string dataPropertyName, string headerText, string columnName)
    {
        venuesDataGridView.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = dataPropertyName,
            HeaderText = headerText,
            Name = columnName
        });
    }

    private async Task LoadVenueFiltersAsync()
    {
        await LoadVenueTypesToComboboxAsync();
        await LoadRegionsAsync();
        await LoadDistrictsAsync();
        await LoadCitiesAsync();

        ResetVenueFilters();
        UpdateVenueResetButtonState();
    }

    private async Task LoadVenueTypesToComboboxAsync()
    {
        cbVenueTypes.DataSource = null;

        var result = await _venueTypesQueryHandler.Handle(
            new GetAllVenueTypesQuery(),
            _cts.Token);

        if (result.IsFailure)
        {
            ShowLoadError(LoadVenueTypesErrorTitle, result.Error);
            return;
        }

        var venueTypes = result.Value
            .OrderBy(x => x.Name)
            .ToList();

        cbVenueTypes.DataSource = venueTypes;
        cbVenueTypes.DisplayMember = nameof(VenueTypeDto.Name);
        cbVenueTypes.ValueMember = nameof(VenueTypeDto.Id);
        cbVenueTypes.SelectedIndex = -1;
    }

    private Task LoadRegionsAsync()
    {
        return LoadDistinctAsync(
            cbRegions,
            new GetDistinctQuery(v => v.Address.Region),
            LoadRegionsErrorTitle);
    }

    private Task LoadDistrictsAsync()
    {
        return LoadDistinctAsync(
            cbDistricts,
            new GetDistinctQuery(v => v.Address.District),
            LoadDistrictsErrorTitle);
    }

    private Task LoadCitiesAsync()
    {
        return LoadDistinctAsync(
            cbCities,
            new GetDistinctQuery(v => v.Address.City),
            LoadCitiesErrorTitle);
    }

    private async Task LoadDistinctAsync(
        ComboBox comboBox,
        GetDistinctQuery query,
        string errorTitle)
    {
        comboBox.DataSource = null;

        var result = await _getDistinctQueryHandler.Handle(query, _cts.Token);

        if (result.IsFailure)
        {
            ShowLoadError(errorTitle, result.Error);
            return;
        }

        comboBox.DataSource = result.Value;
        comboBox.SelectedIndex = -1;
    }

    private async Task LoadVenuesAsync()
    {
        var filter = BuildVenueFilter();
        var orderBy = BuildVenueSort();
        var descending = chkDescending.Checked;

        var result = await _venuesQueryHandler.Handle(
            new GetVenuesQuery(_page, PageSize, filter, orderBy, descending),
            _cts.Token);

        if (result.IsFailure)
        {
            ShowLoadError(LoadVenuesErrorTitle, result.Error);
            return;
        }

        var paged = result.Value;
        _totalCount = paged.TotalCount;

        venuesDataGridView.DataSource = new BindingSource
        {
            DataSource = paged.Items
        };

        UpdateVenuesPagingState();
    }

    private Expression<Func<Venue, bool>> BuildVenueFilter()
    {
        Expression<Func<Venue, bool>> filter = v => true;

        var selectedRegion = cbRegions.SelectedItem as string;
        var selectedDistrict = cbDistricts.SelectedItem as string;
        var selectedCity = cbCities.SelectedItem as string;
        var selectedName = txtName.Text.Trim();
        var selectedStreet = txtStreet.Text.Trim();

        if (cbVenueTypes.SelectedItem is VenueTypeDto selectedVenueType)
        {
            var venueTypeId = new VenueTypeId(selectedVenueType.Id);
            filter = filter.And(v => v.VenueTypeId == venueTypeId);
        }

        if (!string.IsNullOrWhiteSpace(selectedRegion))
        {
            filter = filter.And(v => v.Address.Region == selectedRegion);
        }

        if (!string.IsNullOrWhiteSpace(selectedDistrict))
        {
            filter = filter.And(v => v.Address.District == selectedDistrict);
        }

        if (!string.IsNullOrWhiteSpace(selectedCity))
        {
            filter = filter.And(v => v.Address.City == selectedCity);
        }

        if (!string.IsNullOrWhiteSpace(selectedName))
        {
            var pattern = $"%{selectedName}%";
            filter = filter.And(v => EF.Functions.Like(v.Name.Value, pattern));
        }

        if (!string.IsNullOrWhiteSpace(selectedStreet))
        {
            var pattern = $"%{selectedStreet}%";
            filter = filter.And(v => EF.Functions.Like(v.Address.Street, pattern));
        }

        var ratingFrom = nudRatingFrom.Value;
        var ratingTo = nudRatingTo.Value;

        if (ratingFrom > nudRatingFrom.Minimum)
        {
            filter = filter.And(v => v.Rating.Value >= (double)ratingFrom);
        }

        if (ratingTo < nudRatingTo.Maximum)
        {
            filter = filter.And(v => v.Rating.Value <= (double)ratingTo);
        }

        return filter;
    }

    private Expression<Func<Venue, object>> BuildVenueSort()
    {
        return cbSortOrder.SelectedItem?.ToString() switch
        {
            "Название" => v => v.Name.Value,
            "Тип" => v => v.Type,
            "Регион" => v => v.Address.Region,
            "Район" => v => v.Address.District,
            "Город" => v => v.Address.City,
            "Улица" => v => v.Address.Street,
            "Широта" => v => v.Address.Latitude,
            "Долгота" => v => v.Address.Longitude,
            "Ширина" => v => v.Size.Width,
            "Высота" => v => v.Size.Height,
            _ => v => v.Name.Value
        };
    }

    private void ResetVenueFilters()
    {
        cbVenueTypes.SelectedIndex = -1;
        cbRegions.SelectedIndex = -1;
        cbDistricts.SelectedIndex = -1;
        cbCities.SelectedIndex = -1;

        txtName.Clear();
        txtStreet.Clear();

        nudRatingFrom.Value = nudRatingFrom.Minimum;
        nudRatingTo.Value = nudRatingTo.Maximum;

        cbSortOrder.SelectedIndex = 0;
        chkDescending.Checked = false;
    }

    private void UpdateVenueResetButtonState()
    {
        btnReset.Enabled =
            cbVenueTypes.SelectedIndex >= 0 ||
            cbRegions.SelectedIndex >= 0 ||
            cbDistricts.SelectedIndex >= 0 ||
            cbCities.SelectedIndex >= 0 ||
            !string.IsNullOrWhiteSpace(txtName.Text) ||
            !string.IsNullOrWhiteSpace(txtStreet.Text) ||
            chkDescending.Checked ||
            nudRatingFrom.Value > nudRatingFrom.Minimum ||
            nudRatingTo.Value < nudRatingTo.Maximum ||
            cbSortOrder.SelectedIndex > 0;
    }

    private void UpdateVenuesPagingState()
    {
        venuesPagingUserControl.SetState(_totalCount, _page, TotalPages, _isLoading);
        venuesPagingUserControl.SetAddVisible(true);
        venuesPagingUserControl.SetAddEnabled(!_isLoading);
    }

    private async void BtnApply_Click(object sender, EventArgs e)
    {
        if (_isLoading)
        {
            return;
        }

        await RunUiActionAsync(
            async () =>
            {
                _page = 1;
                await LoadVenuesAsync();
                UpdateVenueResetButtonState();
            },
            "Применение фильтров отменено",
            "Ошибка применения фильтров");
    }

    private async void BtnReset_Click(object sender, EventArgs e)
    {
        if (_isLoading || tabControl1.SelectedTab == tabPage2)
        {
            return;
        }

        await RunUiActionAsync(
            async () =>
            {
                ResetVenueFilters();
                _page = 1;
                await LoadVenuesAsync();
                UpdateVenueResetButtonState();
            },
            "Сброс фильтров отменен",
            "Ошибка сброса фильтров");
    }

    private async void GoToPreviousVenuePage()
    {
        if (_isLoading || _page <= 1)
        {
            return;
        }

        await RunUiActionAsync(
            async () =>
            {
                _page--;
                await LoadVenuesAsync();
            },
            "Переход на предыдущую страницу площадок отменен",
            "Ошибка загрузки предыдущей страницы площадок");
    }

    private async void GoToNextVenuePage()
    {
        if (_isLoading || _page >= TotalPages)
        {
            return;
        }

        await RunUiActionAsync(
            async () =>
            {
                _page++;
                await LoadVenuesAsync();
            },
            "Переход на следующую страницу площадок отменен",
            "Ошибка загрузки следующей страницы площадок");
    }

    private void CreateVenue()
    {
        var form = _serviceProvider.GetRequiredService<VenueForm>();
        form.VenueCreated += OnVenueCreated;
        form.ShowDialog();
        form.VenueCreated -= OnVenueCreated;
    }

    private async void OnVenueCreated()
    {
        await RunUiActionAsync(
            async () =>
            {
                _page = 1;
                await RefreshDataAsync();
            },
            "Обновление данных после создания площадки отменено",
            "Ошибка обновления данных после создания площадки");
    }

    private void VenuesDataGridView_CellDoubleClick(object? sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0)
        {
            return;
        }

        var row = venuesDataGridView.Rows[e.RowIndex];

        if (row.DataBoundItem is not VenueDto venue)
        {
            _logger.LogWarning("Не удалось получить VenueDto из строки DataGridView");
            return;
        }

        OpenTariffForm(venue);
    }

    private void OpenTariffForm(VenueDto venue)
    {
        var form = _serviceProvider.GetRequiredService<TariffForm>();
        form.LoadVenue(venue);
        form.ShowDialog();
    }

    private void TxtName_TextChanged(object sender, EventArgs e)
    {
        UpdateVenueResetButtonState();
    }

    private void CbVenueTypes_SelectedIndexChanged(object sender, EventArgs e)
    {
        UpdateVenueResetButtonState();
    }

    private void CbRegions_SelectedIndexChanged(object sender, EventArgs e)
    {
        UpdateVenueResetButtonState();
    }

    private void CbDistricts_SelectedIndexChanged(object sender, EventArgs e)
    {
        UpdateVenueResetButtonState();
    }

    private void CbCities_SelectedIndexChanged(object sender, EventArgs e)
    {
        UpdateVenueResetButtonState();
    }

    private void TxtStreet_TextChanged(object sender, EventArgs e)
    {
        UpdateVenueResetButtonState();
    }

    private void NudRatingFrom_ValueChanged(object sender, EventArgs e)
    {
        UpdateVenueResetButtonState();
    }

    private void NudRatingTo_ValueChanged(object sender, EventArgs e)
    {
        UpdateVenueResetButtonState();
    }

    private void CbSortOrder_SelectedIndexChanged(object sender, EventArgs e)
    {
        UpdateVenueResetButtonState();
    }

    private void ChkDescending_CheckedChanged(object sender, EventArgs e)
    {
        UpdateVenueResetButtonState();
    }

    #endregion
}