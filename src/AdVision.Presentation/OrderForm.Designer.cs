namespace AdVision.Presentation
{
	partial class OrderForm
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
			txtContractNumber = new TextBox();
			label1 = new Label();
			btnSelect = new Button();
			pbContractValidation = new PictureBox();
			label2 = new Label();
			dgvOrderItems = new DataGridView();
			orderItemsPagingUserControl = new PagingUserControl();
			btnSave = new Button();
			btnClose = new Button();
			tableLayoutPanel1 = new TableLayoutPanel();
			panel1 = new Panel();
			panel2 = new Panel();
			lblTotalAmount = new Label();
			((System.ComponentModel.ISupportInitialize)pbContractValidation).BeginInit();
			((System.ComponentModel.ISupportInitialize)dgvOrderItems).BeginInit();
			tableLayoutPanel1.SuspendLayout();
			panel1.SuspendLayout();
			panel2.SuspendLayout();
			SuspendLayout();
			// 
			// txtContractNumber
			// 
			txtContractNumber.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			txtContractNumber.BorderStyle = BorderStyle.FixedSingle;
			txtContractNumber.Location = new Point(3, 3);
			txtContractNumber.Name = "txtContractNumber";
			txtContractNumber.ReadOnly = true;
			txtContractNumber.Size = new Size(777, 27);
			txtContractNumber.TabIndex = 0;
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Location = new Point(13, 10);
			label1.Name = "label1";
			label1.Size = new Size(69, 20);
			label1.TabIndex = 1;
			label1.Text = "Договор";
			// 
			// btnSelect
			// 
			btnSelect.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
			btnSelect.FlatStyle = FlatStyle.Popup;
			btnSelect.Location = new Point(786, 3);
			btnSelect.Name = "btnSelect";
			btnSelect.Size = new Size(37, 27);
			btnSelect.TabIndex = 2;
			btnSelect.Text = "...";
			btnSelect.UseVisualStyleBackColor = true;
			btnSelect.Click += BtnSelect_Click;
			// 
			// pbContractValidation
			// 
			pbContractValidation.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
			pbContractValidation.BackColor = SystemColors.Control;
			pbContractValidation.Location = new Point(829, 3);
			pbContractValidation.Name = "pbContractValidation";
			pbContractValidation.Size = new Size(27, 27);
			pbContractValidation.TabIndex = 3;
			pbContractValidation.TabStop = false;
			// 
			// label2
			// 
			label2.AutoSize = true;
			label2.Location = new Point(13, 70);
			label2.Name = "label2";
			label2.Size = new Size(72, 20);
			label2.TabIndex = 4;
			label2.Text = "Позиции";
			// 
			// dgvOrderItems
			// 
			dgvOrderItems.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dgvOrderItems.Dock = DockStyle.Fill;
			dgvOrderItems.Location = new Point(13, 93);
			dgvOrderItems.Name = "dgvOrderItems";
			dgvOrderItems.RowHeadersWidth = 51;
			dgvOrderItems.Size = new Size(859, 335);
			dgvOrderItems.TabIndex = 5;
			// 
			// orderItemsPagingUserControl
			// 
			orderItemsPagingUserControl.Dock = DockStyle.Fill;
			orderItemsPagingUserControl.Location = new Point(13, 434);
			orderItemsPagingUserControl.Name = "orderItemsPagingUserControl";
			orderItemsPagingUserControl.Size = new Size(859, 35);
			orderItemsPagingUserControl.TabIndex = 6;
			// 
			// btnSave
			// 
			btnSave.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
			btnSave.FlatStyle = FlatStyle.Popup;
			btnSave.Location = new Point(663, 9);
			btnSave.Name = "btnSave";
			btnSave.Size = new Size(94, 26);
			btnSave.TabIndex = 7;
			btnSave.Text = "Сохранить";
			btnSave.UseVisualStyleBackColor = true;
			// 
			// btnClose
			// 
			btnClose.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
			btnClose.FlatStyle = FlatStyle.Popup;
			btnClose.Location = new Point(762, 9);
			btnClose.Name = "btnClose";
			btnClose.Size = new Size(94, 26);
			btnClose.TabIndex = 7;
			btnClose.Text = "Закрыть";
			btnClose.UseVisualStyleBackColor = true;
			// 
			// tableLayoutPanel1
			// 
			tableLayoutPanel1.ColumnCount = 1;
			tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
			tableLayoutPanel1.Controls.Add(label1, 0, 0);
			tableLayoutPanel1.Controls.Add(panel1, 0, 1);
			tableLayoutPanel1.Controls.Add(label2, 0, 2);
			tableLayoutPanel1.Controls.Add(orderItemsPagingUserControl, 0, 4);
			tableLayoutPanel1.Controls.Add(dgvOrderItems, 0, 3);
			tableLayoutPanel1.Controls.Add(panel2, 0, 5);
			tableLayoutPanel1.Dock = DockStyle.Fill;
			tableLayoutPanel1.Location = new Point(0, 0);
			tableLayoutPanel1.Name = "tableLayoutPanel1";
			tableLayoutPanel1.Padding = new Padding(10);
			tableLayoutPanel1.RowCount = 6;
			tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
			tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
			tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
			tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
			tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 41F));
			tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
			tableLayoutPanel1.Size = new Size(885, 532);
			tableLayoutPanel1.TabIndex = 8;
			// 
			// panel1
			// 
			panel1.Controls.Add(txtContractNumber);
			panel1.Controls.Add(btnSelect);
			panel1.Controls.Add(pbContractValidation);
			panel1.Dock = DockStyle.Fill;
			panel1.Location = new Point(13, 33);
			panel1.Name = "panel1";
			panel1.Size = new Size(859, 34);
			panel1.TabIndex = 2;
			// 
			// panel2
			// 
			panel2.Controls.Add(lblTotalAmount);
			panel2.Controls.Add(btnClose);
			panel2.Controls.Add(btnSave);
			panel2.Dock = DockStyle.Fill;
			panel2.Location = new Point(13, 475);
			panel2.Name = "panel2";
			panel2.Size = new Size(859, 44);
			panel2.TabIndex = 7;
			// 
			// lblTotalAmount
			// 
			lblTotalAmount.AutoSize = true;
			lblTotalAmount.Location = new Point(3, 12);
			lblTotalAmount.Name = "lblTotalAmount";
			lblTotalAmount.Size = new Size(50, 20);
			lblTotalAmount.TabIndex = 8;
			lblTotalAmount.Text = "label3";
			// 
			// OrderForm
			// 
			AutoScaleDimensions = new SizeF(8F, 20F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(885, 532);
			Controls.Add(tableLayoutPanel1);
			Name = "OrderForm";
			StartPosition = FormStartPosition.CenterScreen;
			Text = "Создание заказа";
			((System.ComponentModel.ISupportInitialize)pbContractValidation).EndInit();
			((System.ComponentModel.ISupportInitialize)dgvOrderItems).EndInit();
			tableLayoutPanel1.ResumeLayout(false);
			tableLayoutPanel1.PerformLayout();
			panel1.ResumeLayout(false);
			panel1.PerformLayout();
			panel2.ResumeLayout(false);
			panel2.PerformLayout();
			ResumeLayout(false);
		}

		#endregion

		private TextBox txtContractNumber;
		private Label label1;
		private Button btnSelect;
		private PictureBox pbContractValidation;
		private Label label2;
		private DataGridView dgvOrderItems;
		private PagingUserControl orderItemsPagingUserControl;
		private Button btnSave;
		private Button btnClose;
		private TableLayoutPanel tableLayoutPanel1;
		private Panel panel1;
		private Panel panel2;
		private Label lblTotalAmount;
	}
}