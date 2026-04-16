namespace AdVision.Presentation;

using AdVision.Application.Discounts.GetAllDiscountsQuery;
using AdVision.Application.Positions.GetAllPositionsQuery;
using AdVision.Application.VenueTypes.GetAllVenueTypesQuery;
using AdVision.Contracts;
using AdVision.Presentation.Helpers;
using Microsoft.Extensions.DependencyInjection;
using Shared;
using CSharpFunctionalExtensions;

public partial class MainForm
{
    #region Справочники

    private async Task LoadDirectoryToGridAsync<T>(
        Func<CancellationToken, Task<Result<IReadOnlyList<T>, Errors>>> loader,
        string errorTitle,
        string? nameFilter,
        Func<T, string> nameSelector,
        DirectoryListHelper directoryHelper)
    {
        var result = await loader(_cts.Token);

        if (result.IsFailure)
        {
            ShowLoadError(errorTitle, result.Error);
            return;
        }

        var pagedItems = DirectoryItemsHelper.PreparePage(
            result.Value,
            nameFilter,
            nameSelector,
            directoryHelper);

        dgvDirectories.DataSource = new BindingSource
        {
            DataSource = pagedItems
        };

        UpdateDirectoryPagingState();
        UpdateDirectoryResetButtonState();
    }

    private Task ApplyDirectoryFilterAsync(
        DirectoryListHelper helper,
        Func<Task> reload,
        string cancelMessage,
        string errorMessage)
    {
        return RunUiActionAsync(
            async () =>
            {
                helper.ResetPage();
                await reload();
            },
            cancelMessage,
            errorMessage);
    }

    private Task ResetDirectoryFilterAsync(
        Action resetFilters,
        DirectoryListHelper helper,
        Func<Task> reload,
        string cancelMessage,
        string errorMessage)
    {
        return RunUiActionAsync(
            async () =>
            {
                resetFilters();
                helper.ResetPage();
                await reload();
            },
            cancelMessage,
            errorMessage);
    }

    private Task RefreshDirectoryAfterCreateAsync(
        Action resetFilters,
        DirectoryListHelper helper,
        Func<Task> reload,
        string cancelMessage,
        string errorMessage,
        Func<Task>? afterReload = null)
    {
        return RunUiActionAsync(
            async () =>
            {
                resetFilters();
                helper.ResetPage();
                await reload();

                if (afterReload is not null)
                {
                    await afterReload();
                }

                UpdateDirectoryResetButtonState();
            },
            cancelMessage,
            errorMessage);
    }

    private void ConfigureVenueTypesGrid()
    {
        dgvDirectories.AutoGenerateColumns = false;
        dgvDirectories.Columns.Clear();

        dgvDirectories.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = nameof(VenueTypeDto.Name),
            HeaderText = @"Название",
            Name = "colName",
            AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        });
    }

    private void ConfigurePositionsGrid()
    {
        dgvDirectories.AutoGenerateColumns = false;
        dgvDirectories.Columns.Clear();

        dgvDirectories.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = nameof(PositionDto.Name),
            HeaderText = @"Название",
            Name = "colName",
            AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        });
    }

    private void ConfigureDiscountsGrid()
    {
        dgvDirectories.AutoGenerateColumns = false;
        dgvDirectories.Columns.Clear();

        dgvDirectories.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = nameof(DiscountDto.Name),
            HeaderText = @"Название",
            Name = "colName",
            AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        });

        dgvDirectories.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = nameof(DiscountDto.Percent),
            HeaderText = @"Процент",
            Name = "colPercent"
        });

        dgvDirectories.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = nameof(DiscountDto.MinTotal),
            HeaderText = @"Мин. сумма",
            Name = "colMinTotal"
        });
    }

    private Task LoadVenueTypesToGridAsync()
    {
        return LoadDirectoryToGridAsync(
            ct => _venueTypesQueryHandler.Handle(new GetAllVenueTypesQuery(), ct),
            LoadVenueTypesErrorTitle,
            _venueTypesFilterControl?.NameFilter,
            x => x.Name,
            _venueTypesDirectory);
    }

    private Task LoadPositionsToGridAsync()
    {
        return LoadDirectoryToGridAsync(
            ct => _positionsQueryHandler.Handle(new GetAllPositionsQuery(), ct),
            LoadPositionsErrorTitle,
            _positionsFilterControl?.NameFilter,
            x => x.Name,
            _positionsDirectory);
    }

    private async Task LoadDiscountsToGridAsync()
    {
        var result = await _discountsQueryHandler.Handle(
            new GetAllDiscountsQuery(),
            _cts.Token);

        if (result.IsFailure)
        {
            ShowLoadError(LoadDiscountsErrorTitle, result.Error);
            return;
        }

        var filteredItems = ApplyDiscountFilters(result.Value)
            .OrderBy(x => x.Name)
            .ToList();

        var pagedItems = _discountsDirectory.ApplyPaging(filteredItems);

        dgvDirectories.DataSource = new BindingSource
        {
            DataSource = pagedItems
        };

        UpdateDirectoryPagingState();
        UpdateDirectoryResetButtonState();
    }

    private IEnumerable<DiscountDto> ApplyDiscountFilters(IEnumerable<DiscountDto> discounts)
    {
        var nameFilter = _discountsFilterControl?.NameFilter;
        if (!string.IsNullOrWhiteSpace(nameFilter))
        {
            discounts = discounts.Where(x =>
                x.Name.Contains(nameFilter, StringComparison.OrdinalIgnoreCase));
        }

        var percentFrom = _discountsFilterControl?.PercentFrom;
        if (percentFrom.HasValue)
        {
            discounts = discounts.Where(x => (decimal)x.Percent >= percentFrom.Value);
        }

        var percentTo = _discountsFilterControl?.PercentTo;
        if (percentTo.HasValue)
        {
            discounts = discounts.Where(x => (decimal)x.Percent <= percentTo.Value);
        }

        var minTotalFrom = _discountsFilterControl?.MinTotalFrom;
        if (minTotalFrom.HasValue)
        {
            discounts = discounts.Where(x => x.MinTotal >= minTotalFrom.Value);
        }

        var minTotalTo = _discountsFilterControl?.MinTotalTo;
        if (minTotalTo.HasValue)
        {
            discounts = discounts.Where(x => x.MinTotal <= minTotalTo.Value);
        }

        return discounts;
    }

    private async Task LoadCurrentDirectoryAsync()
    {
        switch (_currentDirectoryType)
        {
            case DirectoryType.VenueTypes:
                await LoadVenueTypesToGridAsync();
                break;
            case DirectoryType.Positions:
                await LoadPositionsToGridAsync();
                break;
            case DirectoryType.Discounts:
                await LoadDiscountsToGridAsync();
                break;
        }
    }

    private TControl LoadDirectoryFilterControl<TControl>(
        Action<TControl> subscribe,
        Action<TControl> configure)
        where TControl : Control
    {
        pnlFilters.Controls.Clear();

        var control = _serviceProvider.GetRequiredService<TControl>();
        control.Dock = DockStyle.Fill;

        subscribe(control);
        configure(control);

        pnlFilters.Controls.Add(control);
        pnlFilters.Height = control.Height;

        return control;
    }

    private void LoadVenueTypesFilterControl()
    {
        _venueTypesFilterControl = LoadDirectoryFilterControl<VenueTypesFilterUserControl>(
            control =>
            {
                control.ApplyClicked += OnVenueTypesFilterApplyClicked;
                control.ResetClicked += OnVenueTypesFilterResetClicked;
                control.FiltersChanged += OnVenueTypesFiltersChanged;
            },
            control => control.SetResetEnabled(false));
    }

    private void LoadPositionsFilterControl()
    {
        _positionsFilterControl = LoadDirectoryFilterControl<PositionsFilterUserControl>(
            control =>
            {
                control.ApplyClicked += OnPositionsFilterApplyClicked;
                control.ResetClicked += OnPositionsFilterResetClicked;
                control.FiltersChanged += OnPositionsFiltersChanged;
            },
            control => control.SetResetEnabled(false));
    }

    private void LoadDiscountsFilterControl()
    {
        _discountsFilterControl = LoadDirectoryFilterControl<DiscountsFilterUserControl>(
            control =>
            {
                control.ApplyClicked += OnDiscountsFilterApplyClicked;
                control.ResetClicked += OnDiscountsFilterResetClicked;
                control.FiltersChanged += OnDiscountsFiltersChanged;
            },
            control => control.SetResetEnabled(false));
    }

    private bool CanGoToPreviousDirectoryPage()
    {
        return _currentDirectoryType switch
        {
            DirectoryType.VenueTypes => _venueTypesDirectory.CanGoPrevious(),
            DirectoryType.Positions => _positionsDirectory.CanGoPrevious(),
            DirectoryType.Discounts => _discountsDirectory.CanGoPrevious(),
            _ => false
        };
    }

    private bool CanGoToNextDirectoryPage()
    {
        return _currentDirectoryType switch
        {
            DirectoryType.VenueTypes => _venueTypesDirectory.CanGoNext(),
            DirectoryType.Positions => _positionsDirectory.CanGoNext(),
            DirectoryType.Discounts => _discountsDirectory.CanGoNext(),
            _ => false
        };
    }

    private void MoveDirectoryPageBackward()
    {
        switch (_currentDirectoryType)
        {
            case DirectoryType.VenueTypes:
                _venueTypesDirectory.GoPrevious();
                break;
            case DirectoryType.Positions:
                _positionsDirectory.GoPrevious();
                break;
            case DirectoryType.Discounts:
                _discountsDirectory.GoPrevious();
                break;
        }
    }

    private void MoveDirectoryPageForward()
    {
        switch (_currentDirectoryType)
        {
            case DirectoryType.VenueTypes:
                _venueTypesDirectory.GoNext();
                break;
            case DirectoryType.Positions:
                _positionsDirectory.GoNext();
                break;
            case DirectoryType.Discounts:
                _discountsDirectory.GoNext();
                break;
        }
    }

    private int GetCurrentDirectoryPage()
    {
        return _currentDirectoryType switch
        {
            DirectoryType.VenueTypes => _venueTypesDirectory.Page,
            DirectoryType.Positions => _positionsDirectory.Page,
            DirectoryType.Discounts => _discountsDirectory.Page,
            _ => 1
        };
    }

    private int GetCurrentDirectoryTotalPages()
    {
        return _currentDirectoryType switch
        {
            DirectoryType.VenueTypes => _venueTypesDirectory.TotalPages,
            DirectoryType.Positions => _positionsDirectory.TotalPages,
            DirectoryType.Discounts => _discountsDirectory.TotalPages,
            _ => 0
        };
    }

    private int GetCurrentDirectoryTotalCount()
    {
        return _currentDirectoryType switch
        {
            DirectoryType.VenueTypes => _venueTypesDirectory.TotalCount,
            DirectoryType.Positions => _positionsDirectory.TotalCount,
            DirectoryType.Discounts => _discountsDirectory.TotalCount,
            _ => 0
        };
    }

    private void UpdateDirectoryPagingState()
    {
        directoriesPagingUserControl.SetState(
            GetCurrentDirectoryTotalCount(),
            GetCurrentDirectoryPage(),
            GetCurrentDirectoryTotalPages(),
            _isLoading);

        directoriesPagingUserControl.SetAddVisible(true);
        directoriesPagingUserControl.SetAddEnabled(!_isLoading && _currentDirectoryType != DirectoryType.None);
    }

    private async void BtnVenueTypes_Click(object sender, EventArgs e)
    {
        await RunUiActionAsync(
            () => OpenDirectoryAsync(DirectoryType.VenueTypes),
            "Загрузка типов площадок отменена",
            "Ошибка загрузки типов площадок");
    }

    private async void BtnPositions_Click(object sender, EventArgs e)
    {
        await RunUiActionAsync(
            () => OpenDirectoryAsync(DirectoryType.Positions),
            "Загрузка позиций отменена",
            "Ошибка загрузки позиций");
    }

    private async void BtnDiscounts_Click(object sender, EventArgs e)
    {
        await RunUiActionAsync(
            () => OpenDirectoryAsync(DirectoryType.Discounts),
            "Загрузка скидок отменена",
            "Ошибка загрузки скидок");
    }

    private async void TabControl1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (tabControl1.SelectedTab == tabPage2 && !_directoriesTabInitialized)
        {
            _directoriesTabInitialized = true;

            await RunUiActionAsync(
                () => OpenDirectoryAsync(DirectoryType.VenueTypes),
                "Загрузка типов площадок отменена",
                "Ошибка загрузки типов площадок");
        }
    }

    private async Task OpenDirectoryAsync(DirectoryType directoryType)
    {
        _currentDirectoryType = directoryType;
        UpdateDirectoryButtonsState();

        switch (directoryType)
        {
            case DirectoryType.VenueTypes:
                LoadVenueTypesFilterControl();
                ConfigureVenueTypesGrid();
                await LoadVenueTypesToGridAsync();
                break;
            case DirectoryType.Positions:
                LoadPositionsFilterControl();
                ConfigurePositionsGrid();
                await LoadPositionsToGridAsync();
                break;
            case DirectoryType.Discounts:
                LoadDiscountsFilterControl();
                ConfigureDiscountsGrid();
                await LoadDiscountsToGridAsync();
                break;
        }
    }

    private async void OnVenueTypesFilterApplyClicked()
    {
        await ApplyDirectoryFilterAsync(
            _venueTypesDirectory,
            LoadVenueTypesToGridAsync,
            "Применение фильтра типов площадок отменено",
            "Ошибка применения фильтра типов площадок");
    }

    private async void OnVenueTypesFilterResetClicked()
    {
        await ResetDirectoryFilterAsync(
            () => _venueTypesFilterControl?.ResetFilters(),
            _venueTypesDirectory,
            LoadVenueTypesToGridAsync,
            "Сброс фильтра типов площадок отменен",
            "Ошибка сброса фильтра типов площадок");
    }

    private async void OnPositionsFilterApplyClicked()
    {
        await ApplyDirectoryFilterAsync(
            _positionsDirectory,
            LoadPositionsToGridAsync,
            "Применение фильтра позиций отменено",
            "Ошибка применения фильтра позиций");
    }

    private async void OnPositionsFilterResetClicked()
    {
        await ResetDirectoryFilterAsync(
            () => _positionsFilterControl?.ResetFilters(),
            _positionsDirectory,
            LoadPositionsToGridAsync,
            "Сброс фильтра позиций отменен",
            "Ошибка сброса фильтра позиций");
    }

    private async void OnDiscountsFilterApplyClicked()
    {
        await ApplyDirectoryFilterAsync(
            _discountsDirectory,
            LoadDiscountsToGridAsync,
            "Применение фильтра скидок отменено",
            "Ошибка применения фильтра скидок");
    }

    private async void OnDiscountsFilterResetClicked()
    {
        await ResetDirectoryFilterAsync(
            () => _discountsFilterControl?.ResetFilters(),
            _discountsDirectory,
            LoadDiscountsToGridAsync,
            "Сброс фильтра скидок отменен",
            "Ошибка сброса фильтра скидок");
    }

    private async void OnVenueTypeCreated(string _)
    {
        await RefreshDirectoryAfterCreateAsync(
            () => _venueTypesFilterControl?.ResetFilters(),
            _venueTypesDirectory,
            LoadVenueTypesToGridAsync,
            "Обновление списка типов площадок отменено",
            "Ошибка обновления списка типов площадок",
            LoadVenueTypesToComboboxAsync);
    }

    private async void OnPositionCreated(string _)
    {
        await RefreshDirectoryAfterCreateAsync(
            () => _positionsFilterControl?.ResetFilters(),
            _positionsDirectory,
            LoadPositionsToGridAsync,
            "Обновление списка позиций отменено",
            "Ошибка обновления списка позиций");
    }

    private async void OnDiscountCreated(string _)
    {
        await RefreshDirectoryAfterCreateAsync(
            () => _discountsFilterControl?.ResetFilters(),
            _discountsDirectory,
            LoadDiscountsToGridAsync,
            "Обновление списка скидок отменено",
            "Ошибка обновления списка скидок");
    }

    private async void GoToPreviousDirectoryPageUi()
    {
        if (_isLoading || !CanGoToPreviousDirectoryPage())
        {
            return;
        }

        await RunUiActionAsync(
            async () =>
            {
                MoveDirectoryPageBackward();
                await LoadCurrentDirectoryAsync();
            },
            "Переход на предыдущую страницу справочника отменен",
            "Ошибка загрузки предыдущей страницы справочника");
    }

    private async void GoToNextDirectoryPageUi()
    {
        if (_isLoading || !CanGoToNextDirectoryPage())
        {
            return;
        }

        await RunUiActionAsync(
            async () =>
            {
                MoveDirectoryPageForward();
                await LoadCurrentDirectoryAsync();
            },
            "Переход на следующую страницу справочника отменен",
            "Ошибка загрузки следующей страницы справочника");
    }

    private void AddDirectoryItem()
    {
        switch (_currentDirectoryType)
        {
            case DirectoryType.VenueTypes:
                OpenVenueTypeForm();
                break;
            case DirectoryType.Positions:
                OpenPositionForm();
                break;
            case DirectoryType.Discounts:
                OpenDiscountForm();
                break;
        }
    }

    private void OpenVenueTypeForm()
    {
        var form = _serviceProvider.GetRequiredService<VenueTypeForm>();
        form.VenueTypeCreated += OnVenueTypeCreated;
        form.ShowDialog();
        form.VenueTypeCreated -= OnVenueTypeCreated;
    }

    private void OpenPositionForm()
    {
        var form = _serviceProvider.GetRequiredService<PositionForm>();
        form.PositionCreated += OnPositionCreated;
        form.ShowDialog();
        form.PositionCreated -= OnPositionCreated;
    }

    private void OpenDiscountForm()
    {
        var form = _serviceProvider.GetRequiredService<DiscountForm>();
        form.DiscountCreated += OnDiscountCreated;
        form.ShowDialog();
        form.DiscountCreated -= OnDiscountCreated;
    }

    private bool HasActiveVenueTypeFilters()
    {
        return !string.IsNullOrWhiteSpace(_venueTypesFilterControl?.NameFilter);
    }

    private bool HasActivePositionFilters()
    {
        return !string.IsNullOrWhiteSpace(_positionsFilterControl?.NameFilter);
    }

    private bool HasActiveDiscountFilters()
    {
        return !string.IsNullOrWhiteSpace(_discountsFilterControl?.NameFilter) ||
               _discountsFilterControl?.PercentFrom is not null ||
               _discountsFilterControl?.PercentTo is not null ||
               _discountsFilterControl?.MinTotalFrom is not null ||
               _discountsFilterControl?.MinTotalTo is not null;
    }

    private void UpdateDirectoryResetButtonState()
    {
        switch (_currentDirectoryType)
        {
            case DirectoryType.VenueTypes:
                _venueTypesFilterControl?.SetResetEnabled(HasActiveVenueTypeFilters());
                break;
            case DirectoryType.Positions:
                _positionsFilterControl?.SetResetEnabled(HasActivePositionFilters());
                break;
            case DirectoryType.Discounts:
                _discountsFilterControl?.SetResetEnabled(HasActiveDiscountFilters());
                break;
        }
    }

    private void OnVenueTypesFiltersChanged()
    {
        UpdateDirectoryResetButtonState();
    }

    private void OnPositionsFiltersChanged()
    {
        UpdateDirectoryResetButtonState();
    }

    private void OnDiscountsFiltersChanged()
    {
        UpdateDirectoryResetButtonState();
    }

    private void UpdateDirectoryButtonsState()
    {
        btnVenueTypes.BackColor = SystemColors.Control;
        btnPositions.BackColor = SystemColors.Control;
        btnEmployees.BackColor = SystemColors.Control;
        btnCustomers.BackColor = SystemColors.Control;
        btnDiscounts.BackColor = SystemColors.Control;

        btnVenueTypes.Enabled = true;
        btnPositions.Enabled = true;
        btnEmployees.Enabled = true;
        btnCustomers.Enabled = true;
        btnDiscounts.Enabled = true;

        switch (_currentDirectoryType)
        {
            case DirectoryType.VenueTypes:
                btnVenueTypes.BackColor = Color.LightBlue;
                btnVenueTypes.Enabled = false;
                break;
            case DirectoryType.Positions:
                btnPositions.BackColor = Color.LightBlue;
                btnPositions.Enabled = false;
                break;
            case DirectoryType.Employees:
                btnEmployees.BackColor = Color.LightBlue;
                btnEmployees.Enabled = false;
                break;
            case DirectoryType.Customers:
                btnCustomers.BackColor = Color.LightBlue;
                btnCustomers.Enabled = false;
                break;
            case DirectoryType.Discounts:
                btnDiscounts.BackColor = Color.LightBlue;
                btnDiscounts.Enabled = false;
                break;
            case DirectoryType.None:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    #endregion
}