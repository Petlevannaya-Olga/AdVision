namespace AdVision.Presentation
{
    public partial class DiscountsFilterUserControl : UserControl
    {
        public event Action? ApplyClicked;
        public event Action? ResetClicked;
        public event Action? FiltersChanged;

        public string NameFilter => txtName.Text.Trim();

        public decimal? PercentFrom => nudDiscountFrom.Value > nudDiscountFrom.Minimum
            ? nudDiscountFrom.Value
            : null;

        public decimal? PercentTo => nudDiscountTo.Value < nudDiscountTo.Maximum
            ? nudDiscountTo.Value
            : null;

        public decimal? MinTotalFrom => nudMinTotalFrom.Value > nudMinTotalFrom.Minimum
            ? nudMinTotalFrom.Value
            : null;

        public decimal? MinTotalTo => nudMinTotalTo.Value < nudMinTotalTo.Maximum
            ? nudMinTotalTo.Value
            : null;

        public DiscountsFilterUserControl()
        {
            InitializeComponent();
        }

        public void ResetFilters()
        {
            txtName.Clear();

            nudDiscountFrom.Value = nudDiscountFrom.Minimum;
            nudDiscountTo.Value = nudDiscountTo.Maximum;

            nudMinTotalFrom.Value = nudMinTotalFrom.Minimum;
            nudMinTotalTo.Value = nudMinTotalTo.Maximum;
        }

        public void SetResetEnabled(bool enabled)
        {
            btnReset.Enabled = enabled;
        }

        private void NudMinTotalFrom_ValueChanged(object sender, EventArgs e)
        {
            FiltersChanged?.Invoke();
        }

        private void NudMinTotalTo_ValueChanged(object sender, EventArgs e)
        {
            FiltersChanged?.Invoke();
        }

        private void NudDiscountFrom_ValueChanged(object sender, EventArgs e)
        {
            FiltersChanged?.Invoke();
        }

        private void NudDiscountTo_ValueChanged(object sender, EventArgs e)
        {
            FiltersChanged?.Invoke();
        }

        private void TxtName_TextChanged(object sender, EventArgs e)
        {
            FiltersChanged?.Invoke();
        }

        private void BtnReset_Click(object sender, EventArgs e)
        {
            ResetFilters();
            ResetClicked?.Invoke();
        }

        private void BtnApply_Click(object sender, EventArgs e)
        {
            ApplyClicked?.Invoke();
        }
    }
}