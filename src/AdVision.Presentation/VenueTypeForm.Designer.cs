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
			components = new System.ComponentModel.Container();
			label1 = new System.Windows.Forms.Label();
			txtName = new System.Windows.Forms.TextBox();
			btnSave = new System.Windows.Forms.Button();
			errorProvider1 = new System.Windows.Forms.ErrorProvider(components);
			((System.ComponentModel.ISupportInitialize)errorProvider1).BeginInit();
			SuspendLayout();
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(28, 15);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(151, 20);
			label1.TabIndex = 0;
			label1.Text = "Название площадки";
			// 
			// txtName
			// 
			txtName.Location = new System.Drawing.Point(28, 38);
			txtName.Name = "txtName";
			txtName.Size = new System.Drawing.Size(299, 27);
			txtName.TabIndex = 1;
			// 
			// btnSave
			// 
			btnSave.Location = new System.Drawing.Point(28, 71);
			btnSave.Name = "btnSave";
			btnSave.Size = new System.Drawing.Size(299, 29);
			btnSave.TabIndex = 2;
			btnSave.Text = "Сохранить";
			btnSave.UseVisualStyleBackColor = true;
			btnSave.Click += BtnSave_Click;
			// 
			// errorProvider1
			// 
			errorProvider1.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
			errorProvider1.ContainerControl = this;
			// 
			// VenueTypeForm
			// 
			AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
			AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			ClientSize = new System.Drawing.Size(355, 118);
			Controls.Add(btnSave);
			Controls.Add(txtName);
			Controls.Add(label1);
			MaximizeBox = false;
			MinimizeBox = false;
			StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			Text = "Новый тип рекламной площадки";
			FormClosed += VenueTypeForm_FormClosed;
			((System.ComponentModel.ISupportInitialize)errorProvider1).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private Label label1;
		private TextBox txtName;
		private Button btnSave;
		private ErrorProvider errorProvider1;
	}
}