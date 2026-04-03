using AdVision.Application.VenueTypes.CreateVenueTypeCommand;
using AdVision.Contracts;
using Shared.Abstractions;

namespace AdVision.Presentation
{
    public partial class VenueTypeForm : Form
    {
        private readonly ICommandHandler<Guid, CreateVenueTypeCommand> _commandHandler;
        private readonly CancellationTokenSource _ct = new ();

        public VenueTypeForm(ICommandHandler<Guid, CreateVenueTypeCommand> commandHandler)
        {
            _commandHandler = commandHandler;
            InitializeComponent();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            errorProvider1.SetError(txtName,
                string.IsNullOrWhiteSpace(txtName.Text) ? "Введите название площадки" : "");

            _commandHandler.Handle(new CreateVenueTypeCommand(new CreateVenueTypeDto(txtName.Text)), _ct.Token);
        }

        private void VenueTypeForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            _ct.Dispose();
        }
    }
}