namespace AdVision.Presentation
{
	partial class DiscountForm
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
			nudPercent = new NumericUpDown();
			label3 = new Label();
			nudMinTotal = new NumericUpDown();
			btnClose = new Button();
			btnSave = new Button();
			pbNameValidation = new PictureBox();
			pbPercentValidation = new PictureBox();
			pbMinTotalValidation = new PictureBox();
			((System.ComponentModel.ISupportInitialize)nudPercent).BeginInit();
			((System.ComponentModel.ISupportInitialize)nudMinTotal).BeginInit();
			((System.ComponentModel.ISupportInitialize)pbNameValidation).BeginInit();
			((System.ComponentModel.ISupportInitialize)pbPercentValidation).BeginInit();
			((System.ComponentModel.ISupportInitialize)pbMinTotalValidation).BeginInit();
			SuspendLayout();
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Location = new Point(12, 9);
			label1.Name = "label1";
			label1.Size = new Size(248, 20);
			label1.TabIndex = 0;
			label1.Text = "Название (от 10 до 200 символов)";
			// 
			// txtName
			// 
			txtName.BorderStyle = BorderStyle.FixedSingle;
			txtName.Location = new Point(12, 32);
			txtName.Name = "txtName";
			txtName.Size = new Size(272, 27);
			txtName.TabIndex = 1;
			txtName.TextChanged += TxtName_TextChanged;
			// 
			// label2
			// 
			label2.AutoSize = true;
			label2.Location = new Point(12, 72);
			label2.Name = "label2";
			label2.Size = new Size(70, 20);
			label2.TabIndex = 0;
			label2.Text = "Процент";
			// 
			// nudPercent
			// 
			nudPercent.Location = new Point(12, 95);
			nudPercent.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
			nudPercent.Name = "nudPercent";
			nudPercent.Size = new Size(272, 27);
			nudPercent.TabIndex = 2;
			nudPercent.Value = new decimal(new int[] { 1, 0, 0, 0 });
			nudPercent.ValueChanged += NudPercent_ValueChanged;
			// 
			// label3
			// 
			label3.AutoSize = true;
			label3.Location = new Point(12, 135);
			label3.Name = "label3";
			label3.Size = new Size(206, 20);
			label3.TabIndex = 0;
			label3.Text = "Минимальная сумма заказа";
			// 
			// nudMinTotal
			// 
			nudMinTotal.DecimalPlaces = 2;
			nudMinTotal.Location = new Point(12, 158);
			nudMinTotal.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
			nudMinTotal.Name = "nudMinTotal";
			nudMinTotal.Size = new Size(272, 27);
			nudMinTotal.TabIndex = 2;
			nudMinTotal.Value = new decimal(new int[] { 1, 0, 0, 0 });
			nudMinTotal.ValueChanged += NudMinTotal_ValueChanged;
			// 
			// btnClose
			// 
			btnClose.Location = new Point(149, 209);
			btnClose.Name = "btnClose";
			btnClose.Size = new Size(135, 29);
			btnClose.TabIndex = 3;
			btnClose.Text = "Закрыть";
			btnClose.UseVisualStyleBackColor = true;
			btnClose.Click += BtnClose_Click;
			// 
			// btnSave
			// 
			btnSave.Location = new Point(12, 209);
			btnSave.Name = "btnSave";
			btnSave.Size = new Size(135, 29);
			btnSave.TabIndex = 3;
			btnSave.Text = "Сохранить";
			btnSave.UseVisualStyleBackColor = true;
			btnSave.Click += BtnSave_Click;
			// 
			// pbNameValidation
			// 
			pbNameValidation.Location = new Point(290, 32);
			pbNameValidation.Name = "pbNameValidation";
			pbNameValidation.Size = new Size(27, 27);
			pbNameValidation.TabIndex = 4;
			pbNameValidation.TabStop = false;
			// 
			// pbPercentValidation
			// 
			pbPercentValidation.Location = new Point(290, 95);
			pbPercentValidation.Name = "pbPercentValidation";
			pbPercentValidation.Size = new Size(27, 27);
			pbPercentValidation.TabIndex = 4;
			pbPercentValidation.TabStop = false;
			// 
			// pbMinTotalValidation
			// 
			pbMinTotalValidation.Location = new Point(290, 158);
			pbMinTotalValidation.Name = "pbMinTotalValidation";
			pbMinTotalValidation.Size = new Size(27, 27);
			pbMinTotalValidation.TabIndex = 4;
			pbMinTotalValidation.TabStop = false;
			// 
			// DiscountForm
			// 
			AutoScaleDimensions = new SizeF(8F, 20F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(327, 256);
			Controls.Add(pbMinTotalValidation);
			Controls.Add(pbPercentValidation);
			Controls.Add(pbNameValidation);
			Controls.Add(btnSave);
			Controls.Add(btnClose);
			Controls.Add(nudMinTotal);
			Controls.Add(nudPercent);
			Controls.Add(txtName);
			Controls.Add(label3);
			Controls.Add(label2);
			Controls.Add(label1);
			FormBorderStyle = FormBorderStyle.FixedSingle;
			MaximizeBox = false;
			MinimizeBox = false;
			Name = "DiscountForm";
			StartPosition = FormStartPosition.CenterScreen;
			Text = "Создание новой скидки";
			FormClosed += DiscountForm_FormClosed;
			((System.ComponentModel.ISupportInitialize)nudPercent).EndInit();
			((System.ComponentModel.ISupportInitialize)nudMinTotal).EndInit();
			((System.ComponentModel.ISupportInitialize)pbNameValidation).EndInit();
			((System.ComponentModel.ISupportInitialize)pbPercentValidation).EndInit();
			((System.ComponentModel.ISupportInitialize)pbMinTotalValidation).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private Label label1;
		private TextBox txtName;
		private Label label2;
		private NumericUpDown nudPercent;
		private Label label3;
		private NumericUpDown nudMinTotal;
		private Button btnClose;
		private Button btnSave;
		private PictureBox pbNameValidation;
		private PictureBox pbPercentValidation;
		private PictureBox pbMinTotalValidation;
	}
}