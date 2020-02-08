namespace SaaSUI
{
	partial class UpgradeCenter
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
			this.btnHome = new System.Windows.Forms.Button();
			this.btnConfirm = new System.Windows.Forms.Button();
			this.button3 = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.cmbEnvironment = new System.Windows.Forms.ComboBox();
			this.lblUpgradeCenter = new System.Windows.Forms.Label();
			this.txtUpgradeCenter = new System.Windows.Forms.TextBox();
			this.cmbClient = new System.Windows.Forms.ComboBox();
			this.cmbServer = new System.Windows.Forms.ComboBox();
			this.linkLabel4 = new System.Windows.Forms.LinkLabel();
			this.linkLabel3 = new System.Windows.Forms.LinkLabel();
			this.linkLabel2 = new System.Windows.Forms.LinkLabel();
			this.lnkMessages = new System.Windows.Forms.LinkLabel();
			this.cmbBaseVersion = new System.Windows.Forms.ComboBox();
			this.SuspendLayout();
			// 
			// btnHome
			// 
			this.btnHome.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnHome.Location = new System.Drawing.Point(341, 549);
			this.btnHome.Name = "btnHome";
			this.btnHome.Size = new System.Drawing.Size(75, 23);
			this.btnHome.TabIndex = 0;
			this.btnHome.Text = "Home";
			this.btnHome.UseVisualStyleBackColor = true;
			this.btnHome.Click += new System.EventHandler(this.btnHome_Click);
			// 
			// btnConfirm
			// 
			this.btnConfirm.Location = new System.Drawing.Point(576, 522);
			this.btnConfirm.Name = "btnConfirm";
			this.btnConfirm.Size = new System.Drawing.Size(75, 23);
			this.btnConfirm.TabIndex = 1;
			this.btnConfirm.Text = "Confirm";
			this.btnConfirm.UseVisualStyleBackColor = true;
			this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
			// 
			// button3
			// 
			this.button3.Location = new System.Drawing.Point(432, 549);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(75, 23);
			this.button3.TabIndex = 2;
			this.button3.Text = "button3";
			this.button3.UseVisualStyleBackColor = true;
			this.button3.Click += new System.EventHandler(this.button3_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.Location = new System.Drawing.Point(658, 522);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 3;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			// 
			// cmbEnvironment
			// 
			this.cmbEnvironment.FormattingEnabled = true;
			this.cmbEnvironment.Location = new System.Drawing.Point(12, 522);
			this.cmbEnvironment.Name = "cmbEnvironment";
			this.cmbEnvironment.Size = new System.Drawing.Size(162, 21);
			this.cmbEnvironment.TabIndex = 4;
			// 
			// lblUpgradeCenter
			// 
			this.lblUpgradeCenter.AutoSize = true;
			this.lblUpgradeCenter.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblUpgradeCenter.Location = new System.Drawing.Point(287, 8);
			this.lblUpgradeCenter.Name = "lblUpgradeCenter";
			this.lblUpgradeCenter.Size = new System.Drawing.Size(178, 25);
			this.lblUpgradeCenter.TabIndex = 5;
			this.lblUpgradeCenter.Text = "Upgrade Center";
			// 
			// txtUpgradeCenter
			// 
			this.txtUpgradeCenter.Location = new System.Drawing.Point(12, 36);
			this.txtUpgradeCenter.Multiline = true;
			this.txtUpgradeCenter.Name = "txtUpgradeCenter";
			this.txtUpgradeCenter.Size = new System.Drawing.Size(721, 480);
			this.txtUpgradeCenter.TabIndex = 6;
			// 
			// cmbClient
			// 
			this.cmbClient.FormattingEnabled = true;
			this.cmbClient.Location = new System.Drawing.Point(444, 522);
			this.cmbClient.Name = "cmbClient";
			this.cmbClient.Size = new System.Drawing.Size(126, 21);
			this.cmbClient.TabIndex = 7;
			// 
			// cmbServer
			// 
			this.cmbServer.FormattingEnabled = true;
			this.cmbServer.Location = new System.Drawing.Point(312, 522);
			this.cmbServer.Name = "cmbServer";
			this.cmbServer.Size = new System.Drawing.Size(126, 21);
			this.cmbServer.TabIndex = 8;
			// 
			// linkLabel4
			// 
			this.linkLabel4.AutoSize = true;
			this.linkLabel4.Location = new System.Drawing.Point(520, 6);
			this.linkLabel4.Name = "linkLabel4";
			this.linkLabel4.Size = new System.Drawing.Size(34, 13);
			this.linkLabel4.TabIndex = 10;
			this.linkLabel4.TabStop = true;
			this.linkLabel4.Text = "Errors";
			// 
			// linkLabel3
			// 
			this.linkLabel3.AutoSize = true;
			this.linkLabel3.Location = new System.Drawing.Point(595, 6);
			this.linkLabel3.Name = "linkLabel3";
			this.linkLabel3.Size = new System.Drawing.Size(67, 13);
			this.linkLabel3.TabIndex = 9;
			this.linkLabel3.TabStop = true;
			this.linkLabel3.Text = "Active Users";
			// 
			// linkLabel2
			// 
			this.linkLabel2.AutoSize = true;
			this.linkLabel2.Location = new System.Drawing.Point(222, 6);
			this.linkLabel2.Name = "linkLabel2";
			this.linkLabel2.Size = new System.Drawing.Size(47, 13);
			this.linkLabel2.TabIndex = 12;
			this.linkLabel2.TabStop = true;
			this.linkLabel2.Text = "Account";
			// 
			// lnkMessages
			// 
			this.lnkMessages.AutoSize = true;
			this.lnkMessages.Location = new System.Drawing.Point(128, 6);
			this.lnkMessages.Name = "lnkMessages";
			this.lnkMessages.Size = new System.Drawing.Size(55, 13);
			this.lnkMessages.TabIndex = 11;
			this.lnkMessages.TabStop = true;
			this.lnkMessages.Text = "Messages";
			// 
			// cmbBaseVersion
			// 
			this.cmbBaseVersion.FormattingEnabled = true;
			this.cmbBaseVersion.Location = new System.Drawing.Point(180, 522);
			this.cmbBaseVersion.Name = "cmbBaseVersion";
			this.cmbBaseVersion.Size = new System.Drawing.Size(126, 21);
			this.cmbBaseVersion.TabIndex = 13;
			this.cmbBaseVersion.SelectedIndexChanged += new System.EventHandler(this.cmbBaseVersion_SelectedIndexChanged);
			// 
			// UpgradeCenter
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(747, 576);
			this.Controls.Add(this.cmbBaseVersion);
			this.Controls.Add(this.linkLabel2);
			this.Controls.Add(this.lnkMessages);
			this.Controls.Add(this.linkLabel4);
			this.Controls.Add(this.linkLabel3);
			this.Controls.Add(this.cmbServer);
			this.Controls.Add(this.cmbClient);
			this.Controls.Add(this.txtUpgradeCenter);
			this.Controls.Add(this.lblUpgradeCenter);
			this.Controls.Add(this.cmbEnvironment);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.button3);
			this.Controls.Add(this.btnConfirm);
			this.Controls.Add(this.btnHome);
			this.Name = "UpgradeCenter";
			this.Text = "Upgrade Center";
			this.Load += new System.EventHandler(this.UpgradeCenter_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btnHome;
		private System.Windows.Forms.Button btnConfirm;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.ComboBox cmbEnvironment;
		private System.Windows.Forms.Label lblUpgradeCenter;
		private System.Windows.Forms.TextBox txtUpgradeCenter;
		private System.Windows.Forms.ComboBox cmbClient;
		private System.Windows.Forms.ComboBox cmbServer;
		private System.Windows.Forms.LinkLabel linkLabel4;
		private System.Windows.Forms.LinkLabel linkLabel3;
		private System.Windows.Forms.LinkLabel linkLabel2;
		private System.Windows.Forms.LinkLabel lnkMessages;
		private System.Windows.Forms.ComboBox cmbBaseVersion;
	}
}