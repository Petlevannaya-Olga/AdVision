using AdVision.Contracts;

namespace AdVision.Presentation
{
	public partial class EmployeesFilterUserControl : UserControl
	{
		public event Action? ApplyClicked;
		public event Action? ResetClicked;
		public event Action? FiltersChanged;

		public string LastNameFilter => txtLastName.Text.Trim();
		public string FirstNameFilter => txtFirstName.Text.Trim();
		public string MiddleNameFilter => txtMiddleName.Text.Trim();
		public string PhoneFilter => txtPhone.Text.Trim();

		public Guid? PositionIdFilter => cbPosition.SelectedItem is PositionDto position
			? position.Id
			: null;

		public EmployeesFilterUserControl()
		{
			InitializeComponent();
		}

		public void SetPositions(IReadOnlyList<PositionDto> positions)
		{
			cbPosition.DataSource = positions.ToList();
			cbPosition.DisplayMember = nameof(PositionDto.Name);
			cbPosition.ValueMember = nameof(PositionDto.Id);
			cbPosition.SelectedIndex = -1;
		}

		public void ResetFilters()
		{
			txtLastName.Clear();
			txtFirstName.Clear();
			txtMiddleName.Clear();
			txtPhone.Clear();
			cbPosition.SelectedIndex = -1;
		}

		public void SetResetEnabled(bool enabled)
		{
			btnReset.Enabled = enabled;
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

		private void TxtLastName_TextChanged(object sender, EventArgs e)
		{
			FiltersChanged?.Invoke();
		}

		private void TxtFirstName_TextChanged(object sender, EventArgs e)
		{
			FiltersChanged?.Invoke();
		}

		private void TxtMiddleName_TextChanged(object sender, EventArgs e)
		{
			FiltersChanged?.Invoke();
		}

		private void CbPosition_SelectedIndexChanged(object sender, EventArgs e)
		{
			FiltersChanged?.Invoke();
		}

		private void TxtPhone_TextChanged(object sender, EventArgs e)
		{
			FiltersChanged?.Invoke();
		}
	}
}