namespace AdVision.Presentation
{
	partial class VenueTypesForm
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
			btnCreate = new Button();
			lvVenueTypes = new ListView();
			SuspendLayout();
			// 
			// btnCreate
			// 
			btnCreate.Location = new Point(12, 206);
			btnCreate.Name = "btnCreate";
			btnCreate.Size = new Size(333, 29);
			btnCreate.TabIndex = 1;
			btnCreate.Text = "Добавить";
			btnCreate.UseVisualStyleBackColor = true;
			btnCreate.Click += BtnCreate_Click;
			// 
			// lvVenueTypes
			// 
			lvVenueTypes.GridLines = true;
			lvVenueTypes.Location = new Point(12, 12);
			lvVenueTypes.Name = "lvVenueTypes";
			lvVenueTypes.Size = new Size(333, 188);
			lvVenueTypes.TabIndex = 2;
			lvVenueTypes.TileSize = new Size(268, 25);
			lvVenueTypes.UseCompatibleStateImageBehavior = false;
			lvVenueTypes.View = View.Tile;
			// 
			// VenueTypesForm
			// 
			AutoScaleDimensions = new SizeF(8F, 20F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(357, 249);
			Controls.Add(lvVenueTypes);
			Controls.Add(btnCreate);
			MaximizeBox = false;
			MinimizeBox = false;
			Name = "VenueTypesForm";
			StartPosition = FormStartPosition.CenterScreen;
			Text = "Типы рекламных площадок";
			ResumeLayout(false);
		}

		#endregion

		private Button btnCreate;
		private ListView lvVenueTypes;
	}
}