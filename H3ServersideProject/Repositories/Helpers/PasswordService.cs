﻿using Microsoft.AspNetCore.Identity;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
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


        /// <summary>
        /// The next 3 methods is used to recover a password.
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public int RandomNumber(int min, int max)
        {
            // Generate a number between 1001 and 9999.
            Random random = new Random();
            return random.Next(min, max);
        }

        public string RandomString(int size, bool lowerCase)
        {
            // Creates a random string.
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            if (lowerCase)
                return builder.ToString().ToLower();
            return builder.ToString();
        }

        public string RandomPassword(int size = 0)
        {
            // Build the new random password with the "RandomString" and "RandomNumber" methods and connects them together.
            StringBuilder builder = new StringBuilder();
            builder.Append(RandomString(4, true));
            builder.Append(RandomNumber(1000, 9999));
            builder.Append(RandomString(2, false));
            return builder.ToString();
        }
    }
}
