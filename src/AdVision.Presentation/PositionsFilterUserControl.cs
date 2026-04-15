namespace AdVision.Presentation
{
    public partial class PositionsFilterUserControl : UserControl
    {
        public event Action? ApplyClicked;
        public event Action? ResetClicked;
        public event Action? FiltersChanged;

        public string NameFilter => txtName.Text.Trim();

        public PositionsFilterUserControl()
        {
            InitializeComponent();
        }

        public void ResetFilters()
        {
            txtName.Clear();
        }

        private void BtnApply_Click(object sender, EventArgs e)
        {
            ApplyClicked?.Invoke();
        }

        private void BtnReset_Click(object sender, EventArgs e)
        {
            ResetFilters();
            ResetClicked?.Invoke();
        }

        public void SetResetEnabled(bool enabled)
        {
            btnReset.Enabled = enabled;
        }

        private void TxtName_TextChanged(object sender, EventArgs e)
        {
            FiltersChanged?.Invoke();
        }
    }
}