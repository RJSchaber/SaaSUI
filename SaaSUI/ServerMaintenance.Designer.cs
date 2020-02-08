namespace SaaSUI
{
	partial class ServerMaintenance
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
			this.btnConfirm = new System.Windows.Forms.Button();
			this.btnClear = new System.Windows.Forms.Button();
			this.btnHome = new System.Windows.Forms.Button();
			this.txtMaintenanceWindow = new System.Windows.Forms.TextBox();
			this.chkbxInactive = new System.Windows.Forms.CheckBox();
			this.cmbClients = new System.Windows.Forms.ComboBox();
			this.cmbCommands = new System.Windows.Forms.ComboBox();
			this.cmbMiddleTiers = new System.Windows.Forms.ComboBox();
			this.linkLabel4 = new System.Windows.Forms.LinkLabel();
			this.linkLabel3 = new System.Windows.Forms.LinkLabel();
			this.linkLabel2 = new System.Windows.Forms.LinkLabel();
			this.lblMaintenance = new System.Windows.Forms.Label();
			this.checkBox1 = new System.Windows.Forms.CheckBox();
			this.btnStop = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// btnConfirm
			// 
			this.btnConfirm.Location = new System.Drawing.Point(387, 404);
			this.btnConfirm.Name = "btnConfirm";
			this.btnConfirm.Size = new System.Drawing.Size(75, 23);
			this.btnConfirm.TabIndex = 4;
			this.btnConfirm.Text = "Confirm";
			this.btnConfirm.UseVisualStyleBackColor = true;
			this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
			// 
			// btnClear
			// 
			this.btnClear.Location = new System.Drawing.Point(522, 15);
			this.btnClear.Name = "btnClear";
			this.btnClear.Size = new System.Drawing.Size(40, 23);
			this.btnClear.TabIndex = 5;
			this.btnClear.Text = "Clear";
			this.btnClear.UseVisualStyleBackColor = true;
			this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
			// 
			// btnHome
			// 
			this.btnHome.Location = new System.Drawing.Point(537, 404);
			this.btnHome.Name = "btnHome";
			this.btnHome.Size = new System.Drawing.Size(75, 23);
			this.btnHome.TabIndex = 6;
			this.btnHome.Text = "Home";
			this.btnHome.UseVisualStyleBackColor = true;
			this.btnHome.Click += new System.EventHandler(this.btnHome_Click);
			// 
			// txtMaintenanceWindow
			// 
			this.txtMaintenanceWindow.BackColor = System.Drawing.SystemColors.InactiveBorder;
			this.txtMaintenanceWindow.Location = new System.Drawing.Point(17, 41);
			this.txtMaintenanceWindow.Multiline = true;
			this.txtMaintenanceWindow.Name = "txtMaintenanceWindow";
			this.txtMaintenanceWindow.ReadOnly = true;
			this.txtMaintenanceWindow.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.txtMaintenanceWindow.Size = new System.Drawing.Size(594, 360);
			this.txtMaintenanceWindow.TabIndex = 0;
			this.txtMaintenanceWindow.TabStop = false;
			// 
			// chkbxInactive
			// 
			this.chkbxInactive.AutoSize = true;
			this.chkbxInactive.Location = new System.Drawing.Point(433, 433);
			this.chkbxInactive.Name = "chkbxInactive";
			this.chkbxInactive.Size = new System.Drawing.Size(178, 17);
			this.chkbxInactive.TabIndex = 8;
			this.chkbxInactive.Text = "Switch to Inactive Environments";
			this.chkbxInactive.UseVisualStyleBackColor = true;
			this.chkbxInactive.CheckedChanged += new System.EventHandler(this.chkbxInactive_CheckedChanged);
			// 
			// cmbClients
			// 
			this.cmbClients.FormattingEnabled = true;
			this.cmbClients.Location = new System.Drawing.Point(17, 405);
			this.cmbClients.Name = "cmbClients";
			this.cmbClients.Size = new System.Drawing.Size(120, 21);
			this.cmbClients.TabIndex = 1;
			this.cmbClients.SelectedIndexChanged += new System.EventHandler(this.cmbClients_SelectedIndexChanged);
			// 
			// cmbCommands
			// 
			this.cmbCommands.FormattingEnabled = true;
			this.cmbCommands.Location = new System.Drawing.Point(262, 405);
			this.cmbCommands.Name = "cmbCommands";
			this.cmbCommands.Size = new System.Drawing.Size(125, 21);
			this.cmbCommands.TabIndex = 3;
			// 
			// cmbMiddleTiers
			// 
			this.cmbMiddleTiers.FormattingEnabled = true;
			this.cmbMiddleTiers.Location = new System.Drawing.Point(139, 405);
			this.cmbMiddleTiers.Name = "cmbMiddleTiers";
			this.cmbMiddleTiers.Size = new System.Drawing.Size(121, 21);
			this.cmbMiddleTiers.TabIndex = 2;
			// 
			// linkLabel4
			// 
			this.linkLabel4.AutoSize = true;
			this.linkLabel4.Location = new System.Drawing.Point(577, 0);
			this.linkLabel4.Name = "linkLabel4";
			this.linkLabel4.Size = new System.Drawing.Size(34, 13);
			this.linkLabel4.TabIndex = 11;
			this.linkLabel4.TabStop = true;
			this.linkLabel4.Text = "Errors";
			// 
			// linkLabel3
			// 
			this.linkLabel3.AutoSize = true;
			this.linkLabel3.Location = new System.Drawing.Point(436, 0);
			this.linkLabel3.Name = "linkLabel3";
			this.linkLabel3.Size = new System.Drawing.Size(67, 13);
			this.linkLabel3.TabIndex = 10;
			this.linkLabel3.TabStop = true;
			this.linkLabel3.Text = "Active Users";
			// 
			// linkLabel2
			// 
			this.linkLabel2.AutoSize = true;
			this.linkLabel2.Location = new System.Drawing.Point(22, 0);
			this.linkLabel2.Name = "linkLabel2";
			this.linkLabel2.Size = new System.Drawing.Size(47, 13);
			this.linkLabel2.TabIndex = 9;
			this.linkLabel2.TabStop = true;
			this.linkLabel2.Text = "Account";
			// 
			// lblMaintenance
			// 
			this.lblMaintenance.AutoSize = true;
			this.lblMaintenance.Font = new System.Drawing.Font("Modern No. 20", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblMaintenance.Location = new System.Drawing.Point(188, 2);
			this.lblMaintenance.Name = "lblMaintenance";
			this.lblMaintenance.Size = new System.Drawing.Size(209, 36);
			this.lblMaintenance.TabIndex = 16;
			this.lblMaintenance.Text = "Maintenance";
			// 
			// checkBox1
			// 
			this.checkBox1.AutoSize = true;
			this.checkBox1.Location = new System.Drawing.Point(278, 433);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.Size = new System.Drawing.Size(149, 17);
			this.checkBox1.TabIndex = 7;
			this.checkBox1.Text = "Switch to SQL Commands";
			this.checkBox1.UseVisualStyleBackColor = true;
			// 
			// btnStop
			// 
			this.btnStop.Location = new System.Drawing.Point(387, 404);
			this.btnStop.Name = "btnStop";
			this.btnStop.Size = new System.Drawing.Size(75, 23);
			this.btnStop.TabIndex = 17;
			this.btnStop.Text = "Stop";
			this.btnStop.UseVisualStyleBackColor = true;
			this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
			// 
			// ServerMaintenance
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(630, 452);
			this.Controls.Add(this.btnStop);
			this.Controls.Add(this.checkBox1);
			this.Controls.Add(this.lblMaintenance);
			this.Controls.Add(this.linkLabel4);
			this.Controls.Add(this.linkLabel3);
			this.Controls.Add(this.linkLabel2);
			this.Controls.Add(this.cmbMiddleTiers);
			this.Controls.Add(this.cmbCommands);
			this.Controls.Add(this.cmbClients);
			this.Controls.Add(this.chkbxInactive);
			this.Controls.Add(this.txtMaintenanceWindow);
			this.Controls.Add(this.btnHome);
			this.Controls.Add(this.btnClear);
			this.Controls.Add(this.btnConfirm);
			this.Name = "ServerMaintenance";
			this.Text = "Server Maintenance";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ServerMaintenance_FormClosing);
			this.Load += new System.EventHandler(this.ServerMaintenance_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.Button btnConfirm;
		private System.Windows.Forms.Button btnClear;
		private System.Windows.Forms.Button btnHome;
		private System.Windows.Forms.TextBox txtMaintenanceWindow;
		private System.Windows.Forms.CheckBox chkbxInactive;
		private System.Windows.Forms.ComboBox cmbClients;
		private System.Windows.Forms.ComboBox cmbCommands;
		private System.Windows.Forms.ComboBox cmbMiddleTiers;
		private System.Windows.Forms.LinkLabel linkLabel4;
		private System.Windows.Forms.LinkLabel linkLabel3;
		private System.Windows.Forms.LinkLabel linkLabel2;
		private System.Windows.Forms.Label lblMaintenance;
		private System.Windows.Forms.CheckBox checkBox1;
		private System.Windows.Forms.Button btnStop;
	}
}