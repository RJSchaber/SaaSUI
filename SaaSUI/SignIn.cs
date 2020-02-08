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
using System.Xml.Linq;
using System.Web.Security;
using System.Net.Mail;



namespace SaaSUI
{
	public partial class SignIn : Form
	{
		OleDbConnection connLogin;

		string connLoginString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\rschaber\Desktop\SSDev\LoginDB.accdb; 
						Persist Security Info=False;"; //connection string
		string currentUser;  
		string errorMessage;

		public SignIn()
		{
			InitializeComponent();
		}

		private void SignIn_Load(object sender, EventArgs e)
		{
			try
			{
				connLogin = new OleDbConnection(connLoginString);  //Make a connection to the database using the connection string
				connLogin.Open();  //Open the new connection
			}
			catch(Exception ex)
			{
				throw new ApplicationException("Could not open database connection: ", ex);
			}
			txtPass.MaxLength = 12;  //Set the maximum input for the password box
			txtPass.PasswordChar = '*'; //Hide the user's password with *
			currentUser = Environment.UserName; //Pull the user's windows login ID


			try
			{
				OleDbCommand check_User_Name = new OleDbCommand("SELECT COUNT(*) FROM Accounts WHERE (UserID ='" + @currentUser + "')", connLogin);  //Check to see if the user's ID is in the database by pulling the count
				int UserExist = (int)check_User_Name.ExecuteScalar();  //Run the sql and convert the query results into an int
				if (UserExist == 1)  //If the query we ran is = 1, user exists, enter their name into the user text box
					txtUser.Text = Environment.UserName;
				else
				{
					MessageBox.Show("Your user does not exist within the database.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}
			}
			catch(Exception ex)
			{
				throw new ApplicationException("There was an issue checking your username in the database: ", ex);
			}
			try
			{
				OleDbCommand checkReset = new OleDbCommand("SELECT Reset FROM Login WHERE UserID = '" + currentUser + "'", connLogin);  //Query to check and see if the user needs a password reset
				bool userReset = (bool)checkReset.ExecuteScalar();
				if(userReset) //if the user does need a reset, this will load the reset form
				{	
					PasswordReset pwForm = new PasswordReset(connLogin, currentUser);
					pwForm.Owner = this;
					pwForm.Show();
				}
			
				PasswordVerifier pwResetCheck = new PasswordVerifier();  //make a new password verifier object
				bool verifiedPW = pwResetCheck.ExpirationReset(connLogin, currentUser, out errorMessage); //Pass the connection, current user and take back any error message
				if(verifiedPW && errorMessage != null) //if the error message does not come back as null, shows the warning.  
				{
					MessageBox.Show(errorMessage, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					Show();
				}
				else if(!verifiedPW) //if verified password comes back false, show the error and make user reset password
				{
					DialogResult resultOK = MessageBox.Show(errorMessage, "Password has expired", MessageBoxButtons.OK, MessageBoxIcon.Error);
					if(resultOK == DialogResult.OK)
					{
						PasswordReset pwForm = new PasswordReset(connLogin, currentUser); //pass login and user variables to the password form
						pwForm.Owner = this;  //bring the password form up front by making it the owner.
						pwForm.Show();
					}
				}
			
			}
			catch (Exception ex)
			{
				throw new ApplicationException("Application encountered an error while attempting to extract the password reset information from database: ", ex);
			}
				
		}

		private void btnLogin_Click(object sender, EventArgs e)
		{
			try
			{
				OleDbCommand checkReset = new OleDbCommand("SELECT Reset FROM Login WHERE UserID = '" + @currentUser + "'", connLogin);  //check the reset on button button click (We check this on the application start but also when the button is clicked)
				bool userReset = (bool)checkReset.ExecuteScalar();
				if(userReset)
				{
					
						PasswordReset pwForm = new PasswordReset(connLogin, currentUser);
						pwForm.Owner = this;
						pwForm.Show();
				}
				else
				{
						OleDbCommand checkPWCommand = new OleDbCommand ("SELECT Password FROM login WHERE UserID='"+ @currentUser +"'", connLogin);  //Query to pull the password from the database
						string dbPass = (string)checkPWCommand.ExecuteScalar(); //Command that pulls the password from the DB and then converts it to a string

						PasswordHasher verifyPassword = new PasswordHasher();  //Create a new passwordhasher object
						int verified = verifyPassword.passwordHashCompare(dbPass, txtPass.Text);  //Send the database password and the user's password from the txt box to be compared and verfied through salted encryption.  Convert the response to an integer. 
						if(verified == 1) //If the integer returned is = to 1, password is good and the main application will load.
						{
							Hide();  
							new GateKeeper().Show();  

							var addToday = DateTime.Now.ToString("MM/dd/yyyy");  //We're going to add todays date and time to the last login column in the following
							OleDbCommand updateLastLogin = new OleDbCommand("UPDATE Accounts SET [LastLogin] = '" + @addToday + "' WHERE [UserID] = '" + @currentUser + "'", connLogin);
							updateLastLogin.ExecuteScalar();

							//ALWAYS CLOSE AND DISPOSE ;)
							connLogin.Close();
							connLogin.Dispose();
						}
						else  
							MessageBox.Show("Invalid username or password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);  
				}
			}
			catch (Exception ex)
			{
				throw new Exception("There was an error Logging in: ", ex);
			}
		}

		private void chkShow_CheckedChanged(object sender, EventArgs e)
		{
			if (chkShow.Checked)
			{
				txtPass.PasswordChar = '\0';  //if the check box to show password is checked, change the password character to \0 which is the default for showing the password
			}
			else
				txtPass.PasswordChar = '*'; //if it isnt checked, keep * as the character shown
		}

		private void txtUser_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == '\r')  //hit enter to cycle from user to password fields
				ActiveControl = txtPass;
		}

		private void txtPass_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == '\r') //hit enter on the password field to do a login button click.
				btnLogin.PerformClick();
		}

		private void SignIn_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (e.CloseReason == CloseReason.UserClosing)  //If the user performaned an action to close the application, show the following.
			{
				DialogResult result = MessageBox.Show("Do you really want to exit?", "Close Application", MessageBoxButtons.YesNo, MessageBoxIcon.Question);  //Save the result from this dialog in result

				if (result == DialogResult.Yes) //If yes was clicked
				{	
					try
					{	
						//Close and dispose, then exit the application
						connLogin.Close();
						connLogin.Dispose();
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

			private void btnCancel_Click(object sender, EventArgs e)
			{
				Close(); //sends the user to the formclosing function
			}

			private void lblForgot_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
			{
				DialogResult result = MessageBox.Show("This will send a temporary password to the email address on your account.", "Password Reset", 
							MessageBoxButtons.YesNo, MessageBoxIcon.Information);  //Verifies that the user wants to reset their password

				if (result == DialogResult.Yes)
				{
					try
					{
						string userEmail;  
						OleDbCommand forgotCommand = new OleDbCommand("SELECT EmailAddress FROM Accounts WHERE UserID='" + @currentUser + "'", connLogin);  //Query to pull the user's email address from the database
						userEmail = (string) forgotCommand.ExecuteScalar();

						if (string.IsNullOrEmpty(userEmail)) //Checks to make sure that an email address exists
						{
							MessageBox.Show(Text, "You do not have an email address attached to your account.", MessageBoxButtons.OK, MessageBoxIcon.Error);
						}
						else
						{
							SendTempPassword(currentUser, userEmail);  //Sends the currentuser and the useremail to the function that sends the temporary password email.
						}
					}
					catch(Exception ex)
					{
						throw new ApplicationException("Application encountered an error while attempting to extract your email address from the database: ", ex);
					}

				}
				else
				{
					return;
				}
			}

			private void SendTempPassword(string currentUser, string userEmail)
			{
				try
				{
					string allowedChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#$%^&*()";  //Characters we want to allow in our temporary password
					char[] chars = new char [10]; //How many characters we want
					Random tempPass = new Random(); //create a new random object

					for (int i = 0; i < 10; i++) //Creates each random character 1 at a time and adds them to the chars array
					{
						chars[i] = allowedChars[tempPass.Next(0, allowedChars.Length)];
					}

					string newpw = new string (chars); //turns the new password array into a string
					string tempPWHashed;

					PasswordHasher tempPWHash = new PasswordHasher();
					tempPWHash.passwordHasher(newpw, out tempPWHashed);  //Hashes the newly made temp password and then sends us that string back.  

					OleDbCommand passwordUpdate = new OleDbCommand("UPDATE Login SET [Password] = '" + @tempPWHashed + "' WHERE [UserID] = '" + @currentUser +"'", connLogin); //Query to update the password
					passwordUpdate.ExecuteScalar(); //Command that executes the query

					EmailPassword(userEmail, newpw); //Sends the newly hashed password and useremail address to emailpassword function.

					OleDbCommand recentlyReset = new OleDbCommand("Update Login SET [Reset] = True WHERE [UserID] = '" + @currentUser + "'", connLogin);  //Query to update the reset check box to true so the user is prompted to reset their password upon next login
					recentlyReset.ExecuteScalar(); //Executes the above query
				}
				catch(Exception ex)
				{
					throw new ApplicationException("Application encountered an error while attempting to generate a new password and update it in the database: ", ex);
				}

			}
			private void EmailPassword(string userEmail, string newpw)
			{
				try
				{
					MailMessage mailPassword = new MailMessage("Norepy@shadowfinancial.com", userEmail); //Make a new mailmessage object from our noreply email to the email address on the user's account
					SmtpClient client = new SmtpClient(); //new smtp object -- filling the creds in the following lines.  
					client.Port = 25;
					client.DeliveryMethod = SmtpDeliveryMethod.Network;
					client.UseDefaultCredentials = false;
					client.Host = "172.20.20.27";
					client.EnableSsl= false;
					mailPassword.Subject = "Password reset for: " + currentUser;
					mailPassword.Body = "Your new password is: " + newpw;
					
					client.Send(mailPassword);
				}
				catch(Exception ex)
				{
					throw new ApplicationException("Application encountered an error while attempting to send password: ", ex);
				}
			}

		private void lnklblCreateUser_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			try
			{
				OleDbCommand checkUserExists = new OleDbCommand("SELECT Count(*) FROM Accounts where UserID = '" + @currentUser + "'", connLogin);
				OleDbDataAdapter userAdapter = new OleDbDataAdapter(checkUserExists);

				DataTable usersTable = new DataTable();
				userAdapter.Fill(usersTable);
 
				if (usersTable.Rows[0][0].ToString() == "1")  
				{
					  MessageBox.Show("The user already exists", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				}
				else
				{

					string allowedChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#$%^&*()";
					char[] chars = new char [10];
					Random tempPass = new Random();

					for (int i = 0; i< 10; i++)
					{
						chars[i] = allowedChars[tempPass.Next(0, allowedChars.Length)];
					}

					string tempPW = new string (chars);
					

					PasswordHasher tempPWHash = new PasswordHasher();

					string tempPWHashed;

					tempPWHash.passwordHasher(tempPW, out tempPWHashed);
					EmailPassword("rschaber@shadowfinancial.com", tempPW +"  This is bullshit   " + tempPWHashed);

					var currentDate = DateTime.Now.ToString("MM/dd/yyyy");


					OleDbCommand addNewUserAccount= new OleDbCommand("INSERT INTO Accounts VALUES ('" + @currentUser + "', 'Rich Schaber', 'RSchaber@ShadowFinancial.com', '" + @currentDate + "', '" + @currentDate + "')", connLogin); 
					addNewUserAccount.ExecuteNonQuery();

					OleDbCommand addNewUserLogin = new OleDbCommand("INSERT INTO Login VALUES ('" + @currentUser + "','" + @tempPWHashed + "', True)", connLogin);
					addNewUserLogin.ExecuteNonQuery();

					string pwIDGen = DateTime.Now.ToString("yyMMddHHmmss");
					OleDbCommand addNewUserPWHist = new OleDbCommand("INSERT INTO PasswordHistory VALUES ('" + @pwIDGen + "','" + @currentUser + "','" + @tempPWHashed + "','" + @currentDate + "')", connLogin);
					addNewUserPWHist.ExecuteNonQuery();
				}
			}
			catch(Exception ex)
			{
				throw new ApplicationException("Application encountered an error while attempting to create a new user.", ex);
			}
		}
	}
}
