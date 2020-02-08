namespace SaaSUI
{
	partial class SignIn
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
			this.txtUser = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.txtPass = new System.Windows.Forms.TextBox();
			this.btnLogin = new System.Windows.Forms.Button();
			this.lblForgot = new System.Windows.Forms.LinkLabel();
			this.chkShow = new System.Windows.Forms.CheckBox();
			this.btnCancel = new System.Windows.Forms.Button();
			this.lnklblCreateUser = new System.Windows.Forms.LinkLabel();
			this.SuspendLayout();
			// 
			// txtUser
			// 
			this.txtUser.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtUser.Enabled = false;
			this.txtUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtUser.Location = new System.Drawing.Point(174, 13);
			this.txtUser.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.txtUser.Name = "txtUser";
			this.txtUser.Size = new System.Drawing.Size(293, 22);
			this.txtUser.TabIndex = 0;
			this.txtUser.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtUser_KeyPress);
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Rockwell", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(12, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(141, 27);
			this.label1.TabIndex = 0;
			this.label1.Text = "User Name:";
			// 
			// label2
			// 
			this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Rockwell", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.Location = new System.Drawing.Point(27, 51);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(126, 27);
			this.label2.TabIndex = 0;
			this.label2.Text = "Password:";
			// 
			// txtPass
			// 
			this.txtPass.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtPass.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtPass.Location = new System.Drawing.Point(174, 56);
			this.txtPass.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.txtPass.Name = "txtPass";
			this.txtPass.Size = new System.Drawing.Size(293, 22);
			this.txtPass.TabIndex = 1;
			this.txtPass.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPass_KeyPress);
			// 
			// btnLogin
			// 
			this.btnLogin.Font = new System.Drawing.Font("Rockwell", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnLogin.Location = new System.Drawing.Point(174, 86);
			this.btnLogin.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.btnLogin.Name = "btnLogin";
			this.btnLogin.Size = new System.Drawing.Size(75, 23);
			this.btnLogin.TabIndex = 2;
			this.btnLogin.Text = "Login";
			this.btnLogin.UseVisualStyleBackColor = true;
			this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
			// 
			// lblForgot
			// 
			this.lblForgot.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lblForgot.AutoSize = true;
			this.lblForgot.Location = new System.Drawing.Point(378, 90);
			this.lblForgot.Name = "lblForgot";
			this.lblForgot.Size = new System.Drawing.Size(100, 16);
			this.lblForgot.TabIndex = 5;
			this.lblForgot.TabStop = true;
			this.lblForgot.Text = "Forgot Password?";
			this.lblForgot.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblForgot_LinkClicked);
			// 
			// chkShow
			// 
			this.chkShow.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.chkShow.AutoSize = true;
			this.chkShow.Location = new System.Drawing.Point(47, 88);
			this.chkShow.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.chkShow.Name = "chkShow";
			this.chkShow.Size = new System.Drawing.Size(106, 20);
			this.chkShow.TabIndex = 4;
			this.chkShow.Text = "Show Password";
			this.chkShow.UseVisualStyleBackColor = true;
			this.chkShow.CheckedChanged += new System.EventHandler(this.chkShow_CheckedChanged);
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.Font = new System.Drawing.Font("Rockwell", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnCancel.Location = new System.Drawing.Point(287, 86);
			this.btnCancel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 3;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// lnklblCreateUser
			// 
			this.lnklblCreateUser.AutoSize = true;
			this.lnklblCreateUser.Location = new System.Drawing.Point(29, 35);
			this.lnklblCreateUser.Name = "lnklblCreateUser";
			this.lnklblCreateUser.Size = new System.Drawing.Size(69, 16);
			this.lnklblCreateUser.TabIndex = 6;
			this.lnklblCreateUser.TabStop = true;
			this.lnklblCreateUser.Text = "Create User";
			this.lnklblCreateUser.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnklblCreateUser_LinkClicked);
			// 
			// SignIn
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Control;
			this.ClientSize = new System.Drawing.Size(479, 115);
			this.Controls.Add(this.lnklblCreateUser);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.chkShow);
			this.Controls.Add(this.lblForgot);
			this.Controls.Add(this.btnLogin);
			this.Controls.Add(this.txtPass);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.txtUser);
			this.Font = new System.Drawing.Font("MV Boli", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.Name = "SignIn";
			this.Text = "GateKeeper Login";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SignIn_FormClosing);
			this.Load += new System.EventHandler(this.SignIn_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox txtUser;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox txtPass;
		private System.Windows.Forms.Button btnLogin;
		private System.Windows.Forms.LinkLabel lblForgot;
		private System.Windows.Forms.CheckBox chkShow;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.LinkLabel lnklblCreateUser;
	}
}