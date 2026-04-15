namespace AdVision.Presentation
{
	partial class PositionsFilterUserControl
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
			label2 = new Label();
			btnApply = new Button();
			label1 = new Label();
			btnReset = new Button();
			txtName = new TextBox();
			panel1.SuspendLayout();
			SuspendLayout();
			// 
			// panel1
			// 
			panel1.BackColor = SystemColors.Window;
			panel1.BorderStyle = BorderStyle.FixedSingle;
			panel1.Controls.Add(label2);
			panel1.Controls.Add(btnApply);
			panel1.Controls.Add(label1);
			panel1.Controls.Add(btnReset);
			panel1.Controls.Add(txtName);
			panel1.Dock = DockStyle.Fill;
			panel1.Location = new Point(0, 0);
			panel1.Name = "panel1";
			panel1.Size = new Size(667, 105);
			panel1.TabIndex = 4;
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
			// btnApply
			// 
			btnApply.FlatStyle = FlatStyle.Popup;
			btnApply.Location = new Point(466, 56);
			btnApply.Name = "btnApply";
			btnApply.Size = new Size(180, 29);
			btnApply.TabIndex = 2;
			btnApply.Text = "Применить";
			btnApply.UseVisualStyleBackColor = true;
			btnApply.Click += BtnApply_Click;
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Location = new Point(18, 33);
			label1.Name = "label1";
			label1.Size = new Size(77, 20);
			label1.TabIndex = 0;
			label1.Text = "Название";
			// 
			// btnReset
			// 
			btnReset.FlatStyle = FlatStyle.Popup;
			btnReset.Location = new Point(280, 56);
			btnReset.Name = "btnReset";
			btnReset.Size = new Size(180, 29);
			btnReset.TabIndex = 2;
			btnReset.Text = "Сбросить";
			btnReset.UseVisualStyleBackColor = true;
			btnReset.Click += BtnReset_Click;
			// 
			// txtName
			// 
			txtName.BorderStyle = BorderStyle.FixedSingle;
			txtName.Location = new Point(18, 56);
			txtName.Name = "txtName";
			txtName.Size = new Size(256, 27);
			txtName.TabIndex = 1;
			txtName.TextChanged += TxtName_TextChanged;
			// 
			// PositionsFilterUserControl
			// 
			AutoScaleDimensions = new SizeF(8F, 20F);
			AutoScaleMode = AutoScaleMode.Font;
			Controls.Add(panel1);
			Name = "PositionsFilterUserControl";
			Size = new Size(667, 105);
			panel1.ResumeLayout(false);
			panel1.PerformLayout();
			ResumeLayout(false);
		}

		#endregion

		private Panel panel1;
		private Label label2;
		private Button btnApply;
		private Label label1;
		private Button btnReset;
		private TextBox txtName;
	}
}
