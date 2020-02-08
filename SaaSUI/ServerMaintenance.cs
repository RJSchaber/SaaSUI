using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Diagnostics;
using System.Threading;
using System.ServiceProcess;
using System.Management;
using System.Management.Instrumentation;
using System.Reflection;
using System.Runtime.Serialization;

namespace SaaSUI
{
	public partial class ServerMaintenance : Form
	{
        //variable declarations

		DataTable clientsTable;
		DataTable mtTable;
		List<PopulateCombos> commandDMSource;
		List<PopulateCombos> commandSource;
		DataTable oldClientsTable;
		OleDbDataAdapter clientsAdapter;
		BackgroundWorker worker;
		bool isDisRec;
		public ServerCommands servercommands {get; set; }
		string selectedCommand;
		public ServerMaintenance()
		{
			try
			{
			
             //connect to the database in a try catch block.

			    using (var appLogin = GetConnection())
			    {
			    	using (var clientsCommand = new OleDbCommand("SELECT * FROM Clients", appLogin))  //Select all from clients table to populate the combo boxes
			    	{
			    		clientsAdapter = new OleDbDataAdapter(clientsCommand); //retrieve the data from the database table and populate the clientsTable
			    		clientsTable = new DataTable();
			    		clientsTable.Locale = System.Globalization.CultureInfo.InvariantCulture; //for converting to and from strings
			    		clientsAdapter.Fill(clientsTable);  //fills the table with the information from the adapter
			    		oldClientsTable = clientsTable;  //declaring an old table (which is the original one) the clients table is going to change later to inactive environments.  So I have the old table available to switch back to the original environments
			    	}	
			    }
			}
			catch(Exception ex)
			{
				throw new ApplicationException("Application encountered an error while loading server maintenance window: ", ex); //generic error if there is an issue pulling the information from the database.  Includes the exception thrown.
			}

			InitializeComponent();
			btnStop.Hide(); //stop button is hidden until a process is running
		}

		private void ServerMaintenance_Load(object sender, EventArgs e)
		{
			PopulateComboboxes(clientsTable);  //when the form loads its going to populate combos based on the clients table we just made
		}

		

		private void PopulateComboboxes(DataTable clientsTable)
		{
			try
			{
				commandDMSource = new List<PopulateCombos>(); //New list for the commands for Both applications

				commandDMSource.Add(new PopulateCombos() {Name = "Command", Value = null });
				commandDMSource.Add(new PopulateCombos() { Name = "Stop Host Service", Value = "ShadowServiceHost" });
				commandDMSource.Add(new PopulateCombos() { Name = "Stop Cluster Service", Value = "ShadowServiceCluster" });
				commandDMSource.Add(new PopulateCombos() { Name = "Start Host Service", Value = "ShadowServiceHost" });
				commandDMSource.Add(new PopulateCombos() { Name = "Start Cluster Service", Value = "ShadowServiceCluster" });
				commandDMSource.Add(new PopulateCombos() { Name = "Restart Host Service", Value = "ShadowServiceHost" });
				commandDMSource.Add(new PopulateCombos() { Name = "Restart Cluster Service", Value = "ShadowServiceCluster" });
				commandDMSource.Add(new PopulateCombos() { Name = "Stop DataMart Host", Value = "ShadowDataMartHost" });
				commandDMSource.Add(new PopulateCombos() { Name = "Stop DataMart Cluster", Value = "ShadowDataMartCluster" });
				commandDMSource.Add(new PopulateCombos() { Name = "Start DataMart Host", Value = "ShadowDataMartHost" });
				commandDMSource.Add(new PopulateCombos() { Name = "Start DataMart Cluster", Value = "ShadowDataMartCluster" });
				commandDMSource.Add(new PopulateCombos() { Name = "Restart DataMart Host", Value = "ShadowDataMartHost" });
				commandDMSource.Add(new PopulateCombos() { Name = "Restart DataMart Cluster", Value = "ShadowDataMartCluster" });
				commandDMSource.Add(new PopulateCombos() { Name = "Query Host Service", Value = "ShadowServiceHost" });
				commandDMSource.Add(new PopulateCombos() { Name = "Query Cluster Service", Value = "ShadowServiceCluster" });
				commandDMSource.Add(new PopulateCombos() { Name = "Query DataMart Host", Value = "ShadowDataMartHost" });
				commandDMSource.Add(new PopulateCombos() { Name = "Query DataMart Cluster", Value = "ShadowDataMartCluster" });
				commandDMSource.Add(new PopulateCombos() { Name = "Query Logged In Users", Value = "QueryUsers" });
				commandDMSource.Add(new PopulateCombos() { Name = "Log Off All Users", Value = "LogOffUsers" });
				commandDMSource.Add(new PopulateCombos() { Name = "Replace DLLs", Value = "ReplaceDLL" });
				commandDMSource.Add(new PopulateCombos() { Name = "Query Processes", Value = "ReplaceDLL" });
				commandDMSource.Add(new PopulateCombos() { Name = "Query Windows Updates", Value = "QWindowsUpdates" });
				commandDMSource.Add(new PopulateCombos() { Name = "Update Windows", Value = "UpdateWindows" });

				commandDMSource.Sort((x, y) => x.Name.CompareTo(y.Name));  //Sort by name

				commandSource = new List<PopulateCombos>();  //New list for commands for one application

				commandSource.Add(new PopulateCombos() {Name = "Command", Value = null });
				commandSource.Add(new PopulateCombos() { Name = "Kill Host Service", Value = "ShadowServiceHost" });
				commandSource.Add(new PopulateCombos() { Name = "Kill Cluster Service", Value = "ShadowServiceCluster" });
				commandSource.Add(new PopulateCombos() { Name = "Start Host Service", Value = "ShadowServiceHost" });
				commandSource.Add(new PopulateCombos() { Name = "Start Cluster Service", Value = "ShadowServiceCluster" });
				commandSource.Add(new PopulateCombos() { Name = "Restart Host Service", Value = "ShadowServiceHost" });
				commandSource.Add(new PopulateCombos() { Name = "Restart Cluster Service", Value = "ShadowServiceCluster" });
				commandSource.Add(new PopulateCombos() { Name = "Query Host Service", Value = "ShadowServiceHost" });
				commandSource.Add(new PopulateCombos() { Name = "Query Cluster Service", Value = "ShadowServiceCluster" });
				commandSource.Add(new PopulateCombos() { Name = "Query Logged In Users", Value = "QueryUsers" });
				commandSource.Add(new PopulateCombos() { Name = "Log Off All Users", Value = "LogOffUsers" });
				commandSource.Add(new PopulateCombos() { Name = "Restart Server", Value = "RestartServer" });
				commandSource.Add(new PopulateCombos() { Name = "Replace DLLs", Value = "ReplaceDLL" });
				commandSource.Add(new PopulateCombos() { Name = "Query Processes", Value = "ReplaceDLL" });
				commandSource.Add(new PopulateCombos() { Name = "Query Windows Updates", Value = "QWindowsUpdates" });
				commandSource.Add(new PopulateCombos() { Name = "Update Windows", Value = "UpdateWindows" });

				commandSource.Sort((x, y) => x.Name.CompareTo(y.Name));  //Sort by name

                var clientList = new List<PopulateCombos>();  //new list for clients drop down
				clientList.Add(new PopulateCombos() { Name = "Clients", Value = null});  
				foreach(DataRow dr in clientsTable.Rows)  //Convert data table to a list
				{
					string displayName = Convert.ToString(dr["DisplayName"]); //converts the column "DisplayName" into a string for each datarow
					string clientID = Convert.ToString(dr["ClientID"]);  //converts the column "ClientID" into a string for each datarow
                    clientList.Add(new PopulateCombos() {Name = displayName, Value = clientID });  //defines that the name of each list item is related to the displayName variable and the value of each item is related to the clientID variable
				}

                //setting the clients list combo box here 

				cmbClients.DataSource = clientList;  //the Client combobox's datasource is the clientList we just made
				cmbClients.DisplayMember = "Name"; //the Client combobox will display the names given
				cmbClients.ValueMember = "Value";  //the valuemember for each item in the combo box will be the associated value
				cmbClients.DropDownStyle = ComboBoxStyle.DropDownList;  //sets the style for the combobox to a drop down list
			}
			catch(Exception ex)
			{
				throw new ApplicationException("Application encountered an error while attempting to populate comboboxes: ", ex);  //catches any exceptions while populating combo boxes
			}
		}

		public class PopulateCombos  //used for our lists to set names/values
		{
		     public string Name { get; set; }
		     public string Value { get; set; }
		     public override string ToString() { return Name; }
		}

		private void cmbClients_SelectedIndexChanged(object sender, EventArgs e)  //when a client is selected is when the middletiers and commands boxes are filled
		{
			try
			{
				ComboBox cmbClients = (ComboBox) sender; //This is for the index change event.  The sender is the new selected item in the box.  We're converting that to a variable here.
				
				string clientSelection = Convert.ToString(cmbClients.SelectedValue);  //pulls the selected value from the item in the combo

				//string displayClient = Convert.ToString(cmbClients.SelectedItem);  //pulls the selected name from the item in the combo

				PopulateMiddleTiers(clientSelection);  //populates the environments combo box with the new selected client's active middle tiers

				if (cmbClients.SelectedValue == null)
				{
					return;
				}
				else
				{
					switch (clientSelection)  //special cases depending on what the value for the clients combobox is
					{
						case "COPROD":  
						cmbCommands.DataSource = commandDMSource;  
						cmbCommands.DisplayMember = "Name";
						cmbCommands.ValueMember = "Value";
						cmbCommands.DropDownStyle = ComboBoxStyle.DropDownList;
						break;

						case "COUAT":
						cmbCommands.DataSource = commandDMSource;
						cmbCommands.DisplayMember = "Name";
						cmbCommands.ValueMember = "Value";
						cmbCommands.DropDownStyle = ComboBoxStyle.DropDownList;
						break;

						case "PCPROD":
						cmbCommands.DataSource = commandDMSource;
						cmbCommands.DisplayMember = "Name";
						cmbCommands.ValueMember = "Value";
						cmbCommands.DropDownStyle = ComboBoxStyle.DropDownList;
						break;

						case "PCUAT":
						cmbCommands.DataSource = commandDMSource;
						cmbCommands.DisplayMember = "Name";
						cmbCommands.ValueMember = "Value";
						cmbCommands.DropDownStyle = ComboBoxStyle.DropDownList;
						break;

						default:
						cmbCommands.DataSource = commandSource;
						cmbCommands.DisplayMember = "Name";
						cmbCommands.ValueMember = "Value";
						cmbCommands.DropDownStyle = ComboBoxStyle.DropDownList;
						break;
					}
				}
			}
			catch(Exception ex)
			{
				throw new ApplicationException("Application encountered an error while attempting to populate comboboxes: ", ex);
			}
		}

		private void PopulateMiddleTiers(string selection)
		{
			mtTable = new DataTable();  //instantiate middletier table

			isDisRec = (from DataRow dr in clientsTable.Rows where (string)dr["ClientID"] == selection select (bool)dr["DisasterRecovery"]).FirstOrDefault(); //determines whether a client is in disaster recovery or not by extracting that boolean value from the clientsTable

			try
			{
				using (var accessDBLogin = GetConnection())
					{
						if(!isDisRec) //if not in disaster recovery, populate the mt table with the production servers
						{
							using (var mtSelection = new OleDbCommand("SELECT * FROM ServerList WHERE ClientID='" + @selection + "' AND EnvironmentType <> 'DR'", accessDBLogin)) //select all from the serverlist table where the client id matches the selection and the environment type is not equal to dr
							{
								accessDBLogin.Open();  //open the connection to the database
								OleDbDataAdapter mtAdapter = new OleDbDataAdapter(mtSelection);  //populate the adapter with the results from the query
								mtTable = new DataTable(); //probably dont need this since I instantiated it in a broader scope
								mtTable.Locale = System.Globalization.CultureInfo.InvariantCulture;
								mtAdapter.Fill(mtTable);  
							}
						}
						else
						{
							using (var mtSelection = new OleDbCommand("SELECT * FROM ServerList WHERE ClientID='" + @selection + "' AND EnvironmentType = 'DR'", accessDBLogin)) 
							{
								accessDBLogin.Open();
								OleDbDataAdapter mtAdapter = new OleDbDataAdapter(mtSelection);
								mtTable = new DataTable();
								mtTable.Locale = System.Globalization.CultureInfo.InvariantCulture;
								mtAdapter.Fill(mtTable);
							}
						}
					}

				var serverList = new List<PopulateCombos>();
				serverList.Add(new PopulateCombos() {Name = "Middle Tier", Value = null }); //title of the combo box
				serverList.Add(new PopulateCombos() {Name = "All", Value = null }); //add an "All" value to the combobox

				var tempList = new List<PopulateCombos>(); //create a temporary list for sorting

				foreach(DataRow dr in mtTable.Rows) //add the servers to the new temp list
				{
					string displayName = Convert.ToString(dr["ServerName"]);
					string clientID = Convert.ToString(dr["ClientID"]);
					tempList.Add(new PopulateCombos() {Name = displayName, Value = clientID });
				}

				tempList.Sort((x, y) => x.Name.CompareTo(y.Name)); //sort the temporary list
				serverList.AddRange(tempList); //populate the serverList with the templist in the templists sorted order
                //populate the middletier combobox
				cmbMiddleTiers.DataSource = serverList;
				cmbMiddleTiers.DisplayMember = "Name";
				cmbMiddleTiers.ValueMember = "Value";
				cmbMiddleTiers.DropDownStyle = ComboBoxStyle.DropDownList;
			}
			catch(Exception ex)
			{
				throw new ApplicationException("Application encountered an error while attempting to populate comboboxes: ", ex);
			}
		}

        //To secure the database connection string it is in its own method that is protected
		protected OleDbConnection GetConnection()
		{
			return new OleDbConnection (@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\rschaber\Desktop\SSDev\LoginDB.accdb; 
				Persist Security Info=False;");
		}

		private void btnConfirm_Click(object sender, EventArgs e) //when the confirm button is clicked, hide the confirm button, show the stop buttom and process the command
		{
			ProcessCommand();
			btnConfirm.Hide();
			btnStop.Show();
		}

		private void ProcessCommand() //here we are going to initialize our workers and assign them work based on the selected client, middletiers and commands.
		{	
			worker = new BackgroundWorker();
			worker.DoWork += worker_DoWork; 
			worker.ProgressChanged += ProgressChanged;
			worker.WorkerReportsProgress = true;
			worker.WorkerSupportsCancellation = true;
			worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(WorkerCompleted);

			txtMaintenanceWindow.Clear();

            //servermaintenance is the parent class of servercommands.  All of the values below are a part of the servercommands class

            selectedCommand = Convert.ToString(cmbCommands.SelectedItem);  //taking the name of the item in the combo box and converting it to a string
			servercommands = new ServerCommands();  //instantiating a new servercommands object
			servercommands.middletier = Convert.ToString(cmbMiddleTiers.SelectedItem);  
			servercommands.selectedClient = Convert.ToString(cmbClients.SelectedItem);
			servercommands.serviceName = Convert.ToString(cmbCommands.SelectedValue);
			servercommands.selectedCommand = Convert.ToString(cmbCommands.SelectedItem);
			servercommands.mtTable = mtTable;
			servercommands.worker = worker;
			worker.RunWorkerAsync();
		}

        private void worker_DoWork(object sender, DoWorkEventArgs e)  //this is going to look at the selectedCommand that we created and run commands based on that selection
        {
            switch (selectedCommand)
            {
                case "Stop Host Service":
                    servercommands.StopService();
                    break;

                case "Stop Cluster Service":
                    servercommands.StopService();
                    break;

                case "Start Host Service":
                    servercommands.StartService();
                    break;

                case "Start Cluster Service":
                    servercommands.StartService();
                    break;

                case "Kill Host Service":
                    servercommands.KillService();
                    break;

                case "Kill Cluster Service":
                    servercommands.KillService();
                    break;

                case "Stop DataMart Host":
                    servercommands.StopService();
                    break;

                case "Stop DataMart Cluster":
                    servercommands.StopService();
                    break;

                case "Start DataMart Host":
                    servercommands.StartService();
                    break;

                case "Start DataMart Cluster":
                    servercommands.StartService();
                    break;

                case "Query Host Service":
                    servercommands.ServiceStatus();
                    break;

                case "Query Cluster Service":
                    servercommands.ServiceStatus();
                    break;

                case "Query DataMart Host":
                    servercommands.ServiceStatus();
                    break;

                case "Query DataMart Cluster":
                    servercommands.ServiceStatus();
                    break;

                case "Restart Host Service":
                    servercommands.StopService();
                    servercommands.StartService();
                    break;

                case "Restart Cluster Service":
                    servercommands.StopService();
                    servercommands.StartService();
                    break;

                case "Restart DataMart Host":
                    servercommands.StopService();
                    servercommands.StartService();
                    break;

                case "Restart DataMart Cluster":
                    servercommands.StopService();
                    servercommands.StartService();
                    break;

                case "Query Logged In Users":
                    servercommands.LoggedUsers();
                    break;

                case "Log Off All Users":
                    servercommands.LogOffUsers();
                    break;

                case "Restart Server":
                    servercommands.RestartServer();
                    break;

                case "Replace DLLs":
                    servercommands.ReplaceDLLs();
                    break;

                case "Query Processes":
                    servercommands.QueryHighCPU(sender, e);
                    break;

                case "Query Windows Updates":
                    servercommands.QueryHighCPU(sender, e);
                    break;

                case "Update Windows":
                    servercommands.QueryHighCPU(sender, e);
                    break;
            }
        }

        void WorkerCompleted(object sender, RunWorkerCompletedEventArgs e)  //this section is used to check the status of the worker.  Completed/errored/cancelled
        {
            // The background process is complete. We need to inspect
            // our response to see if an error occurred, a cancel was
            // requested or if we completed successfully.  
            if (e.Cancelled == true)
            {
                txtMaintenanceWindow.Text += "Task Cancelled.";
                worker.Dispose();
                btnConfirm.Show();
                btnStop.Hide();
            }
            else if (e.Error != null)  // Check to see if an error occurred in the background process.
            {
                txtMaintenanceWindow.Text += e.Error.Message;
                worker.Dispose();
                btnConfirm.Show();
                btnStop.Hide();
            }
            else
            {
                // Everything completed normally.
                txtMaintenanceWindow.Text += "Task Completed." + Environment.NewLine;
                worker.Dispose();
                btnConfirm.Show();
                btnStop.Hide();
            }
        }

        void ProgressChanged(object sender, ProgressChangedEventArgs e)//this works with the background worker report progress method
        {
            string results = (string)e.UserState;
            txtMaintenanceWindow.Text += results;
        }

        private void ServerMaintenance_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (e.CloseReason == CloseReason.UserClosing)  //If the user performaned an action to close the application, show the following.
			{
				DialogResult result = MessageBox.Show("Do you really want to exit?", "Close Application", MessageBoxButtons.YesNo, MessageBoxIcon.Question);  //Save the result from this dialog in result

				if (result == DialogResult.Yes) //If yes was clicked
				{	
					try
					{	
						//Close and dispose, then exit the application
						mtTable.Dispose();
						clientsTable.Dispose();
						Application.Exit();
					}
					catch(Exception ex)
					{
						throw new ApplicationException("Application encountered an error while attempting to close: ", ex);
					}
				}
				else 
				{
					e.Cancel = true; //cancels the popup box and returns back to the application
				}
			}
			else
			{
			   e.Cancel = true;
			}
		}

		private void btnHome_Click(object sender, EventArgs e) //switch back to the home form
		{
			Hide();
			new GateKeeper().Show();  
		}

		private void btnClear_Click(object sender, EventArgs e) //clears the text from the maintenance window and all selected values in the combo boxes
		{
			txtMaintenanceWindow.Clear();
			cmbClients.SelectedIndex = 0;
			cmbCommands.SelectedIndex = 0;
			cmbMiddleTiers.SelectedIndex = 0;
		}

		private void chkbxInactive_CheckedChanged(object sender, EventArgs e) //Inactives checkbox is used to switch from active middle tiers to inactive ones
		{
			if (chkbxInactive.Checked)
			{
				GetInactives(out clientsTable);
				PopulateComboboxes(clientsTable);
			}
			else
			{
				clientsTable = oldClientsTable;
				PopulateComboboxes(clientsTable);
			}
		}

		private void GetInactives(out DataTable clientsTable) //this section basically swaps the active servers with the ones in DR to perform updates on them
		{
			using (var accessDBLogin = GetConnection())
			{
				using (var currentDR = new OleDbCommand("SELECT * FROM Clients WHERE DisplayName LIKE '%Prod%'", accessDBLogin))
				{
					accessDBLogin.Open();
					OleDbDataAdapter mtAdapter = new OleDbDataAdapter(currentDR);
					clientsTable = new DataTable();
					clientsTable.Locale = System.Globalization.CultureInfo.InvariantCulture;
					mtAdapter.Fill(clientsTable);
				}
			}
			foreach (DataRow dr in clientsTable.Rows)
			{
				bool isDisRec = Convert.ToBoolean(dr["DisasterRecovery"]);
				if(isDisRec)
				{
					dr["DisasterRecovery"] = false;
				}
				else
				{
					dr["DisasterRecovery"] = true;
				}
				
			}
		}

		private void btnStop_Click(object sender, EventArgs e)//cancels the worker that is processing current task
		{
			worker.CancelAsync();
		}
	}
}
