using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.DataAccess.Client;
using System.Management;
using System.Data.OleDb;
using System.IO;


namespace SaaSUI
{
	public partial class UpgradeCenter : Form
	{
		OracleConnection testConn; 
		string QADB5;
		string currentUser;
		DataTable clientsTable;

		public UpgradeCenter(DataTable clientstable, string currentuser)
		{	
			clientsTable = clientstable;
			currentUser = currentuser;
			InitializeComponent();
		}

		private void UpgradeCenter_Load(object sender, EventArgs e)
		{
			//testConn = new OracleConnection(); 
			//QADB5 = "(DESCRIPTION =(ADDRESS = (PROTOCOL = TCP)(HOST = phsrdb2.SFSC.Corp)(PORT = 1521))(CONNECT_DATA =(SERVER = dedicated)(SERVICE_NAME = qadb5)))";
			//testConn.ConnectionString = "User Id=RSCHABER;Password=RSCHABER;Data Source="+QADB5; 
			PopulateCombos();
		}

		private void btnHome_Click(object sender, EventArgs e)
		{
			Hide();
			new GateKeeper().Show();  
		}

		private void button3_Click(object sender, EventArgs e)
		{
			testConn.Open(); 
			txtUpgradeCenter.Text += "Connected to Oracle" + testConn.ServerVersion + Environment.NewLine;
			txtUpgradeCenter.Text += "This seems to work fine";
			testConn.Close();
		}

		protected OleDbConnection GetConnection()
		{
			return new OleDbConnection (@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\rschaber\Desktop\SSDev\LoginDB.accdb; 
				Persist Security Info=False;");
		}

		public class BaseVersion
		{
		     public string Name { get; set; }
		     public string Value { get; set; }
		     public override string ToString() { return Name; }
		}

		private void PopulateCombos()
		{
			cmbEnvironment.DataSource = clientsTable;
			cmbEnvironment.DisplayMember = "DisplayName";
			cmbEnvironment.ValueMember = "ClientID";
			cmbEnvironment.DropDownStyle = ComboBoxStyle.DropDownList;

			var baseVerSource = new List<BaseVersion>();

			baseVerSource.Add(new BaseVersion() {Name = "Base Version", Value = null });
			baseVerSource.Add(new BaseVersion() { Name = "10.5", Value = "1" });
			baseVerSource.Add(new BaseVersion() { Name = "10.6", Value = "2" });
			baseVerSource.Add(new BaseVersion() { Name = "10.7", Value = "3" });
			baseVerSource.Add(new BaseVersion() { Name = "10.8", Value = "4" });

			cmbBaseVersion.DataSource = baseVerSource;
			cmbBaseVersion.DisplayMember = "Name";
			cmbBaseVersion.ValueMember = "Value";
			cmbBaseVersion.DropDownStyle = ComboBoxStyle.DropDownList;

		}

		private void PullServerMSI(string selectedBase)
		{
			DirectoryInfo serverMSIDir = new DirectoryInfo(@"S:\ShadoSuite_Installations\x64\SHADOWSUITE\Server");
			DirectoryInfo[] serverSubs = serverMSIDir.GetDirectories(selectedBase +"*",SearchOption.TopDirectoryOnly);

			var serverMSI = new List<BaseVersion>();
			serverMSI.Add(new BaseVersion() {Name = "Server", Value = null });

			foreach(DirectoryInfo dir in serverSubs)
			{
				string msiVersion = dir.Name;
				string msiLocation = dir.FullName;
				serverMSI.Add(new BaseVersion() { Name = msiVersion, Value = msiLocation});
			}

			cmbServer.DataSource = serverMSI;
			cmbServer.DisplayMember = "Name";
			cmbServer.ValueMember = "Value";
			cmbServer.DropDownStyle = ComboBoxStyle.DropDownList;
		}

		private void PullClientMSI(string selectedBase)
		{
			DirectoryInfo clientMSIDir = new DirectoryInfo(@"S:\ShadoSuite_Installations\x64\SHADOWSUITE\Client");
			DirectoryInfo[] clientSubs = clientMSIDir.GetDirectories(selectedBase +"*",SearchOption.TopDirectoryOnly);

			var clientMSI = new List<BaseVersion>();
			clientMSI.Add(new BaseVersion() {Name = "Client", Value = null });

			foreach(DirectoryInfo dir in clientSubs)
			{
				string msiVersion = dir.Name;
				string msiLocation = dir.FullName;
				clientMSI.Add(new BaseVersion() { Name = msiVersion, Value = msiLocation});
			}

			cmbClient.DataSource = clientMSI;
			cmbClient.DisplayMember = "Name";
			cmbClient.ValueMember = "Value";
			cmbClient.DropDownStyle = ComboBoxStyle.DropDownList;
			
		}

		private void cmbBaseVersion_SelectedIndexChanged(object sender, EventArgs e)
		{
			ComboBox cmbBaseVersion = (ComboBox) sender;
			BaseVersion selectedBase = (BaseVersion)cmbBaseVersion.SelectedItem;
			
			string selection = Convert.ToString(selectedBase);
			txtUpgradeCenter.Text += "Selected Base Version: " + selection + Environment.NewLine;

			PullClientMSI(selection);
			PullServerMSI(selection);
		}

		private void btnConfirm_Click(object sender, EventArgs e)
		{
			txtUpgradeCenter.Text += "Selected Base Version: " + cmbBaseVersion.SelectedItem + Environment.NewLine;
			txtUpgradeCenter.Text += "Selected Server Version: " + cmbServer.SelectedItem + "   MSI Location: " + cmbServer.SelectedValue + Environment.NewLine;
			txtUpgradeCenter.Text += "Selected Client Version: " + cmbClient.SelectedItem + "   MSI Location: " + cmbClient.SelectedValue + Environment.NewLine;
		}

		private void PullEnvironmentInfo()
		{
			string userSelection = Convert.ToString(cmbEnvironment.SelectedValue);

		}
	}
}
