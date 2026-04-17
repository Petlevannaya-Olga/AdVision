namespace AdVision.Presentation;

public partial class CustomersFilterUserControl : UserControl
{
    public event Action? ApplyClicked;
    public event Action? ResetClicked;
    public event Action? FiltersChanged;

    public string LastNameFilter => txtLastName.Text.Trim();
    public string FirstNameFilter => txtFirstName.Text.Trim();
    public string MiddleNameFilter => txtMiddleName.Text.Trim();
    public string PhoneFilter => txtPhone.Text?.Trim() ?? string.Empty;

    public CustomersFilterUserControl()
    {
        InitializeComponent();
        SubscribeEvents();
    }

    public void ResetFilters()
    {
        txtLastName.Clear();
        txtFirstName.Clear();
        txtMiddleName.Clear();
        txtPhone.Clear();
    }

    public void SetResetEnabled(bool enabled)
    {
        btnReset.Enabled = enabled;
    }

    private void SubscribeEvents()
    {
        btnApply.Click += BtnApply_Click;
        btnReset.Click += BtnReset_Click;

        txtLastName.TextChanged += AnyFilterChanged;
        txtFirstName.TextChanged += AnyFilterChanged;
        txtMiddleName.TextChanged += AnyFilterChanged;
        txtPhone.TextChanged += AnyFilterChanged;
    }

    private void BtnApply_Click(object? sender, EventArgs e)
    {
        ApplyClicked?.Invoke();
    }

    private void BtnReset_Click(object? sender, EventArgs e)
    {
        ResetFilters();
        ResetClicked?.Invoke();
    }

    private void AnyFilterChanged(object? sender, EventArgs e)
    {
        FiltersChanged?.Invoke();
    }
}