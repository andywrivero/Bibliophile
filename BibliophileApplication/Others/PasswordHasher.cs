/*
 * Password Hashing
 * Based on the idea of Zerkms on Stackoverflow.
 * Library base on the idea of Christian Gollhardt
 * Stackoverflow https://stackoverflow.com/questions/4181198/how-to-hash-a-password
*/

using System;
using System.Security.Cryptography;

namespace BibliophileApplication.Others
{
    public class PasswordHasher
    {
        private static readonly int SALTSIZE = 20;
        private static readonly int HASHSIZE = 20;
        private static readonly int ITERATIONS = 10000;

        private static string HashPasswordWithSalt(string password, byte[] salt)
        {
            byte[] hashBytes = new byte[SALTSIZE + HASHSIZE];

            salt.CopyTo(hashBytes, 0);
            new Rfc2898DeriveBytes(password, salt, ITERATIONS).GetBytes(HASHSIZE).CopyTo(hashBytes, SALTSIZE);

            return Convert.ToBase64String(hashBytes);
        }

        public static string HashPassword(string password)
        {
            if (password == null) return string.Empty;

            byte[] salt = new byte[SALTSIZE];
            new RNGCryptoServiceProvider().GetBytes(salt);

            return HashPasswordWithSalt(password, salt);
        }

        public static bool VerifyPassword(string password, string hashedPassword)
        {
            if (password == null || hashedPassword == null) return false;

            byte[] salt = new byte[SALTSIZE];
            Array.Copy(Convert.FromBase64String(hashedPassword), 0, salt, 0, SALTSIZE);

            return HashPasswordWithSalt(password, salt) == hashedPassword;
        }
    }
}
