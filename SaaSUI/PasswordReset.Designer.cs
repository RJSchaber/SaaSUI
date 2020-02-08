namespace SaaSUI
{
	partial class PasswordReset
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
			this.btnOK = new System.Windows.Forms.Button();
			this.lblOldPW = new System.Windows.Forms.Label();
			this.lblNewPW = new System.Windows.Forms.Label();
			this.txtRetypePW = new System.Windows.Forms.TextBox();
			this.txtNewPW = new System.Windows.Forms.TextBox();
			this.txtOldPW = new System.Windows.Forms.TextBox();
			this.lblRetypePW = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// btnOK
			// 
			this.btnOK.Location = new System.Drawing.Point(304, 130);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(75, 23);
			this.btnOK.TabIndex = 3;
			this.btnOK.Text = "Confirm";
			this.btnOK.UseVisualStyleBackColor = true;
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// lblOldPW
			// 
			this.lblOldPW.AutoSize = true;
			this.lblOldPW.Location = new System.Drawing.Point(12, 35);
			this.lblOldPW.Name = "lblOldPW";
			this.lblOldPW.Size = new System.Drawing.Size(75, 13);
			this.lblOldPW.TabIndex = 1;
			this.lblOldPW.Text = "Old Password:";
			// 
			// lblNewPW
			// 
			this.lblNewPW.AutoSize = true;
			this.lblNewPW.Location = new System.Drawing.Point(12, 61);
			this.lblNewPW.Name = "lblNewPW";
			this.lblNewPW.Size = new System.Drawing.Size(81, 13);
			this.lblNewPW.TabIndex = 2;
			this.lblNewPW.Text = "New Password:";
			// 
			// txtRetypePW
			// 
			this.txtRetypePW.Location = new System.Drawing.Point(120, 84);
			this.txtRetypePW.Name = "txtRetypePW";
			this.txtRetypePW.Size = new System.Drawing.Size(259, 20);
			this.txtRetypePW.TabIndex = 2;
			this.txtRetypePW.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtRetypePW_KeyPress);
			// 
			// txtNewPW
			// 
			this.txtNewPW.Location = new System.Drawing.Point(120, 58);
			this.txtNewPW.Name = "txtNewPW";
			this.txtNewPW.Size = new System.Drawing.Size(259, 20);
			this.txtNewPW.TabIndex = 1;
			this.txtNewPW.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNewPW_KeyPress);
			// 
			// txtOldPW
			// 
			this.txtOldPW.Location = new System.Drawing.Point(120, 32);
			this.txtOldPW.Name = "txtOldPW";
			this.txtOldPW.Size = new System.Drawing.Size(259, 20);
			this.txtOldPW.TabIndex = 0;
			this.txtOldPW.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtOldPW_KeyPress);
			// 
			// lblRetypePW
			// 
			this.lblRetypePW.AutoSize = true;
			this.lblRetypePW.Location = new System.Drawing.Point(12, 87);
			this.lblRetypePW.Name = "lblRetypePW";
			this.lblRetypePW.Size = new System.Drawing.Size(93, 13);
			this.lblRetypePW.TabIndex = 6;
			this.lblRetypePW.Text = "Retype Password:";
			// 
			// PasswordReset
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(411, 165);
			this.Controls.Add(this.lblRetypePW);
			this.Controls.Add(this.txtOldPW);
			this.Controls.Add(this.txtNewPW);
			this.Controls.Add(this.txtRetypePW);
			this.Controls.Add(this.lblNewPW);
			this.Controls.Add(this.lblOldPW);
			this.Controls.Add(this.btnOK);
			this.Name = "PasswordReset";
			this.Text = "PasswordReset";
			this.Load += new System.EventHandler(this.PasswordReset_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Label lblOldPW;
		private System.Windows.Forms.Label lblNewPW;
		private System.Windows.Forms.TextBox txtRetypePW;
		private System.Windows.Forms.TextBox txtNewPW;
		private System.Windows.Forms.TextBox txtOldPW;
		private System.Windows.Forms.Label lblRetypePW;
	}
}