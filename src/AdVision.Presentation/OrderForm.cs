using System.ComponentModel;
using AdVision.Application;
using AdVision.Application.Contracts.GetContractsQuery;
using AdVision.Application.Orders.CreateOrderCommand;
using AdVision.Application.Tariffs.GetAllTariffsQuery;
using AdVision.Contracts;
using AdVision.Presentation.Notifications;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Shared.Abstractions;

namespace AdVision.Presentation;

public partial class OrderForm : Form
{
	private readonly IQueryHandler<PagedResult<ContractDto>, GetContractsQuery> _contractsQueryHandler;
	private readonly IQueryHandler<IReadOnlyList<TariffDto>, GetAllTariffsQuery> _tariffsQueryHandler;
	private readonly ICommandHandler<Guid, CreateOrderCommand> _createOrderCommandHandler;
	private readonly INotificationService _notificationService;
	private readonly IServiceProvider _serviceProvider;
	private readonly ILogger<OrderForm> _logger;

	private Guid? _selectedContractId;
	private readonly BindingList<CreateOrderItemRow> _items = [];

	public event Action? OrderCreated;

	public OrderForm(
		IQueryHandler<PagedResult<ContractDto>, GetContractsQuery> contractsQueryHandler,
		IQueryHandler<IReadOnlyList<TariffDto>, GetAllTariffsQuery> tariffsQueryHandler,
		ICommandHandler<Guid, CreateOrderCommand> createOrderCommandHandler,
		INotificationService notificationService,
		IServiceProvider serviceProvider,
		ILogger<OrderForm> logger)
	{
		_contractsQueryHandler = contractsQueryHandler;
		_tariffsQueryHandler = tariffsQueryHandler;
		_createOrderCommandHandler = createOrderCommandHandler;
		_notificationService = notificationService;
		_serviceProvider = serviceProvider;
		_logger = logger;

		InitializeComponent();
		
		pbContractValidation.Image = Properties.Resources.exception;
	}
	
	private void SubscribeControls()
	{
		orderItemsPagingUserControl.AddClicked += AddPosition;
	}

	protected override async void OnLoad(EventArgs e)
	{
		base.OnLoad(e);
		SubscribeControls();
	}

	private void BtnCancel_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void BtnSelect_Click(object sender, EventArgs e)
	{
		var form = _serviceProvider.GetRequiredService<SelectContractForm>();

		if (form.ShowDialog() != DialogResult.OK || form.SelectedContract is null)
		{
			return;
		}

		var contract = form.SelectedContract;
		txtContractNumber.Text = contract.Number;
		pbContractValidation.Image = Properties.Resources.success;
		_selectedContractId = contract.Id;
	}
	
	private void AddPosition()
	{
		var form = _serviceProvider.GetRequiredService<CreatePositionForm>();

		if (form.ShowDialog() != DialogResult.OK || form.SelectedPosition is null)
		{
			return;
		}

		var position = form.SelectedPosition;

		_items.Add(new CreateOrderItemRow(
			position.TariffId,
			position.VenueName,
			position.StartDate,
			position.EndDate));
	}
}