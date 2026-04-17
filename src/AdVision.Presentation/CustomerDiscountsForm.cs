using AdVision.Application.CustomerDiscounts.AssignDiscountToCustomerCommand;
using AdVision.Application.CustomerDiscounts.GetCustomerDiscountsQuery;
using AdVision.Application.Discounts.GetAllDiscountsQuery;
using AdVision.Contracts;
using AdVision.Presentation.Notifications;
using Microsoft.Extensions.Logging;
using Shared.Abstractions;

namespace AdVision.Presentation;

public partial class CustomerDiscountsForm : Form
{
    private readonly IQueryHandler<IReadOnlyList<CustomerDiscountDetailsDto>, GetCustomerDiscountsQuery> _getCustomerDiscountsHandler;
    private readonly IQueryHandler<IReadOnlyList<DiscountDto>, GetAllDiscountsQuery> _getDiscountsHandler;
    private readonly ICommandHandler<Guid, AssignDiscountToCustomerCommand> _assignHandler;
    private readonly INotificationService _notificationService;
    private readonly ILogger<CustomerDiscountsForm> _logger;

    private Guid _customerId;
    private readonly CancellationTokenSource _cts = new();

    public CustomerDiscountsForm(
        IQueryHandler<IReadOnlyList<CustomerDiscountDetailsDto>, GetCustomerDiscountsQuery> getCustomerDiscountsHandler,
        IQueryHandler<IReadOnlyList<DiscountDto>, GetAllDiscountsQuery> getDiscountsHandler,
        ICommandHandler<Guid, AssignDiscountToCustomerCommand> assignHandler,
        INotificationService notificationService,
        ILogger<CustomerDiscountsForm> logger)
    {
        InitializeComponent();

        _getCustomerDiscountsHandler = getCustomerDiscountsHandler;
        _getDiscountsHandler = getDiscountsHandler;
        _assignHandler = assignHandler;
        _notificationService = notificationService;
        _logger = logger;

        ConfigureGrid();
    }

    public async void LoadCustomer(CustomerDto customer)
    {
        _customerId = customer.Id;

        Text = $"Скидки клиента: {customer.LastName} {customer.FirstName}";

        await LoadDataAsync();
    }

    private void ConfigureGrid()
    {
        dgvDiscounts.AutoGenerateColumns = false;
        dgvDiscounts.Columns.Clear();

        dgvDiscounts.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = nameof(CustomerDiscountDetailsDto.DiscountName),
            HeaderText = "Название",
            AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        });

        dgvDiscounts.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = nameof(CustomerDiscountDetailsDto.Percent),
            HeaderText = "Процент"
        });

        dgvDiscounts.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = nameof(CustomerDiscountDetailsDto.MinTotal),
            HeaderText = "Мин. сумма"
        });
    }

    private async Task LoadDataAsync()
    {
        await LoadCustomerDiscounts();
        await LoadDiscountsToComboBox();
    }

    private async Task LoadCustomerDiscounts()
    {
        var result = await _getCustomerDiscountsHandler.Handle(
            new GetCustomerDiscountsQuery(_customerId),
            _cts.Token);

        if (result.IsFailure)
        {
            _notificationService.ShowError("Ошибка", string.Join("\n", result.Error));
            return;
        }

        dgvDiscounts.DataSource = result.Value;
    }

    private async Task LoadDiscountsToComboBox()
    {
        var discountsResult = await _getDiscountsHandler.Handle(
            new GetAllDiscountsQuery(),
            _cts.Token);

        if (discountsResult.IsFailure)
        {
            _notificationService.ShowError("Ошибка", string.Join("\n", discountsResult.Error));
            return;
        }

        var customerDiscountsResult = await _getCustomerDiscountsHandler.Handle(
            new GetCustomerDiscountsQuery(_customerId),
            _cts.Token);

        if (customerDiscountsResult.IsFailure)
        {
            _notificationService.ShowError("Ошибка", string.Join("\n", customerDiscountsResult.Error));
            return;
        }

        var assignedIds = customerDiscountsResult.Value
            .Select(x => x.DiscountId)
            .ToHashSet();

        var available = discountsResult.Value
            .Where(x => !assignedIds.Contains(x.Id))
            .OrderBy(x => x.Name)
            .ToList();

        if (available.Any())
        {
            cbDiscounts.DataSource = available;
            cbDiscounts.DisplayMember = nameof(DiscountDto.Name);
            cbDiscounts.ValueMember = nameof(DiscountDto.Id);
            cbDiscounts.SelectedIndex = -1;
        }

        UpdateDiscountsComboState(available);
    }

    private async void btnApply_Click(object sender, EventArgs e)
    {
        if (cbDiscounts.SelectedItem is not DiscountDto discount)
        {
            return;
        }

        var command = new AssignDiscountToCustomerCommand(_customerId, discount.Id);

        var result = await _assignHandler.Handle(command, _cts.Token);

        if (result.IsFailure)
        {
            _notificationService.ShowError("Ошибка", string.Join("\n", result.Error));
            return;
        }

        _notificationService.ShowSuccess("Успех", "Скидка назначена");

        await LoadCustomerDiscounts();
        await LoadDiscountsToComboBox();
    }

    private void btnClose_Click(object sender, EventArgs e)
    {
        Close();
    }

    protected override void OnFormClosed(FormClosedEventArgs e)
    {
        if (!_cts.IsCancellationRequested)
        {
            _cts.Cancel();
        }

        _cts.Dispose();
        base.OnFormClosed(e);
    }
    
    private void UpdateDiscountsComboState(List<DiscountDto> discounts)
    {
        var hasItems = discounts.Any();

        cbDiscounts.Enabled = hasItems;
        btnApply.Enabled = hasItems;

        if (!hasItems)
        {
            cbDiscounts.DataSource = null;
            cbDiscounts.Text = "Все скидки уже назначены";
            cbDiscounts.ForeColor = Color.Gray;
        }
        else
        {
            cbDiscounts.ForeColor = SystemColors.WindowText;
        }
    }
}