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
using System.Net.Mail;

namespace SaaSUI
{	
	public partial class GateKeeper : Form
	{
		OleDbDataAdapter updatesAdapter;
		DataTable updatesTable;
		string currentUser;
		OleDbDataAdapter clientsAdapter;
		DataTable clientsTable;

		public GateKeeper()
		{
			InitializeComponent();
		}

		private void GateKeeper_Load(object sender, EventArgs e)
		{
			try
			{
				dtgrdUpdates.DataSource = bindUpdates; //bind the datasource for the datagrid on window load
				currentUser = Environment.UserName; 

				using (var appLogin = GetConnection()) // using our connection, we are going to pull our clients table and fill our combobox with it
				{
					using (var clientsCommand = new OleDbCommand("SELECT * FROM Clients", appLogin))
					{
						clientsAdapter = new OleDbDataAdapter(clientsCommand);
						clientsTable = new DataTable();
						clientsTable.Locale = System.Globalization.CultureInfo.InvariantCulture;
						clientsAdapter.Fill(clientsTable);

						cmbClient.DataSource = clientsTable;  //client combobox is filled with the table we just made.  Display member is the name, value is the client ID
						cmbClient.DisplayMember = "DisplayName";
						cmbClient.ValueMember = "ClientID";
						cmbClient.DropDownStyle = ComboBoxStyle.DropDownList;
					}	
				}
				PopulateUpdates(); //Populate the comboboxes after we pull the table
			}
			catch(Exception ex)
			{
				throw new ApplicationException("Application encountered an error while attempting to load main window: ", ex);
			}
		}

		private void GateKeeper_FormClosing(object sender, FormClosingEventArgs e)
		{
			try
			{
				if (e.CloseReason == CloseReason.UserClosing)  //If the user performaned an action to close the application, show the following.
				{
					DialogResult result = MessageBox.Show("Do you really want to exit?", "Close Application", MessageBoxButtons.YesNo, MessageBoxIcon.Question);  //Save the result from this dialog in result

					if (result == DialogResult.Yes) //If yes was clicked
					{	
						try
						{	
							//Close and dispose, then exit the application
							updatesAdapter.Dispose();
							updatesTable.Dispose();
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
			catch(Exception ex)
			{
				throw new ApplicationException("Application encountered an error while attempting to close form: ", ex);
			}
		}
		private void PopulateUpdates()
		{
			try
			{
				using (var appLogin = GetConnection()) //use our connection, pull the updates from the database
				{
					using (var updatesCommand = new OleDbCommand("SELECT * FROM Updates", appLogin))
					{
						updatesAdapter = new OleDbDataAdapter(updatesCommand);
						updatesTable = new DataTable();
						updatesTable.Locale = System.Globalization.CultureInfo.InvariantCulture;
						updatesAdapter.Fill(updatesTable);
						bindUpdates.DataSource = updatesTable; //bind the datatable to the bindUpdates binding
					}
				}

				dtgrdUpdates.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);  //Autoresize the columns to fit everything including header
				var lastColIndex = dtgrdUpdates.Columns.Count - 1;  
				var lastCol = dtgrdUpdates.Columns[lastColIndex];
				lastCol.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;  
				dtgrdUpdates.Columns[0].Visible=false; //Hide the first column

				updatesAdapter.Dispose();
				updatesTable.Dispose();
			}
			catch(Exception ex)
			{
				throw new ApplicationException("Application encountered an error while attempting to populate updates datagrid: ", ex);
			}

		}

		private void AddEntry()
		{
			try
			{
				var selectedClient = cmbClient.Text;  //Pull the selected client from the combo
				string userInput = txtUpdate.Text;  //Extract user's input
				int messageID = Convert.ToInt32(updatesTable.Rows[updatesTable.Rows.Count -1]["MessageID"]); //Pulls the last message ID
				messageID ++; //Adds one to message ID
				var currentTime = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");  //Pulls the current time
				using (var appLogin = GetConnection()) //Use our DB connection
				{
					using (var insertUpdate = new OleDbCommand("INSERT INTO Updates VALUES (" + @messageID + ",'" + @currentUser + "','" + @currentTime + "','" + @selectedClient + "','" + @userInput + "')",
						appLogin)) 
					{	 
						appLogin.Open();
						insertUpdate.ExecuteNonQuery(); //Insert update entry into the database
					}

					DataRow updateRow = updatesTable.NewRow();  //Make a new row in our working updates Table to reflect the change made to the database
					updateRow["MessageID"] = messageID;
					updateRow["UserID"] = currentUser;
					updateRow["TimeStamp"] = currentTime;
					updateRow["Client"] = selectedClient;
					updateRow["Message"] = userInput;
				}
			}
			catch(Exception ex)
			{
				throw new ApplicationException("Application encountered an error while attempting to add an update: ", ex);
			}
		}

		private void btnSend_Click(object sender, EventArgs e)
		{
			try
			{
				if (string.IsNullOrWhiteSpace(txtUpdate.Text))
				{
					MessageBox.Show("You have to enter a message.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				else
				{
					using (var appLogin = GetConnection())
					{
						appLogin.Open();
						AddEntry(); //Run the add entry method above on button click
					}
					PopulateUpdates();  //After add entry is run, populate updates grid
					txtUpdate.Clear();  //Clear the text from the input box
				}
			}
			catch(Exception ex)
			{
				throw new ApplicationException("Application encountered an error: ", ex);
			}
		}

		protected OleDbConnection GetConnection()
		{
			return new OleDbConnection (@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\rschaber\Desktop\SSDev\LoginDB.accdb; 
				Persist Security Info=False;"); //Database Connection string 
		}

		private void txtUpdate_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == '\r')  //Hit enter on the last text box to perform a button click on OK
				btnSend.PerformClick();  
		}

		private void HandoffEntry()
		{
			try
			{
				DataTable handoffSelects = new DataTable();  //Make a new data table for our Update datagrid selections
				handoffSelects.Columns.Add("Client", typeof(string));
				handoffSelects.Columns.Add("Message", typeof(string));

				Int32 selectedRowCount = dtgrdUpdates.Rows.GetRowCount(DataGridViewElementStates.Selected);  //Get how many rows selected

				if(selectedRowCount > 0)  //If 1 row or more are selected, put them into the new table
				{
					foreach (DataGridViewRow row in dtgrdUpdates.SelectedRows) 
					{
						DataRow updateRow = handoffSelects.NewRow();
						updateRow[0] = (string)row.Cells[3].Value;
						updateRow[1] = (string)row.Cells[4].Value;
						handoffSelects.Rows.Add(updateRow);
					}
				}
				else
				{
					MessageBox.Show("You have to select a row from the Data Grid", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}

				string MailBody = "<b><center>Handoff</b></center> <p>";  //start making the email to send out for the handoff

				for (int loopCount = 0; loopCount < handoffSelects.Rows.Count; loopCount++)
				{
					MailBody += "<b>";
					MailBody += handoffSelects.Rows[loopCount]["Client"];
					MailBody += ": </b>";
					MailBody += handoffSelects.Rows[loopCount]["Message"];
					MailBody += "<p>";
				}

				MailBody += "<b>I sent this from my app by selecting the rows from the DataGrid.  ARE YOU NOT ENTERTAINED MR.TUSK?!</b>";
				EmailHandoff(MailBody);
			}
			catch(Exception ex)
			{
				throw new ApplicationException("Application encountered an while creating handoff email: ", ex);
			}
		}
		private void EmailHandoff(string MailBody)
		{
			try
			{
				string userEmail;
				using (var appLogin = GetConnection())
				{
					using (var useremail = new OleDbCommand("SELECT EmailAddress FROM Accounts WHERE UserID='" + @currentUser + "'", appLogin))  //Query to pull the user's email address from the database
					{
						appLogin.Open();
						userEmail = (string)useremail.ExecuteScalar();
					}
				}

				MailMessage mailHandoff = new MailMessage("Norepy@shadowfinancial.com", userEmail); //Make a new mailmessage object from our noreply email to the email address on the user's account
				SmtpClient client = new SmtpClient(); //new smtp object -- filling the creds in the following lines.  
				client.Port = 25;
				client.DeliveryMethod = SmtpDeliveryMethod.Network;
				client.UseDefaultCredentials = false;
				client.Host = "172.20.20.27";
				client.EnableSsl= false;
				mailHandoff.Subject = "Handoff for Shift 1";
				mailHandoff.Body = MailBody;
				mailHandoff.IsBodyHtml = true;
				mailHandoff.To.Add("CBennis@ShadowFinancial.com");
				client.Send(mailHandoff);
			}
			catch(Exception ex)
			{
				throw new ApplicationException("Application encountered an error while attempting to send password: ", ex);
			}
		}

		private void btnHandoff_Click(object sender, EventArgs e)
		{
			HandoffEntry();
		}

		private void btnUpgrades_Click(object sender, EventArgs e)
		{
			Hide();
			new UpgradeCenter(clientsTable, currentUser).Show();
		}

		private void servermaintenance_Click(object sender, EventArgs e)
		{
			Hide();
			new ServerMaintenance().Show();
		}
	}
}
