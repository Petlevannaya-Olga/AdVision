using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AdVision.Presentation
{
	public partial class PagingUserControl : UserControl
	{
		public event Action? PrevClicked;
		public event Action? NextClicked;
		public event Action? AddClicked;

		public PagingUserControl()
		{
			InitializeComponent();
		}

		public void SetState(
			int totalCount,
			int page,
			int totalPages,
			bool isLoading)
		{
			btnPrev.Enabled = !isLoading && page > 1;
			btnNext.Enabled = !isLoading && page < totalPages;

			lblPaging.Text = totalCount == 0
				? "Количество записей: 0"
				: $"Количество записей: {totalCount}, стр. {page} из {totalPages}";
		}

		public void SetAddVisible(bool visible)
		{
			btnAdd.Visible = visible;
		}

		public void SetAddEnabled(bool enabled)
		{
			btnAdd.Enabled = enabled;
		}

		private void BtnPrev_Click(object sender, EventArgs e)
		{
			PrevClicked?.Invoke();
		}

		private void BtnNext_Click(object sender, EventArgs e)
		{
			NextClicked?.Invoke();
		}

		private void BtnAdd_Click(object sender, EventArgs e)
		{
			AddClicked?.Invoke();
		}
	}
}
