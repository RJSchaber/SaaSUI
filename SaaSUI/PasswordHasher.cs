using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;


namespace SaaSUI
{
	class PasswordHasher
	{
		public void passwordHasher(string passwordInput, out string hashedPass)
		{
			byte[] salt;  //create the salt array

			new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);  //This generates the random salt up to 16 bytes

			var pbkdf2 = new Rfc2898DeriveBytes(passwordInput, salt, 10000);  //This takes the password, salts is and then hashes it to 10000 iterations
			byte[] hash = pbkdf2.GetBytes(20); //Adds the new salted hash to a byte array

			byte[] hashBytes = new byte[36];  //creates a new byte array with 36 bytes.  20 for the new hash and 16 for the salt.  Must save the salt for comparing passwords

			Array.Copy(salt, 0, hashBytes, 0, 16);  //Copies the salt to hashbytes starting at array[0] going to [15]
			Array.Copy(hash, 0, hashBytes, 16, 20);  //Copies the hash to hashbytes starting at [16] and going to [35] 

			hashedPass = Convert.ToBase64String(hashBytes);  //Convert the array to a string

		}

		public int passwordHashCompare(string dbPasswordInput, string txtPasswordInput)
		{
			byte[] dbHashBytes = Convert.FromBase64String(dbPasswordInput);  //convert hashed database password from string to byte array

			byte[] salt = new byte[16];  //Make a  byte array for the salt
			Array.Copy(dbHashBytes, 0, salt, 0, 16);  //Copy the salt from the first 16 characters in the DB password to the salt array

			var pbkdf2 = new Rfc2898DeriveBytes(txtPasswordInput, salt, 10000);  //Take the txt input from the user and the salt to perform the same hashing as above

			byte[] txtHashBytes = pbkdf2.GetBytes(20);  //Put the new hash into a new array

			for (int i = 0; i <20; i++)  //Compare arrays 1 byte at a time starting afrer the salt in the database pw (The salt is attached to the beginning of the password)
			{
				if (dbHashBytes[i+16] != txtHashBytes[i])
					return 0;
			}

			return 1;
		}



	}
}
