using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BibliophileApplication.Others
{
    public class PasswordHasher
    {
        private static readonly int SALTSIZE = 20;
        private static readonly int HASHSIZE = 20;
        private static readonly int ITERATIONS = 10000;

        private static string HashPasswordWithSalt(string password, byte[] salt)
        {
            byte[] hash = new Rfc2898DeriveBytes(password, salt, ITERATIONS).GetBytes(HASHSIZE);
            byte[] hashBytes = new byte[salt.Length + hash.Length];

            salt.CopyTo(hashBytes, 0);
            hash.CopyTo(hashBytes, salt.Length);

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

            byte[] hashBytes = Convert.FromBase64String(hashedPassword);
            byte[] salt = new byte[SALTSIZE];

            Array.Copy(hashBytes, 0, salt, 0, SALTSIZE);

            return HashPasswordWithSalt(password, salt) == hashedPassword;
        }
    }
}
