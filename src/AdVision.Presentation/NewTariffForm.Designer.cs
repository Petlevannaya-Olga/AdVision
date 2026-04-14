namespace AdVision.Presentation
{
	partial class NewTariffForm
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
			label2 = new Label();
			label3 = new Label();
			txtPrice = new TextBox();
			btnSave = new Button();
			btnExit = new Button();
			dtpStartDate = new DateTimePicker();
			dtpEndDate = new DateTimePicker();
			SuspendLayout();
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Location = new Point(12, 14);
			label1.Name = "label1";
			label1.Size = new Size(94, 20);
			label1.TabIndex = 1;
			label1.Text = "Дата начала";
			// 
			// label2
			// 
			label2.AutoSize = true;
			label2.Location = new Point(12, 77);
			label2.Name = "label2";
			label2.Size = new Size(121, 20);
			label2.TabIndex = 1;
			label2.Text = "Дата окончания";
			// 
			// label3
			// 
			label3.AutoSize = true;
			label3.Location = new Point(12, 139);
			label3.Name = "label3";
			label3.Size = new Size(235, 20);
			label3.TabIndex = 1;
			label3.Text = "Стоимость размещения за сутки";
			// 
			// txtPrice
			// 
			txtPrice.BorderStyle = BorderStyle.FixedSingle;
			txtPrice.Location = new Point(12, 161);
			txtPrice.Name = "txtPrice";
			txtPrice.Size = new Size(250, 27);
			txtPrice.TabIndex = 2;
			txtPrice.TextChanged += TxtPrice_TextChanged;
			// 
			// btnSave
			// 
			btnSave.Location = new Point(12, 194);
			btnSave.Name = "btnSave";
			btnSave.Size = new Size(121, 29);
			btnSave.TabIndex = 3;
			btnSave.Text = "Сохранить";
			btnSave.UseVisualStyleBackColor = true;
			btnSave.Click += BtnSave_Click;
			// 
			// btnExit
			// 
			btnExit.Location = new Point(141, 194);
			btnExit.Name = "btnExit";
			btnExit.Size = new Size(121, 29);
			btnExit.TabIndex = 3;
			btnExit.Text = "Закрыть";
			btnExit.UseVisualStyleBackColor = true;
			btnExit.Click += BtnExit_Click;
			// 
			// dtpStartDate
			// 
			dtpStartDate.Location = new Point(12, 37);
			dtpStartDate.Name = "dtpStartDate";
			dtpStartDate.Size = new Size(250, 27);
			dtpStartDate.TabIndex = 4;
			// 
			// dtpEndDate
			// 
			dtpEndDate.Location = new Point(12, 100);
			dtpEndDate.Name = "dtpEndDate";
			dtpEndDate.Size = new Size(250, 27);
			dtpEndDate.TabIndex = 5;
			// 
			// NewTariffForm
			// 
			AutoScaleDimensions = new SizeF(8F, 20F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(274, 237);
			Controls.Add(dtpEndDate);
			Controls.Add(dtpStartDate);
			Controls.Add(btnExit);
			Controls.Add(btnSave);
			Controls.Add(txtPrice);
			Controls.Add(label3);
			Controls.Add(label2);
			Controls.Add(label1);
			FormBorderStyle = FormBorderStyle.FixedSingle;
			MaximizeBox = false;
			MinimizeBox = false;
			Name = "NewTariffForm";
			StartPosition = FormStartPosition.CenterScreen;
			Text = "Создание нового тарифа";
			FormClosed += NewTariffForm_FormClosed;
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private Label label1;
		private Label label2;
		private Label label3;
		private TextBox txtPrice;
		private Button btnSave;
		private Button btnExit;
		private DateTimePicker dtpStartDate;
		private DateTimePicker dtpEndDate;
	}
}