namespace AdVision.Presentation
{
	partial class CustomersFilterUserControl
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
			label12 = new Label();
			btnApply = new Button();
			txtPhone = new MaskedTextBox();
			btnReset = new Button();
			label1 = new Label();
			label8 = new Label();
			txtLastName = new TextBox();
			label2 = new Label();
			label3 = new Label();
			txtFirstName = new TextBox();
			txtMiddleName = new TextBox();
			SuspendLayout();
			// 
			// label12
			// 
			label12.AutoSize = true;
			label12.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 204);
			label12.Location = new Point(12, 9);
			label12.Name = "label12";
			label12.Size = new Size(64, 20);
			label12.TabIndex = 20;
			label12.Text = "Фильтр";
			// 
			// btnApply
			// 
			btnApply.FlatStyle = FlatStyle.Popup;
			btnApply.Location = new Point(543, 158);
			btnApply.Name = "btnApply";
			btnApply.Size = new Size(169, 29);
			btnApply.TabIndex = 29;
			btnApply.Text = "Применить";
			btnApply.UseVisualStyleBackColor = true;
			// 
			// txtPhone
			// 
			txtPhone.BorderStyle = BorderStyle.FixedSingle;
			txtPhone.Location = new Point(375, 116);
			txtPhone.Mask = "+7(999) 000-0000";
			txtPhone.Name = "txtPhone";
			txtPhone.Size = new Size(337, 27);
			txtPhone.TabIndex = 27;
			txtPhone.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
			// 
			// btnReset
			// 
			btnReset.FlatStyle = FlatStyle.Popup;
			btnReset.Location = new Point(375, 158);
			btnReset.Name = "btnReset";
			btnReset.Size = new Size(162, 29);
			btnReset.TabIndex = 30;
			btnReset.Text = "Сбросить";
			btnReset.UseVisualStyleBackColor = true;
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Location = new Point(12, 39);
			label1.Name = "label1";
			label1.Size = new Size(73, 20);
			label1.TabIndex = 23;
			label1.Text = "Фамилия";
			// 
			// label8
			// 
			label8.AutoSize = true;
			label8.Location = new Point(375, 93);
			label8.Name = "label8";
			label8.Size = new Size(127, 20);
			label8.TabIndex = 28;
			label8.Text = "Номер телефона";
			// 
			// txtLastName
			// 
			txtLastName.BorderStyle = BorderStyle.FixedSingle;
			txtLastName.Location = new Point(12, 62);
			txtLastName.Name = "txtLastName";
			txtLastName.Size = new Size(337, 27);
			txtLastName.TabIndex = 26;
			// 
			// label2
			// 
			label2.AutoSize = true;
			label2.Location = new Point(12, 92);
			label2.Name = "label2";
			label2.Size = new Size(39, 20);
			label2.TabIndex = 22;
			label2.Text = "Имя";
			// 
			// label3
			// 
			label3.AutoSize = true;
			label3.Location = new Point(375, 40);
			label3.Name = "label3";
			label3.Size = new Size(72, 20);
			label3.TabIndex = 21;
			label3.Text = "Отчество";
			// 
			// txtFirstName
			// 
			txtFirstName.BorderStyle = BorderStyle.FixedSingle;
			txtFirstName.Location = new Point(12, 116);
			txtFirstName.Name = "txtFirstName";
			txtFirstName.Size = new Size(337, 27);
			txtFirstName.TabIndex = 25;
			// 
			// txtMiddleName
			// 
			txtMiddleName.BorderStyle = BorderStyle.FixedSingle;
			txtMiddleName.Location = new Point(375, 63);
			txtMiddleName.Name = "txtMiddleName";
			txtMiddleName.Size = new Size(337, 27);
			txtMiddleName.TabIndex = 24;
			// 
			// CustomersFilterUserControl
			// 
			AutoScaleDimensions = new SizeF(8F, 20F);
			AutoScaleMode = AutoScaleMode.Font;
			Controls.Add(label12);
			Controls.Add(btnApply);
			Controls.Add(txtPhone);
			Controls.Add(btnReset);
			Controls.Add(label1);
			Controls.Add(label8);
			Controls.Add(txtLastName);
			Controls.Add(label2);
			Controls.Add(label3);
			Controls.Add(txtFirstName);
			Controls.Add(txtMiddleName);
			Name = "CustomersFilterUserControl";
			Size = new Size(742, 206);
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private Label label12;
		private Button btnApply;
		private MaskedTextBox txtPhone;
		private Button btnReset;
		private Label label1;
		private Label label8;
		private TextBox txtLastName;
		private Label label2;
		private Label label3;
		private TextBox txtFirstName;
		private TextBox txtMiddleName;
	}
}
