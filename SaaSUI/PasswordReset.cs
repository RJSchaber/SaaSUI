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

namespace SaaSUI
{
	public partial class PasswordReset : Form
	{
		OleDbConnection connlogin;
		string currentuser;
		public PasswordReset(OleDbConnection connLogin, string currentUser)
		{
			InitializeComponent();
			connlogin = connLogin;
			currentuser = currentUser;
		}

		private void PasswordReset_Load(object sender, EventArgs e)
		{
			//These lines hide the entered passwords
			txtOldPW.PasswordChar = '*';
			txtNewPW.PasswordChar = '*';
			txtRetypePW.PasswordChar = '*';
		}
		private void btnOK_Click(object sender, EventArgs e)
		{
			if (txtNewPW.Text == txtRetypePW.Text) //Checks to make sure that the user entered two identical passwords
			{	
				
				try 
				{
					string errorMessage;

					PasswordVerifier pwVerifier = new PasswordVerifier(); //New password verifier object
					bool verifiedPW = pwVerifier.ValidatePassword(connlogin, currentuser, txtNewPW.Text, 
						txtOldPW.Text, out errorMessage);  //Send the connection, user, and new/old password inputs to the validate password method and return an errorMessage
					if (verifiedPW) //If the password is verified without error
					{
						string newPWHashed;
						PasswordHasher newPWHasher = new PasswordHasher();
						newPWHasher.passwordHasher(txtNewPW.Text, out newPWHashed); //Send the newly entered password to be salted and hashed
					
						OleDbCommand updatePW = new OleDbCommand("UPDATE Login SET [Password] = '" + @newPWHashed + "' WHERE [UserID] = '" + 
							@currentuser + "'", connlogin); //Query to update the password
						updatePW.ExecuteScalar();

						OleDbCommand updateReset = new OleDbCommand("UPDATE Login SET [Reset] = False WHERE [UserID] = '" 
							+ @currentuser + "'", connlogin);  //Query to update the Needs a Reset flag
						updateReset.ExecuteScalar();

						var addToday = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");  
						OleDbCommand updateLastPWReset = new OleDbCommand("UPDATE Accounts SET [LastReset] = '" + 
							@addToday + "' WHERE [UserID] = '" + @currentuser + "'", connlogin);  //Query to update the Last Reset date/time
						updateLastPWReset.ExecuteScalar();

						OleDbCommand historyQuery = new OleDbCommand("SELECT Password FROM PasswordHistory WHERE UserID = '" + 
							@currentuser + "'", connlogin);  // Query to update the password history  
						OleDbDataAdapter historyAdapter = new OleDbDataAdapter(historyQuery);  //The adapter to translate the information from the query
						DataTable historyTable = new DataTable();
						
						historyAdapter.Fill(historyTable);  //Add the information from the adapter to the table
						int totalRows = historyTable.Rows.Count; //Count the total rows in the returned table
						if (totalRows >= 5)  //Only saving 5 passwords in the history per person
						{
							OleDbCommand updateQuery = new OleDbCommand("SELECT MIN(UpdateDate) FROM PasswordHistory WHERE UserID = '" + 
								@currentuser + "'", connlogin);  //Query to select the oldest update reset date
							var oldestPW = updateQuery.ExecuteScalar();

							OleDbCommand deleteOldest = new OleDbCommand("DELETE FROM PasswordHistory WHERE UpdateDate = '" + 
								@oldestPW + "'", connlogin);  //Query to delete the oldest date that we just pulled
							deleteOldest.ExecuteNonQuery();

							var updateTime = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");  //This is the current time down to seconds for when the 
															//new Password History entry was made

							string pwIDGen = DateTime.Now.ToString("yyMMddHHmmss");  //Using this date time string as the password ID.  
							OleDbCommand insertNewPW = new OleDbCommand("INSERT INTO PasswordHistory VALUES (@pwIDGen, @currentuser, @hashedPass, @updateTime)", 
								connlogin);  //Query to add a new Password History entry

							//The following commands update the @ values in the query with the variables being attached
							insertNewPW.Parameters.AddWithValue("@pwIDGen", pwIDGen);  
							insertNewPW.Parameters.AddWithValue("@currentuser", currentuser);
							insertNewPW.Parameters.AddWithValue("@hashedPass", newPWHashed);
							insertNewPW.Parameters.AddWithValue("@updateTime", updateTime);

							insertNewPW.ExecuteNonQuery();

						}
						else
						{	
							//If the password history doesnt have 5 entries, just add the new entry without deleting anything
							var updateTime = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
							string pwIDGen = DateTime.Now.ToString("yyMMddHHmmss");
							OleDbCommand insertNewPW = new OleDbCommand("INSERT INTO PasswordHistory VALUES (@pwIDGen, @currentuser, @hashedPass, @updateTime)", connlogin);

							insertNewPW.Parameters.AddWithValue("@pwIDGen", pwIDGen);
							insertNewPW.Parameters.AddWithValue("@currentuser", currentuser);
							insertNewPW.Parameters.AddWithValue("@hashedPass", newPWHashed);
							insertNewPW.Parameters.AddWithValue("@updateTime", updateTime);

							insertNewPW.ExecuteNonQuery();
						}

						Close();
					}
					else
					{
						MessageBox.Show(errorMessage, "Password Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
				}
				catch (Exception ex)
				{
					throw new ApplicationException("There was an error while updating your password: ", ex);
				}
			}
			else
			{
				MessageBox.Show("Passwords Do Not Match.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void txtOldPW_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == '\r') //Hit enter to move to the next text box
				ActiveControl = txtNewPW;
		}

		private void txtNewPW_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == '\r') //Hit enter to move to the next text box
				ActiveControl = txtRetypePW;
		}

		private void txtRetypePW_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == '\r')  //Hit enter on the last text box to perform a button click on OK
				btnOK.PerformClick();
		}
	}
}
