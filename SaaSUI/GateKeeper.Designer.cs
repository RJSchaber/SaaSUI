namespace SaaSUI
{
	partial class GateKeeper
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
			this.components = new System.ComponentModel.Container();
			this.dtgrdUpdates = new System.Windows.Forms.DataGridView();
			this.txtUpdate = new System.Windows.Forms.TextBox();
			this.btnSend = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.lnkConfigurations = new System.Windows.Forms.LinkLabel();
			this.linkLabel2 = new System.Windows.Forms.LinkLabel();
			this.linkLabel3 = new System.Windows.Forms.LinkLabel();
			this.linkLabel4 = new System.Windows.Forms.LinkLabel();
			this.btnServMaint = new System.Windows.Forms.Button();
			this.btnUpgrades = new System.Windows.Forms.Button();
			this.button4 = new System.Windows.Forms.Button();
			this.cmbClient = new System.Windows.Forms.ComboBox();
			this.bindUpdates = new System.Windows.Forms.BindingSource(this.components);
			this.btnHandoff = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.dtgrdUpdates)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.bindUpdates)).BeginInit();
			this.SuspendLayout();
			// 
			// dtgrdUpdates
			// 
			this.dtgrdUpdates.AllowUserToOrderColumns = true;
			this.dtgrdUpdates.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dtgrdUpdates.Location = new System.Drawing.Point(16, 31);
			this.dtgrdUpdates.Name = "dtgrdUpdates";
			this.dtgrdUpdates.Size = new System.Drawing.Size(695, 459);
			this.dtgrdUpdates.TabIndex = 0;
			// 
			// txtUpdate
			// 
			this.txtUpdate.Location = new System.Drawing.Point(143, 496);
			this.txtUpdate.Name = "txtUpdate";
			this.txtUpdate.Size = new System.Drawing.Size(487, 20);
			this.txtUpdate.TabIndex = 1;
			this.txtUpdate.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtUpdate_KeyPress);
			// 
			// btnSend
			// 
			this.btnSend.Location = new System.Drawing.Point(636, 494);
			this.btnSend.Name = "btnSend";
			this.btnSend.Size = new System.Drawing.Size(75, 23);
			this.btnSend.TabIndex = 2;
			this.btnSend.Text = "Send";
			this.btnSend.UseVisualStyleBackColor = true;
			this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Rockwell", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(296, -5);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(122, 33);
			this.label1.TabIndex = 3;
			this.label1.Text = "Updates";
			this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// lnkConfigurations
			// 
			this.lnkConfigurations.AutoSize = true;
			this.lnkConfigurations.Location = new System.Drawing.Point(56, 0);
			this.lnkConfigurations.Name = "lnkConfigurations";
			this.lnkConfigurations.Size = new System.Drawing.Size(74, 13);
			this.lnkConfigurations.TabIndex = 4;
			this.lnkConfigurations.TabStop = true;
			this.lnkConfigurations.Text = "Configurations";
			// 
			// linkLabel2
			// 
			this.linkLabel2.AutoSize = true;
			this.linkLabel2.Location = new System.Drawing.Point(178, 0);
			this.linkLabel2.Name = "linkLabel2";
			this.linkLabel2.Size = new System.Drawing.Size(47, 13);
			this.linkLabel2.TabIndex = 5;
			this.linkLabel2.TabStop = true;
			this.linkLabel2.Text = "Account";
			// 
			// linkLabel3
			// 
			this.linkLabel3.AutoSize = true;
			this.linkLabel3.Location = new System.Drawing.Point(478, 0);
			this.linkLabel3.Name = "linkLabel3";
			this.linkLabel3.Size = new System.Drawing.Size(67, 13);
			this.linkLabel3.TabIndex = 6;
			this.linkLabel3.TabStop = true;
			this.linkLabel3.Text = "Active Users";
			// 
			// linkLabel4
			// 
			this.linkLabel4.AutoSize = true;
			this.linkLabel4.Location = new System.Drawing.Point(621, 0);
			this.linkLabel4.Name = "linkLabel4";
			this.linkLabel4.Size = new System.Drawing.Size(34, 13);
			this.linkLabel4.TabIndex = 7;
			this.linkLabel4.TabStop = true;
			this.linkLabel4.Text = "Errors";
			// 
			// btnServMaint
			// 
			this.btnServMaint.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnServMaint.Location = new System.Drawing.Point(85, 522);
			this.btnServMaint.Name = "btnServMaint";
			this.btnServMaint.Size = new System.Drawing.Size(75, 23);
			this.btnServMaint.TabIndex = 8;
			this.btnServMaint.Text = "Server Maintenance";
			this.btnServMaint.UseVisualStyleBackColor = true;
			this.btnServMaint.Click += new System.EventHandler(this.servermaintenance_Click);
			// 
			// btnUpgrades
			// 
			this.btnUpgrades.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnUpgrades.Location = new System.Drawing.Point(233, 522);
			this.btnUpgrades.Name = "btnUpgrades";
			this.btnUpgrades.Size = new System.Drawing.Size(75, 23);
			this.btnUpgrades.TabIndex = 9;
			this.btnUpgrades.Text = "Upgrades";
			this.btnUpgrades.UseVisualStyleBackColor = true;
			this.btnUpgrades.Click += new System.EventHandler(this.btnUpgrades_Click);
			// 
			// button4
			// 
			this.button4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.button4.Location = new System.Drawing.Point(542, 522);
			this.button4.Name = "button4";
			this.button4.Size = new System.Drawing.Size(75, 23);
			this.button4.TabIndex = 10;
			this.button4.Text = "Work Center";
			this.button4.UseVisualStyleBackColor = true;
			// 
			// cmbClient
			// 
			this.cmbClient.FormattingEnabled = true;
			this.cmbClient.Location = new System.Drawing.Point(16, 496);
			this.cmbClient.Name = "cmbClient";
			this.cmbClient.Size = new System.Drawing.Size(121, 21);
			this.cmbClient.TabIndex = 11;
			// 
			// btnHandoff
			// 
			this.btnHandoff.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnHandoff.Location = new System.Drawing.Point(386, 522);
			this.btnHandoff.Name = "btnHandoff";
			this.btnHandoff.Size = new System.Drawing.Size(75, 23);
			this.btnHandoff.TabIndex = 12;
			this.btnHandoff.Text = "Handoff";
			this.btnHandoff.UseVisualStyleBackColor = true;
			this.btnHandoff.Click += new System.EventHandler(this.btnHandoff_Click);
			// 
			// GateKeeper
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(730, 553);
			this.Controls.Add(this.btnHandoff);
			this.Controls.Add(this.cmbClient);
			this.Controls.Add(this.button4);
			this.Controls.Add(this.btnUpgrades);
			this.Controls.Add(this.btnServMaint);
			this.Controls.Add(this.linkLabel4);
			this.Controls.Add(this.linkLabel3);
			this.Controls.Add(this.linkLabel2);
			this.Controls.Add(this.lnkConfigurations);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.btnSend);
			this.Controls.Add(this.txtUpdate);
			this.Controls.Add(this.dtgrdUpdates);
			this.Name = "GateKeeper";
			this.Text = "SaaS GateKeeper";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.GateKeeper_FormClosing);
			this.Load += new System.EventHandler(this.GateKeeper_Load);
			((System.ComponentModel.ISupportInitialize)(this.dtgrdUpdates)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.bindUpdates)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.DataGridView dtgrdUpdates;
		private System.Windows.Forms.TextBox txtUpdate;
		private System.Windows.Forms.Button btnSend;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.LinkLabel lnkConfigurations;
		private System.Windows.Forms.LinkLabel linkLabel2;
		private System.Windows.Forms.LinkLabel linkLabel3;
		private System.Windows.Forms.LinkLabel linkLabel4;
		private System.Windows.Forms.Button btnServMaint;
		private System.Windows.Forms.Button btnUpgrades;
		private System.Windows.Forms.Button button4;
		private System.Windows.Forms.ComboBox cmbClient;
		private System.Windows.Forms.BindingSource bindUpdates;
		private System.Windows.Forms.Button btnHandoff;
	}
}

