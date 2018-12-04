using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using BibliophileApplication.Others;
using BibliophileApplication.Models;
using System.Windows;

namespace BibliophileApplication.ViewModels
{
    public class LoginWindowViewModel 
    {
        public Admin Admin { get; private set; } = null;
        public string UserName { get; set; }
        public string PassWord { get; set; }
        public DelegateCommand LoginDelegateCommand { get; set; }

        public LoginWindowViewModel()
        {
            LoginDelegateCommand = new DelegateCommand(LoginUser, VerifyUser);
        }

        public void LoginUser (object obj)
        {
            // Get the admin employee with such username and password
            Admin = GetAdmin(UserName, PassWord);

            // Check such admin exist
            if (Admin == null)
            {
                MessageBox.Show("Incorrect login information", "Error", MessageBoxButton.OK);
                return;
            }
        }

        public bool VerifyUser(object obj)
        {
            if (obj == null) return false;
            if (!(obj is PasswordBox passwordBox)) return false;

            PassWord = passwordBox.Password;

            // Check the username and password is not null or empty
            return !string.IsNullOrWhiteSpace(UserName) && !string.IsNullOrWhiteSpace(PassWord);
        }

        // Find and return the admin from the database that matches username and password
        private Admin GetAdmin(string username, string password)
        {
            Admin admin = null;

            using (var db = new BibliophileContext())
            {
                // Find the admin employee in the database that matches username and password
                admin = (from user in db.Users
                         where user is Admin
                         select user as Admin).AsEnumerable().FirstOrDefault(a => a.UserName == username && PasswordHasher.VerifyPassword(password, a.PassWord));
            }

            return admin;
        }
    }
}
