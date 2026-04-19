namespace AdVision.Presentation
{
	partial class CreatePositionForm
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
			btnReset = new Button();
			btnApply = new Button();
			nudPriceTo = new NumericUpDown();
			nudPriceFrom = new NumericUpDown();
			label15 = new Label();
			label14 = new Label();
			label12 = new Label();
			label11 = new Label();
			dtpDateTo = new DateTimePicker();
			dtpDateFrom = new DateTimePicker();
			label13 = new Label();
			label7 = new Label();
			nudRatingTo = new NumericUpDown();
			nudRatingFrom = new NumericUpDown();
			label10 = new Label();
			label9 = new Label();
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
			dgvVenues = new DataGridView();
			panel2 = new Panel();
			dtpBookingTo = new DateTimePicker();
			label18 = new Label();
			dtpBookingFrom = new DateTimePicker();
			label17 = new Label();
			label16 = new Label();
			btnAdd = new Button();
			tableLayoutPanel1.SuspendLayout();
			panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)nudPriceTo).BeginInit();
			((System.ComponentModel.ISupportInitialize)nudPriceFrom).BeginInit();
			((System.ComponentModel.ISupportInitialize)nudRatingTo).BeginInit();
			((System.ComponentModel.ISupportInitialize)nudRatingFrom).BeginInit();
			((System.ComponentModel.ISupportInitialize)dgvVenues).BeginInit();
			panel2.SuspendLayout();
			SuspendLayout();
			// 
			// tableLayoutPanel1
			// 
			tableLayoutPanel1.ColumnCount = 1;
			tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
			tableLayoutPanel1.Controls.Add(panel1, 0, 0);
			tableLayoutPanel1.Controls.Add(dgvVenues, 0, 1);
			tableLayoutPanel1.Controls.Add(panel2, 0, 2);
			tableLayoutPanel1.Dock = DockStyle.Fill;
			tableLayoutPanel1.Location = new Point(0, 0);
			tableLayoutPanel1.Name = "tableLayoutPanel1";
			tableLayoutPanel1.Padding = new Padding(10);
			tableLayoutPanel1.RowCount = 3;
			tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 300F));
			tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
			tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
			tableLayoutPanel1.Size = new Size(963, 590);
			tableLayoutPanel1.TabIndex = 0;
			// 
			// panel1
			// 
			panel1.Controls.Add(btnReset);
			panel1.Controls.Add(btnApply);
			panel1.Controls.Add(nudPriceTo);
			panel1.Controls.Add(nudPriceFrom);
			panel1.Controls.Add(label15);
			panel1.Controls.Add(label14);
			panel1.Controls.Add(label12);
			panel1.Controls.Add(label11);
			panel1.Controls.Add(dtpDateTo);
			panel1.Controls.Add(dtpDateFrom);
			panel1.Controls.Add(label13);
			panel1.Controls.Add(label7);
			panel1.Controls.Add(nudRatingTo);
			panel1.Controls.Add(nudRatingFrom);
			panel1.Controls.Add(label10);
			panel1.Controls.Add(label9);
			panel1.Controls.Add(cbCities);
			panel1.Controls.Add(cbDistricts);
			panel1.Controls.Add(label5);
			panel1.Controls.Add(cbRegions);
			panel1.Controls.Add(label4);
			panel1.Controls.Add(cbVenueTypes);
			panel1.Controls.Add(label3);
			panel1.Controls.Add(txtStreet);
			panel1.Controls.Add(label8);
			panel1.Controls.Add(txtName);
			panel1.Controls.Add(label6);
			panel1.Controls.Add(label2);
			panel1.Controls.Add(label1);
			panel1.Dock = DockStyle.Fill;
			panel1.Location = new Point(13, 13);
			panel1.Name = "panel1";
			panel1.Size = new Size(937, 294);
			panel1.TabIndex = 0;
			// 
			// btnReset
			// 
			btnReset.FlatStyle = FlatStyle.Popup;
			btnReset.Location = new Point(420, 252);
			btnReset.Name = "btnReset";
			btnReset.Size = new Size(192, 29);
			btnReset.TabIndex = 28;
			btnReset.Text = "Сбросить";
			btnReset.UseVisualStyleBackColor = true;
			btnReset.Click += BtnReset_Click;
			// 
			// btnApply
			// 
			btnApply.FlatStyle = FlatStyle.Popup;
			btnApply.Location = new Point(619, 252);
			btnApply.Name = "btnApply";
			btnApply.Size = new Size(192, 29);
			btnApply.TabIndex = 28;
			btnApply.Text = "Применить";
			btnApply.UseVisualStyleBackColor = true;
			btnApply.Click += BtnApply_Click;
			// 
			// nudPriceTo
			// 
			nudPriceTo.Location = new Point(242, 254);
			nudPriceTo.Name = "nudPriceTo";
			nudPriceTo.Size = new Size(159, 27);
			nudPriceTo.TabIndex = 27;
			nudPriceTo.ValueChanged += NudPriceTo_ValueChanged;
			// 
			// nudPriceFrom
			// 
			nudPriceFrom.Location = new Point(45, 254);
			nudPriceFrom.Name = "nudPriceFrom";
			nudPriceFrom.Size = new Size(159, 27);
			nudPriceFrom.TabIndex = 27;
			nudPriceFrom.ValueChanged += NudPriceFrom_ValueChanged;
			// 
			// label15
			// 
			label15.AutoSize = true;
			label15.Location = new Point(210, 258);
			label15.Name = "label15";
			label15.Size = new Size(26, 20);
			label15.TabIndex = 26;
			label15.Text = "до";
			// 
			// label14
			// 
			label14.AutoSize = true;
			label14.Location = new Point(20, 258);
			label14.Name = "label14";
			label14.Size = new Size(24, 20);
			label14.TabIndex = 26;
			label14.Text = "от";
			// 
			// label12
			// 
			label12.AutoSize = true;
			label12.Location = new Point(210, 201);
			label12.Name = "label12";
			label12.Size = new Size(27, 20);
			label12.TabIndex = 26;
			label12.Text = "по";
			// 
			// label11
			// 
			label11.AutoSize = true;
			label11.Location = new Point(20, 201);
			label11.Name = "label11";
			label11.Size = new Size(16, 20);
			label11.TabIndex = 26;
			label11.Text = "с";
			// 
			// dtpDateTo
			// 
			dtpDateTo.Location = new Point(242, 198);
			dtpDateTo.Name = "dtpDateTo";
			dtpDateTo.Size = new Size(158, 27);
			dtpDateTo.TabIndex = 25;
			dtpDateTo.ValueChanged += DtpDateTo_ValueChanged;
			// 
			// dtpDateFrom
			// 
			dtpDateFrom.Location = new Point(45, 198);
			dtpDateFrom.Name = "dtpDateFrom";
			dtpDateFrom.Size = new Size(158, 27);
			dtpDateFrom.TabIndex = 25;
			dtpDateFrom.ValueChanged += DtpDateFrom_ValueChanged;
			// 
			// label13
			// 
			label13.AutoSize = true;
			label13.Location = new Point(9, 232);
			label13.Name = "label13";
			label13.Size = new Size(83, 20);
			label13.TabIndex = 24;
			label13.Text = "Стоимость";
			// 
			// label7
			// 
			label7.AutoSize = true;
			label7.Location = new Point(9, 174);
			label7.Name = "label7";
			label7.Size = new Size(126, 20);
			label7.TabIndex = 24;
			label7.Text = "Свободные даты";
			// 
			// nudRatingTo
			// 
			nudRatingTo.Location = new Point(242, 142);
			nudRatingTo.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
			nudRatingTo.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
			nudRatingTo.Name = "nudRatingTo";
			nudRatingTo.Size = new Size(158, 27);
			nudRatingTo.TabIndex = 23;
			nudRatingTo.Value = new decimal(new int[] { 10, 0, 0, 0 });
			nudRatingTo.ValueChanged += NudRatingTo_ValueChanged;
			// 
			// nudRatingFrom
			// 
			nudRatingFrom.Location = new Point(46, 142);
			nudRatingFrom.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
			nudRatingFrom.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
			nudRatingFrom.Name = "nudRatingFrom";
			nudRatingFrom.Size = new Size(158, 27);
			nudRatingFrom.TabIndex = 22;
			nudRatingFrom.Value = new decimal(new int[] { 1, 0, 0, 0 });
			nudRatingFrom.ValueChanged += NudRatingFrom_ValueChanged;
			// 
			// label10
			// 
			label10.AutoSize = true;
			label10.Location = new Point(210, 144);
			label10.Name = "label10";
			label10.Size = new Size(26, 20);
			label10.TabIndex = 21;
			label10.Text = "до";
			// 
			// label9
			// 
			label9.AutoSize = true;
			label9.Location = new Point(16, 144);
			label9.Name = "label9";
			label9.Size = new Size(24, 20);
			label9.TabIndex = 20;
			label9.Text = "от";
			// 
			// cbCities
			// 
			cbCities.FormattingEnabled = true;
			cbCities.Location = new Point(420, 141);
			cbCities.Name = "cbCities";
			cbCities.Size = new Size(391, 28);
			cbCities.TabIndex = 17;
			cbCities.SelectedIndexChanged += CbCities_SelectedIndexChanged;
			// 
			// cbDistricts
			// 
			cbDistricts.FormattingEnabled = true;
			cbDistricts.Location = new Point(420, 85);
			cbDistricts.Name = "cbDistricts";
			cbDistricts.Size = new Size(391, 28);
			cbDistricts.TabIndex = 19;
			cbDistricts.SelectedIndexChanged += CbDistricts_SelectedIndexChanged;
			// 
			// label5
			// 
			label5.AutoSize = true;
			label5.Location = new Point(421, 119);
			label5.Name = "label5";
			label5.Size = new Size(51, 20);
			label5.TabIndex = 12;
			label5.Text = "Город";
			// 
			// cbRegions
			// 
			cbRegions.FormattingEnabled = true;
			cbRegions.Location = new Point(420, 28);
			cbRegions.Name = "cbRegions";
			cbRegions.Size = new Size(391, 28);
			cbRegions.TabIndex = 16;
			cbRegions.SelectedIndexChanged += CbRegions_SelectedIndexChanged;
			// 
			// label4
			// 
			label4.AutoSize = true;
			label4.Location = new Point(420, 59);
			label4.Name = "label4";
			label4.Size = new Size(52, 20);
			label4.TabIndex = 7;
			label4.Text = "Район";
			// 
			// cbVenueTypes
			// 
			cbVenueTypes.FormattingEnabled = true;
			cbVenueTypes.Location = new Point(9, 85);
			cbVenueTypes.Name = "cbVenueTypes";
			cbVenueTypes.Size = new Size(391, 28);
			cbVenueTypes.TabIndex = 18;
			cbVenueTypes.SelectedIndexChanged += CbVenueTypes_SelectedIndexChanged;
			// 
			// label3
			// 
			label3.AutoSize = true;
			label3.Location = new Point(420, 6);
			label3.Name = "label3";
			label3.Size = new Size(58, 20);
			label3.TabIndex = 10;
			label3.Text = "Регион";
			// 
			// txtStreet
			// 
			txtStreet.BorderStyle = BorderStyle.FixedSingle;
			txtStreet.Location = new Point(420, 198);
			txtStreet.Name = "txtStreet";
			txtStreet.Size = new Size(391, 27);
			txtStreet.TabIndex = 14;
			txtStreet.TextChanged += TxtStreet_TextChanged;
			// 
			// label8
			// 
			label8.AutoSize = true;
			label8.Location = new Point(9, 119);
			label8.Name = "label8";
			label8.Size = new Size(64, 20);
			label8.TabIndex = 9;
			label8.Text = "Рейтинг";
			// 
			// txtName
			// 
			txtName.BorderStyle = BorderStyle.FixedSingle;
			txtName.Location = new Point(9, 29);
			txtName.Name = "txtName";
			txtName.Size = new Size(391, 27);
			txtName.TabIndex = 15;
			txtName.TextChanged += TxtName_TextChanged;
			// 
			// label6
			// 
			label6.AutoSize = true;
			label6.Location = new Point(420, 172);
			label6.Name = "label6";
			label6.Size = new Size(52, 20);
			label6.TabIndex = 8;
			label6.Text = "Улица";
			// 
			// label2
			// 
			label2.AutoSize = true;
			label2.Location = new Point(9, 59);
			label2.Name = "label2";
			label2.Size = new Size(35, 20);
			label2.TabIndex = 11;
			label2.Text = "Тип";
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Location = new Point(9, 6);
			label1.Name = "label1";
			label1.Size = new Size(77, 20);
			label1.TabIndex = 13;
			label1.Text = "Название";
			// 
			// dgvVenues
			// 
			dgvVenues.AllowUserToAddRows = false;
			dgvVenues.AllowUserToDeleteRows = false;
			dgvVenues.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
			dgvVenues.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dgvVenues.Dock = DockStyle.Fill;
			dgvVenues.Location = new Point(13, 313);
			dgvVenues.Name = "dgvVenues";
			dgvVenues.ReadOnly = true;
			dgvVenues.RowHeadersWidth = 51;
			dgvVenues.Size = new Size(937, 224);
			dgvVenues.TabIndex = 2;
			// 
			// panel2
			// 
			panel2.Controls.Add(dtpBookingTo);
			panel2.Controls.Add(label18);
			panel2.Controls.Add(dtpBookingFrom);
			panel2.Controls.Add(label17);
			panel2.Controls.Add(label16);
			panel2.Controls.Add(btnAdd);
			panel2.Dock = DockStyle.Fill;
			panel2.Location = new Point(13, 543);
			panel2.Name = "panel2";
			panel2.Size = new Size(937, 34);
			panel2.TabIndex = 3;
			// 
			// dtpBookingTo
			// 
			dtpBookingTo.Location = new Point(432, 4);
			dtpBookingTo.Name = "dtpBookingTo";
			dtpBookingTo.Size = new Size(182, 27);
			dtpBookingTo.TabIndex = 3;
			// 
			// label18
			// 
			label18.AutoSize = true;
			label18.Location = new Point(399, 7);
			label18.Name = "label18";
			label18.Size = new Size(27, 20);
			label18.TabIndex = 2;
			label18.Text = "по";
			// 
			// dtpBookingFrom
			// 
			dtpBookingFrom.Location = new Point(203, 4);
			dtpBookingFrom.Name = "dtpBookingFrom";
			dtpBookingFrom.Size = new Size(182, 27);
			dtpBookingFrom.TabIndex = 3;
			// 
			// label17
			// 
			label17.AutoSize = true;
			label17.Location = new Point(181, 7);
			label17.Name = "label17";
			label17.Size = new Size(16, 20);
			label17.TabIndex = 2;
			label17.Text = "с";
			// 
			// label16
			// 
			label16.AutoSize = true;
			label16.Location = new Point(3, 7);
			label16.Name = "label16";
			label16.Size = new Size(155, 20);
			label16.TabIndex = 1;
			label16.Text = "Забронировать даты";
			// 
			// btnAdd
			// 
			btnAdd.Location = new Point(736, 2);
			btnAdd.Name = "btnAdd";
			btnAdd.Size = new Size(198, 29);
			btnAdd.TabIndex = 0;
			btnAdd.Text = "Добавить";
			btnAdd.UseVisualStyleBackColor = true;
			// 
			// CreatePositionForm
			// 
			AutoScaleDimensions = new SizeF(8F, 20F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(963, 590);
			Controls.Add(tableLayoutPanel1);
			Name = "CreatePositionForm";
			StartPosition = FormStartPosition.CenterScreen;
			Text = "Добавление новой позиции к заказу";
			tableLayoutPanel1.ResumeLayout(false);
			panel1.ResumeLayout(false);
			panel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)nudPriceTo).EndInit();
			((System.ComponentModel.ISupportInitialize)nudPriceFrom).EndInit();
			((System.ComponentModel.ISupportInitialize)nudRatingTo).EndInit();
			((System.ComponentModel.ISupportInitialize)nudRatingFrom).EndInit();
			((System.ComponentModel.ISupportInitialize)dgvVenues).EndInit();
			panel2.ResumeLayout(false);
			panel2.PerformLayout();
			ResumeLayout(false);
		}

		#endregion

		private TableLayoutPanel tableLayoutPanel1;
		private Panel panel1;
		private Label label7;
		private NumericUpDown nudRatingTo;
		private NumericUpDown nudRatingFrom;
		private Label label10;
		private Label label9;
		private ComboBox cbCities;
		private ComboBox cbDistricts;
		private Label label5;
		private ComboBox cbRegions;
		private Label label4;
		private ComboBox cbVenueTypes;
		private Label label3;
		private TextBox txtStreet;
		private Label label8;
		private TextBox txtName;
		private Label label6;
		private Label label2;
		private Label label1;
		private NumericUpDown nudPriceTo;
		private NumericUpDown nudPriceFrom;
		private Label label15;
		private Label label14;
		private Label label12;
		private Label label11;
		private DateTimePicker dtpDateTo;
		private DateTimePicker dtpDateFrom;
		private Label label13;
		private DataGridView dgvVenues;
		private Button btnReset;
		private Button btnApply;
		private Panel panel2;
		private Button btnAdd;
		private DateTimePicker dtpBookingFrom;
		private Label label17;
		private Label label16;
		private DateTimePicker dtpBookingTo;
		private Label label18;
	}
}