namespace AdVision.Presentation
{
	partial class OrderItemForm
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
			label19 = new Label();
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
			btnPrevPage = new Button();
			btnNextPage = new Button();
			lblPageInfo = new Label();
			panel3 = new Panel();
			btnAdd = new Button();
			panel4 = new Panel();
			dtpBookingTo = new DateTimePicker();
			btnCheck = new Button();
			btnBookingReset = new Button();
			label20 = new Label();
			label18 = new Label();
			label17 = new Label();
			dtpBookingFrom = new DateTimePicker();
			tableLayoutPanel1.SuspendLayout();
			panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)nudPriceTo).BeginInit();
			((System.ComponentModel.ISupportInitialize)nudPriceFrom).BeginInit();
			((System.ComponentModel.ISupportInitialize)nudRatingTo).BeginInit();
			((System.ComponentModel.ISupportInitialize)nudRatingFrom).BeginInit();
			((System.ComponentModel.ISupportInitialize)dgvVenues).BeginInit();
			panel2.SuspendLayout();
			panel3.SuspendLayout();
			panel4.SuspendLayout();
			SuspendLayout();
			// 
			// tableLayoutPanel1
			// 
			tableLayoutPanel1.ColumnCount = 1;
			tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
			tableLayoutPanel1.Controls.Add(panel1, 0, 0);
			tableLayoutPanel1.Controls.Add(dgvVenues, 0, 2);
			tableLayoutPanel1.Controls.Add(panel2, 0, 3);
			tableLayoutPanel1.Controls.Add(panel3, 0, 4);
			tableLayoutPanel1.Controls.Add(panel4, 0, 1);
			tableLayoutPanel1.Dock = DockStyle.Fill;
			tableLayoutPanel1.Location = new Point(0, 0);
			tableLayoutPanel1.Name = "tableLayoutPanel1";
			tableLayoutPanel1.Padding = new Padding(10);
			tableLayoutPanel1.RowCount = 5;
			tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 328F));
			tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 88F));
			tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
			tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
			tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
			tableLayoutPanel1.Size = new Size(847, 761);
			tableLayoutPanel1.TabIndex = 0;
			// 
			// panel1
			// 
			panel1.BackColor = SystemColors.Window;
			panel1.BorderStyle = BorderStyle.FixedSingle;
			panel1.Controls.Add(label19);
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
			panel1.Size = new Size(821, 322);
			panel1.TabIndex = 0;
			// 
			// label19
			// 
			label19.AutoSize = true;
			label19.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 204);
			label19.Location = new Point(8, 7);
			label19.Name = "label19";
			label19.Size = new Size(64, 20);
			label19.TabIndex = 29;
			label19.Text = "Фильтр";
			// 
			// btnReset
			// 
			btnReset.FlatStyle = FlatStyle.Popup;
			btnReset.Location = new Point(419, 279);
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
			btnApply.Location = new Point(618, 279);
			btnApply.Name = "btnApply";
			btnApply.Size = new Size(192, 29);
			btnApply.TabIndex = 28;
			btnApply.Text = "Применить";
			btnApply.UseVisualStyleBackColor = true;
			btnApply.Click += BtnApply_Click;
			// 
			// nudPriceTo
			// 
			nudPriceTo.Location = new Point(241, 281);
			nudPriceTo.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
			nudPriceTo.Name = "nudPriceTo";
			nudPriceTo.Size = new Size(159, 27);
			nudPriceTo.TabIndex = 27;
			nudPriceTo.ValueChanged += NudPriceTo_ValueChanged;
			// 
			// nudPriceFrom
			// 
			nudPriceFrom.Location = new Point(44, 281);
			nudPriceFrom.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
			nudPriceFrom.Name = "nudPriceFrom";
			nudPriceFrom.Size = new Size(159, 27);
			nudPriceFrom.TabIndex = 27;
			nudPriceFrom.ValueChanged += NudPriceFrom_ValueChanged;
			// 
			// label15
			// 
			label15.AutoSize = true;
			label15.Location = new Point(209, 285);
			label15.Name = "label15";
			label15.Size = new Size(26, 20);
			label15.TabIndex = 26;
			label15.Text = "до";
			// 
			// label14
			// 
			label14.AutoSize = true;
			label14.Location = new Point(19, 285);
			label14.Name = "label14";
			label14.Size = new Size(24, 20);
			label14.TabIndex = 26;
			label14.Text = "от";
			// 
			// label12
			// 
			label12.AutoSize = true;
			label12.Location = new Point(209, 228);
			label12.Name = "label12";
			label12.Size = new Size(27, 20);
			label12.TabIndex = 26;
			label12.Text = "по";
			// 
			// label11
			// 
			label11.AutoSize = true;
			label11.Location = new Point(19, 228);
			label11.Name = "label11";
			label11.Size = new Size(16, 20);
			label11.TabIndex = 26;
			label11.Text = "с";
			// 
			// dtpDateTo
			// 
			dtpDateTo.Location = new Point(241, 225);
			dtpDateTo.Name = "dtpDateTo";
			dtpDateTo.Size = new Size(158, 27);
			dtpDateTo.TabIndex = 25;
			dtpDateTo.ValueChanged += DtpDateTo_ValueChanged;
			// 
			// dtpDateFrom
			// 
			dtpDateFrom.Location = new Point(44, 225);
			dtpDateFrom.Name = "dtpDateFrom";
			dtpDateFrom.Size = new Size(158, 27);
			dtpDateFrom.TabIndex = 25;
			dtpDateFrom.ValueChanged += DtpDateFrom_ValueChanged;
			// 
			// label13
			// 
			label13.AutoSize = true;
			label13.Location = new Point(8, 259);
			label13.Name = "label13";
			label13.Size = new Size(83, 20);
			label13.TabIndex = 24;
			label13.Text = "Стоимость";
			// 
			// label7
			// 
			label7.AutoSize = true;
			label7.Location = new Point(8, 201);
			label7.Name = "label7";
			label7.Size = new Size(126, 20);
			label7.TabIndex = 24;
			label7.Text = "Свободные даты";
			// 
			// nudRatingTo
			// 
			nudRatingTo.Location = new Point(241, 169);
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
			nudRatingFrom.Location = new Point(45, 169);
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
			label10.Location = new Point(209, 171);
			label10.Name = "label10";
			label10.Size = new Size(26, 20);
			label10.TabIndex = 21;
			label10.Text = "до";
			// 
			// label9
			// 
			label9.AutoSize = true;
			label9.Location = new Point(15, 171);
			label9.Name = "label9";
			label9.Size = new Size(24, 20);
			label9.TabIndex = 20;
			label9.Text = "от";
			// 
			// cbCities
			// 
			cbCities.FormattingEnabled = true;
			cbCities.Location = new Point(419, 168);
			cbCities.Name = "cbCities";
			cbCities.Size = new Size(391, 28);
			cbCities.TabIndex = 17;
			cbCities.SelectedIndexChanged += CbCities_SelectedIndexChanged;
			// 
			// cbDistricts
			// 
			cbDistricts.FormattingEnabled = true;
			cbDistricts.Location = new Point(419, 112);
			cbDistricts.Name = "cbDistricts";
			cbDistricts.Size = new Size(391, 28);
			cbDistricts.TabIndex = 19;
			cbDistricts.SelectedIndexChanged += CbDistricts_SelectedIndexChanged;
			// 
			// label5
			// 
			label5.AutoSize = true;
			label5.Location = new Point(420, 146);
			label5.Name = "label5";
			label5.Size = new Size(51, 20);
			label5.TabIndex = 12;
			label5.Text = "Город";
			// 
			// cbRegions
			// 
			cbRegions.FormattingEnabled = true;
			cbRegions.Location = new Point(419, 55);
			cbRegions.Name = "cbRegions";
			cbRegions.Size = new Size(391, 28);
			cbRegions.TabIndex = 16;
			cbRegions.SelectedIndexChanged += CbRegions_SelectedIndexChanged;
			// 
			// label4
			// 
			label4.AutoSize = true;
			label4.Location = new Point(419, 86);
			label4.Name = "label4";
			label4.Size = new Size(52, 20);
			label4.TabIndex = 7;
			label4.Text = "Район";
			// 
			// cbVenueTypes
			// 
			cbVenueTypes.FormattingEnabled = true;
			cbVenueTypes.Location = new Point(8, 112);
			cbVenueTypes.Name = "cbVenueTypes";
			cbVenueTypes.Size = new Size(391, 28);
			cbVenueTypes.TabIndex = 18;
			cbVenueTypes.SelectedIndexChanged += CbVenueTypes_SelectedIndexChanged;
			// 
			// label3
			// 
			label3.AutoSize = true;
			label3.Location = new Point(419, 33);
			label3.Name = "label3";
			label3.Size = new Size(58, 20);
			label3.TabIndex = 10;
			label3.Text = "Регион";
			// 
			// txtStreet
			// 
			txtStreet.BorderStyle = BorderStyle.FixedSingle;
			txtStreet.Location = new Point(419, 225);
			txtStreet.Name = "txtStreet";
			txtStreet.Size = new Size(391, 27);
			txtStreet.TabIndex = 14;
			txtStreet.TextChanged += TxtStreet_TextChanged;
			// 
			// label8
			// 
			label8.AutoSize = true;
			label8.Location = new Point(8, 146);
			label8.Name = "label8";
			label8.Size = new Size(64, 20);
			label8.TabIndex = 9;
			label8.Text = "Рейтинг";
			// 
			// txtName
			// 
			txtName.BorderStyle = BorderStyle.FixedSingle;
			txtName.Location = new Point(8, 56);
			txtName.Name = "txtName";
			txtName.Size = new Size(391, 27);
			txtName.TabIndex = 15;
			txtName.TextChanged += TxtName_TextChanged;
			// 
			// label6
			// 
			label6.AutoSize = true;
			label6.Location = new Point(419, 199);
			label6.Name = "label6";
			label6.Size = new Size(52, 20);
			label6.TabIndex = 8;
			label6.Text = "Улица";
			// 
			// label2
			// 
			label2.AutoSize = true;
			label2.Location = new Point(8, 86);
			label2.Name = "label2";
			label2.Size = new Size(35, 20);
			label2.TabIndex = 11;
			label2.Text = "Тип";
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Location = new Point(8, 33);
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
			dgvVenues.Location = new Point(13, 429);
			dgvVenues.Name = "dgvVenues";
			dgvVenues.ReadOnly = true;
			dgvVenues.RowHeadersWidth = 51;
			dgvVenues.Size = new Size(821, 239);
			dgvVenues.TabIndex = 2;
			// 
			// panel2
			// 
			panel2.Controls.Add(btnPrevPage);
			panel2.Controls.Add(btnNextPage);
			panel2.Controls.Add(lblPageInfo);
			panel2.Dock = DockStyle.Fill;
			panel2.Location = new Point(13, 674);
			panel2.Name = "panel2";
			panel2.Size = new Size(821, 34);
			panel2.TabIndex = 3;
			// 
			// btnPrevPage
			// 
			btnPrevPage.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
			btnPrevPage.FlatStyle = FlatStyle.Popup;
			btnPrevPage.Location = new Point(713, 2);
			btnPrevPage.Name = "btnPrevPage";
			btnPrevPage.Size = new Size(46, 29);
			btnPrevPage.TabIndex = 1;
			btnPrevPage.Text = "<";
			btnPrevPage.UseVisualStyleBackColor = true;
			// 
			// btnNextPage
			// 
			btnNextPage.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
			btnNextPage.FlatStyle = FlatStyle.Popup;
			btnNextPage.Location = new Point(765, 2);
			btnNextPage.Name = "btnNextPage";
			btnNextPage.Size = new Size(46, 29);
			btnNextPage.TabIndex = 1;
			btnNextPage.Text = ">";
			btnNextPage.UseVisualStyleBackColor = true;
			// 
			// lblPageInfo
			// 
			lblPageInfo.AutoSize = true;
			lblPageInfo.Location = new Point(3, 7);
			lblPageInfo.Name = "lblPageInfo";
			lblPageInfo.Size = new Size(58, 20);
			lblPageInfo.TabIndex = 0;
			lblPageInfo.Text = "label19";
			// 
			// panel3
			// 
			panel3.Controls.Add(btnAdd);
			panel3.Dock = DockStyle.Fill;
			panel3.Location = new Point(13, 714);
			panel3.Name = "panel3";
			panel3.Size = new Size(821, 34);
			panel3.TabIndex = 4;
			// 
			// btnAdd
			// 
			btnAdd.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
			btnAdd.FlatStyle = FlatStyle.Popup;
			btnAdd.Location = new Point(620, 2);
			btnAdd.Name = "btnAdd";
			btnAdd.Size = new Size(191, 29);
			btnAdd.TabIndex = 0;
			btnAdd.Text = "Добавить";
			btnAdd.UseVisualStyleBackColor = true;
			// 
			// panel4
			// 
			panel4.BackColor = SystemColors.Window;
			panel4.BorderStyle = BorderStyle.FixedSingle;
			panel4.Controls.Add(dtpBookingTo);
			panel4.Controls.Add(btnCheck);
			panel4.Controls.Add(btnBookingReset);
			panel4.Controls.Add(label20);
			panel4.Controls.Add(label18);
			panel4.Controls.Add(label17);
			panel4.Controls.Add(dtpBookingFrom);
			panel4.Dock = DockStyle.Fill;
			panel4.Location = new Point(13, 341);
			panel4.Name = "panel4";
			panel4.Size = new Size(821, 82);
			panel4.TabIndex = 5;
			// 
			// dtpBookingTo
			// 
			dtpBookingTo.Location = new Point(242, 32);
			dtpBookingTo.Name = "dtpBookingTo";
			dtpBookingTo.Size = new Size(159, 27);
			dtpBookingTo.TabIndex = 3;
			dtpBookingTo.ValueChanged += DtpBookingTo_ValueChanged;
			// 
			// btnCheck
			// 
			btnCheck.FlatStyle = FlatStyle.Popup;
			btnCheck.Location = new Point(619, 31);
			btnCheck.Name = "btnCheck";
			btnCheck.Size = new Size(192, 29);
			btnCheck.TabIndex = 28;
			btnCheck.Text = "Проверить";
			btnCheck.UseVisualStyleBackColor = true;
			btnCheck.Click += BtnCheck_Click;
			// 
			// btnBookingReset
			// 
			btnBookingReset.FlatStyle = FlatStyle.Popup;
			btnBookingReset.Location = new Point(421, 31);
			btnBookingReset.Name = "btnBookingReset";
			btnBookingReset.Size = new Size(192, 29);
			btnBookingReset.TabIndex = 28;
			btnBookingReset.Text = "Сбросить";
			btnBookingReset.UseVisualStyleBackColor = true;
			btnBookingReset.Click += BtnBookingReset_Click;
			// 
			// label20
			// 
			label20.AutoSize = true;
			label20.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 204);
			label20.Location = new Point(9, 7);
			label20.Name = "label20";
			label20.Size = new Size(116, 20);
			label20.TabIndex = 29;
			label20.Text = "Бронирование";
			// 
			// label18
			// 
			label18.AutoSize = true;
			label18.Location = new Point(210, 35);
			label18.Name = "label18";
			label18.Size = new Size(27, 20);
			label18.TabIndex = 2;
			label18.Text = "по";
			// 
			// label17
			// 
			label17.AutoSize = true;
			label17.Location = new Point(21, 35);
			label17.Name = "label17";
			label17.Size = new Size(16, 20);
			label17.TabIndex = 2;
			label17.Text = "с";
			// 
			// dtpBookingFrom
			// 
			dtpBookingFrom.Location = new Point(43, 32);
			dtpBookingFrom.Name = "dtpBookingFrom";
			dtpBookingFrom.Size = new Size(160, 27);
			dtpBookingFrom.TabIndex = 3;
			dtpBookingFrom.ValueChanged += DtpBookingFrom_ValueChanged;
			// 
			// OrderItemForm
			// 
			AutoScaleDimensions = new SizeF(8F, 20F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(847, 761);
			Controls.Add(tableLayoutPanel1);
			Name = "OrderItemForm";
			StartPosition = FormStartPosition.CenterScreen;
			Text = "Добавление новой позиции к заказу";
			FormClosed += OrderItemForm_FormClosed;
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
			panel3.ResumeLayout(false);
			panel4.ResumeLayout(false);
			panel4.PerformLayout();
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
		private DateTimePicker dtpBookingTo;
		private Label label18;
		private Button btnPrevPage;
		private Button btnNextPage;
		private Label lblPageInfo;
		private Panel panel3;
		private Label label19;
		private Panel panel4;
		private Button btnCheck;
		private Button btnBookingReset;
		private Label label20;
	}
}