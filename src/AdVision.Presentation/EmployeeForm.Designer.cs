namespace AdVision.Presentation
{
	partial class EmployeeForm
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
			txtLastName = new TextBox();
			pbLastNameValidation = new PictureBox();
			label2 = new Label();
			txtFirstName = new TextBox();
			pbFirstNameValidation = new PictureBox();
			label3 = new Label();
			txtMiddleName = new TextBox();
			pbMiddleNameValidation = new PictureBox();
			label4 = new Label();
			cbPosition = new ComboBox();
			pbPositionValidation = new PictureBox();
			button1 = new Button();
			label5 = new Label();
			pbSeriesValidation = new PictureBox();
			txtSeries = new MaskedTextBox();
			label6 = new Label();
			pbNumberValidation = new PictureBox();
			txtNumber = new MaskedTextBox();
			label7 = new Label();
			txtAddress = new TextBox();
			pbAddressValidation = new PictureBox();
			label8 = new Label();
			txtPhone = new MaskedTextBox();
			pbPhoneValidation = new PictureBox();
			btnSave = new Button();
			btnClose = new Button();
			btnGenerate = new Button();
			((System.ComponentModel.ISupportInitialize)pbLastNameValidation).BeginInit();
			((System.ComponentModel.ISupportInitialize)pbFirstNameValidation).BeginInit();
			((System.ComponentModel.ISupportInitialize)pbMiddleNameValidation).BeginInit();
			((System.ComponentModel.ISupportInitialize)pbPositionValidation).BeginInit();
			((System.ComponentModel.ISupportInitialize)pbSeriesValidation).BeginInit();
			((System.ComponentModel.ISupportInitialize)pbNumberValidation).BeginInit();
			((System.ComponentModel.ISupportInitialize)pbAddressValidation).BeginInit();
			((System.ComponentModel.ISupportInitialize)pbPhoneValidation).BeginInit();
			SuspendLayout();
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Location = new Point(12, 9);
			label1.Name = "label1";
			label1.Size = new Size(73, 20);
			label1.TabIndex = 0;
			label1.Text = "Фамилия";
			// 
			// txtLastName
			// 
			txtLastName.BorderStyle = BorderStyle.FixedSingle;
			txtLastName.Location = new Point(12, 32);
			txtLastName.Name = "txtLastName";
			txtLastName.Size = new Size(337, 27);
			txtLastName.TabIndex = 1;
			txtLastName.TextChanged += TxtLastName_TextChanged;
			// 
			// pbLastNameValidation
			// 
			pbLastNameValidation.Location = new Point(358, 32);
			pbLastNameValidation.Name = "pbLastNameValidation";
			pbLastNameValidation.Size = new Size(27, 27);
			pbLastNameValidation.TabIndex = 2;
			pbLastNameValidation.TabStop = false;
			// 
			// label2
			// 
			label2.AutoSize = true;
			label2.Location = new Point(12, 62);
			label2.Name = "label2";
			label2.Size = new Size(39, 20);
			label2.TabIndex = 0;
			label2.Text = "Имя";
			// 
			// txtFirstName
			// 
			txtFirstName.BorderStyle = BorderStyle.FixedSingle;
			txtFirstName.Location = new Point(12, 86);
			txtFirstName.Name = "txtFirstName";
			txtFirstName.Size = new Size(337, 27);
			txtFirstName.TabIndex = 1;
			txtFirstName.TextChanged += TxtFirstName_TextChanged;
			// 
			// pbFirstNameValidation
			// 
			pbFirstNameValidation.Location = new Point(358, 86);
			pbFirstNameValidation.Name = "pbFirstNameValidation";
			pbFirstNameValidation.Size = new Size(27, 27);
			pbFirstNameValidation.TabIndex = 2;
			pbFirstNameValidation.TabStop = false;
			// 
			// label3
			// 
			label3.AutoSize = true;
			label3.Location = new Point(12, 117);
			label3.Name = "label3";
			label3.Size = new Size(72, 20);
			label3.TabIndex = 0;
			label3.Text = "Отчество";
			// 
			// txtMiddleName
			// 
			txtMiddleName.BorderStyle = BorderStyle.FixedSingle;
			txtMiddleName.Location = new Point(12, 140);
			txtMiddleName.Name = "txtMiddleName";
			txtMiddleName.Size = new Size(337, 27);
			txtMiddleName.TabIndex = 1;
			txtMiddleName.TextChanged += TxtMiddleName_TextChanged;
			// 
			// pbMiddleNameValidation
			// 
			pbMiddleNameValidation.Location = new Point(358, 140);
			pbMiddleNameValidation.Name = "pbMiddleNameValidation";
			pbMiddleNameValidation.Size = new Size(27, 27);
			pbMiddleNameValidation.TabIndex = 2;
			pbMiddleNameValidation.TabStop = false;
			// 
			// label4
			// 
			label4.AutoSize = true;
			label4.Location = new Point(12, 368);
			label4.Name = "label4";
			label4.Size = new Size(86, 20);
			label4.TabIndex = 0;
			label4.Text = "Должность";
			// 
			// cbPosition
			// 
			cbPosition.FormattingEnabled = true;
			cbPosition.Location = new Point(12, 391);
			cbPosition.Name = "cbPosition";
			cbPosition.Size = new Size(296, 28);
			cbPosition.TabIndex = 3;
			cbPosition.SelectedIndexChanged += CbPosition_SelectedIndexChanged;
			// 
			// pbPositionValidation
			// 
			pbPositionValidation.Location = new Point(358, 393);
			pbPositionValidation.Name = "pbPositionValidation";
			pbPositionValidation.Size = new Size(27, 27);
			pbPositionValidation.TabIndex = 2;
			pbPositionValidation.TabStop = false;
			// 
			// button1
			// 
			button1.FlatStyle = FlatStyle.Popup;
			button1.Location = new Point(314, 391);
			button1.Name = "button1";
			button1.Size = new Size(35, 29);
			button1.TabIndex = 4;
			button1.Text = "...";
			button1.UseVisualStyleBackColor = true;
			// 
			// label5
			// 
			label5.AutoSize = true;
			label5.Location = new Point(12, 171);
			label5.Name = "label5";
			label5.Size = new Size(121, 20);
			label5.TabIndex = 0;
			label5.Text = "Серия паспорта";
			// 
			// pbSeriesValidation
			// 
			pbSeriesValidation.Location = new Point(164, 194);
			pbSeriesValidation.Name = "pbSeriesValidation";
			pbSeriesValidation.Size = new Size(27, 27);
			pbSeriesValidation.TabIndex = 2;
			pbSeriesValidation.TabStop = false;
			// 
			// txtSeries
			// 
			txtSeries.BorderStyle = BorderStyle.FixedSingle;
			txtSeries.Location = new Point(12, 194);
			txtSeries.Mask = "0000";
			txtSeries.Name = "txtSeries";
			txtSeries.Size = new Size(146, 27);
			txtSeries.TabIndex = 5;
			txtSeries.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
			txtSeries.ValidatingType = typeof(int);
			txtSeries.MaskInputRejected += TxtSeries_MaskInputRejected;
			txtSeries.TextChanged += TxtSeries_TextChanged;
			// 
			// label6
			// 
			label6.AutoSize = true;
			label6.Location = new Point(223, 171);
			label6.Name = "label6";
			label6.Size = new Size(126, 20);
			label6.TabIndex = 0;
			label6.Text = "Номер паспорта";
			// 
			// pbNumberValidation
			// 
			pbNumberValidation.Location = new Point(358, 194);
			pbNumberValidation.Name = "pbNumberValidation";
			pbNumberValidation.Size = new Size(27, 27);
			pbNumberValidation.TabIndex = 2;
			pbNumberValidation.TabStop = false;
			// 
			// txtNumber
			// 
			txtNumber.BorderStyle = BorderStyle.FixedSingle;
			txtNumber.Location = new Point(206, 194);
			txtNumber.Mask = "000000";
			txtNumber.Name = "txtNumber";
			txtNumber.Size = new Size(146, 27);
			txtNumber.TabIndex = 5;
			txtNumber.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
			txtNumber.ValidatingType = typeof(int);
			txtNumber.MaskInputRejected += TxtNumber_MaskInputRejected;
			txtNumber.TextChanged += TxtNumber_TextChanged;
			// 
			// label7
			// 
			label7.AutoSize = true;
			label7.Location = new Point(12, 224);
			label7.Name = "label7";
			label7.Size = new Size(144, 20);
			label7.TabIndex = 0;
			label7.Text = "Адрес по прописке";
			// 
			// txtAddress
			// 
			txtAddress.BorderStyle = BorderStyle.FixedSingle;
			txtAddress.Location = new Point(12, 248);
			txtAddress.Multiline = true;
			txtAddress.Name = "txtAddress";
			txtAddress.Size = new Size(337, 62);
			txtAddress.TabIndex = 7;
			txtAddress.TextChanged += TxtAddress_TextChanged;
			// 
			// pbAddressValidation
			// 
			pbAddressValidation.Location = new Point(358, 248);
			pbAddressValidation.Name = "pbAddressValidation";
			pbAddressValidation.Size = new Size(27, 27);
			pbAddressValidation.TabIndex = 2;
			pbAddressValidation.TabStop = false;
			// 
			// label8
			// 
			label8.AutoSize = true;
			label8.Location = new Point(12, 313);
			label8.Name = "label8";
			label8.Size = new Size(127, 20);
			label8.TabIndex = 0;
			label8.Text = "Номер телефона";
			// 
			// txtPhone
			// 
			txtPhone.BorderStyle = BorderStyle.FixedSingle;
			txtPhone.Location = new Point(12, 337);
			txtPhone.Mask = "+7(999) 000-0000";
			txtPhone.Name = "txtPhone";
			txtPhone.Size = new Size(337, 27);
			txtPhone.TabIndex = 8;
			txtPhone.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
			txtPhone.MaskInputRejected += TxtPhone_MaskInputRejected;
			txtPhone.TextChanged += TxtPhone_TextChanged;
			// 
			// pbPhoneValidation
			// 
			pbPhoneValidation.Location = new Point(358, 337);
			pbPhoneValidation.Name = "pbPhoneValidation";
			pbPhoneValidation.Size = new Size(27, 27);
			pbPhoneValidation.TabIndex = 2;
			pbPhoneValidation.TabStop = false;
			// 
			// btnSave
			// 
			btnSave.FlatStyle = FlatStyle.Popup;
			btnSave.Location = new Point(140, 436);
			btnSave.Name = "btnSave";
			btnSave.Size = new Size(115, 29);
			btnSave.TabIndex = 9;
			btnSave.Text = "Сохранить";
			btnSave.UseVisualStyleBackColor = true;
			btnSave.Click += BtnSave_Click;
			// 
			// btnClose
			// 
			btnClose.FlatStyle = FlatStyle.Popup;
			btnClose.Location = new Point(261, 436);
			btnClose.Name = "btnClose";
			btnClose.Size = new Size(127, 29);
			btnClose.TabIndex = 9;
			btnClose.Text = "Закрыть";
			btnClose.UseVisualStyleBackColor = true;
			btnClose.Click += BtnClose_Click;
			// 
			// btnGenerate
			// 
			btnGenerate.FlatStyle = FlatStyle.Popup;
			btnGenerate.Location = new Point(12, 436);
			btnGenerate.Name = "btnGenerate";
			btnGenerate.Size = new Size(122, 29);
			btnGenerate.TabIndex = 9;
			btnGenerate.Text = "Генерировать";
			btnGenerate.UseVisualStyleBackColor = true;
			btnGenerate.Click += BtnGenerate_Click;
			// 
			// EmployeeForm
			// 
			AutoScaleDimensions = new SizeF(8F, 20F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(400, 480);
			Controls.Add(btnClose);
			Controls.Add(btnGenerate);
			Controls.Add(btnSave);
			Controls.Add(txtPhone);
			Controls.Add(txtAddress);
			Controls.Add(txtNumber);
			Controls.Add(txtSeries);
			Controls.Add(button1);
			Controls.Add(cbPosition);
			Controls.Add(pbPhoneValidation);
			Controls.Add(pbAddressValidation);
			Controls.Add(pbNumberValidation);
			Controls.Add(pbPositionValidation);
			Controls.Add(pbSeriesValidation);
			Controls.Add(pbMiddleNameValidation);
			Controls.Add(txtMiddleName);
			Controls.Add(pbFirstNameValidation);
			Controls.Add(label6);
			Controls.Add(txtFirstName);
			Controls.Add(label8);
			Controls.Add(label7);
			Controls.Add(label5);
			Controls.Add(label4);
			Controls.Add(label3);
			Controls.Add(pbLastNameValidation);
			Controls.Add(label2);
			Controls.Add(txtLastName);
			Controls.Add(label1);
			FormBorderStyle = FormBorderStyle.FixedSingle;
			MaximizeBox = false;
			MinimizeBox = false;
			Name = "EmployeeForm";
			StartPosition = FormStartPosition.CenterScreen;
			Text = "Добавление нового сотрудника";
			FormClosed += EmployeeForm_FormClosed;
			((System.ComponentModel.ISupportInitialize)pbLastNameValidation).EndInit();
			((System.ComponentModel.ISupportInitialize)pbFirstNameValidation).EndInit();
			((System.ComponentModel.ISupportInitialize)pbMiddleNameValidation).EndInit();
			((System.ComponentModel.ISupportInitialize)pbPositionValidation).EndInit();
			((System.ComponentModel.ISupportInitialize)pbSeriesValidation).EndInit();
			((System.ComponentModel.ISupportInitialize)pbNumberValidation).EndInit();
			((System.ComponentModel.ISupportInitialize)pbAddressValidation).EndInit();
			((System.ComponentModel.ISupportInitialize)pbPhoneValidation).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private Label label1;
		private TextBox txtLastName;
		private PictureBox pbLastNameValidation;
		private Label label2;
		private TextBox txtFirstName;
		private PictureBox pbFirstNameValidation;
		private Label label3;
		private TextBox txtMiddleName;
		private PictureBox pbMiddleNameValidation;
		private Label label4;
		private ComboBox cbPosition;
		private PictureBox pbPositionValidation;
		private Button button1;
		private Label label5;
		private PictureBox pbSeriesValidation;
		private MaskedTextBox txtSeries;
		private Label label6;
		private PictureBox pbNumberValidation;
		private MaskedTextBox txtNumber;
		private Label label7;
		private TextBox txtAddress;
		private PictureBox pbAddressValidation;
		private Label label8;
		private MaskedTextBox txtPhone;
		private PictureBox pbPhoneValidation;
		private Button btnSave;
		private Button btnClose;
		private Button btnGenerate;
	}
}