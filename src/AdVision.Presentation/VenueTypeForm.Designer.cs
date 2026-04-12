namespace AdVision.Presentation
{
	partial class VenueTypeForm
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
			btnSave = new Button();
			pbValidation = new PictureBox();
			((System.ComponentModel.ISupportInitialize)pbValidation).BeginInit();
			SuspendLayout();
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Location = new Point(28, 15);
			label1.Name = "label1";
			label1.Size = new Size(240, 20);
			label1.TabIndex = 0;
			label1.Text = "Название (от 3 до 100 символов)";
			// 
			// txtName
			// 
			txtName.BorderStyle = BorderStyle.FixedSingle;
			txtName.Location = new Point(28, 38);
			txtName.Name = "txtName";
			txtName.Size = new Size(282, 27);
			txtName.TabIndex = 1;
			txtName.TextChanged += TxtName_TextChanged;
			// 
			// btnSave
			// 
			btnSave.FlatStyle = FlatStyle.Popup;
			btnSave.Location = new Point(28, 71);
			btnSave.Name = "btnSave";
			btnSave.Size = new Size(282, 29);
			btnSave.TabIndex = 2;
			btnSave.Text = "Сохранить";
			btnSave.UseVisualStyleBackColor = true;
			btnSave.Click += BtnSave_Click;
			// 
			// pbValidation
			// 
			pbValidation.Location = new Point(316, 38);
			pbValidation.Name = "pbValidation";
			pbValidation.Size = new Size(27, 27);
			pbValidation.TabIndex = 3;
			pbValidation.TabStop = false;
			// 
			// VenueTypeForm
			// 
			AutoScaleDimensions = new SizeF(8F, 20F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(355, 118);
			Controls.Add(pbValidation);
			Controls.Add(btnSave);
			Controls.Add(txtName);
			Controls.Add(label1);
			FormBorderStyle = FormBorderStyle.FixedSingle;
			MaximizeBox = false;
			MinimizeBox = false;
			Name = "VenueTypeForm";
			StartPosition = FormStartPosition.CenterScreen;
			Text = "Новый тип рекламной площадки";
			FormClosed += VenueTypeForm_FormClosed;
			((System.ComponentModel.ISupportInitialize)pbValidation).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private Label label1;
		private System.Windows.Forms.TextBox txtName;
		private Button btnSave;
		private PictureBox pbValidation;
	}
}