namespace AdVision.Presentation
{
	partial class PositionForm
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
			pbValidation = new PictureBox();
			btnCreate = new Button();
			btnClose = new Button();
			((System.ComponentModel.ISupportInitialize)pbValidation).BeginInit();
			SuspendLayout();
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Location = new Point(12, 9);
			label1.Name = "label1";
			label1.Size = new Size(77, 20);
			label1.TabIndex = 0;
			label1.Text = "Название";
			// 
			// txtName
			// 
			txtName.BorderStyle = BorderStyle.FixedSingle;
			txtName.Location = new Point(12, 32);
			txtName.Name = "txtName";
			txtName.Size = new Size(274, 27);
			txtName.TabIndex = 1;
			txtName.TextChanged += TxtName_TextChanged;
			// 
			// pbValidation
			// 
			pbValidation.Location = new Point(292, 32);
			pbValidation.Name = "pbValidation";
			pbValidation.Size = new Size(27, 27);
			pbValidation.TabIndex = 2;
			pbValidation.TabStop = false;
			// 
			// btnCreate
			// 
			btnCreate.FlatStyle = FlatStyle.Popup;
			btnCreate.Location = new Point(12, 65);
			btnCreate.Name = "btnCreate";
			btnCreate.Size = new Size(150, 29);
			btnCreate.TabIndex = 3;
			btnCreate.Text = "Создать";
			btnCreate.UseVisualStyleBackColor = true;
			btnCreate.Click += BtnCreate_Click;
			// 
			// btnClose
			// 
			btnClose.FlatStyle = FlatStyle.Popup;
			btnClose.Location = new Point(169, 65);
			btnClose.Name = "btnClose";
			btnClose.Size = new Size(150, 29);
			btnClose.TabIndex = 3;
			btnClose.Text = "Закрыть";
			btnClose.UseVisualStyleBackColor = true;
			btnClose.Click += BtnClose_Click;
			// 
			// PositionForm
			// 
			AutoScaleDimensions = new SizeF(8F, 20F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(334, 113);
			Controls.Add(btnClose);
			Controls.Add(btnCreate);
			Controls.Add(pbValidation);
			Controls.Add(txtName);
			Controls.Add(label1);
			FormBorderStyle = FormBorderStyle.FixedSingle;
			MaximizeBox = false;
			MinimizeBox = false;
			Name = "PositionForm";
			StartPosition = FormStartPosition.CenterScreen;
			Text = "Создание новой должности";
			((System.ComponentModel.ISupportInitialize)pbValidation).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private Label label1;
		private TextBox txtName;
		private PictureBox pbValidation;
		private Button btnCreate;
		private Button btnClose;
	}
}