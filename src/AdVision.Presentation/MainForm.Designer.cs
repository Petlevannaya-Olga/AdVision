namespace AdVision.Presentation;

partial class MainForm
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
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
	///  Required method for Designer support - do not modify
	///  the contents of this method with the code editor.
	/// </summary>
	private void InitializeComponent()
	{
		tabControl1 = new TabControl();
		tabPage1 = new TabPage();
		tabPage2 = new TabPage();
		tableLayoutPanel1 = new TableLayoutPanel();
		richTextBox1 = new RichTextBox();
		dataGridView1 = new DataGridView();
		button1 = new Button();
		tabControl1.SuspendLayout();
		tabPage1.SuspendLayout();
		tableLayoutPanel1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
		SuspendLayout();
		// 
		// tabControl1
		// 
		tabControl1.Controls.Add(tabPage1);
		tabControl1.Controls.Add(tabPage2);
		tabControl1.Dock = DockStyle.Fill;
		tabControl1.Location = new Point(3, 3);
		tabControl1.Name = "tabControl1";
		tabControl1.SelectedIndex = 0;
		tabControl1.Size = new Size(794, 309);
		tabControl1.TabIndex = 0;
		// 
		// tabPage1
		// 
		tabPage1.Controls.Add(button1);
		tabPage1.Controls.Add(dataGridView1);
		tabPage1.Location = new Point(4, 29);
		tabPage1.Name = "tabPage1";
		tabPage1.Padding = new Padding(3);
		tabPage1.Size = new Size(786, 276);
		tabPage1.TabIndex = 0;
		tabPage1.Text = "Площадки";
		tabPage1.UseVisualStyleBackColor = true;
		// 
		// tabPage2
		// 
		tabPage2.Location = new Point(4, 29);
		tabPage2.Name = "tabPage2";
		tabPage2.Padding = new Padding(3);
		tabPage2.Size = new Size(786, 276);
		tabPage2.TabIndex = 1;
		tabPage2.Text = "tabPage2";
		tabPage2.UseVisualStyleBackColor = true;
		// 
		// tableLayoutPanel1
		// 
		tableLayoutPanel1.ColumnCount = 1;
		tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
		tableLayoutPanel1.Controls.Add(tabControl1, 0, 0);
		tableLayoutPanel1.Controls.Add(richTextBox1, 0, 1);
		tableLayoutPanel1.Dock = DockStyle.Fill;
		tableLayoutPanel1.Location = new Point(0, 0);
		tableLayoutPanel1.Name = "tableLayoutPanel1";
		tableLayoutPanel1.RowCount = 2;
		tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 70F));
		tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 30F));
		tableLayoutPanel1.Size = new Size(800, 450);
		tableLayoutPanel1.TabIndex = 1;
		// 
		// richTextBox1
		// 
		richTextBox1.Dock = DockStyle.Fill;
		richTextBox1.Location = new Point(3, 318);
		richTextBox1.Name = "richTextBox1";
		richTextBox1.Size = new Size(794, 129);
		richTextBox1.TabIndex = 1;
		richTextBox1.Text = "";
		// 
		// dataGridView1
		// 
		dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		dataGridView1.Location = new Point(6, 3);
		dataGridView1.Name = "dataGridView1";
		dataGridView1.RowHeadersWidth = 51;
		dataGridView1.Size = new Size(774, 231);
		dataGridView1.TabIndex = 0;
		// 
		// button1
		// 
		button1.Location = new Point(686, 240);
		button1.Name = "button1";
		button1.Size = new Size(94, 29);
		button1.TabIndex = 1;
		button1.Text = "Добавить";
		button1.UseVisualStyleBackColor = true;
		// 
		// MainForm
		// 
		AutoScaleDimensions = new SizeF(8F, 20F);
		AutoScaleMode = AutoScaleMode.Font;
		ClientSize = new Size(800, 450);
		Controls.Add(tableLayoutPanel1);
		Name = "MainForm";
		Text = "Информационная система рекламной компании";
		tabControl1.ResumeLayout(false);
		tabPage1.ResumeLayout(false);
		tableLayoutPanel1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
		ResumeLayout(false);
	}

	#endregion

	private TabControl tabControl1;
	private TabPage tabPage1;
	private TabPage tabPage2;
	private TableLayoutPanel tableLayoutPanel1;
	private RichTextBox richTextBox1;
	private Button button1;
	private DataGridView dataGridView1;
}