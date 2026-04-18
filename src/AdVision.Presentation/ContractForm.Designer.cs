namespace AdVision.Presentation
{
	partial class ContractForm
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
			btnSave = new Button();
			label17 = new Label();
			dtpSignedDate = new DateTimePicker();
			label16 = new Label();
			dtpEndDate = new DateTimePicker();
			label15 = new Label();
			dtpStartDate = new DateTimePicker();
			label14 = new Label();
			cbCustomers = new ComboBox();
			label13 = new Label();
			cbStatuses = new ComboBox();
			cbEmployees = new ComboBox();
			label12 = new Label();
			txtContractNumber = new TextBox();
			label11 = new Label();
			btnGenerate = new Button();
			pbContractNumberValidation = new PictureBox();
			pbEmployeeValidation = new PictureBox();
			pbCustomerValidation = new PictureBox();
			pbStartDateValidation = new PictureBox();
			pbEndDateValidation = new PictureBox();
			pbSignedDateValidation = new PictureBox();
			pbStatusValidation = new PictureBox();
			((System.ComponentModel.ISupportInitialize)pbContractNumberValidation).BeginInit();
			((System.ComponentModel.ISupportInitialize)pbEmployeeValidation).BeginInit();
			((System.ComponentModel.ISupportInitialize)pbCustomerValidation).BeginInit();
			((System.ComponentModel.ISupportInitialize)pbStartDateValidation).BeginInit();
			((System.ComponentModel.ISupportInitialize)pbEndDateValidation).BeginInit();
			((System.ComponentModel.ISupportInitialize)pbSignedDateValidation).BeginInit();
			((System.ComponentModel.ISupportInitialize)pbStatusValidation).BeginInit();
			SuspendLayout();
			// 
			// btnClose
			// 
			btnClose.FlatStyle = FlatStyle.Popup;
			btnClose.Location = new Point(316, 407);
			btnClose.Name = "btnClose";
			btnClose.Size = new Size(141, 29);
			btnClose.TabIndex = 25;
			btnClose.Text = "Закрыть";
			btnClose.UseVisualStyleBackColor = true;
			btnClose.Click += BtnClose_Click;
			// 
			// btnSave
			// 
			btnSave.FlatStyle = FlatStyle.Popup;
			btnSave.Location = new Point(164, 407);
			btnSave.Name = "btnSave";
			btnSave.Size = new Size(146, 29);
			btnSave.TabIndex = 26;
			btnSave.Text = "Сохранить";
			btnSave.UseVisualStyleBackColor = true;
			btnSave.Click += BtnSave_Click;
			// 
			// label17
			// 
			label17.AutoSize = true;
			label17.Location = new Point(12, 340);
			label17.Name = "label17";
			label17.Size = new Size(52, 20);
			label17.TabIndex = 24;
			label17.Text = "Статус";
			// 
			// dtpSignedDate
			// 
			dtpSignedDate.Location = new Point(12, 309);
			dtpSignedDate.Name = "dtpSignedDate";
			dtpSignedDate.Size = new Size(412, 27);
			dtpSignedDate.TabIndex = 21;
			// 
			// label16
			// 
			label16.AutoSize = true;
			label16.Location = new Point(12, 286);
			label16.Name = "label16";
			label16.Size = new Size(130, 20);
			label16.TabIndex = 18;
			label16.Text = "Дата подписания";
			// 
			// dtpEndDate
			// 
			dtpEndDate.Location = new Point(12, 255);
			dtpEndDate.Name = "dtpEndDate";
			dtpEndDate.Size = new Size(412, 27);
			dtpEndDate.TabIndex = 22;
			// 
			// label15
			// 
			label15.AutoSize = true;
			label15.Location = new Point(12, 232);
			label15.Name = "label15";
			label15.Size = new Size(121, 20);
			label15.TabIndex = 19;
			label15.Text = "Дата окончания";
			// 
			// dtpStartDate
			// 
			dtpStartDate.Location = new Point(12, 201);
			dtpStartDate.Name = "dtpStartDate";
			dtpStartDate.Size = new Size(412, 27);
			dtpStartDate.TabIndex = 23;
			// 
			// label14
			// 
			label14.AutoSize = true;
			label14.Location = new Point(12, 177);
			label14.Name = "label14";
			label14.Size = new Size(94, 20);
			label14.TabIndex = 20;
			label14.Text = "Дата начала";
			// 
			// cbCustomers
			// 
			cbCustomers.FormattingEnabled = true;
			cbCustomers.Location = new Point(12, 146);
			cbCustomers.Name = "cbCustomers";
			cbCustomers.Size = new Size(412, 28);
			cbCustomers.TabIndex = 15;
			// 
			// label13
			// 
			label13.AutoSize = true;
			label13.Location = new Point(12, 123);
			label13.Name = "label13";
			label13.Size = new Size(71, 20);
			label13.TabIndex = 13;
			label13.Text = "Заказчик";
			// 
			// cbStatuses
			// 
			cbStatuses.FormattingEnabled = true;
			cbStatuses.Items.AddRange(new object[] { "Все", "Проект (Черновик)", "Активный", "Подписан", "Завершен", "Отменен" });
			cbStatuses.Location = new Point(12, 363);
			cbStatuses.Name = "cbStatuses";
			cbStatuses.Size = new Size(412, 28);
			cbStatuses.TabIndex = 16;
			// 
			// cbEmployees
			// 
			cbEmployees.FormattingEnabled = true;
			cbEmployees.Location = new Point(12, 91);
			cbEmployees.Name = "cbEmployees";
			cbEmployees.Size = new Size(412, 28);
			cbEmployees.TabIndex = 17;
			// 
			// label12
			// 
			label12.AutoSize = true;
			label12.Location = new Point(12, 68);
			label12.Name = "label12";
			label12.Size = new Size(101, 20);
			label12.TabIndex = 14;
			label12.Text = "Исполнитель";
			// 
			// txtContractNumber
			// 
			txtContractNumber.BorderStyle = BorderStyle.FixedSingle;
			txtContractNumber.Location = new Point(12, 37);
			txtContractNumber.Name = "txtContractNumber";
			txtContractNumber.Size = new Size(412, 27);
			txtContractNumber.TabIndex = 12;
			// 
			// label11
			// 
			label11.AutoSize = true;
			label11.Location = new Point(12, 14);
			label11.Name = "label11";
			label11.Size = new Size(127, 20);
			label11.TabIndex = 11;
			label11.Text = "Номер договора";
			// 
			// btnGenerate
			// 
			btnGenerate.FlatStyle = FlatStyle.Popup;
			btnGenerate.Location = new Point(12, 407);
			btnGenerate.Name = "btnGenerate";
			btnGenerate.Size = new Size(146, 29);
			btnGenerate.TabIndex = 26;
			btnGenerate.Text = "Сгенерировать";
			btnGenerate.UseVisualStyleBackColor = true;
			btnGenerate.Click += BtnGenerate_Click;
			// 
			// pbContractNumberValidation
			// 
			pbContractNumberValidation.Location = new Point(430, 37);
			pbContractNumberValidation.Name = "pbContractNumberValidation";
			pbContractNumberValidation.Size = new Size(27, 27);
			pbContractNumberValidation.TabIndex = 27;
			pbContractNumberValidation.TabStop = false;
			// 
			// pbEmployeeValidation
			// 
			pbEmployeeValidation.Location = new Point(430, 92);
			pbEmployeeValidation.Name = "pbEmployeeValidation";
			pbEmployeeValidation.Size = new Size(27, 27);
			pbEmployeeValidation.TabIndex = 27;
			pbEmployeeValidation.TabStop = false;
			// 
			// pbCustomerValidation
			// 
			pbCustomerValidation.Location = new Point(430, 147);
			pbCustomerValidation.Name = "pbCustomerValidation";
			pbCustomerValidation.Size = new Size(27, 27);
			pbCustomerValidation.TabIndex = 27;
			pbCustomerValidation.TabStop = false;
			// 
			// pbStartDateValidation
			// 
			pbStartDateValidation.Location = new Point(430, 201);
			pbStartDateValidation.Name = "pbStartDateValidation";
			pbStartDateValidation.Size = new Size(27, 27);
			pbStartDateValidation.TabIndex = 27;
			pbStartDateValidation.TabStop = false;
			// 
			// pbEndDateValidation
			// 
			pbEndDateValidation.Location = new Point(430, 255);
			pbEndDateValidation.Name = "pbEndDateValidation";
			pbEndDateValidation.Size = new Size(27, 27);
			pbEndDateValidation.TabIndex = 27;
			pbEndDateValidation.TabStop = false;
			// 
			// pbSignedDateValidation
			// 
			pbSignedDateValidation.Location = new Point(430, 309);
			pbSignedDateValidation.Name = "pbSignedDateValidation";
			pbSignedDateValidation.Size = new Size(27, 27);
			pbSignedDateValidation.TabIndex = 27;
			pbSignedDateValidation.TabStop = false;
			// 
			// pbStatusValidation
			// 
			pbStatusValidation.Location = new Point(430, 364);
			pbStatusValidation.Name = "pbStatusValidation";
			pbStatusValidation.Size = new Size(27, 27);
			pbStatusValidation.TabIndex = 27;
			pbStatusValidation.TabStop = false;
			// 
			// ContractForm
			// 
			AutoScaleDimensions = new SizeF(8F, 20F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(477, 455);
			Controls.Add(pbStatusValidation);
			Controls.Add(pbSignedDateValidation);
			Controls.Add(pbEndDateValidation);
			Controls.Add(pbStartDateValidation);
			Controls.Add(pbCustomerValidation);
			Controls.Add(pbEmployeeValidation);
			Controls.Add(pbContractNumberValidation);
			Controls.Add(btnClose);
			Controls.Add(btnGenerate);
			Controls.Add(btnSave);
			Controls.Add(label17);
			Controls.Add(dtpSignedDate);
			Controls.Add(label16);
			Controls.Add(dtpEndDate);
			Controls.Add(label15);
			Controls.Add(dtpStartDate);
			Controls.Add(label14);
			Controls.Add(cbCustomers);
			Controls.Add(label13);
			Controls.Add(cbStatuses);
			Controls.Add(cbEmployees);
			Controls.Add(label12);
			Controls.Add(txtContractNumber);
			Controls.Add(label11);
			FormBorderStyle = FormBorderStyle.FixedSingle;
			MaximizeBox = false;
			MinimizeBox = false;
			Name = "ContractForm";
			StartPosition = FormStartPosition.CenterScreen;
			Text = "Создание контракта";
			((System.ComponentModel.ISupportInitialize)pbContractNumberValidation).EndInit();
			((System.ComponentModel.ISupportInitialize)pbEmployeeValidation).EndInit();
			((System.ComponentModel.ISupportInitialize)pbCustomerValidation).EndInit();
			((System.ComponentModel.ISupportInitialize)pbStartDateValidation).EndInit();
			((System.ComponentModel.ISupportInitialize)pbEndDateValidation).EndInit();
			((System.ComponentModel.ISupportInitialize)pbSignedDateValidation).EndInit();
			((System.ComponentModel.ISupportInitialize)pbStatusValidation).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private Button btnClose;
		private Button btnSave;
		private Label label17;
		private DateTimePicker dtpSignedDate;
		private Label label16;
		private DateTimePicker dtpEndDate;
		private Label label15;
		private DateTimePicker dtpStartDate;
		private Label label14;
		private ComboBox cbCustomers;
		private Label label13;
		private ComboBox cbStatuses;
		private ComboBox cbEmployees;
		private Label label12;
		private TextBox txtContractNumber;
		private Label label11;
		private Button btnGenerate;
		private PictureBox pbContractNumberValidation;
		private PictureBox pbEmployeeValidation;
		private PictureBox pbCustomerValidation;
		private PictureBox pbStartDateValidation;
		private PictureBox pbEndDateValidation;
		private PictureBox pbSignedDateValidation;
		private PictureBox pbStatusValidation;
	}
}