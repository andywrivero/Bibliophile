using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BibliophileApplication.ViewModels;
using BibliophileApplication.Models;
using BibliophileApplication.Others;

namespace BibliophileApplication.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Admin_Button_Click(object sender, RoutedEventArgs e)
        {
            // Create a login window 
            LoginWindowViewModel loginVM;
            LoginWindow loginWindow = new LoginWindow(loginVM = new LoginWindowViewModel()) { Owner = this };
            loginWindow.ShowDialog();

            // Check if a valid username and password was set in the view model by the login window
            if (loginVM.UserName != null && loginVM.PassWord != null)
            {
                // Get the admin employee with such username and password
                Admin admin = GetAdmin(loginVM.UserName, loginVM.PassWord);

                // Check the login information
                if (admin == null)
                {
                    MessageBox.Show("Incorrect login information", "Error", MessageBoxButton.OK);
                    return;
                }

                // Create a new admin window
                AdminMainWindow adminWindow = new AdminMainWindow(admin.UserId.Value);
                // handle the closed event of the admin window to restore the main window
                adminWindow.Closed += (sender2, e2) => { Show(); };
                // hide main window, and show admin window
                Hide();
                adminWindow.Show();
            }
        }

        private void User_Button_Click(object sender, RoutedEventArgs e)
        {
            // create user window
            GuestMainWindow userWindow = new GuestMainWindow();
            // handle the closed event of the user window to restore the main window
            userWindow.Closed += (sender2, e2) => { Show(); };
            // hide main window, and show user window
            Hide();
            userWindow.Show();
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
