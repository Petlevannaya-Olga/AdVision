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
			lblPaging = new Label();
			btnPrev = new Button();
			btnNext = new Button();
			btnAdd = new Button();
			SuspendLayout();
			// 
			// lblPaging
			// 
			lblPaging.AutoSize = true;
			lblPaging.Location = new Point(3, 9);
			lblPaging.Name = "lblPaging";
			lblPaging.Size = new Size(58, 20);
			lblPaging.TabIndex = 6;
			lblPaging.Text = "label11";
			// 
			// btnPrev
			// 
			btnPrev.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			btnPrev.BackColor = Color.Transparent;
			btnPrev.FlatStyle = FlatStyle.Popup;
			btnPrev.Location = new Point(276, 9);
			btnPrev.Name = "btnPrev";
			btnPrev.Size = new Size(44, 29);
			btnPrev.TabIndex = 7;
			btnPrev.Text = "<";
			btnPrev.UseVisualStyleBackColor = false;
			btnPrev.Click += BtnPrev_Click;
			// 
			// btnNext
			// 
			btnNext.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			btnNext.BackColor = Color.Transparent;
			btnNext.FlatStyle = FlatStyle.Popup;
			btnNext.Location = new Point(326, 9);
			btnNext.Name = "btnNext";
			btnNext.Size = new Size(44, 29);
			btnNext.TabIndex = 8;
			btnNext.Text = ">";
			btnNext.UseVisualStyleBackColor = false;
			btnNext.Click += BtnNext_Click;
			// 
			// btnAdd
			// 
			btnAdd.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			btnAdd.BackColor = Color.Transparent;
			btnAdd.FlatStyle = FlatStyle.Popup;
			btnAdd.Location = new Point(376, 9);
			btnAdd.Name = "btnAdd";
			btnAdd.Size = new Size(94, 29);
			btnAdd.TabIndex = 9;
			btnAdd.Text = "Добавить";
			btnAdd.UseVisualStyleBackColor = false;
			btnAdd.Click += BtnAdd_Click;
			// 
			// PagingUserControl
			// 
			AutoScaleDimensions = new SizeF(8F, 20F);
			AutoScaleMode = AutoScaleMode.Font;
			Controls.Add(btnPrev);
			Controls.Add(btnNext);
			Controls.Add(btnAdd);
			Controls.Add(lblPaging);
			Name = "PagingUserControl";
			Size = new Size(483, 47);
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private Label lblPaging;
		private Button btnPrev;
		private Button btnNext;
		private Button btnAdd;
	}
}
