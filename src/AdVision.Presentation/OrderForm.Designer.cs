namespace AdVision.Presentation
{
	partial class OrderForm
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
			textBox1 = new TextBox();
			label1 = new Label();
			button1 = new Button();
			pictureBox1 = new PictureBox();
			label2 = new Label();
			dataGridView1 = new DataGridView();
			pagingUserControl1 = new PagingUserControl();
			button2 = new Button();
			button3 = new Button();
			((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
			((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
			SuspendLayout();
			// 
			// textBox1
			// 
			textBox1.BorderStyle = BorderStyle.FixedSingle;
			textBox1.Location = new Point(12, 32);
			textBox1.Name = "textBox1";
			textBox1.Size = new Size(442, 27);
			textBox1.TabIndex = 0;
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Location = new Point(12, 9);
			label1.Name = "label1";
			label1.Size = new Size(69, 20);
			label1.TabIndex = 1;
			label1.Text = "Договор";
			// 
			// button1
			// 
			button1.FlatStyle = FlatStyle.Popup;
			button1.Location = new Point(460, 32);
			button1.Name = "button1";
			button1.Size = new Size(37, 27);
			button1.TabIndex = 2;
			button1.Text = "...";
			button1.UseVisualStyleBackColor = true;
			// 
			// pictureBox1
			// 
			pictureBox1.Location = new Point(503, 32);
			pictureBox1.Name = "pictureBox1";
			pictureBox1.Size = new Size(27, 27);
			pictureBox1.TabIndex = 3;
			pictureBox1.TabStop = false;
			// 
			// label2
			// 
			label2.AutoSize = true;
			label2.Location = new Point(12, 71);
			label2.Name = "label2";
			label2.Size = new Size(72, 20);
			label2.TabIndex = 4;
			label2.Text = "Позиции";
			// 
			// dataGridView1
			// 
			dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dataGridView1.Location = new Point(12, 94);
			dataGridView1.Name = "dataGridView1";
			dataGridView1.RowHeadersWidth = 51;
			dataGridView1.Size = new Size(518, 339);
			dataGridView1.TabIndex = 5;
			// 
			// pagingUserControl1
			// 
			pagingUserControl1.Location = new Point(4, 439);
			pagingUserControl1.Name = "pagingUserControl1";
			pagingUserControl1.Size = new Size(539, 45);
			pagingUserControl1.TabIndex = 6;
			// 
			// button2
			// 
			button2.FlatStyle = FlatStyle.Popup;
			button2.Location = new Point(337, 490);
			button2.Name = "button2";
			button2.Size = new Size(94, 29);
			button2.TabIndex = 7;
			button2.Text = "Сохранить";
			button2.UseVisualStyleBackColor = true;
			// 
			// button3
			// 
			button3.FlatStyle = FlatStyle.Popup;
			button3.Location = new Point(436, 490);
			button3.Name = "button3";
			button3.Size = new Size(94, 29);
			button3.TabIndex = 7;
			button3.Text = "Закрыть";
			button3.UseVisualStyleBackColor = true;
			// 
			// OrderForm
			// 
			AutoScaleDimensions = new SizeF(8F, 20F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(544, 532);
			Controls.Add(button3);
			Controls.Add(button2);
			Controls.Add(pagingUserControl1);
			Controls.Add(dataGridView1);
			Controls.Add(label2);
			Controls.Add(pictureBox1);
			Controls.Add(button1);
			Controls.Add(label1);
			Controls.Add(textBox1);
			Name = "OrderForm";
			Text = "Создание заказа";
			((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
			((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private TextBox textBox1;
		private Label label1;
		private Button button1;
		private PictureBox pictureBox1;
		private Label label2;
		private DataGridView dataGridView1;
		private PagingUserControl pagingUserControl1;
		private Button button2;
		private Button button3;
	}
}