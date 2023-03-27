using Microsoft.AspNetCore.Identity;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Web.Helpers;

namespace H3ServersideProject.Repositories.Helpers
{
    public class PasswordService
    {
        /// <summary>
        /// Method to hash the password.
        /// </summary>
        /// <param name="password"></param>
        /// <param name="passwordHash"></param>
        /// <param name="passwordSalt"></param>
        public void CreatePasswordHash(string password, out byte[] newHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                // Sets the passwordSalt with HMAC algoritm.
                passwordSalt = hmac.Key;
                // Hash the user password.
                byte [] passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                ComboHash(passwordHash, passwordSalt, out newHash);
            }
        }
        
        /// <summary>
        /// Method to verify the password when the user needs to login.
        /// </summary>
        /// <param name="password"></param>
        /// <param name="passwordHash"></param>
        /// <param name="passwordSalt"></param>
        /// <returns></returns>
        public bool VerifyPassword(string password, byte[]passwordHash, byte[] passwordSalt)
        {
            // Use the password salt to use the algoritm so it can be identical.
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                // Hash the password to compare it with stored hash password.
                var Hash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                ComboHash(Hash, passwordSalt, out byte[] newHash);

                return newHash.SequenceEqual(passwordHash);
            }
        }

        private void ComboHash(byte[] passwordHash, byte[] passwordSalt, out byte[] newHash)
        {
            //Create a new array based on the total number of two array elements to be merged
            newHash = new byte[passwordHash.Length + passwordSalt.Length];

            //Copy the first array to the newly created array
            Array.Copy(passwordHash, 0, newHash, 0, passwordHash.Length);

            //Copy the second array to the newly created array
            Array.Copy(passwordSalt, 0, newHash, passwordHash.Length, passwordSalt.Length);
        }
    }
}
