namespace AdVision.Presentation
{
	partial class CustomerForm
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
			btnClose = new Button();
			btnGenerate = new Button();
			btnSave = new Button();
			txtPhone = new MaskedTextBox();
			pbPhoneValidation = new PictureBox();
			pbMiddleNameValidation = new PictureBox();
			txtMiddleName = new TextBox();
			pbFirstNameValidation = new PictureBox();
			txtFirstName = new TextBox();
			label8 = new Label();
			label3 = new Label();
			pbLastNameValidation = new PictureBox();
			label2 = new Label();
			txtLastName = new TextBox();
			label1 = new Label();
			((System.ComponentModel.ISupportInitialize)pbPhoneValidation).BeginInit();
			((System.ComponentModel.ISupportInitialize)pbMiddleNameValidation).BeginInit();
			((System.ComponentModel.ISupportInitialize)pbFirstNameValidation).BeginInit();
			((System.ComponentModel.ISupportInitialize)pbLastNameValidation).BeginInit();
			SuspendLayout();
			// 
			// btnClose
			// 
			btnClose.FlatStyle = FlatStyle.Popup;
			btnClose.Location = new Point(260, 239);
			btnClose.Name = "btnClose";
			btnClose.Size = new Size(127, 29);
			btnClose.TabIndex = 22;
			btnClose.Text = "Закрыть";
			btnClose.UseVisualStyleBackColor = true;
			btnClose.Click += BtnClose_Click;
			// 
			// btnGenerate
			// 
			btnGenerate.FlatStyle = FlatStyle.Popup;
			btnGenerate.Location = new Point(11, 239);
			btnGenerate.Name = "btnGenerate";
			btnGenerate.Size = new Size(122, 29);
			btnGenerate.TabIndex = 23;
			btnGenerate.Text = "Генерировать";
			btnGenerate.UseVisualStyleBackColor = true;
			btnGenerate.Click += BtnGenerate_Click;
			// 
			// btnSave
			// 
			btnSave.FlatStyle = FlatStyle.Popup;
			btnSave.Location = new Point(139, 239);
			btnSave.Name = "btnSave";
			btnSave.Size = new Size(115, 29);
			btnSave.TabIndex = 24;
			btnSave.Text = "Сохранить";
			btnSave.UseVisualStyleBackColor = true;
			btnSave.Click += BtnSave_Click;
			// 
			// txtPhone
			// 
			txtPhone.BorderStyle = BorderStyle.FixedSingle;
			txtPhone.Location = new Point(12, 196);
			txtPhone.Mask = "+7(999) 000-0000";
			txtPhone.Name = "txtPhone";
			txtPhone.Size = new Size(337, 27);
			txtPhone.TabIndex = 21;
			txtPhone.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
			// 
			// pbPhoneValidation
			// 
			pbPhoneValidation.Location = new Point(358, 196);
			pbPhoneValidation.Name = "pbPhoneValidation";
			pbPhoneValidation.Size = new Size(27, 27);
			pbPhoneValidation.TabIndex = 17;
			pbPhoneValidation.TabStop = false;
			// 
			// pbMiddleNameValidation
			// 
			pbMiddleNameValidation.Location = new Point(358, 142);
			pbMiddleNameValidation.Name = "pbMiddleNameValidation";
			pbMiddleNameValidation.Size = new Size(27, 27);
			pbMiddleNameValidation.TabIndex = 18;
			pbMiddleNameValidation.TabStop = false;
			// 
			// txtMiddleName
			// 
			txtMiddleName.BorderStyle = BorderStyle.FixedSingle;
			txtMiddleName.Location = new Point(12, 142);
			txtMiddleName.Name = "txtMiddleName";
			txtMiddleName.Size = new Size(337, 27);
			txtMiddleName.TabIndex = 14;
			// 
			// pbFirstNameValidation
			// 
			pbFirstNameValidation.Location = new Point(358, 88);
			pbFirstNameValidation.Name = "pbFirstNameValidation";
			pbFirstNameValidation.Size = new Size(27, 27);
			pbFirstNameValidation.TabIndex = 19;
			pbFirstNameValidation.TabStop = false;
			// 
			// txtFirstName
			// 
			txtFirstName.BorderStyle = BorderStyle.FixedSingle;
			txtFirstName.Location = new Point(12, 88);
			txtFirstName.Name = "txtFirstName";
			txtFirstName.Size = new Size(337, 27);
			txtFirstName.TabIndex = 15;
			// 
			// label8
			// 
			label8.AutoSize = true;
			label8.Location = new Point(12, 172);
			label8.Name = "label8";
			label8.Size = new Size(127, 20);
			label8.TabIndex = 10;
			label8.Text = "Номер телефона";
			// 
			// label3
			// 
			label3.AutoSize = true;
			label3.Location = new Point(12, 119);
			label3.Name = "label3";
			label3.Size = new Size(72, 20);
			label3.TabIndex = 11;
			label3.Text = "Отчество";
			// 
			// pbLastNameValidation
			// 
			pbLastNameValidation.Location = new Point(358, 34);
			pbLastNameValidation.Name = "pbLastNameValidation";
			pbLastNameValidation.Size = new Size(27, 27);
			pbLastNameValidation.TabIndex = 20;
			pbLastNameValidation.TabStop = false;
			// 
			// label2
			// 
			label2.AutoSize = true;
			label2.Location = new Point(12, 64);
			label2.Name = "label2";
			label2.Size = new Size(39, 20);
			label2.TabIndex = 12;
			label2.Text = "Имя";
			// 
			// txtLastName
			// 
			txtLastName.BorderStyle = BorderStyle.FixedSingle;
			txtLastName.Location = new Point(12, 34);
			txtLastName.Name = "txtLastName";
			txtLastName.Size = new Size(337, 27);
			txtLastName.TabIndex = 16;
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Location = new Point(12, 11);
			label1.Name = "label1";
			label1.Size = new Size(73, 20);
			label1.TabIndex = 13;
			label1.Text = "Фамилия";
			// 
			// CustomerForm
			// 
			AutoScaleDimensions = new SizeF(8F, 20F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(401, 288);
			Controls.Add(btnClose);
			Controls.Add(btnGenerate);
			Controls.Add(btnSave);
			Controls.Add(txtPhone);
			Controls.Add(pbPhoneValidation);
			Controls.Add(pbMiddleNameValidation);
			Controls.Add(txtMiddleName);
			Controls.Add(pbFirstNameValidation);
			Controls.Add(txtFirstName);
			Controls.Add(label8);
			Controls.Add(label3);
			Controls.Add(pbLastNameValidation);
			Controls.Add(label2);
			Controls.Add(txtLastName);
			Controls.Add(label1);
			FormBorderStyle = FormBorderStyle.FixedSingle;
			MaximizeBox = false;
			MinimizeBox = false;
			Name = "CustomerForm";
			StartPosition = FormStartPosition.CenterScreen;
			Text = "Создание нового заказчика";
			((System.ComponentModel.ISupportInitialize)pbPhoneValidation).EndInit();
			((System.ComponentModel.ISupportInitialize)pbMiddleNameValidation).EndInit();
			((System.ComponentModel.ISupportInitialize)pbFirstNameValidation).EndInit();
			((System.ComponentModel.ISupportInitialize)pbLastNameValidation).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private Button btnClose;
		private Button btnGenerate;
		private Button btnSave;
		private MaskedTextBox txtPhone;
		private PictureBox pbPhoneValidation;
		private PictureBox pbMiddleNameValidation;
		private TextBox txtMiddleName;
		private PictureBox pbFirstNameValidation;
		private TextBox txtFirstName;
		private Label label8;
		private Label label3;
		private PictureBox pbLastNameValidation;
		private Label label2;
		private TextBox txtLastName;
		private Label label1;
	}
}