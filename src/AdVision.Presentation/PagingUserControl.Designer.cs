namespace AdVision.Presentation
{
	partial class PagingUserControl
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
			tableLayoutPanel1 = new TableLayoutPanel();
			btnPrev = new Button();
			btnNext = new Button();
			btnAdd = new Button();
			lblPaging = new Label();
			tableLayoutPanel1.SuspendLayout();
			SuspendLayout();
			// 
			// tableLayoutPanel1
			// 
			tableLayoutPanel1.ColumnCount = 5;
			tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
			tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 50F));
			tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 50F));
			tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 50F));
			tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 150F));
			tableLayoutPanel1.Controls.Add(lblPaging, 0, 0);
			tableLayoutPanel1.Controls.Add(btnNext, 3, 0);
			tableLayoutPanel1.Controls.Add(btnPrev, 1, 0);
			tableLayoutPanel1.Controls.Add(btnAdd, 4, 0);
			tableLayoutPanel1.Dock = DockStyle.Fill;
			tableLayoutPanel1.Location = new Point(0, 0);
			tableLayoutPanel1.Name = "tableLayoutPanel1";
			tableLayoutPanel1.RowCount = 1;
			tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
			tableLayoutPanel1.Size = new Size(754, 56);
			tableLayoutPanel1.TabIndex = 0;
			// 
			// btnPrev
			// 
			btnPrev.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			btnPrev.BackColor = Color.Transparent;
			btnPrev.FlatStyle = FlatStyle.Popup;
			btnPrev.Location = new Point(457, 24);
			btnPrev.Name = "btnPrev";
			btnPrev.Size = new Size(44, 29);
			btnPrev.TabIndex = 14;
			btnPrev.Text = "<";
			btnPrev.UseVisualStyleBackColor = false;
			// 
			// btnNext
			// 
			btnNext.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			btnNext.BackColor = Color.Transparent;
			btnNext.FlatStyle = FlatStyle.Popup;
			btnNext.Location = new Point(557, 24);
			btnNext.Name = "btnNext";
			btnNext.Size = new Size(44, 29);
			btnNext.TabIndex = 15;
			btnNext.Text = ">";
			btnNext.UseVisualStyleBackColor = false;
			// 
			// btnAdd
			// 
			btnAdd.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			btnAdd.BackColor = Color.Transparent;
			btnAdd.FlatStyle = FlatStyle.Popup;
			btnAdd.Location = new Point(657, 24);
			btnAdd.Name = "btnAdd";
			btnAdd.Size = new Size(94, 29);
			btnAdd.TabIndex = 16;
			btnAdd.Text = "Добавить";
			btnAdd.UseVisualStyleBackColor = false;
			// 
			// lblPaging
			// 
			lblPaging.AutoSize = true;
			lblPaging.Location = new Point(3, 0);
			lblPaging.Name = "lblPaging";
			lblPaging.Size = new Size(58, 20);
			lblPaging.TabIndex = 13;
			lblPaging.Text = "label11";
			// 
			// PagingUserControl
			// 
			AutoScaleDimensions = new SizeF(8F, 20F);
			AutoScaleMode = AutoScaleMode.Font;
			Controls.Add(tableLayoutPanel1);
			Name = "PagingUserControl";
			Size = new Size(754, 56);
			tableLayoutPanel1.ResumeLayout(false);
			tableLayoutPanel1.PerformLayout();
			ResumeLayout(false);
		}

		#endregion

		private TableLayoutPanel tableLayoutPanel1;
		private Label lblPaging;
		private Button btnNext;
		private Button btnPrev;
		private Button btnAdd;
	}
}
