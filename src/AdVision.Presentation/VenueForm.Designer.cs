namespace AdVision.Presentation
{
	partial class VenueForm
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
			txtName = new TextBox();
			label2 = new Label();
			cbVenueTypes = new ComboBox();
			openButton = new Button();
			label7 = new Label();
			label6 = new Label();
			label5 = new Label();
			label4 = new Label();
			label3 = new Label();
			txtHouseNumber = new TextBox();
			txtStreet = new TextBox();
			txtCity = new TextBox();
			txtDistrict = new TextBox();
			txtRegion = new TextBox();
			label11 = new Label();
			label12 = new Label();
			txtDescription = new TextBox();
			btnGenerate = new Button();
			btnSave = new Button();
			panel1 = new Panel();
			pbRatingValidation = new PictureBox();
			pbNameValidation = new PictureBox();
			pbVenueTypeValidation = new PictureBox();
			nudRating = new NumericUpDown();
			label13 = new Label();
			panel2 = new Panel();
			pbHouserNumberValidation = new PictureBox();
			pbStreetValidation = new PictureBox();
			pbCityValidation = new PictureBox();
			pbDistrictValidation = new PictureBox();
			pbRegionValidation = new PictureBox();
			label14 = new Label();
			panel3 = new Panel();
			label8 = new Label();
			txtLatitude = new TextBox();
			pbLongitudeValidation = new PictureBox();
			pbLatitudeValidation = new PictureBox();
			txtLongitude = new TextBox();
			label17 = new Label();
			label18 = new Label();
			panel4 = new Panel();
			label9 = new Label();
			txtWidth = new TextBox();
			pbHeightValidation = new PictureBox();
			pbWidthValidation = new PictureBox();
			txtHeight = new TextBox();
			label15 = new Label();
			label16 = new Label();
			panel5 = new Panel();
			pbDescriptionValidation = new PictureBox();
			btnCancel = new Button();
			panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)pbRatingValidation).BeginInit();
			((System.ComponentModel.ISupportInitialize)pbNameValidation).BeginInit();
			((System.ComponentModel.ISupportInitialize)pbVenueTypeValidation).BeginInit();
			((System.ComponentModel.ISupportInitialize)nudRating).BeginInit();
			panel2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)pbHouserNumberValidation).BeginInit();
			((System.ComponentModel.ISupportInitialize)pbStreetValidation).BeginInit();
			((System.ComponentModel.ISupportInitialize)pbCityValidation).BeginInit();
			((System.ComponentModel.ISupportInitialize)pbDistrictValidation).BeginInit();
			((System.ComponentModel.ISupportInitialize)pbRegionValidation).BeginInit();
			panel3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)pbLongitudeValidation).BeginInit();
			((System.ComponentModel.ISupportInitialize)pbLatitudeValidation).BeginInit();
			panel4.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)pbHeightValidation).BeginInit();
			((System.ComponentModel.ISupportInitialize)pbWidthValidation).BeginInit();
			panel5.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)pbDescriptionValidation).BeginInit();
			SuspendLayout();
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Location = new Point(16, 39);
			label1.Name = "label1";
			label1.Size = new Size(248, 20);
			label1.TabIndex = 0;
			label1.Text = "Название (от 10 до 500 символов)";
			// 
			// txtName
			// 
			txtName.BorderStyle = BorderStyle.FixedSingle;
			txtName.Location = new Point(16, 62);
			txtName.Name = "txtName";
			txtName.Size = new Size(335, 27);
			txtName.TabIndex = 1;
			txtName.TextChanged += TxtName_TextChanged;
			// 
			// label2
			// 
			label2.AutoSize = true;
			label2.Location = new Point(16, 92);
			label2.Name = "label2";
			label2.Size = new Size(109, 20);
			label2.TabIndex = 2;
			label2.Text = "Тип площадки";
			// 
			// cbVenueTypes
			// 
			cbVenueTypes.FormattingEnabled = true;
			cbVenueTypes.Location = new Point(16, 114);
			cbVenueTypes.Name = "cbVenueTypes";
			cbVenueTypes.Size = new Size(299, 28);
			cbVenueTypes.TabIndex = 3;
			cbVenueTypes.TextChanged += CbVenueTypes_TextChanged;
			// 
			// openButton
			// 
			openButton.FlatStyle = FlatStyle.Popup;
			openButton.Location = new Point(321, 114);
			openButton.Name = "openButton";
			openButton.Size = new Size(30, 28);
			openButton.TabIndex = 4;
			openButton.Text = "...";
			openButton.UseVisualStyleBackColor = true;
			openButton.Click += OpenButton_Click;
			// 
			// label7
			// 
			label7.AutoSize = true;
			label7.Location = new Point(16, 259);
			label7.Name = "label7";
			label7.Size = new Size(97, 20);
			label7.TabIndex = 1;
			label7.Text = "Номер дома";
			// 
			// label6
			// 
			label6.AutoSize = true;
			label6.Location = new Point(16, 204);
			label6.Name = "label6";
			label6.Size = new Size(223, 20);
			label6.TabIndex = 1;
			label6.Text = "Улица (от 10 до 300 символов)";
			// 
			// label5
			// 
			label5.AutoSize = true;
			label5.Location = new Point(17, 149);
			label5.Name = "label5";
			label5.Size = new Size(222, 20);
			label5.TabIndex = 1;
			label5.Text = "Город (от 10 до 300 символов)";
			// 
			// label4
			// 
			label4.AutoSize = true;
			label4.Location = new Point(16, 94);
			label4.Name = "label4";
			label4.Size = new Size(223, 20);
			label4.TabIndex = 1;
			label4.Text = "Район (от 10 до 300 символов)";
			// 
			// label3
			// 
			label3.AutoSize = true;
			label3.Location = new Point(16, 39);
			label3.Name = "label3";
			label3.Size = new Size(229, 20);
			label3.TabIndex = 1;
			label3.Text = "Регион (от 10 до 300 символов)";
			// 
			// txtHouseNumber
			// 
			txtHouseNumber.BorderStyle = BorderStyle.FixedSingle;
			txtHouseNumber.Location = new Point(16, 282);
			txtHouseNumber.Name = "txtHouseNumber";
			txtHouseNumber.Size = new Size(335, 27);
			txtHouseNumber.TabIndex = 0;
			txtHouseNumber.TextChanged += TxtHouseNumber_TextChanged;
			// 
			// txtStreet
			// 
			txtStreet.BorderStyle = BorderStyle.FixedSingle;
			txtStreet.Location = new Point(16, 227);
			txtStreet.Name = "txtStreet";
			txtStreet.Size = new Size(335, 27);
			txtStreet.TabIndex = 0;
			txtStreet.TextChanged += TxtStreet_TextChanged;
			// 
			// txtCity
			// 
			txtCity.BorderStyle = BorderStyle.FixedSingle;
			txtCity.Location = new Point(17, 172);
			txtCity.Name = "txtCity";
			txtCity.Size = new Size(334, 27);
			txtCity.TabIndex = 0;
			txtCity.TextChanged += TxtCity_TextChanged;
			// 
			// txtDistrict
			// 
			txtDistrict.BorderStyle = BorderStyle.FixedSingle;
			txtDistrict.Location = new Point(17, 117);
			txtDistrict.Name = "txtDistrict";
			txtDistrict.Size = new Size(334, 27);
			txtDistrict.TabIndex = 0;
			txtDistrict.TextChanged += TxtDistrict_TextChanged;
			// 
			// txtRegion
			// 
			txtRegion.BorderStyle = BorderStyle.FixedSingle;
			txtRegion.Location = new Point(16, 62);
			txtRegion.Name = "txtRegion";
			txtRegion.Size = new Size(335, 27);
			txtRegion.TabIndex = 0;
			txtRegion.TextChanged += TxtRegion_TextChanged;
			// 
			// label11
			// 
			label11.AutoSize = true;
			label11.Location = new Point(16, 145);
			label11.Name = "label11";
			label11.Size = new Size(146, 20);
			label11.TabIndex = 1;
			label11.Text = "Рейтинг (от 1 до 10)";
			// 
			// label12
			// 
			label12.AutoSize = true;
			label12.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 204);
			label12.Location = new Point(16, 8);
			label12.Name = "label12";
			label12.Size = new Size(285, 20);
			label12.TabIndex = 8;
			label12.Text = "Описание (от 100 до 2000 символов) ";
			// 
			// txtDescription
			// 
			txtDescription.BorderStyle = BorderStyle.FixedSingle;
			txtDescription.Location = new Point(16, 40);
			txtDescription.Multiline = true;
			txtDescription.Name = "txtDescription";
			txtDescription.Size = new Size(331, 270);
			txtDescription.TabIndex = 9;
			txtDescription.TextChanged += TxtDescription_TextChanged;
			// 
			// btnGenerate
			// 
			btnGenerate.BackColor = SystemColors.GradientInactiveCaption;
			btnGenerate.FlatStyle = FlatStyle.Popup;
			btnGenerate.Location = new Point(211, 572);
			btnGenerate.Name = "btnGenerate";
			btnGenerate.Size = new Size(190, 29);
			btnGenerate.TabIndex = 10;
			btnGenerate.Text = "Сгенерировать";
			btnGenerate.UseVisualStyleBackColor = false;
			btnGenerate.Click += BtnGenerate_Click;
			// 
			// btnSave
			// 
			btnSave.BackColor = SystemColors.HotTrack;
			btnSave.FlatStyle = FlatStyle.Popup;
			btnSave.ForeColor = SystemColors.Window;
			btnSave.Location = new Point(407, 572);
			btnSave.Name = "btnSave";
			btnSave.Size = new Size(190, 29);
			btnSave.TabIndex = 10;
			btnSave.Text = "Сохранить";
			btnSave.UseVisualStyleBackColor = false;
			btnSave.Click += BtnSave_Click;
			// 
			// panel1
			// 
			panel1.BackColor = SystemColors.Window;
			panel1.BorderStyle = BorderStyle.FixedSingle;
			panel1.Controls.Add(pbRatingValidation);
			panel1.Controls.Add(pbNameValidation);
			panel1.Controls.Add(pbVenueTypeValidation);
			panel1.Controls.Add(nudRating);
			panel1.Controls.Add(label13);
			panel1.Controls.Add(label1);
			panel1.Controls.Add(txtName);
			panel1.Controls.Add(cbVenueTypes);
			panel1.Controls.Add(label2);
			panel1.Controls.Add(openButton);
			panel1.Controls.Add(label11);
			panel1.Location = new Point(12, 12);
			panel1.Name = "panel1";
			panel1.Size = new Size(389, 220);
			panel1.TabIndex = 11;
			// 
			// pbRatingValidation
			// 
			pbRatingValidation.Location = new Point(357, 168);
			pbRatingValidation.Name = "pbRatingValidation";
			pbRatingValidation.Size = new Size(27, 27);
			pbRatingValidation.TabIndex = 6;
			pbRatingValidation.TabStop = false;
			// 
			// pbNameValidation
			// 
			pbNameValidation.Location = new Point(357, 62);
			pbNameValidation.Name = "pbNameValidation";
			pbNameValidation.Size = new Size(27, 27);
			pbNameValidation.TabIndex = 6;
			pbNameValidation.TabStop = false;
			// 
			// pbVenueTypeValidation
			// 
			pbVenueTypeValidation.Location = new Point(357, 115);
			pbVenueTypeValidation.Name = "pbVenueTypeValidation";
			pbVenueTypeValidation.Size = new Size(27, 27);
			pbVenueTypeValidation.TabIndex = 6;
			pbVenueTypeValidation.TabStop = false;
			// 
			// nudRating
			// 
			nudRating.BorderStyle = BorderStyle.FixedSingle;
			nudRating.Location = new Point(16, 168);
			nudRating.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
			nudRating.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
			nudRating.Name = "nudRating";
			nudRating.Size = new Size(335, 27);
			nudRating.TabIndex = 5;
			nudRating.Value = new decimal(new int[] { 1, 0, 0, 0 });
			nudRating.ValueChanged += NudRating_ValueChanged;
			// 
			// label13
			// 
			label13.AutoSize = true;
			label13.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 204);
			label13.Location = new Point(16, 10);
			label13.Name = "label13";
			label13.Size = new Size(180, 20);
			label13.TabIndex = 0;
			label13.Text = "Основная информация";
			// 
			// panel2
			// 
			panel2.BackColor = SystemColors.Window;
			panel2.BorderStyle = BorderStyle.FixedSingle;
			panel2.Controls.Add(pbHouserNumberValidation);
			panel2.Controls.Add(pbStreetValidation);
			panel2.Controls.Add(pbCityValidation);
			panel2.Controls.Add(pbDistrictValidation);
			panel2.Controls.Add(pbRegionValidation);
			panel2.Controls.Add(label14);
			panel2.Controls.Add(txtRegion);
			panel2.Controls.Add(txtDistrict);
			panel2.Controls.Add(label7);
			panel2.Controls.Add(txtCity);
			panel2.Controls.Add(label6);
			panel2.Controls.Add(txtStreet);
			panel2.Controls.Add(label5);
			panel2.Controls.Add(txtHouseNumber);
			panel2.Controls.Add(label4);
			panel2.Controls.Add(label3);
			panel2.Location = new Point(12, 238);
			panel2.Name = "panel2";
			panel2.Size = new Size(389, 328);
			panel2.TabIndex = 12;
			// 
			// pbHouserNumberValidation
			// 
			pbHouserNumberValidation.Location = new Point(357, 282);
			pbHouserNumberValidation.Name = "pbHouserNumberValidation";
			pbHouserNumberValidation.Size = new Size(27, 27);
			pbHouserNumberValidation.TabIndex = 6;
			pbHouserNumberValidation.TabStop = false;
			// 
			// pbStreetValidation
			// 
			pbStreetValidation.Location = new Point(357, 227);
			pbStreetValidation.Name = "pbStreetValidation";
			pbStreetValidation.Size = new Size(27, 27);
			pbStreetValidation.TabIndex = 6;
			pbStreetValidation.TabStop = false;
			// 
			// pbCityValidation
			// 
			pbCityValidation.Location = new Point(357, 172);
			pbCityValidation.Name = "pbCityValidation";
			pbCityValidation.Size = new Size(27, 27);
			pbCityValidation.TabIndex = 6;
			pbCityValidation.TabStop = false;
			// 
			// pbDistrictValidation
			// 
			pbDistrictValidation.Location = new Point(357, 117);
			pbDistrictValidation.Name = "pbDistrictValidation";
			pbDistrictValidation.Size = new Size(27, 27);
			pbDistrictValidation.TabIndex = 6;
			pbDistrictValidation.TabStop = false;
			// 
			// pbRegionValidation
			// 
			pbRegionValidation.Location = new Point(357, 62);
			pbRegionValidation.Name = "pbRegionValidation";
			pbRegionValidation.Size = new Size(27, 27);
			pbRegionValidation.TabIndex = 6;
			pbRegionValidation.TabStop = false;
			// 
			// label14
			// 
			label14.AutoSize = true;
			label14.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 204);
			label14.Location = new Point(17, 7);
			label14.Name = "label14";
			label14.Size = new Size(53, 20);
			label14.TabIndex = 5;
			label14.Text = "Адрес";
			// 
			// panel3
			// 
			panel3.BackColor = SystemColors.Window;
			panel3.BorderStyle = BorderStyle.FixedSingle;
			panel3.Controls.Add(label8);
			panel3.Controls.Add(txtLatitude);
			panel3.Controls.Add(pbLongitudeValidation);
			panel3.Controls.Add(pbLatitudeValidation);
			panel3.Controls.Add(txtLongitude);
			panel3.Controls.Add(label17);
			panel3.Controls.Add(label18);
			panel3.Location = new Point(407, 12);
			panel3.Name = "panel3";
			panel3.Size = new Size(389, 106);
			panel3.TabIndex = 12;
			// 
			// label8
			// 
			label8.AutoSize = true;
			label8.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 204);
			label8.Location = new Point(17, 7);
			label8.Name = "label8";
			label8.Size = new Size(100, 20);
			label8.TabIndex = 5;
			label8.Text = "Координаты";
			// 
			// txtLatitude
			// 
			txtLatitude.BorderStyle = BorderStyle.FixedSingle;
			txtLatitude.Location = new Point(16, 62);
			txtLatitude.Name = "txtLatitude";
			txtLatitude.Size = new Size(145, 27);
			txtLatitude.TabIndex = 0;
			txtLatitude.TextChanged += TxtLatitude_TextChanged;
			// 
			// pbLongitudeValidation
			// 
			pbLongitudeValidation.Location = new Point(353, 62);
			pbLongitudeValidation.Name = "pbLongitudeValidation";
			pbLongitudeValidation.Size = new Size(27, 27);
			pbLongitudeValidation.TabIndex = 6;
			pbLongitudeValidation.TabStop = false;
			// 
			// pbLatitudeValidation
			// 
			pbLatitudeValidation.Location = new Point(167, 62);
			pbLatitudeValidation.Name = "pbLatitudeValidation";
			pbLatitudeValidation.Size = new Size(27, 27);
			pbLatitudeValidation.TabIndex = 6;
			pbLatitudeValidation.TabStop = false;
			// 
			// txtLongitude
			// 
			txtLongitude.BorderStyle = BorderStyle.FixedSingle;
			txtLongitude.Location = new Point(202, 62);
			txtLongitude.Name = "txtLongitude";
			txtLongitude.Size = new Size(145, 27);
			txtLongitude.TabIndex = 0;
			txtLongitude.TextChanged += TxtLongitude_TextChanged;
			// 
			// label17
			// 
			label17.AutoSize = true;
			label17.Location = new Point(201, 39);
			label17.Name = "label17";
			label17.Size = new Size(140, 20);
			label17.TabIndex = 1;
			label17.Text = "Долгота [-180; 180]";
			// 
			// label18
			// 
			label18.AutoSize = true;
			label18.Location = new Point(16, 39);
			label18.Name = "label18";
			label18.Size = new Size(123, 20);
			label18.TabIndex = 1;
			label18.Text = "Широта [-90; 90]";
			// 
			// panel4
			// 
			panel4.BackColor = SystemColors.Window;
			panel4.BorderStyle = BorderStyle.FixedSingle;
			panel4.Controls.Add(label9);
			panel4.Controls.Add(txtWidth);
			panel4.Controls.Add(pbHeightValidation);
			panel4.Controls.Add(pbWidthValidation);
			panel4.Controls.Add(txtHeight);
			panel4.Controls.Add(label15);
			panel4.Controls.Add(label16);
			panel4.Location = new Point(407, 125);
			panel4.Name = "panel4";
			panel4.Size = new Size(389, 106);
			panel4.TabIndex = 12;
			// 
			// label9
			// 
			label9.AutoSize = true;
			label9.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 204);
			label9.Location = new Point(17, 7);
			label9.Name = "label9";
			label9.Size = new Size(219, 20);
			label9.TabIndex = 5;
			label9.Text = "Размер (от 100 до 10000 см)";
			// 
			// txtWidth
			// 
			txtWidth.BorderStyle = BorderStyle.FixedSingle;
			txtWidth.Location = new Point(16, 62);
			txtWidth.Name = "txtWidth";
			txtWidth.Size = new Size(145, 27);
			txtWidth.TabIndex = 0;
			txtWidth.TextChanged += TxtWidth_TextChanged;
			// 
			// pbHeightValidation
			// 
			pbHeightValidation.Location = new Point(353, 62);
			pbHeightValidation.Name = "pbHeightValidation";
			pbHeightValidation.Size = new Size(27, 27);
			pbHeightValidation.TabIndex = 6;
			pbHeightValidation.TabStop = false;
			// 
			// pbWidthValidation
			// 
			pbWidthValidation.Location = new Point(167, 62);
			pbWidthValidation.Name = "pbWidthValidation";
			pbWidthValidation.Size = new Size(27, 27);
			pbWidthValidation.TabIndex = 6;
			pbWidthValidation.TabStop = false;
			// 
			// txtHeight
			// 
			txtHeight.BorderStyle = BorderStyle.FixedSingle;
			txtHeight.Location = new Point(202, 62);
			txtHeight.Name = "txtHeight";
			txtHeight.Size = new Size(145, 27);
			txtHeight.TabIndex = 0;
			txtHeight.TextChanged += TxtHeight_TextChanged;
			// 
			// label15
			// 
			label15.AutoSize = true;
			label15.Location = new Point(201, 39);
			label15.Name = "label15";
			label15.Size = new Size(59, 20);
			label15.TabIndex = 1;
			label15.Text = "Высота";
			// 
			// label16
			// 
			label16.AutoSize = true;
			label16.Location = new Point(16, 39);
			label16.Name = "label16";
			label16.Size = new Size(67, 20);
			label16.TabIndex = 1;
			label16.Text = "Ширина";
			// 
			// panel5
			// 
			panel5.BackColor = SystemColors.Window;
			panel5.BorderStyle = BorderStyle.FixedSingle;
			panel5.Controls.Add(label12);
			panel5.Controls.Add(txtDescription);
			panel5.Controls.Add(pbDescriptionValidation);
			panel5.Location = new Point(407, 237);
			panel5.Name = "panel5";
			panel5.Size = new Size(389, 329);
			panel5.TabIndex = 13;
			// 
			// pbDescriptionValidation
			// 
			pbDescriptionValidation.Location = new Point(353, 40);
			pbDescriptionValidation.Name = "pbDescriptionValidation";
			pbDescriptionValidation.Size = new Size(27, 27);
			pbDescriptionValidation.TabIndex = 6;
			pbDescriptionValidation.TabStop = false;
			// 
			// btnCancel
			// 
			btnCancel.FlatStyle = FlatStyle.Popup;
			btnCancel.Location = new Point(606, 572);
			btnCancel.Name = "btnCancel";
			btnCancel.Size = new Size(190, 29);
			btnCancel.TabIndex = 10;
			btnCancel.Text = "Отменить";
			btnCancel.UseVisualStyleBackColor = true;
			btnCancel.Click += BtnCancel_Click;
			// 
			// VenueForm
			// 
			AutoScaleDimensions = new SizeF(8F, 20F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(810, 612);
			Controls.Add(panel5);
			Controls.Add(panel4);
			Controls.Add(panel3);
			Controls.Add(panel2);
			Controls.Add(panel1);
			Controls.Add(btnCancel);
			Controls.Add(btnSave);
			Controls.Add(btnGenerate);
			FormBorderStyle = FormBorderStyle.FixedSingle;
			MaximizeBox = false;
			MinimizeBox = false;
			Name = "VenueForm";
			StartPosition = FormStartPosition.CenterScreen;
			Text = "Новая площадка";
			panel1.ResumeLayout(false);
			panel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)pbRatingValidation).EndInit();
			((System.ComponentModel.ISupportInitialize)pbNameValidation).EndInit();
			((System.ComponentModel.ISupportInitialize)pbVenueTypeValidation).EndInit();
			((System.ComponentModel.ISupportInitialize)nudRating).EndInit();
			panel2.ResumeLayout(false);
			panel2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)pbHouserNumberValidation).EndInit();
			((System.ComponentModel.ISupportInitialize)pbStreetValidation).EndInit();
			((System.ComponentModel.ISupportInitialize)pbCityValidation).EndInit();
			((System.ComponentModel.ISupportInitialize)pbDistrictValidation).EndInit();
			((System.ComponentModel.ISupportInitialize)pbRegionValidation).EndInit();
			panel3.ResumeLayout(false);
			panel3.PerformLayout();
			((System.ComponentModel.ISupportInitialize)pbLongitudeValidation).EndInit();
			((System.ComponentModel.ISupportInitialize)pbLatitudeValidation).EndInit();
			panel4.ResumeLayout(false);
			panel4.PerformLayout();
			((System.ComponentModel.ISupportInitialize)pbHeightValidation).EndInit();
			((System.ComponentModel.ISupportInitialize)pbWidthValidation).EndInit();
			panel5.ResumeLayout(false);
			panel5.PerformLayout();
			((System.ComponentModel.ISupportInitialize)pbDescriptionValidation).EndInit();
			ResumeLayout(false);
		}

		#endregion

		private Label label1;
		private TextBox txtName;
		private Label label2;
		private ComboBox cbVenueTypes;
		private Button openButton;
		private Label label3;
		private TextBox txtRegion;
		private Label label4;
		private TextBox txtDistrict;
		private Label label7;
		private Label label6;
		private Label label5;
		private TextBox txtHouseNumber;
		private TextBox txtStreet;
		private TextBox txtCity;
		private Label label11;
		private Label label12;
		private TextBox txtDescription;
		private Button btnGenerate;
		private System.Windows.Forms.Button btnSave;
		private Panel panel1;
		private Label label13;
		private Panel panel2;
		private Label label14;
		private Panel panel3;
		private Label label8;
		private TextBox txtLatitude;
		private TextBox txtLongitude;
		private Label label17;
		private Label label18;
		private Panel panel4;
		private Label label9;
		private TextBox txtWidth;
		private TextBox txtHeight;
		private Label label15;
		private Label label16;
		private Panel panel5;
		private System.Windows.Forms.Button btnCancel;
		private NumericUpDown nudRating;
		private PictureBox pbRatingValidation;
		private PictureBox pbNameValidation;
		private PictureBox pbVenueTypeValidation;
		private PictureBox pbHouserNumberValidation;
		private PictureBox pbStreetValidation;
		private PictureBox pbCityValidation;
		private PictureBox pbDistrictValidation;
		private PictureBox pbRegionValidation;
		private PictureBox pbLongitudeValidation;
		private PictureBox pbLatitudeValidation;
		private PictureBox pbHeightValidation;
		private PictureBox pbWidthValidation;
		private PictureBox pbDescriptionValidation;
	}
}