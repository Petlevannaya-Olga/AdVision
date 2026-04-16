namespace AdVision.Presentation
{
	partial class EmployeesFilterUserControl
	{
		/// <summary> 
		/// Обязательная переменная конструктора.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Освободить все используемые ресурсы.
		/// </summary>
		/// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Код, автоматически созданный конструктором компонентов

		/// <summary> 
		/// Требуемый метод для поддержки конструктора — не изменяйте 
		/// содержимое этого метода с помощью редактора кода.
		/// </summary>
		private void InitializeComponent()
		{
			txtPhone = new MaskedTextBox();
			cbPosition = new ComboBox();
			txtMiddleName = new TextBox();
			txtFirstName = new TextBox();
			label3 = new Label();
			label2 = new Label();
			txtLastName = new TextBox();
			label1 = new Label();
			label8 = new Label();
			label4 = new Label();
			btnReset = new Button();
			btnApply = new Button();
			panel1 = new Panel();
			label12 = new Label();
			panel1.SuspendLayout();
			SuspendLayout();
			// 
			// txtPhone
			// 
			txtPhone.BorderStyle = BorderStyle.FixedSingle;
			txtPhone.Location = new Point(377, 66);
			txtPhone.Mask = "+7(999) 000-0000";
			txtPhone.Name = "txtPhone";
			txtPhone.Size = new Size(337, 27);
			txtPhone.TabIndex = 16;
			txtPhone.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
			txtPhone.TextChanged += TxtPhone_TextChanged;
			// 
			// cbPosition
			// 
			cbPosition.FormattingEnabled = true;
			cbPosition.Location = new Point(377, 120);
			cbPosition.Name = "cbPosition";
			cbPosition.Size = new Size(337, 28);
			cbPosition.TabIndex = 15;
			// 
			// txtMiddleName
			// 
			txtMiddleName.BorderStyle = BorderStyle.FixedSingle;
			txtMiddleName.Location = new Point(14, 174);
			txtMiddleName.Name = "txtMiddleName";
			txtMiddleName.Size = new Size(337, 27);
			txtMiddleName.TabIndex = 12;
			txtMiddleName.TextChanged += TxtMiddleName_TextChanged;
			// 
			// txtFirstName
			// 
			txtFirstName.BorderStyle = BorderStyle.FixedSingle;
			txtFirstName.Location = new Point(14, 120);
			txtFirstName.Name = "txtFirstName";
			txtFirstName.Size = new Size(337, 27);
			txtFirstName.TabIndex = 13;
			txtFirstName.TextChanged += TxtFirstName_TextChanged;
			// 
			// label3
			// 
			label3.AutoSize = true;
			label3.Location = new Point(14, 151);
			label3.Name = "label3";
			label3.Size = new Size(72, 20);
			label3.TabIndex = 9;
			label3.Text = "Отчество";
			// 
			// label2
			// 
			label2.AutoSize = true;
			label2.Location = new Point(14, 96);
			label2.Name = "label2";
			label2.Size = new Size(39, 20);
			label2.TabIndex = 10;
			label2.Text = "Имя";
			// 
			// txtLastName
			// 
			txtLastName.BorderStyle = BorderStyle.FixedSingle;
			txtLastName.Location = new Point(14, 66);
			txtLastName.Name = "txtLastName";
			txtLastName.Size = new Size(337, 27);
			txtLastName.TabIndex = 14;
			txtLastName.TextChanged += TxtLastName_TextChanged;
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Location = new Point(14, 43);
			label1.Name = "label1";
			label1.Size = new Size(73, 20);
			label1.TabIndex = 11;
			label1.Text = "Фамилия";
			// 
			// label8
			// 
			label8.AutoSize = true;
			label8.Location = new Point(377, 43);
			label8.Name = "label8";
			label8.Size = new Size(127, 20);
			label8.TabIndex = 17;
			label8.Text = "Номер телефона";
			// 
			// label4
			// 
			label4.AutoSize = true;
			label4.Location = new Point(377, 96);
			label4.Name = "label4";
			label4.Size = new Size(86, 20);
			label4.TabIndex = 18;
			label4.Text = "Должность";
			// 
			// btnReset
			// 
			btnReset.FlatStyle = FlatStyle.Popup;
			btnReset.Location = new Point(377, 172);
			btnReset.Name = "btnReset";
			btnReset.Size = new Size(162, 29);
			btnReset.TabIndex = 19;
			btnReset.Text = "Сбросить";
			btnReset.UseVisualStyleBackColor = true;
			btnReset.Click += BtnReset_Click;
			// 
			// btnApply
			// 
			btnApply.FlatStyle = FlatStyle.Popup;
			btnApply.Location = new Point(545, 172);
			btnApply.Name = "btnApply";
			btnApply.Size = new Size(169, 29);
			btnApply.TabIndex = 19;
			btnApply.Text = "Применить";
			btnApply.UseVisualStyleBackColor = true;
			btnApply.Click += BtnApply_Click;
			// 
			// panel1
			// 
			panel1.BackColor = SystemColors.Window;
			panel1.BorderStyle = BorderStyle.FixedSingle;
			panel1.Controls.Add(label12);
			panel1.Controls.Add(btnApply);
			panel1.Controls.Add(txtPhone);
			panel1.Controls.Add(btnReset);
			panel1.Controls.Add(label1);
			panel1.Controls.Add(label8);
			panel1.Controls.Add(txtLastName);
			panel1.Controls.Add(label4);
			panel1.Controls.Add(label2);
			panel1.Controls.Add(label3);
			panel1.Controls.Add(cbPosition);
			panel1.Controls.Add(txtFirstName);
			panel1.Controls.Add(txtMiddleName);
			panel1.Dock = DockStyle.Fill;
			panel1.Location = new Point(0, 0);
			panel1.Name = "panel1";
			panel1.Size = new Size(854, 224);
			panel1.TabIndex = 20;
			// 
			// label12
			// 
			label12.AutoSize = true;
			label12.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 204);
			label12.Location = new Point(14, 13);
			label12.Name = "label12";
			label12.Size = new Size(64, 20);
			label12.TabIndex = 3;
			label12.Text = "Фильтр";
			// 
			// EmployeesFilterUserControl
			// 
			AutoScaleDimensions = new SizeF(8F, 20F);
			AutoScaleMode = AutoScaleMode.Font;
			Controls.Add(panel1);
			Name = "EmployeesFilterUserControl";
			Size = new Size(854, 224);
			panel1.ResumeLayout(false);
			panel1.PerformLayout();
			ResumeLayout(false);
		}

		#endregion

		private MaskedTextBox txtPhone;
		private ComboBox cbPosition;
		private TextBox txtMiddleName;
		private TextBox txtFirstName;
		private Label label3;
		private Label label2;
		private TextBox txtLastName;
		private Label label1;
		private Label label8;
		private Label label4;
		private Button btnReset;
		private Button btnApply;
		private Panel panel1;
		private Label label12;
	}
}
