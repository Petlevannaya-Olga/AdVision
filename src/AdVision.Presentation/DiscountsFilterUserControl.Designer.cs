namespace AdVision.Presentation
{
	partial class DiscountsFilterUserControl
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
			panel1 = new Panel();
			nudDiscountTo = new NumericUpDown();
			btnReset = new Button();
			btnApply = new Button();
			nudDiscountFrom = new NumericUpDown();
			nudMinTotalTo = new NumericUpDown();
			label8 = new Label();
			nudMinTotalFrom = new NumericUpDown();
			label7 = new Label();
			label5 = new Label();
			label6 = new Label();
			label4 = new Label();
			label3 = new Label();
			label2 = new Label();
			label1 = new Label();
			txtName = new TextBox();
			panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)nudDiscountTo).BeginInit();
			((System.ComponentModel.ISupportInitialize)nudDiscountFrom).BeginInit();
			((System.ComponentModel.ISupportInitialize)nudMinTotalTo).BeginInit();
			((System.ComponentModel.ISupportInitialize)nudMinTotalFrom).BeginInit();
			SuspendLayout();
			// 
			// panel1
			// 
			panel1.BackColor = SystemColors.Window;
			panel1.BorderStyle = BorderStyle.FixedSingle;
			panel1.Controls.Add(nudDiscountTo);
			panel1.Controls.Add(btnReset);
			panel1.Controls.Add(btnApply);
			panel1.Controls.Add(nudDiscountFrom);
			panel1.Controls.Add(nudMinTotalTo);
			panel1.Controls.Add(label8);
			panel1.Controls.Add(nudMinTotalFrom);
			panel1.Controls.Add(label7);
			panel1.Controls.Add(label5);
			panel1.Controls.Add(label6);
			panel1.Controls.Add(label4);
			panel1.Controls.Add(label3);
			panel1.Controls.Add(label2);
			panel1.Controls.Add(label1);
			panel1.Controls.Add(txtName);
			panel1.Dock = DockStyle.Fill;
			panel1.Location = new Point(0, 0);
			panel1.Name = "panel1";
			panel1.Size = new Size(652, 199);
			panel1.TabIndex = 5;
			// 
			// nudDiscountTo
			// 
			nudDiscountTo.Location = new Point(468, 115);
			nudDiscountTo.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
			nudDiscountTo.Name = "nudDiscountTo";
			nudDiscountTo.Size = new Size(150, 27);
			nudDiscountTo.TabIndex = 8;
			nudDiscountTo.Value = new decimal(new int[] { 100, 0, 0, 0 });
			nudDiscountTo.ValueChanged += NudDiscountTo_ValueChanged;
			// 
			// btnReset
			// 
			btnReset.FlatStyle = FlatStyle.Popup;
			btnReset.Location = new Point(246, 156);
			btnReset.Name = "btnReset";
			btnReset.Size = new Size(180, 29);
			btnReset.TabIndex = 2;
			btnReset.Text = "Сбросить";
			btnReset.UseVisualStyleBackColor = true;
			btnReset.Click += BtnReset_Click;
			// 
			// btnApply
			// 
			btnApply.FlatStyle = FlatStyle.Popup;
			btnApply.Location = new Point(438, 156);
			btnApply.Name = "btnApply";
			btnApply.Size = new Size(180, 29);
			btnApply.TabIndex = 2;
			btnApply.Text = "Применить";
			btnApply.UseVisualStyleBackColor = true;
			btnApply.Click += BtnApply_Click;
			// 
			// nudDiscountFrom
			// 
			nudDiscountFrom.Location = new Point(276, 115);
			nudDiscountFrom.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
			nudDiscountFrom.Name = "nudDiscountFrom";
			nudDiscountFrom.Size = new Size(150, 27);
			nudDiscountFrom.TabIndex = 7;
			nudDiscountFrom.Value = new decimal(new int[] { 1, 0, 0, 0 });
			nudDiscountFrom.ValueChanged += NudDiscountFrom_ValueChanged;
			// 
			// nudMinTotalTo
			// 
			nudMinTotalTo.DecimalPlaces = 2;
			nudMinTotalTo.Location = new Point(468, 73);
			nudMinTotalTo.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
			nudMinTotalTo.Name = "nudMinTotalTo";
			nudMinTotalTo.Size = new Size(150, 27);
			nudMinTotalTo.TabIndex = 8;
			nudMinTotalTo.Value = new decimal(new int[] { 1000000, 0, 0, 0 });
			nudMinTotalTo.ValueChanged += NudMinTotalTo_ValueChanged;
			// 
			// label8
			// 
			label8.AutoSize = true;
			label8.Location = new Point(436, 117);
			label8.Name = "label8";
			label8.Size = new Size(26, 20);
			label8.TabIndex = 6;
			label8.Text = "до";
			// 
			// nudMinTotalFrom
			// 
			nudMinTotalFrom.DecimalPlaces = 2;
			nudMinTotalFrom.Location = new Point(276, 73);
			nudMinTotalFrom.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
			nudMinTotalFrom.Name = "nudMinTotalFrom";
			nudMinTotalFrom.Size = new Size(150, 27);
			nudMinTotalFrom.TabIndex = 7;
			nudMinTotalFrom.Value = new decimal(new int[] { 1, 0, 0, 0 });
			nudMinTotalFrom.ValueChanged += NudMinTotalFrom_ValueChanged;
			// 
			// label7
			// 
			label7.AutoSize = true;
			label7.Location = new Point(246, 120);
			label7.Name = "label7";
			label7.Size = new Size(24, 20);
			label7.TabIndex = 5;
			label7.Text = "от";
			// 
			// label5
			// 
			label5.AutoSize = true;
			label5.Location = new Point(434, 75);
			label5.Name = "label5";
			label5.Size = new Size(26, 20);
			label5.TabIndex = 6;
			label5.Text = "до";
			// 
			// label6
			// 
			label6.AutoSize = true;
			label6.Location = new Point(18, 117);
			label6.Name = "label6";
			label6.Size = new Size(57, 20);
			label6.TabIndex = 4;
			label6.Text = "Скидка";
			// 
			// label4
			// 
			label4.AutoSize = true;
			label4.Location = new Point(246, 75);
			label4.Name = "label4";
			label4.Size = new Size(24, 20);
			label4.TabIndex = 5;
			label4.Text = "от";
			// 
			// label3
			// 
			label3.AutoSize = true;
			label3.Location = new Point(18, 75);
			label3.Name = "label3";
			label3.Size = new Size(206, 20);
			label3.TabIndex = 4;
			label3.Text = "Минимальная сумма заказа";
			// 
			// label2
			// 
			label2.AutoSize = true;
			label2.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 204);
			label2.Location = new Point(18, 5);
			label2.Name = "label2";
			label2.Size = new Size(64, 20);
			label2.TabIndex = 3;
			label2.Text = "Фильтр";
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Location = new Point(18, 38);
			label1.Name = "label1";
			label1.Size = new Size(77, 20);
			label1.TabIndex = 0;
			label1.Text = "Название";
			// 
			// txtName
			// 
			txtName.BorderStyle = BorderStyle.FixedSingle;
			txtName.Location = new Point(246, 36);
			txtName.Name = "txtName";
			txtName.Size = new Size(372, 27);
			txtName.TabIndex = 1;
			txtName.TextChanged += TxtName_TextChanged;
			// 
			// DiscountsFilterUserControl
			// 
			AutoScaleDimensions = new SizeF(8F, 20F);
			AutoScaleMode = AutoScaleMode.Font;
			Controls.Add(panel1);
			Name = "DiscountsFilterUserControl";
			Size = new Size(652, 199);
			panel1.ResumeLayout(false);
			panel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)nudDiscountTo).EndInit();
			((System.ComponentModel.ISupportInitialize)nudDiscountFrom).EndInit();
			((System.ComponentModel.ISupportInitialize)nudMinTotalTo).EndInit();
			((System.ComponentModel.ISupportInitialize)nudMinTotalFrom).EndInit();
			ResumeLayout(false);
		}

		#endregion

		private Panel panel1;
		private NumericUpDown nudDiscountTo;
		private Button btnReset;
		private Button btnApply;
		private NumericUpDown nudDiscountFrom;
		private NumericUpDown nudMinTotalTo;
		private Label label8;
		private NumericUpDown nudMinTotalFrom;
		private Label label7;
		private Label label5;
		private Label label6;
		private Label label4;
		private Label label3;
		private Label label2;
		private Label label1;
		private TextBox txtName;
	}
}
