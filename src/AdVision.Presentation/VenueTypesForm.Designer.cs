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
			button1 = new Button();
			listView1 = new ListView();
			SuspendLayout();
			// 
			// button1
			// 
			button1.Location = new Point(12, 206);
			button1.Name = "button1";
			button1.Size = new Size(333, 29);
			button1.TabIndex = 1;
			button1.Text = "Добавить";
			button1.UseVisualStyleBackColor = true;
			// 
			// listView1
			// 
			listView1.Location = new Point(12, 12);
			listView1.Name = "listView1";
			listView1.Size = new Size(333, 188);
			listView1.TabIndex = 2;
			listView1.UseCompatibleStateImageBehavior = false;
			// 
			// VenueTypeForm
			// 
			AutoScaleDimensions = new SizeF(8F, 20F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(357, 249);
			Controls.Add(listView1);
			Controls.Add(button1);
			Name = "VenueTypeForm";
			Text = "Типы рекламных площадок";
			ResumeLayout(false);
		}

		#endregion

		private Button button1;
		private ListView listView1;
	}
}