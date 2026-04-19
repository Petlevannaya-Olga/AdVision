namespace AdVision.Presentation
{
	partial class SelectContractForm
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
			tableLayoutPanel1 = new TableLayoutPanel();
			panel1 = new Panel();
			btnApply = new Button();
			btnReset = new Button();
			cbCustomer = new ComboBox();
			label13 = new Label();
			cbEmployee = new ComboBox();
			label12 = new Label();
			txtNumber = new TextBox();
			label11 = new Label();
			dgvContracts = new DataGridView();
			panel2 = new Panel();
			btnSelect = new Button();
			btnClose = new Button();
			tableLayoutPanel1.SuspendLayout();
			panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dgvContracts).BeginInit();
			panel2.SuspendLayout();
			SuspendLayout();
			// 
			// tableLayoutPanel1
			// 
			tableLayoutPanel1.ColumnCount = 1;
			tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
			tableLayoutPanel1.Controls.Add(panel1, 0, 0);
			tableLayoutPanel1.Controls.Add(dgvContracts, 0, 1);
			tableLayoutPanel1.Controls.Add(panel2, 0, 2);
			tableLayoutPanel1.Dock = DockStyle.Fill;
			tableLayoutPanel1.Location = new Point(0, 0);
			tableLayoutPanel1.Name = "tableLayoutPanel1";
			tableLayoutPanel1.RowCount = 3;
			tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 131F));
			tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
			tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
			tableLayoutPanel1.Size = new Size(800, 380);
			tableLayoutPanel1.TabIndex = 0;
			// 
			// panel1
			// 
			panel1.Controls.Add(btnApply);
			panel1.Controls.Add(btnReset);
			panel1.Controls.Add(cbCustomer);
			panel1.Controls.Add(label13);
			panel1.Controls.Add(cbEmployee);
			panel1.Controls.Add(label12);
			panel1.Controls.Add(txtNumber);
			panel1.Controls.Add(label11);
			panel1.Dock = DockStyle.Fill;
			panel1.Location = new Point(3, 3);
			panel1.Name = "panel1";
			panel1.Size = new Size(794, 125);
			panel1.TabIndex = 0;
			// 
			// btnApply
			// 
			btnApply.FlatStyle = FlatStyle.Popup;
			btnApply.Location = new Point(453, 84);
			btnApply.Name = "btnApply";
			btnApply.Size = new Size(138, 29);
			btnApply.TabIndex = 19;
			btnApply.Text = "Применить";
			btnApply.UseVisualStyleBackColor = true;
			btnApply.Click += BtnApply_Click;
			// 
			// btnReset
			// 
			btnReset.FlatStyle = FlatStyle.Popup;
			btnReset.Location = new Point(304, 84);
			btnReset.Name = "btnReset";
			btnReset.Size = new Size(146, 29);
			btnReset.TabIndex = 20;
			btnReset.Text = "Сбросить";
			btnReset.UseVisualStyleBackColor = true;
			btnReset.Click += BtnReset_Click;
			// 
			// cbCustomer
			// 
			cbCustomer.FormattingEnabled = true;
			cbCustomer.Location = new Point(303, 31);
			cbCustomer.Name = "cbCustomer";
			cbCustomer.Size = new Size(288, 28);
			cbCustomer.TabIndex = 15;
			// 
			// label13
			// 
			label13.AutoSize = true;
			label13.Location = new Point(303, 8);
			label13.Name = "label13";
			label13.Size = new Size(71, 20);
			label13.TabIndex = 13;
			label13.Text = "Заказчик";
			// 
			// cbEmployee
			// 
			cbEmployee.FormattingEnabled = true;
			cbEmployee.Location = new Point(9, 85);
			cbEmployee.Name = "cbEmployee";
			cbEmployee.Size = new Size(288, 28);
			cbEmployee.TabIndex = 17;
			// 
			// label12
			// 
			label12.AutoSize = true;
			label12.Location = new Point(9, 62);
			label12.Name = "label12";
			label12.Size = new Size(101, 20);
			label12.TabIndex = 14;
			label12.Text = "Исполнитель";
			// 
			// txtNumber
			// 
			txtNumber.BorderStyle = BorderStyle.FixedSingle;
			txtNumber.Location = new Point(9, 31);
			txtNumber.Name = "txtNumber";
			txtNumber.Size = new Size(288, 27);
			txtNumber.TabIndex = 12;
			// 
			// label11
			// 
			label11.AutoSize = true;
			label11.Location = new Point(9, 8);
			label11.Name = "label11";
			label11.Size = new Size(127, 20);
			label11.TabIndex = 11;
			label11.Text = "Номер договора";
			// 
			// dgvContracts
			// 
			dgvContracts.AllowUserToAddRows = false;
			dgvContracts.AllowUserToDeleteRows = false;
			dgvContracts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
			dgvContracts.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dgvContracts.Dock = DockStyle.Fill;
			dgvContracts.Location = new Point(3, 134);
			dgvContracts.Name = "dgvContracts";
			dgvContracts.ReadOnly = true;
			dgvContracts.RowHeadersWidth = 51;
			dgvContracts.Size = new Size(794, 203);
			dgvContracts.TabIndex = 1;
			dgvContracts.CellDoubleClick += DgvContracts_CellDoubleClick;
			// 
			// panel2
			// 
			panel2.Controls.Add(btnSelect);
			panel2.Controls.Add(btnClose);
			panel2.Dock = DockStyle.Fill;
			panel2.Location = new Point(3, 343);
			panel2.Name = "panel2";
			panel2.Size = new Size(794, 34);
			panel2.TabIndex = 2;
			// 
			// btnSelect
			// 
			btnSelect.FlatStyle = FlatStyle.Popup;
			btnSelect.Location = new Point(485, 3);
			btnSelect.Name = "btnSelect";
			btnSelect.Size = new Size(150, 29);
			btnSelect.TabIndex = 0;
			btnSelect.Text = "Выбрать";
			btnSelect.UseVisualStyleBackColor = true;
			btnSelect.Click += BtnSelect_Click;
			// 
			// btnClose
			// 
			btnClose.FlatStyle = FlatStyle.Popup;
			btnClose.Location = new Point(641, 3);
			btnClose.Name = "btnClose";
			btnClose.Size = new Size(150, 29);
			btnClose.TabIndex = 0;
			btnClose.Text = "Закрыть";
			btnClose.UseVisualStyleBackColor = true;
			btnClose.Click += BtnClose_Click;
			// 
			// SelectContractForm
			// 
			AutoScaleDimensions = new SizeF(8F, 20F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(800, 380);
			Controls.Add(tableLayoutPanel1);
			Name = "SelectContractForm";
			StartPosition = FormStartPosition.CenterScreen;
			Text = "Выбор договора";
			tableLayoutPanel1.ResumeLayout(false);
			panel1.ResumeLayout(false);
			panel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)dgvContracts).EndInit();
			panel2.ResumeLayout(false);
			ResumeLayout(false);
		}

		#endregion

		private TableLayoutPanel tableLayoutPanel1;
		private Panel panel1;
		private Button btnApply;
		private Button btnReset;
		private ComboBox cbCustomer;
		private Label label13;
		private ComboBox cbEmployee;
		private Label label12;
		private TextBox txtNumber;
		private Label label11;
		private DataGridView dgvContracts;
		private Panel panel2;
		private Button btnSelect;
		private Button btnClose;
	}
}