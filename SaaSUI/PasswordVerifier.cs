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
using System.Text.RegularExpressions;

namespace SaaSUI
{
	class PasswordVerifier
	{
		public bool ValidatePassword(OleDbConnection connLogin, string currentUser, string txtNewPW, string txtOldPW, out string ErrorMessage)
		{
			//Setting the parameters for password validation
			var hasNumber = new Regex(@"[0-9]+");
			var hasSymbols = new Regex(@"[!@#$%^&*()_+=\[{\]};:<>|./?,-]");
			var hasUpperChar = new Regex(@"[A-Z]+");
			var hasMinMaxChars = new Regex(@".{8,12}");
			var hasLowerChar = new Regex(@"[a-z]+");

			var isValidated = hasNumber.IsMatch(txtNewPW) && hasUpperChar.IsMatch(txtNewPW) && hasMinMaxChars.IsMatch(txtNewPW) && hasSymbols.IsMatch(txtNewPW) 
				&& hasLowerChar.IsMatch(txtNewPW);  //This checks the password for each parameter and puts the bool result into the variable

			if (!isValidated) //If the password fails the validation check, return the following error
			{
				ErrorMessage = "The password you entered did not meet the criteria: 1 uppercase character, 1 lowercase characeter, 1 number, 1 symbol, and it must be between 8-12 characters.";
				return false;
			}
			
			else if(!CheckOldPassword(currentUser, txtOldPW, connLogin, out ErrorMessage)) //Takes the old password input and validates it against what is in the DB
			{
				return false;
			}
			else if(!PasswordHistoryCheck(connLogin, currentUser, txtNewPW)) //Checks password history to make sure that your new password isnt one of your last 5
			{
				ErrorMessage = "The password you entered is one of the last 5 you used.";
				return false;
			}
			else
			{
				ErrorMessage = "";
				return true;
			}
		}

		private bool PasswordHistoryCheck(OleDbConnection connLogin, string currentUser, string txtNewPW)
		{
			OleDbCommand historyQuery = new OleDbCommand("SELECT Password FROM PasswordHistory WHERE UserID = '" + @currentUser + "'", 
				connLogin);  //Query to select all Password History values for intended user

			OleDbDataAdapter historyAdapter = new OleDbDataAdapter(historyQuery);  //Data adapter for translating the query
			DataTable historyTable = new DataTable();
			
			historyAdapter.Fill(historyTable); //fills the table with the translated values

			PasswordHasher historyHasher = new PasswordHasher(); //new history 

			foreach (DataRow row in historyTable.Rows)
			{
				string dbValue = row["Password"].ToString();  //Converts current row under password column to a string
				int doesExist = historyHasher.passwordHashCompare(dbValue, txtNewPW); //compares the database password string with the input string
				if(doesExist == 1) //if doesExist == 1, it exists and the password cant be used so return false
				{	
					return false;
				}

			}
			return true; //If the if statement doesnt return false in any iteration, return true.
			
		}

		private bool CheckOldPassword(string currentUser, string txtOldPW, OleDbConnection connLogin, out string errorMessage)
		{
			OleDbCommand checkOldPW = new OleDbCommand("SELECT Password FROM Login WHERE UserID='" + @currentUser + "'", connLogin);  //Query to select the current/"Old" password
			string oldDBPW = (string) checkOldPW.ExecuteScalar(); //Execute the query and save the string to a variable

			PasswordHasher oldPWCompare = new PasswordHasher(); 

			int pwCompareConfirm = oldPWCompare.passwordHashCompare(oldDBPW, txtOldPW);  //Compares 2 salted hash passwords and saves the result as an int

			if (pwCompareConfirm == 0) //The output is either a 1 or a 0.  0 means they dont match.  1 means they do.
			{
				errorMessage = "Passwords do not match";
				return false;
			}
			else
			{
				errorMessage = "";
				return true;
			}
		}

		public bool ExpirationReset(OleDbConnection connLogin, string currentUser, out string errorMessage)
		{
			OleDbCommand updateQuery = new OleDbCommand("SELECT LastReset FROM Accounts WHERE UserID = '" + @currentUser + "'", connLogin);  //Query to select when the last reset w
			var lastReset = (DateTime)updateQuery.ExecuteScalar();  //Command to run the query and convert the output to DateTime
			var currentDate = DateTime.Now;  

			var totaldays = (currentDate - lastReset).TotalDays;  //Total days from last reset

			if (totaldays < 80)  //Do nothing if the users last reset is within 80 days from today
			{
				errorMessage = null;
				return true;
			}
			else if(totaldays >= 80 && totaldays < 90)  //If the users last reset was between 80 - 90 days ago, let them know.
			{
				int daysRemaining = (int)(90 - totaldays);
				errorMessage = "You have " + daysRemaining + " days before your password expires";

				return true;
			}
			else
			{
				errorMessage = "You must reset your password now.";  //Password reset was at least 90 days ago, time to reset it.
				return false;
			}
		}
	}
}
