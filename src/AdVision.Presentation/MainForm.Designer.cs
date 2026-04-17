namespace AdVision.Presentation;

partial class MainForm
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
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
	///  Required method for Designer support - do not modify
	///  the contents of this method with the code editor.
	/// </summary>
	private void InitializeComponent()
	{
		tabControl1 = new TabControl();
		tabPage1 = new TabPage();
		tableLayoutPanel2 = new TableLayoutPanel();
		venuesDataGridView = new DataGridView();
		panel2 = new Panel();
		btnApply = new Button();
		btnReset = new Button();
		chkDescending = new CheckBox();
		nudRatingTo = new NumericUpDown();
		nudRatingFrom = new NumericUpDown();
		label10 = new Label();
		label9 = new Label();
		cbSortOrder = new ComboBox();
		label7 = new Label();
		cbCities = new ComboBox();
		cbDistricts = new ComboBox();
		label5 = new Label();
		cbRegions = new ComboBox();
		label4 = new Label();
		cbVenueTypes = new ComboBox();
		label3 = new Label();
		txtStreet = new TextBox();
		label8 = new Label();
		txtName = new TextBox();
		label6 = new Label();
		label2 = new Label();
		label1 = new Label();
		venuesPagingUserControl = new PagingUserControl();
		tabPage2 = new TabPage();
		tableLayoutPanel3 = new TableLayoutPanel();
		tableLayoutPanel1 = new TableLayoutPanel();
		panel3 = new Panel();
		btnDiscounts = new Button();
		btnCustomers = new Button();
		btnEmployees = new Button();
		btnPositions = new Button();
		btnVenueTypes = new Button();
		tableLayoutPanel4 = new TableLayoutPanel();
		pnlFilters = new Panel();
		dgvDirectories = new DataGridView();
		panel4 = new Panel();
		directoriesPagingUserControl = new PagingUserControl();
		tabPage3 = new TabPage();
		tabPage4 = new TabPage();
		tabPage5 = new TabPage();
		tabControl1.SuspendLayout();
		tabPage1.SuspendLayout();
		tableLayoutPanel2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)venuesDataGridView).BeginInit();
		panel2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)nudRatingTo).BeginInit();
		((System.ComponentModel.ISupportInitialize)nudRatingFrom).BeginInit();
		tabPage2.SuspendLayout();
		tableLayoutPanel3.SuspendLayout();
		tableLayoutPanel1.SuspendLayout();
		panel3.SuspendLayout();
		tableLayoutPanel4.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)dgvDirectories).BeginInit();
		panel4.SuspendLayout();
		SuspendLayout();
		// 
		// tabControl1
		// 
		tabControl1.Controls.Add(tabPage1);
		tabControl1.Controls.Add(tabPage2);
		tabControl1.Controls.Add(tabPage3);
		tabControl1.Controls.Add(tabPage4);
		tabControl1.Controls.Add(tabPage5);
		tabControl1.Dock = DockStyle.Fill;
		tabControl1.Location = new Point(0, 0);
		tabControl1.Name = "tabControl1";
		tabControl1.SelectedIndex = 0;
		tabControl1.Size = new Size(1090, 572);
		tabControl1.TabIndex = 0;
		tabControl1.SelectedIndexChanged += TabControl1_SelectedIndexChanged;
		// 
		// tabPage1
		// 
		tabPage1.Controls.Add(tableLayoutPanel2);
		tabPage1.Location = new Point(4, 29);
		tabPage1.Name = "tabPage1";
		tabPage1.Padding = new Padding(3);
		tabPage1.Size = new Size(1082, 539);
		tabPage1.TabIndex = 0;
		tabPage1.Text = "Площадки";
		tabPage1.UseVisualStyleBackColor = true;
		// 
		// tableLayoutPanel2
		// 
		tableLayoutPanel2.ColumnCount = 2;
		tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 300F));
		tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
		tableLayoutPanel2.Controls.Add(venuesDataGridView, 1, 0);
		tableLayoutPanel2.Controls.Add(panel2, 0, 0);
		tableLayoutPanel2.Controls.Add(venuesPagingUserControl, 1, 1);
		tableLayoutPanel2.Dock = DockStyle.Fill;
		tableLayoutPanel2.Location = new Point(3, 3);
		tableLayoutPanel2.Name = "tableLayoutPanel2";
		tableLayoutPanel2.RowCount = 2;
		tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
		tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 53F));
		tableLayoutPanel2.Size = new Size(1076, 533);
		tableLayoutPanel2.TabIndex = 2;
		// 
		// venuesDataGridView
		// 
		venuesDataGridView.AllowUserToAddRows = false;
		venuesDataGridView.AllowUserToDeleteRows = false;
		venuesDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
		venuesDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		venuesDataGridView.Dock = DockStyle.Fill;
		venuesDataGridView.Location = new Point(303, 3);
		venuesDataGridView.Name = "venuesDataGridView";
		venuesDataGridView.RowHeadersWidth = 51;
		venuesDataGridView.Size = new Size(770, 474);
		venuesDataGridView.TabIndex = 0;
		// 
		// panel2
		// 
		panel2.Controls.Add(btnApply);
		panel2.Controls.Add(btnReset);
		panel2.Controls.Add(chkDescending);
		panel2.Controls.Add(nudRatingTo);
		panel2.Controls.Add(nudRatingFrom);
		panel2.Controls.Add(label10);
		panel2.Controls.Add(label9);
		panel2.Controls.Add(cbSortOrder);
		panel2.Controls.Add(label7);
		panel2.Controls.Add(cbCities);
		panel2.Controls.Add(cbDistricts);
		panel2.Controls.Add(label5);
		panel2.Controls.Add(cbRegions);
		panel2.Controls.Add(label4);
		panel2.Controls.Add(cbVenueTypes);
		panel2.Controls.Add(label3);
		panel2.Controls.Add(txtStreet);
		panel2.Controls.Add(label8);
		panel2.Controls.Add(txtName);
		panel2.Controls.Add(label6);
		panel2.Controls.Add(label2);
		panel2.Controls.Add(label1);
		panel2.Dock = DockStyle.Fill;
		panel2.Location = new Point(3, 3);
		panel2.Name = "panel2";
		panel2.Size = new Size(294, 474);
		panel2.TabIndex = 3;
		// 
		// btnApply
		// 
		btnApply.FlatStyle = FlatStyle.Popup;
		btnApply.Location = new Point(149, 427);
		btnApply.Name = "btnApply";
		btnApply.Size = new Size(145, 29);
		btnApply.TabIndex = 4;
		btnApply.Text = "Применить";
		btnApply.UseVisualStyleBackColor = true;
		btnApply.Click += BtnApply_Click;
		// 
		// btnReset
		// 
		btnReset.FlatStyle = FlatStyle.Popup;
		btnReset.Location = new Point(0, 427);
		btnReset.Name = "btnReset";
		btnReset.Size = new Size(146, 29);
		btnReset.TabIndex = 4;
		btnReset.Text = "Сбросить";
		btnReset.UseVisualStyleBackColor = true;
		btnReset.Click += BtnReset_Click;
		// 
		// chkDescending
		// 
		chkDescending.AutoSize = true;
		chkDescending.Location = new Point(152, 391);
		chkDescending.Name = "chkDescending";
		chkDescending.Size = new Size(128, 24);
		chkDescending.TabIndex = 7;
		chkDescending.Text = "По убыванию";
		chkDescending.UseVisualStyleBackColor = true;
		chkDescending.CheckedChanged += ChkDescending_CheckedChanged;
		// 
		// nudRatingTo
		// 
		nudRatingTo.Location = new Point(178, 334);
		nudRatingTo.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
		nudRatingTo.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
		nudRatingTo.Name = "nudRatingTo";
		nudRatingTo.Size = new Size(113, 27);
		nudRatingTo.TabIndex = 6;
		nudRatingTo.Value = new decimal(new int[] { 10, 0, 0, 0 });
		nudRatingTo.ValueChanged += NudRatingTo_ValueChanged;
		// 
		// nudRatingFrom
		// 
		nudRatingFrom.Location = new Point(30, 334);
		nudRatingFrom.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
		nudRatingFrom.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
		nudRatingFrom.Name = "nudRatingFrom";
		nudRatingFrom.Size = new Size(113, 27);
		nudRatingFrom.TabIndex = 6;
		nudRatingFrom.Value = new decimal(new int[] { 1, 0, 0, 0 });
		nudRatingFrom.ValueChanged += NudRatingFrom_ValueChanged;
		// 
		// label10
		// 
		label10.AutoSize = true;
		label10.Location = new Point(149, 338);
		label10.Name = "label10";
		label10.Size = new Size(26, 20);
		label10.TabIndex = 5;
		label10.Text = "до";
		// 
		// label9
		// 
		label9.AutoSize = true;
		label9.Location = new Point(3, 336);
		label9.Name = "label9";
		label9.Size = new Size(24, 20);
		label9.TabIndex = 5;
		label9.Text = "от";
		// 
		// cbSortOrder
		// 
		cbSortOrder.FormattingEnabled = true;
		cbSortOrder.Items.AddRange(new object[] { "Название", "Тип", "Регион", "Район", "Город", "Улица", "Широта", "Долгота", "Ширина", "Высота" });
		cbSortOrder.Location = new Point(0, 389);
		cbSortOrder.Name = "cbSortOrder";
		cbSortOrder.Size = new Size(146, 28);
		cbSortOrder.TabIndex = 4;
		cbSortOrder.SelectedIndexChanged += CbSortOrder_SelectedIndexChanged;
		// 
		// label7
		// 
		label7.AutoSize = true;
		label7.Location = new Point(3, 367);
		label7.Name = "label7";
		label7.Size = new Size(121, 20);
		label7.TabIndex = 3;
		label7.Text = "Сортировать по";
		// 
		// cbCities
		// 
		cbCities.FormattingEnabled = true;
		cbCities.Location = new Point(0, 230);
		cbCities.Name = "cbCities";
		cbCities.Size = new Size(291, 28);
		cbCities.TabIndex = 2;
		cbCities.SelectedIndexChanged += CbCities_SelectedIndexChanged;
		// 
		// cbDistricts
		// 
		cbDistricts.FormattingEnabled = true;
		cbDistricts.Location = new Point(0, 178);
		cbDistricts.Name = "cbDistricts";
		cbDistricts.Size = new Size(291, 28);
		cbDistricts.TabIndex = 2;
		cbDistricts.SelectedIndexChanged += CbDistricts_SelectedIndexChanged;
		// 
		// label5
		// 
		label5.AutoSize = true;
		label5.Location = new Point(0, 208);
		label5.Name = "label5";
		label5.Size = new Size(51, 20);
		label5.TabIndex = 0;
		label5.Text = "Город";
		// 
		// cbRegions
		// 
		cbRegions.FormattingEnabled = true;
		cbRegions.Location = new Point(0, 126);
		cbRegions.Name = "cbRegions";
		cbRegions.Size = new Size(291, 28);
		cbRegions.TabIndex = 2;
		cbRegions.SelectedIndexChanged += CbRegions_SelectedIndexChanged;
		// 
		// label4
		// 
		label4.AutoSize = true;
		label4.Location = new Point(0, 156);
		label4.Name = "label4";
		label4.Size = new Size(52, 20);
		label4.TabIndex = 0;
		label4.Text = "Район";
		// 
		// cbVenueTypes
		// 
		cbVenueTypes.FormattingEnabled = true;
		cbVenueTypes.Location = new Point(0, 74);
		cbVenueTypes.Name = "cbVenueTypes";
		cbVenueTypes.Size = new Size(291, 28);
		cbVenueTypes.TabIndex = 2;
		cbVenueTypes.SelectedIndexChanged += CbVenueTypes_SelectedIndexChanged;
		// 
		// label3
		// 
		label3.AutoSize = true;
		label3.Location = new Point(0, 104);
		label3.Name = "label3";
		label3.Size = new Size(58, 20);
		label3.TabIndex = 0;
		label3.Text = "Регион";
		// 
		// txtStreet
		// 
		txtStreet.BorderStyle = BorderStyle.FixedSingle;
		txtStreet.Location = new Point(0, 282);
		txtStreet.Name = "txtStreet";
		txtStreet.Size = new Size(291, 27);
		txtStreet.TabIndex = 1;
		txtStreet.TextChanged += TxtStreet_TextChanged;
		// 
		// label8
		// 
		label8.AutoSize = true;
		label8.Location = new Point(0, 311);
		label8.Name = "label8";
		label8.Size = new Size(64, 20);
		label8.TabIndex = 0;
		label8.Text = "Рейтинг";
		// 
		// txtName
		// 
		txtName.BorderStyle = BorderStyle.FixedSingle;
		txtName.Location = new Point(0, 23);
		txtName.Name = "txtName";
		txtName.Size = new Size(291, 27);
		txtName.TabIndex = 1;
		txtName.TextChanged += TxtName_TextChanged;
		// 
		// label6
		// 
		label6.AutoSize = true;
		label6.Location = new Point(0, 260);
		label6.Name = "label6";
		label6.Size = new Size(52, 20);
		label6.TabIndex = 0;
		label6.Text = "Улица";
		// 
		// label2
		// 
		label2.AutoSize = true;
		label2.Location = new Point(0, 52);
		label2.Name = "label2";
		label2.Size = new Size(35, 20);
		label2.TabIndex = 0;
		label2.Text = "Тип";
		// 
		// label1
		// 
		label1.AutoSize = true;
		label1.Location = new Point(0, 0);
		label1.Name = "label1";
		label1.Size = new Size(77, 20);
		label1.TabIndex = 0;
		label1.Text = "Название";
		// 
		// venuesPagingUserControl
		// 
		venuesPagingUserControl.Dock = DockStyle.Fill;
		venuesPagingUserControl.Location = new Point(303, 483);
		venuesPagingUserControl.Name = "venuesPagingUserControl";
		venuesPagingUserControl.Size = new Size(770, 47);
		venuesPagingUserControl.TabIndex = 4;
		// 
		// tabPage2
		// 
		tabPage2.Controls.Add(tableLayoutPanel3);
		tabPage2.Location = new Point(4, 29);
		tabPage2.Name = "tabPage2";
		tabPage2.Padding = new Padding(3);
		tabPage2.Size = new Size(1082, 539);
		tabPage2.TabIndex = 1;
		tabPage2.Text = "Справочники";
		tabPage2.UseVisualStyleBackColor = true;
		// 
		// tableLayoutPanel3
		// 
		tableLayoutPanel3.ColumnCount = 1;
		tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
		tableLayoutPanel3.Controls.Add(tableLayoutPanel1, 0, 0);
		tableLayoutPanel3.Controls.Add(panel4, 0, 1);
		tableLayoutPanel3.Dock = DockStyle.Fill;
		tableLayoutPanel3.Location = new Point(3, 3);
		tableLayoutPanel3.Name = "tableLayoutPanel3";
		tableLayoutPanel3.RowCount = 2;
		tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
		tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 44F));
		tableLayoutPanel3.Size = new Size(1076, 533);
		tableLayoutPanel3.TabIndex = 1;
		// 
		// tableLayoutPanel1
		// 
		tableLayoutPanel1.ColumnCount = 2;
		tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
		tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
		tableLayoutPanel1.Controls.Add(panel3, 0, 0);
		tableLayoutPanel1.Controls.Add(tableLayoutPanel4, 1, 0);
		tableLayoutPanel1.Dock = DockStyle.Fill;
		tableLayoutPanel1.Location = new Point(3, 3);
		tableLayoutPanel1.Name = "tableLayoutPanel1";
		tableLayoutPanel1.RowCount = 1;
		tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
		tableLayoutPanel1.Size = new Size(1070, 483);
		tableLayoutPanel1.TabIndex = 0;
		// 
		// panel3
		// 
		panel3.Controls.Add(btnDiscounts);
		panel3.Controls.Add(btnCustomers);
		panel3.Controls.Add(btnEmployees);
		panel3.Controls.Add(btnPositions);
		panel3.Controls.Add(btnVenueTypes);
		panel3.Dock = DockStyle.Fill;
		panel3.Location = new Point(3, 3);
		panel3.Name = "panel3";
		panel3.Size = new Size(294, 477);
		panel3.TabIndex = 0;
		// 
		// btnDiscounts
		// 
		btnDiscounts.FlatStyle = FlatStyle.Popup;
		btnDiscounts.Location = new Point(3, 143);
		btnDiscounts.Name = "btnDiscounts";
		btnDiscounts.Size = new Size(288, 29);
		btnDiscounts.TabIndex = 0;
		btnDiscounts.Text = "Скидки";
		btnDiscounts.UseVisualStyleBackColor = true;
		btnDiscounts.Click += BtnDiscounts_Click;
		// 
		// btnCustomers
		// 
		btnCustomers.FlatStyle = FlatStyle.Popup;
		btnCustomers.Location = new Point(3, 108);
		btnCustomers.Name = "btnCustomers";
		btnCustomers.Size = new Size(288, 29);
		btnCustomers.TabIndex = 0;
		btnCustomers.Text = "Заказчики";
		btnCustomers.UseVisualStyleBackColor = true;
		btnCustomers.Click += BtnCustomers_Click;
		// 
		// btnEmployees
		// 
		btnEmployees.FlatStyle = FlatStyle.Popup;
		btnEmployees.Location = new Point(3, 73);
		btnEmployees.Name = "btnEmployees";
		btnEmployees.Size = new Size(288, 29);
		btnEmployees.TabIndex = 0;
		btnEmployees.Text = "Сотрудники";
		btnEmployees.UseVisualStyleBackColor = true;
		btnEmployees.Click += BtnEmployees_Click;
		// 
		// btnPositions
		// 
		btnPositions.FlatStyle = FlatStyle.Popup;
		btnPositions.Location = new Point(3, 38);
		btnPositions.Name = "btnPositions";
		btnPositions.Size = new Size(288, 29);
		btnPositions.TabIndex = 0;
		btnPositions.Text = "Должности";
		btnPositions.UseVisualStyleBackColor = true;
		btnPositions.Click += BtnPositions_Click;
		// 
		// btnVenueTypes
		// 
		btnVenueTypes.FlatStyle = FlatStyle.Popup;
		btnVenueTypes.Location = new Point(3, 3);
		btnVenueTypes.Name = "btnVenueTypes";
		btnVenueTypes.Size = new Size(288, 29);
		btnVenueTypes.TabIndex = 0;
		btnVenueTypes.Text = "Типы площадок";
		btnVenueTypes.UseVisualStyleBackColor = true;
		btnVenueTypes.Click += BtnVenueTypes_Click;
		// 
		// tableLayoutPanel4
		// 
		tableLayoutPanel4.ColumnCount = 1;
		tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
		tableLayoutPanel4.Controls.Add(pnlFilters, 0, 0);
		tableLayoutPanel4.Controls.Add(dgvDirectories, 0, 1);
		tableLayoutPanel4.Dock = DockStyle.Fill;
		tableLayoutPanel4.Location = new Point(303, 3);
		tableLayoutPanel4.Name = "tableLayoutPanel4";
		tableLayoutPanel4.RowCount = 2;
		tableLayoutPanel4.RowStyles.Add(new RowStyle());
		tableLayoutPanel4.RowStyles.Add(new RowStyle());
		tableLayoutPanel4.Size = new Size(764, 477);
		tableLayoutPanel4.TabIndex = 1;
		// 
		// pnlFilters
		// 
		pnlFilters.Dock = DockStyle.Fill;
		pnlFilters.Location = new Point(3, 3);
		pnlFilters.Name = "pnlFilters";
		pnlFilters.Size = new Size(758, 235);
		pnlFilters.TabIndex = 1;
		// 
		// dgvDirectories
		// 
		dgvDirectories.AllowUserToAddRows = false;
		dgvDirectories.AllowUserToDeleteRows = false;
		dgvDirectories.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
		dgvDirectories.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		dgvDirectories.Dock = DockStyle.Fill;
		dgvDirectories.Location = new Point(3, 244);
		dgvDirectories.Name = "dgvDirectories";
		dgvDirectories.ReadOnly = true;
		dgvDirectories.RowHeadersWidth = 51;
		dgvDirectories.Size = new Size(758, 262);
		dgvDirectories.TabIndex = 1;
		dgvDirectories.CellDoubleClick += DgvDirectories_CellDoubleClick;
		// 
		// panel4
		// 
		panel4.Controls.Add(directoriesPagingUserControl);
		panel4.Dock = DockStyle.Fill;
		panel4.Location = new Point(3, 492);
		panel4.Name = "panel4";
		panel4.Size = new Size(1070, 38);
		panel4.TabIndex = 1;
		// 
		// directoriesPagingUserControl
		// 
		directoriesPagingUserControl.Dock = DockStyle.Fill;
		directoriesPagingUserControl.Location = new Point(0, 0);
		directoriesPagingUserControl.Name = "directoriesPagingUserControl";
		directoriesPagingUserControl.Size = new Size(1070, 38);
		directoriesPagingUserControl.TabIndex = 0;
		// 
		// tabPage3
		// 
		tabPage3.Location = new Point(4, 29);
		tabPage3.Name = "tabPage3";
		tabPage3.Padding = new Padding(3);
		tabPage3.Size = new Size(1082, 539);
		tabPage3.TabIndex = 2;
		tabPage3.Text = "Договоры";
		tabPage3.UseVisualStyleBackColor = true;
		// 
		// tabPage4
		// 
		tabPage4.Location = new Point(4, 29);
		tabPage4.Name = "tabPage4";
		tabPage4.Size = new Size(1082, 539);
		tabPage4.TabIndex = 3;
		tabPage4.Text = "Заказы";
		tabPage4.UseVisualStyleBackColor = true;
		// 
		// tabPage5
		// 
		tabPage5.Location = new Point(4, 29);
		tabPage5.Name = "tabPage5";
		tabPage5.Size = new Size(1082, 539);
		tabPage5.TabIndex = 4;
		tabPage5.Text = "Оплаты";
		tabPage5.UseVisualStyleBackColor = true;
		// 
		// MainForm
		// 
		AutoScaleDimensions = new SizeF(8F, 20F);
		AutoScaleMode = AutoScaleMode.Font;
		ClientSize = new Size(1090, 572);
		Controls.Add(tabControl1);
		Name = "MainForm";
		StartPosition = FormStartPosition.CenterScreen;
		Text = "Информационная система рекламной компании";
		FormClosed += MainForm_FormClosed;
		tabControl1.ResumeLayout(false);
		tabPage1.ResumeLayout(false);
		tableLayoutPanel2.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)venuesDataGridView).EndInit();
		panel2.ResumeLayout(false);
		panel2.PerformLayout();
		((System.ComponentModel.ISupportInitialize)nudRatingTo).EndInit();
		((System.ComponentModel.ISupportInitialize)nudRatingFrom).EndInit();
		tabPage2.ResumeLayout(false);
		tableLayoutPanel3.ResumeLayout(false);
		tableLayoutPanel1.ResumeLayout(false);
		panel3.ResumeLayout(false);
		tableLayoutPanel4.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)dgvDirectories).EndInit();
		panel4.ResumeLayout(false);
		ResumeLayout(false);
	}

	#endregion

	private TabControl tabControl1;
	private TabPage tabPage1;
	private TabPage tabPage2;
	private DataGridView venuesDataGridView;
	private TableLayoutPanel tableLayoutPanel2;
	private Panel panel2;
	private TextBox txtName;
	private Label label2;
	private Label label1;
	private ComboBox cbVenueTypes;
	private ComboBox cbCities;
	private ComboBox cbDistricts;
	private Label label5;
	private ComboBox cbRegions;
	private Label label4;
	private Label label3;
	private TextBox txtStreet;
	private Label label6;
	private Button btnApply;
	private ComboBox cbSortOrder;
	private Label label7;
	private Label label10;
	private Label label9;
	private Label label8;
	private NumericUpDown nudRatingTo;
	private NumericUpDown nudRatingFrom;
	private Button btnReset;
	private CheckBox chkDescending;
	private TableLayoutPanel tableLayoutPanel3;
	private TableLayoutPanel tableLayoutPanel1;
	private Panel panel3;
	private Button btnPositions;
	private Button btnVenueTypes;
	private TabPage tabPage3;
	private TabPage tabPage4;
	private TabPage tabPage5;
	private Button btnCustomers;
	private Button btnEmployees;
	private DataGridView dgvDirectories;
	private Button btnDiscounts;
	private Panel pnlFilters;
	private TableLayoutPanel tableLayoutPanel4;
	private Panel panel4;
	private PagingUserControl venuesPagingUserControl;
	private PagingUserControl directoriesPagingUserControl;
}