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
		panel1 = new Panel();
		btnPrev = new Button();
		btnNext = new Button();
		btnCreate = new Button();
		panel2 = new Panel();
		numericUpDown2 = new NumericUpDown();
		numericUpDown1 = new NumericUpDown();
		label10 = new Label();
		label9 = new Label();
		cbSortOrder = new ComboBox();
		label7 = new Label();
		cbCities = new ComboBox();
		cbDistricts = new ComboBox();
		label5 = new Label();
		cbRegions = new ComboBox();
		label4 = new Label();
		cbVenueTypes = new ComboBox();
		label3 = new Label();
		txtStreet = new TextBox();
		label8 = new Label();
		txtName = new TextBox();
		label6 = new Label();
		label2 = new Label();
		label1 = new Label();
		btnApply = new Button();
		tabPage2 = new TabPage();
		tabControl1.SuspendLayout();
		tabPage1.SuspendLayout();
		tableLayoutPanel2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)venuesDataGridView).BeginInit();
		panel1.SuspendLayout();
		panel2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)numericUpDown2).BeginInit();
		((System.ComponentModel.ISupportInitialize)numericUpDown1).BeginInit();
		SuspendLayout();
		// 
		// tabControl1
		// 
		tabControl1.Controls.Add(tabPage1);
		tabControl1.Controls.Add(tabPage2);
		tabControl1.Dock = DockStyle.Fill;
		tabControl1.Location = new Point(0, 0);
		tabControl1.Name = "tabControl1";
		tabControl1.SelectedIndex = 0;
		tabControl1.Size = new Size(800, 504);
		tabControl1.TabIndex = 0;
		// 
		// tabPage1
		// 
		tabPage1.Controls.Add(tableLayoutPanel2);
		tabPage1.Location = new Point(4, 29);
		tabPage1.Name = "tabPage1";
		tabPage1.Padding = new Padding(3);
		tabPage1.Size = new Size(792, 471);
		tabPage1.TabIndex = 0;
		tabPage1.Text = "Площадки";
		tabPage1.UseVisualStyleBackColor = true;
		// 
		// tableLayoutPanel2
		// 
		tableLayoutPanel2.ColumnCount = 2;
		tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 300F));
		tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
		tableLayoutPanel2.Controls.Add(venuesDataGridView, 1, 0);
		tableLayoutPanel2.Controls.Add(panel1, 1, 1);
		tableLayoutPanel2.Controls.Add(panel2, 0, 0);
		tableLayoutPanel2.Controls.Add(btnApply, 0, 1);
		tableLayoutPanel2.Dock = DockStyle.Fill;
		tableLayoutPanel2.Location = new Point(3, 3);
		tableLayoutPanel2.Name = "tableLayoutPanel2";
		tableLayoutPanel2.RowCount = 2;
		tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
		tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
		tableLayoutPanel2.Size = new Size(786, 465);
		tableLayoutPanel2.TabIndex = 2;
		// 
		// venuesDataGridView
		// 
		venuesDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		venuesDataGridView.Dock = DockStyle.Fill;
		venuesDataGridView.Location = new Point(303, 3);
		venuesDataGridView.Name = "venuesDataGridView";
		venuesDataGridView.RowHeadersWidth = 51;
		venuesDataGridView.Size = new Size(480, 424);
		venuesDataGridView.TabIndex = 0;
		// 
		// panel1
		// 
		panel1.Controls.Add(btnPrev);
		panel1.Controls.Add(btnNext);
		panel1.Controls.Add(btnCreate);
		panel1.Dock = DockStyle.Fill;
		panel1.Location = new Point(303, 433);
		panel1.Name = "panel1";
		panel1.Size = new Size(480, 29);
		panel1.TabIndex = 2;
		// 
		// btnPrev
		// 
		btnPrev.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
		btnPrev.BackColor = Color.Transparent;
		btnPrev.FlatStyle = FlatStyle.Popup;
		btnPrev.Location = new Point(283, 0);
		btnPrev.Name = "btnPrev";
		btnPrev.Size = new Size(44, 29);
		btnPrev.TabIndex = 1;
		btnPrev.Text = "<";
		btnPrev.UseVisualStyleBackColor = false;
		btnPrev.Click += BtnCreate_Click;
		// 
		// btnNext
		// 
		btnNext.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
		btnNext.BackColor = Color.Transparent;
		btnNext.FlatStyle = FlatStyle.Popup;
		btnNext.Location = new Point(333, 0);
		btnNext.Name = "btnNext";
		btnNext.Size = new Size(44, 29);
		btnNext.TabIndex = 1;
		btnNext.Text = ">";
		btnNext.UseVisualStyleBackColor = false;
		btnNext.Click += BtnCreate_Click;
		// 
		// btnCreate
		// 
		btnCreate.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
		btnCreate.BackColor = Color.Transparent;
		btnCreate.FlatStyle = FlatStyle.Popup;
		btnCreate.Location = new Point(383, 0);
		btnCreate.Name = "btnCreate";
		btnCreate.Size = new Size(94, 29);
		btnCreate.TabIndex = 1;
		btnCreate.Text = "Добавить";
		btnCreate.UseVisualStyleBackColor = false;
		btnCreate.Click += BtnCreate_Click;
		// 
		// panel2
		// 
		panel2.Controls.Add(numericUpDown2);
		panel2.Controls.Add(numericUpDown1);
		panel2.Controls.Add(label10);
		panel2.Controls.Add(label9);
		panel2.Controls.Add(cbSortOrder);
		panel2.Controls.Add(label7);
		panel2.Controls.Add(cbCities);
		panel2.Controls.Add(cbDistricts);
		panel2.Controls.Add(label5);
		panel2.Controls.Add(cbRegions);
		panel2.Controls.Add(label4);
		panel2.Controls.Add(cbVenueTypes);
		panel2.Controls.Add(label3);
		panel2.Controls.Add(txtStreet);
		panel2.Controls.Add(label8);
		panel2.Controls.Add(txtName);
		panel2.Controls.Add(label6);
		panel2.Controls.Add(label2);
		panel2.Controls.Add(label1);
		panel2.Dock = DockStyle.Fill;
		panel2.Location = new Point(3, 3);
		panel2.Name = "panel2";
		panel2.Size = new Size(294, 424);
		panel2.TabIndex = 3;
		// 
		// numericUpDown2
		// 
		numericUpDown2.Location = new Point(178, 334);
		numericUpDown2.Name = "numericUpDown2";
		numericUpDown2.Size = new Size(113, 27);
		numericUpDown2.TabIndex = 6;
		// 
		// numericUpDown1
		// 
		numericUpDown1.Location = new Point(30, 334);
		numericUpDown1.Name = "numericUpDown1";
		numericUpDown1.Size = new Size(113, 27);
		numericUpDown1.TabIndex = 6;
		// 
		// label10
		// 
		label10.AutoSize = true;
		label10.Location = new Point(149, 338);
		label10.Name = "label10";
		label10.Size = new Size(26, 20);
		label10.TabIndex = 5;
		label10.Text = "до";
		// 
		// label9
		// 
		label9.AutoSize = true;
		label9.Location = new Point(3, 336);
		label9.Name = "label9";
		label9.Size = new Size(24, 20);
		label9.TabIndex = 5;
		label9.Text = "от";
		// 
		// cbSortOrder
		// 
		cbSortOrder.FormattingEnabled = true;
		cbSortOrder.Location = new Point(3, 393);
		cbSortOrder.Name = "cbSortOrder";
		cbSortOrder.Size = new Size(291, 28);
		cbSortOrder.TabIndex = 4;
		// 
		// label7
		// 
		label7.AutoSize = true;
		label7.Location = new Point(3, 370);
		label7.Name = "label7";
		label7.Size = new Size(121, 20);
		label7.TabIndex = 3;
		label7.Text = "Сортировать по";
		// 
		// cbCities
		// 
		cbCities.FormattingEnabled = true;
		cbCities.Location = new Point(0, 230);
		cbCities.Name = "cbCities";
		cbCities.Size = new Size(291, 28);
		cbCities.TabIndex = 2;
		// 
		// cbDistricts
		// 
		cbDistricts.FormattingEnabled = true;
		cbDistricts.Location = new Point(0, 178);
		cbDistricts.Name = "cbDistricts";
		cbDistricts.Size = new Size(291, 28);
		cbDistricts.TabIndex = 2;
		// 
		// label5
		// 
		label5.AutoSize = true;
		label5.Location = new Point(0, 208);
		label5.Name = "label5";
		label5.Size = new Size(51, 20);
		label5.TabIndex = 0;
		label5.Text = "Город";
		// 
		// cbRegions
		// 
		cbRegions.FormattingEnabled = true;
		cbRegions.Location = new Point(0, 126);
		cbRegions.Name = "cbRegions";
		cbRegions.Size = new Size(291, 28);
		cbRegions.TabIndex = 2;
		// 
		// label4
		// 
		label4.AutoSize = true;
		label4.Location = new Point(0, 156);
		label4.Name = "label4";
		label4.Size = new Size(52, 20);
		label4.TabIndex = 0;
		label4.Text = "Район";
		// 
		// cbVenueTypes
		// 
		cbVenueTypes.FormattingEnabled = true;
		cbVenueTypes.Location = new Point(0, 74);
		cbVenueTypes.Name = "cbVenueTypes";
		cbVenueTypes.Size = new Size(291, 28);
		cbVenueTypes.TabIndex = 2;
		// 
		// label3
		// 
		label3.AutoSize = true;
		label3.Location = new Point(0, 104);
		label3.Name = "label3";
		label3.Size = new Size(58, 20);
		label3.TabIndex = 0;
		label3.Text = "Регион";
		// 
		// txtStreet
		// 
		txtStreet.BorderStyle = BorderStyle.FixedSingle;
		txtStreet.Location = new Point(0, 282);
		txtStreet.Name = "txtStreet";
		txtStreet.Size = new Size(291, 27);
		txtStreet.TabIndex = 1;
		// 
		// label8
		// 
		label8.AutoSize = true;
		label8.Location = new Point(0, 311);
		label8.Name = "label8";
		label8.Size = new Size(64, 20);
		label8.TabIndex = 0;
		label8.Text = "Рейтинг";
		// 
		// txtName
		// 
		txtName.BorderStyle = BorderStyle.FixedSingle;
		txtName.Location = new Point(0, 23);
		txtName.Name = "txtName";
		txtName.Size = new Size(291, 27);
		txtName.TabIndex = 1;
		// 
		// label6
		// 
		label6.AutoSize = true;
		label6.Location = new Point(0, 260);
		label6.Name = "label6";
		label6.Size = new Size(52, 20);
		label6.TabIndex = 0;
		label6.Text = "Улица";
		// 
		// label2
		// 
		label2.AutoSize = true;
		label2.Location = new Point(0, 52);
		label2.Name = "label2";
		label2.Size = new Size(35, 20);
		label2.TabIndex = 0;
		label2.Text = "Тип";
		// 
		// label1
		// 
		label1.AutoSize = true;
		label1.Location = new Point(0, 0);
		label1.Name = "label1";
		label1.Size = new Size(77, 20);
		label1.TabIndex = 0;
		label1.Text = "Название";
		// 
		// btnApply
		// 
		btnApply.Dock = DockStyle.Fill;
		btnApply.FlatStyle = FlatStyle.Popup;
		btnApply.Location = new Point(3, 433);
		btnApply.Name = "btnApply";
		btnApply.Size = new Size(294, 29);
		btnApply.TabIndex = 4;
		btnApply.Text = "Применить";
		btnApply.UseVisualStyleBackColor = true;
		// 
		// tabPage2
		// 
		tabPage2.Location = new Point(4, 29);
		tabPage2.Name = "tabPage2";
		tabPage2.Padding = new Padding(3);
		tabPage2.Size = new Size(792, 471);
		tabPage2.TabIndex = 1;
		tabPage2.Text = "tabPage2";
		tabPage2.UseVisualStyleBackColor = true;
		// 
		// MainForm
		// 
		AutoScaleDimensions = new SizeF(8F, 20F);
		AutoScaleMode = AutoScaleMode.Font;
		ClientSize = new Size(800, 504);
		Controls.Add(tabControl1);
		Name = "MainForm";
		StartPosition = FormStartPosition.CenterScreen;
		Text = "Информационная система рекламной компании";
		FormClosed += MainForm_FormClosed;
		tabControl1.ResumeLayout(false);
		tabPage1.ResumeLayout(false);
		tableLayoutPanel2.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)venuesDataGridView).EndInit();
		panel1.ResumeLayout(false);
		panel2.ResumeLayout(false);
		panel2.PerformLayout();
		((System.ComponentModel.ISupportInitialize)numericUpDown2).EndInit();
		((System.ComponentModel.ISupportInitialize)numericUpDown1).EndInit();
		ResumeLayout(false);
	}

	#endregion

	private TabControl tabControl1;
	private TabPage tabPage1;
	private TabPage tabPage2;
	private Button btnCreate;
	private DataGridView venuesDataGridView;
	private TableLayoutPanel tableLayoutPanel2;
	private Panel panel1;
	private Button btnPrev;
	private Button btnNext;
	private Panel panel2;
	private TextBox txtName;
	private Label label2;
	private Label label1;
	private ComboBox cbVenueTypes;
	private ComboBox cbCities;
	private ComboBox cbDistricts;
	private Label label5;
	private ComboBox cbRegions;
	private Label label4;
	private Label label3;
	private TextBox txtStreet;
	private Label label6;
	private Button btnApply;
	private ComboBox cbSortOrder;
	private Label label7;
	private Label label10;
	private Label label9;
	private Label label8;
	private NumericUpDown numericUpDown2;
	private NumericUpDown numericUpDown1;
}