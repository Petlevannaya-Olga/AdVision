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
			components = new System.ComponentModel.Container();
			label1 = new System.Windows.Forms.Label();
			txtName = new System.Windows.Forms.TextBox();
			label2 = new System.Windows.Forms.Label();
			cbVenueTypes = new System.Windows.Forms.ComboBox();
			openButton = new System.Windows.Forms.Button();
			label7 = new System.Windows.Forms.Label();
			label6 = new System.Windows.Forms.Label();
			label5 = new System.Windows.Forms.Label();
			label4 = new System.Windows.Forms.Label();
			label3 = new System.Windows.Forms.Label();
			txtHouseNumber = new System.Windows.Forms.TextBox();
			txtStreet = new System.Windows.Forms.TextBox();
			txtCity = new System.Windows.Forms.TextBox();
			txtDistrict = new System.Windows.Forms.TextBox();
			txtRegion = new System.Windows.Forms.TextBox();
			label11 = new System.Windows.Forms.Label();
			label12 = new System.Windows.Forms.Label();
			txtDescription = new System.Windows.Forms.TextBox();
			btnGenerate = new System.Windows.Forms.Button();
			btnSave = new System.Windows.Forms.Button();
			panel1 = new System.Windows.Forms.Panel();
			nudRating = new System.Windows.Forms.NumericUpDown();
			label13 = new System.Windows.Forms.Label();
			panel2 = new System.Windows.Forms.Panel();
			label14 = new System.Windows.Forms.Label();
			panel3 = new System.Windows.Forms.Panel();
			label8 = new System.Windows.Forms.Label();
			txtLatitude = new System.Windows.Forms.TextBox();
			txtLongitude = new System.Windows.Forms.TextBox();
			label17 = new System.Windows.Forms.Label();
			label18 = new System.Windows.Forms.Label();
			panel4 = new System.Windows.Forms.Panel();
			label9 = new System.Windows.Forms.Label();
			txtWidth = new System.Windows.Forms.TextBox();
			txtHeight = new System.Windows.Forms.TextBox();
			label15 = new System.Windows.Forms.Label();
			label16 = new System.Windows.Forms.Label();
			panel5 = new System.Windows.Forms.Panel();
			btnCancel = new System.Windows.Forms.Button();
			errorProvider1 = new System.Windows.Forms.ErrorProvider(components);
			panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)nudRating).BeginInit();
			panel2.SuspendLayout();
			panel3.SuspendLayout();
			panel4.SuspendLayout();
			panel5.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)errorProvider1).BeginInit();
			SuspendLayout();
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(16, 39);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(248, 20);
			label1.TabIndex = 0;
			label1.Text = "Название (от 10 до 500 символов)";
			// 
			// txtName
			// 
			txtName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			txtName.Location = new System.Drawing.Point(16, 62);
			txtName.Name = "txtName";
			txtName.Size = new System.Drawing.Size(351, 27);
			txtName.TabIndex = 1;
			// 
			// label2
			// 
			label2.AutoSize = true;
			label2.Location = new System.Drawing.Point(16, 92);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(109, 20);
			label2.TabIndex = 2;
			label2.Text = "Тип площадки";
			// 
			// cbVenueTypes
			// 
			cbVenueTypes.FormattingEnabled = true;
			cbVenueTypes.Location = new System.Drawing.Point(16, 114);
			cbVenueTypes.Name = "cbVenueTypes";
			cbVenueTypes.Size = new System.Drawing.Size(318, 28);
			cbVenueTypes.TabIndex = 3;
			// 
			// openButton
			// 
			openButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			openButton.Location = new System.Drawing.Point(337, 115);
			openButton.Name = "openButton";
			openButton.Size = new System.Drawing.Size(30, 28);
			openButton.TabIndex = 4;
			openButton.Text = "...";
			openButton.UseVisualStyleBackColor = true;
			openButton.Click += OpenButton_Click;
			// 
			// label7
			// 
			label7.AutoSize = true;
			label7.Location = new System.Drawing.Point(16, 259);
			label7.Name = "label7";
			label7.Size = new System.Drawing.Size(97, 20);
			label7.TabIndex = 1;
			label7.Text = "Номер дома";
			// 
			// label6
			// 
			label6.AutoSize = true;
			label6.Location = new System.Drawing.Point(16, 204);
			label6.Name = "label6";
			label6.Size = new System.Drawing.Size(223, 20);
			label6.TabIndex = 1;
			label6.Text = "Улица (от 10 до 300 символов)";
			// 
			// label5
			// 
			label5.AutoSize = true;
			label5.Location = new System.Drawing.Point(17, 149);
			label5.Name = "label5";
			label5.Size = new System.Drawing.Size(222, 20);
			label5.TabIndex = 1;
			label5.Text = "Город (от 10 до 300 символов)";
			// 
			// label4
			// 
			label4.AutoSize = true;
			label4.Location = new System.Drawing.Point(16, 94);
			label4.Name = "label4";
			label4.Size = new System.Drawing.Size(223, 20);
			label4.TabIndex = 1;
			label4.Text = "Район (от 10 до 300 символов)";
			// 
			// label3
			// 
			label3.AutoSize = true;
			label3.Location = new System.Drawing.Point(16, 39);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(229, 20);
			label3.TabIndex = 1;
			label3.Text = "Регион (от 10 до 300 символов)";
			// 
			// txtHouseNumber
			// 
			txtHouseNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			txtHouseNumber.Location = new System.Drawing.Point(16, 282);
			txtHouseNumber.Name = "txtHouseNumber";
			txtHouseNumber.Size = new System.Drawing.Size(353, 27);
			txtHouseNumber.TabIndex = 0;
			// 
			// txtStreet
			// 
			txtStreet.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			txtStreet.Location = new System.Drawing.Point(16, 227);
			txtStreet.Name = "txtStreet";
			txtStreet.Size = new System.Drawing.Size(353, 27);
			txtStreet.TabIndex = 0;
			// 
			// txtCity
			// 
			txtCity.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			txtCity.Location = new System.Drawing.Point(17, 172);
			txtCity.Name = "txtCity";
			txtCity.Size = new System.Drawing.Size(353, 27);
			txtCity.TabIndex = 0;
			// 
			// txtDistrict
			// 
			txtDistrict.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			txtDistrict.Location = new System.Drawing.Point(17, 117);
			txtDistrict.Name = "txtDistrict";
			txtDistrict.Size = new System.Drawing.Size(353, 27);
			txtDistrict.TabIndex = 0;
			// 
			// txtRegion
			// 
			txtRegion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			txtRegion.Location = new System.Drawing.Point(16, 62);
			txtRegion.Name = "txtRegion";
			txtRegion.Size = new System.Drawing.Size(353, 27);
			txtRegion.TabIndex = 0;
			// 
			// label11
			// 
			label11.AutoSize = true;
			label11.Location = new System.Drawing.Point(16, 145);
			label11.Name = "label11";
			label11.Size = new System.Drawing.Size(146, 20);
			label11.TabIndex = 1;
			label11.Text = "Рейтинг (от 1 до 10)";
			// 
			// label12
			// 
			label12.AutoSize = true;
			label12.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)204));
			label12.Location = new System.Drawing.Point(16, 8);
			label12.Name = "label12";
			label12.Size = new System.Drawing.Size(285, 20);
			label12.TabIndex = 8;
			label12.Text = "Описание (от 100 до 2000 символов) ";
			// 
			// txtDescription
			// 
			txtDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			txtDescription.Location = new System.Drawing.Point(16, 40);
			txtDescription.Multiline = true;
			txtDescription.Name = "txtDescription";
			txtDescription.Size = new System.Drawing.Size(353, 270);
			txtDescription.TabIndex = 9;
			// 
			// btnGenerate
			// 
			btnGenerate.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
			btnGenerate.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			btnGenerate.Location = new System.Drawing.Point(211, 572);
			btnGenerate.Name = "btnGenerate";
			btnGenerate.Size = new System.Drawing.Size(190, 29);
			btnGenerate.TabIndex = 10;
			btnGenerate.Text = "Сгенерировать";
			btnGenerate.UseVisualStyleBackColor = false;
			btnGenerate.Click += BtnGenerate_Click;
			// 
			// btnSave
			// 
			btnSave.BackColor = System.Drawing.SystemColors.HotTrack;
			btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			btnSave.ForeColor = System.Drawing.SystemColors.Window;
			btnSave.Location = new System.Drawing.Point(407, 572);
			btnSave.Name = "btnSave";
			btnSave.Size = new System.Drawing.Size(190, 29);
			btnSave.TabIndex = 10;
			btnSave.Text = "Сохранить";
			btnSave.UseVisualStyleBackColor = false;
			btnSave.Click += BtnSave_Click;
			// 
			// panel1
			// 
			panel1.BackColor = System.Drawing.SystemColors.Window;
			panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			panel1.Controls.Add(nudRating);
			panel1.Controls.Add(label13);
			panel1.Controls.Add(label1);
			panel1.Controls.Add(txtName);
			panel1.Controls.Add(cbVenueTypes);
			panel1.Controls.Add(label2);
			panel1.Controls.Add(openButton);
			panel1.Controls.Add(label11);
			panel1.Location = new System.Drawing.Point(12, 12);
			panel1.Name = "panel1";
			panel1.Size = new System.Drawing.Size(389, 220);
			panel1.TabIndex = 11;
			// 
			// nudRating
			// 
			nudRating.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			nudRating.Location = new System.Drawing.Point(16, 168);
			nudRating.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
			nudRating.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
			nudRating.Name = "nudRating";
			nudRating.Size = new System.Drawing.Size(351, 27);
			nudRating.TabIndex = 5;
			nudRating.Value = new decimal(new int[] { 1, 0, 0, 0 });
			// 
			// label13
			// 
			label13.AutoSize = true;
			label13.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)204));
			label13.Location = new System.Drawing.Point(16, 10);
			label13.Name = "label13";
			label13.Size = new System.Drawing.Size(180, 20);
			label13.TabIndex = 0;
			label13.Text = "Основная информация";
			// 
			// panel2
			// 
			panel2.BackColor = System.Drawing.SystemColors.Window;
			panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
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
			panel2.Location = new System.Drawing.Point(12, 238);
			panel2.Name = "panel2";
			panel2.Size = new System.Drawing.Size(389, 328);
			panel2.TabIndex = 12;
			// 
			// label14
			// 
			label14.AutoSize = true;
			label14.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)204));
			label14.Location = new System.Drawing.Point(17, 7);
			label14.Name = "label14";
			label14.Size = new System.Drawing.Size(53, 20);
			label14.TabIndex = 5;
			label14.Text = "Адрес";
			// 
			// panel3
			// 
			panel3.BackColor = System.Drawing.SystemColors.Window;
			panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			panel3.Controls.Add(label8);
			panel3.Controls.Add(txtLatitude);
			panel3.Controls.Add(txtLongitude);
			panel3.Controls.Add(label17);
			panel3.Controls.Add(label18);
			panel3.Location = new System.Drawing.Point(407, 12);
			panel3.Name = "panel3";
			panel3.Size = new System.Drawing.Size(389, 106);
			panel3.TabIndex = 12;
			// 
			// label8
			// 
			label8.AutoSize = true;
			label8.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)204));
			label8.Location = new System.Drawing.Point(17, 7);
			label8.Name = "label8";
			label8.Size = new System.Drawing.Size(100, 20);
			label8.TabIndex = 5;
			label8.Text = "Координаты";
			// 
			// txtLatitude
			// 
			txtLatitude.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			txtLatitude.Location = new System.Drawing.Point(16, 62);
			txtLatitude.Name = "txtLatitude";
			txtLatitude.Size = new System.Drawing.Size(180, 27);
			txtLatitude.TabIndex = 0;
			// 
			// txtLongitude
			// 
			txtLongitude.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			txtLongitude.Location = new System.Drawing.Point(202, 62);
			txtLongitude.Name = "txtLongitude";
			txtLongitude.Size = new System.Drawing.Size(168, 27);
			txtLongitude.TabIndex = 0;
			// 
			// label17
			// 
			label17.AutoSize = true;
			label17.Location = new System.Drawing.Point(201, 39);
			label17.Name = "label17";
			label17.Size = new System.Drawing.Size(140, 20);
			label17.TabIndex = 1;
			label17.Text = "Долгота [-180; 180]";
			// 
			// label18
			// 
			label18.AutoSize = true;
			label18.Location = new System.Drawing.Point(16, 39);
			label18.Name = "label18";
			label18.Size = new System.Drawing.Size(123, 20);
			label18.TabIndex = 1;
			label18.Text = "Широта [-90; 90]";
			// 
			// panel4
			// 
			panel4.BackColor = System.Drawing.SystemColors.Window;
			panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			panel4.Controls.Add(label9);
			panel4.Controls.Add(txtWidth);
			panel4.Controls.Add(txtHeight);
			panel4.Controls.Add(label15);
			panel4.Controls.Add(label16);
			panel4.Location = new System.Drawing.Point(407, 125);
			panel4.Name = "panel4";
			panel4.Size = new System.Drawing.Size(389, 106);
			panel4.TabIndex = 12;
			// 
			// label9
			// 
			label9.AutoSize = true;
			label9.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)204));
			label9.Location = new System.Drawing.Point(17, 7);
			label9.Name = "label9";
			label9.Size = new System.Drawing.Size(219, 20);
			label9.TabIndex = 5;
			label9.Text = "Размер (от 100 до 10000 см)";
			// 
			// txtWidth
			// 
			txtWidth.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			txtWidth.Location = new System.Drawing.Point(16, 62);
			txtWidth.Name = "txtWidth";
			txtWidth.Size = new System.Drawing.Size(180, 27);
			txtWidth.TabIndex = 0;
			// 
			// txtHeight
			// 
			txtHeight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			txtHeight.Location = new System.Drawing.Point(202, 62);
			txtHeight.Name = "txtHeight";
			txtHeight.Size = new System.Drawing.Size(168, 27);
			txtHeight.TabIndex = 0;
			// 
			// label15
			// 
			label15.AutoSize = true;
			label15.Location = new System.Drawing.Point(201, 39);
			label15.Name = "label15";
			label15.Size = new System.Drawing.Size(59, 20);
			label15.TabIndex = 1;
			label15.Text = "Высота";
			// 
			// label16
			// 
			label16.AutoSize = true;
			label16.Location = new System.Drawing.Point(16, 39);
			label16.Name = "label16";
			label16.Size = new System.Drawing.Size(67, 20);
			label16.TabIndex = 1;
			label16.Text = "Ширина";
			// 
			// panel5
			// 
			panel5.BackColor = System.Drawing.SystemColors.Window;
			panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			panel5.Controls.Add(label12);
			panel5.Controls.Add(txtDescription);
			panel5.Location = new System.Drawing.Point(407, 237);
			panel5.Name = "panel5";
			panel5.Size = new System.Drawing.Size(389, 329);
			panel5.TabIndex = 13;
			// 
			// btnCancel
			// 
			btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			btnCancel.Location = new System.Drawing.Point(606, 572);
			btnCancel.Name = "btnCancel";
			btnCancel.Size = new System.Drawing.Size(190, 29);
			btnCancel.TabIndex = 10;
			btnCancel.Text = "Отменить";
			btnCancel.UseVisualStyleBackColor = true;
			btnCancel.Click += BtnCancel_Click;
			// 
			// errorProvider1
			// 
			errorProvider1.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
			errorProvider1.ContainerControl = this;
			// 
			// VenueForm
			// 
			AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
			AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			ClientSize = new System.Drawing.Size(810, 612);
			Controls.Add(panel5);
			Controls.Add(panel4);
			Controls.Add(panel3);
			Controls.Add(panel2);
			Controls.Add(panel1);
			Controls.Add(btnCancel);
			Controls.Add(btnSave);
			Controls.Add(btnGenerate);
			FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			MaximizeBox = false;
			MinimizeBox = false;
			StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			Text = "Новая площадка";
			panel1.ResumeLayout(false);
			panel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)nudRating).EndInit();
			panel2.ResumeLayout(false);
			panel2.PerformLayout();
			panel3.ResumeLayout(false);
			panel3.PerformLayout();
			panel4.ResumeLayout(false);
			panel4.PerformLayout();
			panel5.ResumeLayout(false);
			panel5.PerformLayout();
			((System.ComponentModel.ISupportInitialize)errorProvider1).EndInit();
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
		private ErrorProvider errorProvider1;
		private NumericUpDown nudRating;
	}
}