namespace AdVision.Presentation
{
	partial class CustomerDiscountsForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			label1 = new Label();
			dgvDiscounts = new DataGridView();
			cbDiscounts = new ComboBox();
			label2 = new Label();
			btnApply = new Button();
			btnClose = new Button();
			((System.ComponentModel.ISupportInitialize)dgvDiscounts).BeginInit();
			SuspendLayout();
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Location = new Point(10, 11);
			label1.Name = "label1";
			label1.Size = new Size(136, 20);
			label1.TabIndex = 0;
			label1.Text = "Доступные скидки";
			// 
			// dgvDiscounts
			// 
			dgvDiscounts.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dgvDiscounts.Location = new Point(10, 34);
			dgvDiscounts.Name = "dgvDiscounts";
			dgvDiscounts.RowHeadersWidth = 51;
			dgvDiscounts.Size = new Size(514, 188);
			dgvDiscounts.TabIndex = 1;
			// 
			// cbDiscounts
			// 
			cbDiscounts.FormattingEnabled = true;
			cbDiscounts.Location = new Point(12, 257);
			cbDiscounts.Name = "cbDiscounts";
			cbDiscounts.Size = new Size(344, 28);
			cbDiscounts.TabIndex = 2;
			// 
			// label2
			// 
			label2.AutoSize = true;
			label2.Location = new Point(12, 234);
			label2.Name = "label2";
			label2.Size = new Size(181, 20);
			label2.TabIndex = 3;
			label2.Text = "Дополнительные скидки";
			// 
			// btnApply
			// 
			btnApply.FlatStyle = FlatStyle.Popup;
			btnApply.Location = new Point(362, 257);
			btnApply.Name = "btnApply";
			btnApply.Size = new Size(162, 29);
			btnApply.TabIndex = 4;
			btnApply.Text = "Применить";
			btnApply.UseVisualStyleBackColor = true;
			btnApply.Click += btnApply_Click;
			// 
			// btnClose
			// 
			btnClose.FlatStyle = FlatStyle.Popup;
			btnClose.Location = new Point(362, 292);
			btnClose.Name = "btnClose";
			btnClose.Size = new Size(162, 29);
			btnClose.TabIndex = 4;
			btnClose.Text = "Закрыть";
			btnClose.UseVisualStyleBackColor = true;
			btnClose.Click += btnClose_Click;
			// 
			// CustomerDiscountsForm
			// 
			AutoScaleDimensions = new SizeF(8F, 20F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(536, 339);
			Controls.Add(btnClose);
			Controls.Add(btnApply);
			Controls.Add(label2);
			Controls.Add(cbDiscounts);
			Controls.Add(dgvDiscounts);
			Controls.Add(label1);
			FormBorderStyle = FormBorderStyle.FixedSingle;
			MaximizeBox = false;
			MinimizeBox = false;
			Name = "CustomerDiscountsForm";
			StartPosition = FormStartPosition.CenterScreen;
			Text = "Скидки клиента";
			((System.ComponentModel.ISupportInitialize)dgvDiscounts).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private Label label1;
		private DataGridView dgvDiscounts;
		private ComboBox cbDiscounts;
		private Label label2;
		private Button btnApply;
		private Button btnClose;
	}
}