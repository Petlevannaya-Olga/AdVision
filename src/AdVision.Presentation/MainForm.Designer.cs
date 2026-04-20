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
		tableLayoutPanel5 = new TableLayoutPanel();
		dgvContracts = new DataGridView();
		panel1 = new Panel();
		label24 = new Label();
		label22 = new Label();
		label20 = new Label();
		dtpSignedDateTo = new DateTimePicker();
		label23 = new Label();
		dtpEndDateTo = new DateTimePicker();
		label21 = new Label();
		dtpStartDateTo = new DateTimePicker();
		label19 = new Label();
		btnContractsApply = new Button();
		btnContractsReset = new Button();
		cbDesc = new CheckBox();
		cbOrder = new ComboBox();
		label18 = new Label();
		label17 = new Label();
		dtpSignedDateFrom = new DateTimePicker();
		label16 = new Label();
		dtpEndDateFrom = new DateTimePicker();
		label15 = new Label();
		dtpStartDateFrom = new DateTimePicker();
		label14 = new Label();
		cbCustomer = new ComboBox();
		label13 = new Label();
		cbStatuses = new ComboBox();
		cbEmployee = new ComboBox();
		label12 = new Label();
		txtContractNumber = new TextBox();
		label11 = new Label();
		contractsPagingUserControl = new PagingUserControl();
		tabPage4 = new TabPage();
		tableLayoutPanel6 = new TableLayoutPanel();
		tableLayoutPanel7 = new TableLayoutPanel();
		dgvOrders = new DataGridView();
		dgvOrderItems = new DataGridView();
		ordersPagingUserControl = new PagingUserControl();
		panel5 = new Panel();
		tabPage5 = new TabPage();
		label25 = new Label();
		label26 = new Label();
		label27 = new Label();
		label28 = new Label();
		dtpOrderEndDateTo = new DateTimePicker();
		label29 = new Label();
		dtpOrderStartDateTo = new DateTimePicker();
		label30 = new Label();
		label31 = new Label();
		dtpOrderEndDateFrom = new DateTimePicker();
		label32 = new Label();
		dtpOrderStartDateFrom = new DateTimePicker();
		label33 = new Label();
		cbOrderCustomers = new ComboBox();
		label34 = new Label();
		cbOrderStatuses = new ComboBox();
		cbOrderEmployees = new ComboBox();
		label35 = new Label();
		txtOrderContractNumber = new TextBox();
		label36 = new Label();
		btnOrderApply = new Button();
		btnOrderReset = new Button();
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
		tabPage3.SuspendLayout();
		tableLayoutPanel5.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)dgvContracts).BeginInit();
		panel1.SuspendLayout();
		tabPage4.SuspendLayout();
		tableLayoutPanel6.SuspendLayout();
		tableLayoutPanel7.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)dgvOrders).BeginInit();
		((System.ComponentModel.ISupportInitialize)dgvOrderItems).BeginInit();
		panel5.SuspendLayout();
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
		tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
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
		venuesDataGridView.ReadOnly = true;
		venuesDataGridView.RowHeadersWidth = 51;
		venuesDataGridView.Size = new Size(770, 487);
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
		panel2.Size = new Size(294, 487);
		panel2.TabIndex = 3;
		// 
		// btnApply
		// 
		btnApply.FlatStyle = FlatStyle.Popup;
		btnApply.Location = new Point(149, 427);
		btnApply.Name = "btnApply";
		btnApply.Size = new Size(142, 29);
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
		venuesPagingUserControl.Location = new Point(303, 496);
		venuesPagingUserControl.Name = "venuesPagingUserControl";
		venuesPagingUserControl.Size = new Size(770, 34);
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
		tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
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
		tableLayoutPanel1.Size = new Size(1070, 487);
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
		panel3.Size = new Size(294, 481);
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
		tableLayoutPanel4.Size = new Size(764, 481);
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
		panel4.Location = new Point(3, 496);
		panel4.Name = "panel4";
		panel4.Size = new Size(1070, 34);
		panel4.TabIndex = 1;
		// 
		// directoriesPagingUserControl
		// 
		directoriesPagingUserControl.Dock = DockStyle.Fill;
		directoriesPagingUserControl.Location = new Point(0, 0);
		directoriesPagingUserControl.Name = "directoriesPagingUserControl";
		directoriesPagingUserControl.Size = new Size(1070, 34);
		directoriesPagingUserControl.TabIndex = 0;
		// 
		// tabPage3
		// 
		tabPage3.Controls.Add(tableLayoutPanel5);
		tabPage3.Location = new Point(4, 29);
		tabPage3.Name = "tabPage3";
		tabPage3.Padding = new Padding(3);
		tabPage3.Size = new Size(1082, 539);
		tabPage3.TabIndex = 2;
		tabPage3.Text = "Договоры";
		tabPage3.UseVisualStyleBackColor = true;
		// 
		// tableLayoutPanel5
		// 
		tableLayoutPanel5.ColumnCount = 2;
		tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 300F));
		tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
		tableLayoutPanel5.Controls.Add(dgvContracts, 1, 0);
		tableLayoutPanel5.Controls.Add(panel1, 0, 0);
		tableLayoutPanel5.Controls.Add(contractsPagingUserControl, 1, 1);
		tableLayoutPanel5.Dock = DockStyle.Fill;
		tableLayoutPanel5.Location = new Point(3, 3);
		tableLayoutPanel5.Name = "tableLayoutPanel5";
		tableLayoutPanel5.RowCount = 2;
		tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
		tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
		tableLayoutPanel5.Size = new Size(1076, 533);
		tableLayoutPanel5.TabIndex = 0;
		// 
		// dgvContracts
		// 
		dgvContracts.AllowUserToAddRows = false;
		dgvContracts.AllowUserToDeleteRows = false;
		dgvContracts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
		dgvContracts.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		dgvContracts.Dock = DockStyle.Fill;
		dgvContracts.Location = new Point(303, 3);
		dgvContracts.Name = "dgvContracts";
		dgvContracts.ReadOnly = true;
		dgvContracts.RowHeadersWidth = 51;
		dgvContracts.Size = new Size(770, 487);
		dgvContracts.TabIndex = 1;
		// 
		// panel1
		// 
		panel1.Controls.Add(label24);
		panel1.Controls.Add(label22);
		panel1.Controls.Add(label20);
		panel1.Controls.Add(dtpSignedDateTo);
		panel1.Controls.Add(label23);
		panel1.Controls.Add(dtpEndDateTo);
		panel1.Controls.Add(label21);
		panel1.Controls.Add(dtpStartDateTo);
		panel1.Controls.Add(label19);
		panel1.Controls.Add(btnContractsApply);
		panel1.Controls.Add(btnContractsReset);
		panel1.Controls.Add(cbDesc);
		panel1.Controls.Add(cbOrder);
		panel1.Controls.Add(label18);
		panel1.Controls.Add(label17);
		panel1.Controls.Add(dtpSignedDateFrom);
		panel1.Controls.Add(label16);
		panel1.Controls.Add(dtpEndDateFrom);
		panel1.Controls.Add(label15);
		panel1.Controls.Add(dtpStartDateFrom);
		panel1.Controls.Add(label14);
		panel1.Controls.Add(cbCustomer);
		panel1.Controls.Add(label13);
		panel1.Controls.Add(cbStatuses);
		panel1.Controls.Add(cbEmployee);
		panel1.Controls.Add(label12);
		panel1.Controls.Add(txtContractNumber);
		panel1.Controls.Add(label11);
		panel1.Dock = DockStyle.Fill;
		panel1.Location = new Point(3, 3);
		panel1.Name = "panel1";
		panel1.Size = new Size(294, 487);
		panel1.TabIndex = 2;
		// 
		// label24
		// 
		label24.AutoSize = true;
		label24.Location = new Point(146, 300);
		label24.Name = "label24";
		label24.Size = new Size(26, 20);
		label24.TabIndex = 15;
		label24.Text = "до";
		// 
		// label22
		// 
		label22.AutoSize = true;
		label22.Location = new Point(146, 246);
		label22.Name = "label22";
		label22.Size = new Size(26, 20);
		label22.TabIndex = 15;
		label22.Text = "до";
		// 
		// label20
		// 
		label20.AutoSize = true;
		label20.Location = new Point(146, 192);
		label20.Name = "label20";
		label20.Size = new Size(26, 20);
		label20.TabIndex = 15;
		label20.Text = "до";
		// 
		// dtpSignedDateTo
		// 
		dtpSignedDateTo.Format = DateTimePickerFormat.Short;
		dtpSignedDateTo.Location = new Point(173, 296);
		dtpSignedDateTo.Name = "dtpSignedDateTo";
		dtpSignedDateTo.Size = new Size(118, 27);
		dtpSignedDateTo.TabIndex = 14;
		dtpSignedDateTo.ValueChanged += DtpSignedDateTo_ValueChanged;
		// 
		// label23
		// 
		label23.AutoSize = true;
		label23.Location = new Point(3, 300);
		label23.Name = "label23";
		label23.Size = new Size(24, 20);
		label23.TabIndex = 13;
		label23.Text = "от";
		// 
		// dtpEndDateTo
		// 
		dtpEndDateTo.Format = DateTimePickerFormat.Short;
		dtpEndDateTo.Location = new Point(173, 242);
		dtpEndDateTo.Name = "dtpEndDateTo";
		dtpEndDateTo.Size = new Size(118, 27);
		dtpEndDateTo.TabIndex = 14;
		dtpEndDateTo.ValueChanged += DtpEndDateTo_ValueChanged;
		// 
		// label21
		// 
		label21.AutoSize = true;
		label21.Location = new Point(3, 246);
		label21.Name = "label21";
		label21.Size = new Size(24, 20);
		label21.TabIndex = 13;
		label21.Text = "от";
		// 
		// dtpStartDateTo
		// 
		dtpStartDateTo.Format = DateTimePickerFormat.Short;
		dtpStartDateTo.Location = new Point(173, 188);
		dtpStartDateTo.Name = "dtpStartDateTo";
		dtpStartDateTo.Size = new Size(118, 27);
		dtpStartDateTo.TabIndex = 14;
		dtpStartDateTo.ValueChanged += DtpStartDateTo_ValueChanged;
		// 
		// label19
		// 
		label19.AutoSize = true;
		label19.Location = new Point(3, 192);
		label19.Name = "label19";
		label19.Size = new Size(24, 20);
		label19.TabIndex = 13;
		label19.Text = "от";
		// 
		// btnContractsApply
		// 
		btnContractsApply.FlatStyle = FlatStyle.Popup;
		btnContractsApply.Location = new Point(149, 442);
		btnContractsApply.Name = "btnContractsApply";
		btnContractsApply.Size = new Size(145, 29);
		btnContractsApply.TabIndex = 9;
		btnContractsApply.Text = "Применить";
		btnContractsApply.UseVisualStyleBackColor = true;
		btnContractsApply.Click += BtnContractsApply_Click;
		// 
		// btnContractsReset
		// 
		btnContractsReset.FlatStyle = FlatStyle.Popup;
		btnContractsReset.Location = new Point(0, 442);
		btnContractsReset.Name = "btnContractsReset";
		btnContractsReset.Size = new Size(146, 29);
		btnContractsReset.TabIndex = 10;
		btnContractsReset.Text = "Сбросить";
		btnContractsReset.UseVisualStyleBackColor = true;
		btnContractsReset.Click += BtnContractsReset_Click;
		// 
		// cbDesc
		// 
		cbDesc.AutoSize = true;
		cbDesc.Location = new Point(152, 406);
		cbDesc.Name = "cbDesc";
		cbDesc.Size = new Size(128, 24);
		cbDesc.TabIndex = 12;
		cbDesc.Text = "По убыванию";
		cbDesc.UseVisualStyleBackColor = true;
		cbDesc.CheckedChanged += CbDesc_CheckedChanged;
		// 
		// cbOrder
		// 
		cbOrder.FormattingEnabled = true;
		cbOrder.Items.AddRange(new object[] { "Название", "Тип", "Регион", "Район", "Город", "Улица", "Широта", "Долгота", "Ширина", "Высота" });
		cbOrder.Location = new Point(3, 404);
		cbOrder.Name = "cbOrder";
		cbOrder.Size = new Size(146, 28);
		cbOrder.TabIndex = 11;
		cbOrder.SelectedIndexChanged += CbOrder_SelectedIndexChanged;
		// 
		// label18
		// 
		label18.AutoSize = true;
		label18.Location = new Point(3, 381);
		label18.Name = "label18";
		label18.Size = new Size(121, 20);
		label18.TabIndex = 8;
		label18.Text = "Сортировать по";
		// 
		// label17
		// 
		label17.AutoSize = true;
		label17.Location = new Point(3, 326);
		label17.Name = "label17";
		label17.Size = new Size(52, 20);
		label17.TabIndex = 6;
		label17.Text = "Статус";
		// 
		// dtpSignedDateFrom
		// 
		dtpSignedDateFrom.Format = DateTimePickerFormat.Short;
		dtpSignedDateFrom.Location = new Point(28, 296);
		dtpSignedDateFrom.Name = "dtpSignedDateFrom";
		dtpSignedDateFrom.Size = new Size(116, 27);
		dtpSignedDateFrom.TabIndex = 5;
		dtpSignedDateFrom.ValueChanged += DtpSignedDateFrom_ValueChanged;
		// 
		// label16
		// 
		label16.AutoSize = true;
		label16.Location = new Point(3, 272);
		label16.Name = "label16";
		label16.Size = new Size(130, 20);
		label16.TabIndex = 4;
		label16.Text = "Дата подписания";
		// 
		// dtpEndDateFrom
		// 
		dtpEndDateFrom.Format = DateTimePickerFormat.Short;
		dtpEndDateFrom.Location = new Point(28, 242);
		dtpEndDateFrom.Name = "dtpEndDateFrom";
		dtpEndDateFrom.Size = new Size(116, 27);
		dtpEndDateFrom.TabIndex = 5;
		dtpEndDateFrom.ValueChanged += DtpEndDateFrom_ValueChanged;
		// 
		// label15
		// 
		label15.AutoSize = true;
		label15.Location = new Point(3, 218);
		label15.Name = "label15";
		label15.Size = new Size(121, 20);
		label15.TabIndex = 4;
		label15.Text = "Дата окончания";
		// 
		// dtpStartDateFrom
		// 
		dtpStartDateFrom.Format = DateTimePickerFormat.Short;
		dtpStartDateFrom.Location = new Point(28, 188);
		dtpStartDateFrom.Name = "dtpStartDateFrom";
		dtpStartDateFrom.Size = new Size(116, 27);
		dtpStartDateFrom.TabIndex = 5;
		dtpStartDateFrom.ValueChanged += DtpStartDateFrom_ValueChanged;
		// 
		// label14
		// 
		label14.AutoSize = true;
		label14.Location = new Point(3, 163);
		label14.Name = "label14";
		label14.Size = new Size(94, 20);
		label14.TabIndex = 4;
		label14.Text = "Дата начала";
		// 
		// cbCustomer
		// 
		cbCustomer.FormattingEnabled = true;
		cbCustomer.Location = new Point(3, 132);
		cbCustomer.Name = "cbCustomer";
		cbCustomer.Size = new Size(288, 28);
		cbCustomer.TabIndex = 3;
		cbCustomer.SelectedIndexChanged += CbCustomer_SelectedIndexChanged;
		// 
		// label13
		// 
		label13.AutoSize = true;
		label13.Location = new Point(3, 109);
		label13.Name = "label13";
		label13.Size = new Size(71, 20);
		label13.TabIndex = 2;
		label13.Text = "Заказчик";
		// 
		// cbStatuses
		// 
		cbStatuses.FormattingEnabled = true;
		cbStatuses.Location = new Point(3, 349);
		cbStatuses.Name = "cbStatuses";
		cbStatuses.Size = new Size(288, 28);
		cbStatuses.TabIndex = 3;
		cbStatuses.SelectedIndexChanged += CbStatuses_SelectedIndexChanged;
		// 
		// cbEmployee
		// 
		cbEmployee.FormattingEnabled = true;
		cbEmployee.Location = new Point(3, 77);
		cbEmployee.Name = "cbEmployee";
		cbEmployee.Size = new Size(288, 28);
		cbEmployee.TabIndex = 3;
		cbEmployee.SelectedIndexChanged += CbEmployee_SelectedIndexChanged;
		// 
		// label12
		// 
		label12.AutoSize = true;
		label12.Location = new Point(3, 54);
		label12.Name = "label12";
		label12.Size = new Size(101, 20);
		label12.TabIndex = 2;
		label12.Text = "Исполнитель";
		// 
		// txtContractNumber
		// 
		txtContractNumber.BorderStyle = BorderStyle.FixedSingle;
		txtContractNumber.Location = new Point(3, 23);
		txtContractNumber.Name = "txtContractNumber";
		txtContractNumber.Size = new Size(288, 27);
		txtContractNumber.TabIndex = 1;
		txtContractNumber.TextChanged += TxtContractNumber_TextChanged;
		// 
		// label11
		// 
		label11.AutoSize = true;
		label11.Location = new Point(3, 0);
		label11.Name = "label11";
		label11.Size = new Size(127, 20);
		label11.TabIndex = 0;
		label11.Text = "Номер договора";
		// 
		// contractsPagingUserControl
		// 
		contractsPagingUserControl.Dock = DockStyle.Fill;
		contractsPagingUserControl.Location = new Point(303, 496);
		contractsPagingUserControl.Name = "contractsPagingUserControl";
		contractsPagingUserControl.Size = new Size(770, 34);
		contractsPagingUserControl.TabIndex = 3;
		// 
		// tabPage4
		// 
		tabPage4.Controls.Add(tableLayoutPanel6);
		tabPage4.Location = new Point(4, 29);
		tabPage4.Name = "tabPage4";
		tabPage4.Size = new Size(1082, 539);
		tabPage4.TabIndex = 3;
		tabPage4.Text = "Заказы";
		tabPage4.UseVisualStyleBackColor = true;
		// 
		// tableLayoutPanel6
		// 
		tableLayoutPanel6.ColumnCount = 2;
		tableLayoutPanel6.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 300F));
		tableLayoutPanel6.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
		tableLayoutPanel6.Controls.Add(tableLayoutPanel7, 1, 0);
		tableLayoutPanel6.Controls.Add(ordersPagingUserControl, 1, 1);
		tableLayoutPanel6.Controls.Add(panel5, 0, 0);
		tableLayoutPanel6.Dock = DockStyle.Fill;
		tableLayoutPanel6.Location = new Point(0, 0);
		tableLayoutPanel6.Name = "tableLayoutPanel6";
		tableLayoutPanel6.RowCount = 2;
		tableLayoutPanel6.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
		tableLayoutPanel6.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
		tableLayoutPanel6.Size = new Size(1082, 539);
		tableLayoutPanel6.TabIndex = 0;
		// 
		// tableLayoutPanel7
		// 
		tableLayoutPanel7.ColumnCount = 1;
		tableLayoutPanel7.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
		tableLayoutPanel7.Controls.Add(dgvOrders, 0, 1);
		tableLayoutPanel7.Controls.Add(dgvOrderItems, 0, 3);
		tableLayoutPanel7.Controls.Add(label25, 0, 0);
		tableLayoutPanel7.Controls.Add(label26, 0, 2);
		tableLayoutPanel7.Dock = DockStyle.Fill;
		tableLayoutPanel7.Location = new Point(303, 3);
		tableLayoutPanel7.Name = "tableLayoutPanel7";
		tableLayoutPanel7.RowCount = 4;
		tableLayoutPanel7.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
		tableLayoutPanel7.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
		tableLayoutPanel7.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
		tableLayoutPanel7.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
		tableLayoutPanel7.Size = new Size(776, 493);
		tableLayoutPanel7.TabIndex = 0;
		// 
		// dgvOrders
		// 
		dgvOrders.AllowUserToAddRows = false;
		dgvOrders.AllowUserToDeleteRows = false;
		dgvOrders.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
		dgvOrders.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		dgvOrders.Dock = DockStyle.Fill;
		dgvOrders.Location = new Point(3, 23);
		dgvOrders.Name = "dgvOrders";
		dgvOrders.ReadOnly = true;
		dgvOrders.RowHeadersWidth = 51;
		dgvOrders.Size = new Size(770, 220);
		dgvOrders.TabIndex = 0;
		dgvOrders.SelectionChanged += DgvOrders_SelectionChanged;
		// 
		// dgvOrderItems
		// 
		dgvOrderItems.AllowUserToAddRows = false;
		dgvOrderItems.AllowUserToDeleteRows = false;
		dgvOrderItems.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
		dgvOrderItems.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		dgvOrderItems.Dock = DockStyle.Fill;
		dgvOrderItems.Location = new Point(3, 269);
		dgvOrderItems.Name = "dgvOrderItems";
		dgvOrderItems.ReadOnly = true;
		dgvOrderItems.RowHeadersWidth = 51;
		dgvOrderItems.Size = new Size(770, 221);
		dgvOrderItems.TabIndex = 1;
		// 
		// ordersPagingUserControl
		// 
		ordersPagingUserControl.Dock = DockStyle.Fill;
		ordersPagingUserControl.Location = new Point(303, 502);
		ordersPagingUserControl.Name = "ordersPagingUserControl";
		ordersPagingUserControl.Size = new Size(776, 34);
		ordersPagingUserControl.TabIndex = 1;
		// 
		// panel5
		// 
		panel5.Controls.Add(btnOrderApply);
		panel5.Controls.Add(btnOrderReset);
		panel5.Controls.Add(label27);
		panel5.Controls.Add(label28);
		panel5.Controls.Add(dtpOrderEndDateTo);
		panel5.Controls.Add(label29);
		panel5.Controls.Add(dtpOrderStartDateTo);
		panel5.Controls.Add(label30);
		panel5.Controls.Add(label31);
		panel5.Controls.Add(dtpOrderEndDateFrom);
		panel5.Controls.Add(label32);
		panel5.Controls.Add(dtpOrderStartDateFrom);
		panel5.Controls.Add(label33);
		panel5.Controls.Add(cbOrderCustomers);
		panel5.Controls.Add(label34);
		panel5.Controls.Add(cbOrderStatuses);
		panel5.Controls.Add(cbOrderEmployees);
		panel5.Controls.Add(label35);
		panel5.Controls.Add(txtOrderContractNumber);
		panel5.Controls.Add(label36);
		panel5.Dock = DockStyle.Fill;
		panel5.Location = new Point(3, 3);
		panel5.Name = "panel5";
		panel5.Size = new Size(294, 493);
		panel5.TabIndex = 2;
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
		// label25
		// 
		label25.AutoSize = true;
		label25.Location = new Point(3, 0);
		label25.Name = "label25";
		label25.Size = new Size(58, 20);
		label25.TabIndex = 2;
		label25.Text = "Заказы";
		// 
		// label26
		// 
		label26.AutoSize = true;
		label26.Location = new Point(3, 246);
		label26.Name = "label26";
		label26.Size = new Size(133, 20);
		label26.TabIndex = 3;
		label26.Text = "Позиции в заказе";
		// 
		// label27
		// 
		label27.AutoSize = true;
		label27.Location = new Point(146, 246);
		label27.Name = "label27";
		label27.Size = new Size(26, 20);
		label27.TabIndex = 33;
		label27.Text = "до";
		// 
		// label28
		// 
		label28.AutoSize = true;
		label28.Location = new Point(146, 192);
		label28.Name = "label28";
		label28.Size = new Size(26, 20);
		label28.TabIndex = 32;
		label28.Text = "до";
		// 
		// dtpOrderEndDateTo
		// 
		dtpOrderEndDateTo.Format = DateTimePickerFormat.Short;
		dtpOrderEndDateTo.Location = new Point(173, 242);
		dtpOrderEndDateTo.Name = "dtpOrderEndDateTo";
		dtpOrderEndDateTo.Size = new Size(118, 27);
		dtpOrderEndDateTo.TabIndex = 31;
		dtpOrderEndDateTo.ValueChanged += DtpOrderEndDateTo_ValueChanged;
		// 
		// label29
		// 
		label29.AutoSize = true;
		label29.Location = new Point(3, 246);
		label29.Name = "label29";
		label29.Size = new Size(24, 20);
		label29.TabIndex = 29;
		label29.Text = "от";
		// 
		// dtpOrderStartDateTo
		// 
		dtpOrderStartDateTo.Format = DateTimePickerFormat.Short;
		dtpOrderStartDateTo.Location = new Point(173, 188);
		dtpOrderStartDateTo.Name = "dtpOrderStartDateTo";
		dtpOrderStartDateTo.Size = new Size(118, 27);
		dtpOrderStartDateTo.TabIndex = 30;
		dtpOrderStartDateTo.ValueChanged += DtpOrderStartDateTo_ValueChanged;
		// 
		// label30
		// 
		label30.AutoSize = true;
		label30.Location = new Point(3, 192);
		label30.Name = "label30";
		label30.Size = new Size(24, 20);
		label30.TabIndex = 28;
		label30.Text = "от";
		// 
		// label31
		// 
		label31.AutoSize = true;
		label31.Location = new Point(3, 271);
		label31.Name = "label31";
		label31.Size = new Size(52, 20);
		label31.TabIndex = 27;
		label31.Text = "Статус";
		// 
		// dtpOrderEndDateFrom
		// 
		dtpOrderEndDateFrom.Format = DateTimePickerFormat.Short;
		dtpOrderEndDateFrom.Location = new Point(28, 242);
		dtpOrderEndDateFrom.Name = "dtpOrderEndDateFrom";
		dtpOrderEndDateFrom.Size = new Size(116, 27);
		dtpOrderEndDateFrom.TabIndex = 26;
		dtpOrderEndDateFrom.ValueChanged += DtpOrderEndDateFrom_ValueChanged;
		// 
		// label32
		// 
		label32.AutoSize = true;
		label32.Location = new Point(3, 218);
		label32.Name = "label32";
		label32.Size = new Size(121, 20);
		label32.TabIndex = 24;
		label32.Text = "Дата окончания";
		// 
		// dtpOrderStartDateFrom
		// 
		dtpOrderStartDateFrom.Format = DateTimePickerFormat.Short;
		dtpOrderStartDateFrom.Location = new Point(28, 188);
		dtpOrderStartDateFrom.Name = "dtpOrderStartDateFrom";
		dtpOrderStartDateFrom.Size = new Size(116, 27);
		dtpOrderStartDateFrom.TabIndex = 25;
		dtpOrderStartDateFrom.ValueChanged += DtpOrderStartDateFrom_ValueChanged;
		// 
		// label33
		// 
		label33.AutoSize = true;
		label33.Location = new Point(3, 163);
		label33.Name = "label33";
		label33.Size = new Size(94, 20);
		label33.TabIndex = 23;
		label33.Text = "Дата начала";
		// 
		// cbOrderCustomers
		// 
		cbOrderCustomers.FormattingEnabled = true;
		cbOrderCustomers.Location = new Point(3, 132);
		cbOrderCustomers.Name = "cbOrderCustomers";
		cbOrderCustomers.Size = new Size(288, 28);
		cbOrderCustomers.TabIndex = 22;
		cbOrderCustomers.SelectedIndexChanged += CbOrderCustomers_SelectedIndexChanged;
		// 
		// label34
		// 
		label34.AutoSize = true;
		label34.Location = new Point(3, 109);
		label34.Name = "label34";
		label34.Size = new Size(71, 20);
		label34.TabIndex = 19;
		label34.Text = "Заказчик";
		// 
		// cbOrderStatuses
		// 
		cbOrderStatuses.FormattingEnabled = true;
		cbOrderStatuses.Location = new Point(3, 294);
		cbOrderStatuses.Name = "cbOrderStatuses";
		cbOrderStatuses.Size = new Size(288, 28);
		cbOrderStatuses.TabIndex = 21;
		cbOrderStatuses.SelectedIndexChanged += CbOrderStatuses_SelectedIndexChanged;
		// 
		// cbOrderEmployees
		// 
		cbOrderEmployees.FormattingEnabled = true;
		cbOrderEmployees.Location = new Point(3, 77);
		cbOrderEmployees.Name = "cbOrderEmployees";
		cbOrderEmployees.Size = new Size(288, 28);
		cbOrderEmployees.TabIndex = 20;
		cbOrderEmployees.SelectedIndexChanged += CbOrderEmployees_SelectedIndexChanged;
		// 
		// label35
		// 
		label35.AutoSize = true;
		label35.Location = new Point(3, 54);
		label35.Name = "label35";
		label35.Size = new Size(101, 20);
		label35.TabIndex = 18;
		label35.Text = "Исполнитель";
		// 
		// txtOrderContractNumber
		// 
		txtOrderContractNumber.BorderStyle = BorderStyle.FixedSingle;
		txtOrderContractNumber.Location = new Point(3, 23);
		txtOrderContractNumber.Name = "txtOrderContractNumber";
		txtOrderContractNumber.Size = new Size(288, 27);
		txtOrderContractNumber.TabIndex = 17;
		txtOrderContractNumber.TextChanged += TxtOrderContractNumber_TextChanged;
		// 
		// label36
		// 
		label36.AutoSize = true;
		label36.Location = new Point(3, 0);
		label36.Name = "label36";
		label36.Size = new Size(127, 20);
		label36.TabIndex = 16;
		label36.Text = "Номер договора";
		// 
		// btnOrderApply
		// 
		btnOrderApply.FlatStyle = FlatStyle.Popup;
		btnOrderApply.Location = new Point(149, 337);
		btnOrderApply.Name = "btnOrderApply";
		btnOrderApply.Size = new Size(145, 29);
		btnOrderApply.TabIndex = 34;
		btnOrderApply.Text = "Применить";
		btnOrderApply.UseVisualStyleBackColor = true;
		btnOrderApply.Click += BtnOrderApply_Click;
		// 
		// btnOrderReset
		// 
		btnOrderReset.FlatStyle = FlatStyle.Popup;
		btnOrderReset.Location = new Point(3, 337);
		btnOrderReset.Name = "btnOrderReset";
		btnOrderReset.Size = new Size(143, 29);
		btnOrderReset.TabIndex = 35;
		btnOrderReset.Text = "Сбросить";
		btnOrderReset.UseVisualStyleBackColor = true;
		btnOrderReset.Click += BtnOrderReset_Click;
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
		tabPage3.ResumeLayout(false);
		tableLayoutPanel5.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)dgvContracts).EndInit();
		panel1.ResumeLayout(false);
		panel1.PerformLayout();
		tabPage4.ResumeLayout(false);
		tableLayoutPanel6.ResumeLayout(false);
		tableLayoutPanel7.ResumeLayout(false);
		tableLayoutPanel7.PerformLayout();
		((System.ComponentModel.ISupportInitialize)dgvOrders).EndInit();
		((System.ComponentModel.ISupportInitialize)dgvOrderItems).EndInit();
		panel5.ResumeLayout(false);
		panel5.PerformLayout();
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
	private TableLayoutPanel tableLayoutPanel5;
	private DataGridView dgvContracts;
	private Panel panel1;
	private Button btnContractsApply;
	private Button btnContractsReset;
	private CheckBox cbDesc;
	private ComboBox cbOrder;
	private Label label18;
	private Label label17;
	private Label label16;
	private Label label15;
	private DateTimePicker dtpStartDateFrom;
	private Label label14;
	private ComboBox cbCustomer;
	private Label label13;
	private ComboBox cbStatuses;
	private ComboBox cbEmployee;
	private Label label12;
	private TextBox txtContractNumber;
	private Label label11;
	private PagingUserControl pagingUserControl1;
	private PagingUserControl contractsPagingUserControl;
	private Label label19;
	private Label label24;
	private Label label22;
	private Label label20;
	private DateTimePicker dtpSignedDateTo;
	private Label label23;
	private DateTimePicker dtpEndDateTo;
	private Label label21;
	private DateTimePicker dtpStartDateTo;
	private DateTimePicker dtpSignedDateFrom;
	private DateTimePicker dtpEndDateFrom;
	private TableLayoutPanel tableLayoutPanel6;
	private TableLayoutPanel tableLayoutPanel7;
	private DataGridView dgvOrders;
	private DataGridView dgvOrderItems;
	private PagingUserControl ordersPagingUserControl;
	private Panel panel5;
	private Label label25;
	private Label label26;
	private Button btnOrderApply;
	private Button btnOrderReset;
	private Label label27;
	private Label label28;
	private DateTimePicker dtpOrderEndDateTo;
	private Label label29;
	private DateTimePicker dtpOrderStartDateTo;
	private Label label30;
	private Label label31;
	private DateTimePicker dtpOrderEndDateFrom;
	private Label label32;
	private DateTimePicker dtpOrderStartDateFrom;
	private Label label33;
	private ComboBox cbOrderCustomers;
	private Label label34;
	private ComboBox cbOrderStatuses;
	private ComboBox cbOrderEmployees;
	private Label label35;
	private TextBox txtOrderContractNumber;
	private Label label36;
}