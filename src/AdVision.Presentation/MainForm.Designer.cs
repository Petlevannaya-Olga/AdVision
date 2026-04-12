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
		tableLayoutPanel2 = new TableLayoutPanel();
		venuesDataGridView = new DataGridView();
		btnCreate = new Button();
		tabPage2 = new TabPage();
		tableLayoutPanel1 = new TableLayoutPanel();
		tabControl1.SuspendLayout();
		tabPage1.SuspendLayout();
		tableLayoutPanel2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)venuesDataGridView).BeginInit();
		tableLayoutPanel1.SuspendLayout();
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
		tabPage1.Controls.Add(tableLayoutPanel2);
		tabPage1.Location = new Point(4, 29);
		tabPage1.Name = "tabPage1";
		tabPage1.Padding = new Padding(3);
		tabPage1.Size = new Size(786, 276);
		tabPage1.TabIndex = 0;
		tabPage1.Text = "Площадки";
		tabPage1.UseVisualStyleBackColor = true;
		// 
		// tableLayoutPanel2
		// 
		tableLayoutPanel2.ColumnCount = 1;
		tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
		tableLayoutPanel2.Controls.Add(venuesDataGridView, 0, 0);
		tableLayoutPanel2.Controls.Add(btnCreate, 0, 1);
		tableLayoutPanel2.Dock = DockStyle.Fill;
		tableLayoutPanel2.Location = new Point(3, 3);
		tableLayoutPanel2.Name = "tableLayoutPanel2";
		tableLayoutPanel2.RowCount = 2;
		tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
		tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
		tableLayoutPanel2.Size = new Size(780, 270);
		tableLayoutPanel2.TabIndex = 2;
		// 
		// venuesDataGridView
		// 
		venuesDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		venuesDataGridView.Dock = DockStyle.Fill;
		venuesDataGridView.Location = new Point(3, 3);
		venuesDataGridView.Name = "venuesDataGridView";
		venuesDataGridView.RowHeadersWidth = 51;
		venuesDataGridView.Size = new Size(774, 229);
		venuesDataGridView.TabIndex = 0;
		// 
		// btnCreate
		// 
		btnCreate.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
		btnCreate.BackColor = Color.Transparent;
		btnCreate.FlatStyle = FlatStyle.Popup;
		btnCreate.Location = new Point(683, 238);
		btnCreate.Name = "btnCreate";
		btnCreate.Size = new Size(94, 29);
		btnCreate.TabIndex = 1;
		btnCreate.Text = "Добавить";
		btnCreate.UseVisualStyleBackColor = false;
		btnCreate.Click += BtnCreate_Click;
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
		tableLayoutPanel1.Dock = DockStyle.Fill;
		tableLayoutPanel1.Location = new Point(0, 0);
		tableLayoutPanel1.Name = "tableLayoutPanel1";
		tableLayoutPanel1.RowCount = 2;
		tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 70F));
		tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 30F));
		tableLayoutPanel1.Size = new Size(800, 450);
		tableLayoutPanel1.TabIndex = 1;
		// 
		// MainForm
		// 
		AutoScaleDimensions = new SizeF(8F, 20F);
		AutoScaleMode = AutoScaleMode.Font;
		ClientSize = new Size(800, 450);
		Controls.Add(tableLayoutPanel1);
		Name = "MainForm";
		StartPosition = FormStartPosition.CenterScreen;
		Text = "Информационная система рекламной компании";
		tabControl1.ResumeLayout(false);
		tabPage1.ResumeLayout(false);
		tableLayoutPanel2.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)venuesDataGridView).EndInit();
		tableLayoutPanel1.ResumeLayout(false);
		ResumeLayout(false);
	}

	#endregion

	private TabControl tabControl1;
	private TabPage tabPage1;
	private TabPage tabPage2;
	private TableLayoutPanel tableLayoutPanel1;
	private Button btnCreate;
	private DataGridView venuesDataGridView;
	private TableLayoutPanel tableLayoutPanel2;
}