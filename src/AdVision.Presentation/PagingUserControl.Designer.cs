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
			lblPaging = new Label();
			btnNext = new Button();
			btnPrev = new Button();
			btnAdd = new Button();
			panel1 = new Panel();
			tableLayoutPanel1.SuspendLayout();
			panel1.SuspendLayout();
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
			tableLayoutPanel1.Controls.Add(btnNext, 3, 0);
			tableLayoutPanel1.Controls.Add(btnPrev, 2, 0);
			tableLayoutPanel1.Controls.Add(panel1, 0, 0);
			tableLayoutPanel1.Controls.Add(btnAdd, 4, 0);
			tableLayoutPanel1.Location = new Point(0, 0);
			tableLayoutPanel1.Name = "tableLayoutPanel1";
			tableLayoutPanel1.RowCount = 1;
			tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
			tableLayoutPanel1.Size = new Size(754, 38);
			tableLayoutPanel1.TabIndex = 0;
			// 
			// lblPaging
			// 
			lblPaging.AutoSize = true;
			lblPaging.Location = new Point(3, 7);
			lblPaging.Name = "lblPaging";
			lblPaging.Size = new Size(58, 20);
			lblPaging.TabIndex = 13;
			lblPaging.Text = "label11";
			// 
			// btnNext
			// 
			btnNext.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			btnNext.BackColor = Color.Transparent;
			btnNext.FlatStyle = FlatStyle.Popup;
			btnNext.Location = new Point(557, 6);
			btnNext.Name = "btnNext";
			btnNext.Size = new Size(44, 29);
			btnNext.TabIndex = 15;
			btnNext.Text = ">";
			btnNext.UseVisualStyleBackColor = false;
			btnNext.Click += BtnNext_Click;
			// 
			// btnPrev
			// 
			btnPrev.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			btnPrev.BackColor = Color.Transparent;
			btnPrev.FlatStyle = FlatStyle.Popup;
			btnPrev.Location = new Point(507, 6);
			btnPrev.Name = "btnPrev";
			btnPrev.Size = new Size(44, 29);
			btnPrev.TabIndex = 14;
			btnPrev.Text = "<";
			btnPrev.UseVisualStyleBackColor = false;
			btnPrev.Click += BtnPrev_Click;
			// 
			// btnAdd
			// 
			btnAdd.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			btnAdd.BackColor = Color.Transparent;
			btnAdd.FlatStyle = FlatStyle.Popup;
			btnAdd.Location = new Point(657, 6);
			btnAdd.Name = "btnAdd";
			btnAdd.Size = new Size(94, 29);
			btnAdd.TabIndex = 16;
			btnAdd.Text = "Добавить";
			btnAdd.UseVisualStyleBackColor = false;
			btnAdd.Click += BtnAdd_Click;
			// 
			// panel1
			// 
			panel1.Controls.Add(lblPaging);
			panel1.Dock = DockStyle.Fill;
			panel1.Location = new Point(3, 3);
			panel1.Name = "panel1";
			panel1.Size = new Size(448, 32);
			panel1.TabIndex = 17;
			// 
			// PagingUserControl
			// 
			AutoScaleDimensions = new SizeF(8F, 20F);
			AutoScaleMode = AutoScaleMode.Font;
			Controls.Add(tableLayoutPanel1);
			Name = "PagingUserControl";
			Size = new Size(754, 38);
			tableLayoutPanel1.ResumeLayout(false);
			panel1.ResumeLayout(false);
			panel1.PerformLayout();
			ResumeLayout(false);
		}

		#endregion

		private TableLayoutPanel tableLayoutPanel1;
		private Label lblPaging;
		private Button btnNext;
		private Button btnPrev;
		private Button btnAdd;
		private Panel panel1;
	}
}
