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

namespace BibliophileApplication.Views
{
    public partial class MainWindow : Window
    {
        private ViewModels.LoginWindowViewModel LoginVM { get; set; }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Admin_Button_Click(object sender, RoutedEventArgs e)
        {
            // Create a login window 
            LoginWindow loginWindow = new LoginWindow(LoginVM = new ViewModels.LoginWindowViewModel()) { Owner = this };
            // handle the closed event of the login window to authenticate the admin
            loginWindow.Closed += LoginWindow_Closed;
            // Show login window
            loginWindow.ShowDialog();
        }

        private void LoginWindow_Closed(object sender, EventArgs e)
        {
            if (LoginVM.UserName == null && LoginVM.Password == null) return;

            // Validate admin login information
            Models.Admin admin = GetAdmin(LoginVM.UserName, LoginVM.Password);

            // check the admin exist
            if (admin == null)
                MessageBox.Show("UserName/Password mismatch. Try again", "Error", MessageBoxButton.OK);
            else
            {
                // create the admin window 
                AdminMainWindow adminWindow = new AdminMainWindow();
                // handle the closed event of the admin window to restore the main window 
                adminWindow.Closed += (sender2, e2) => { Show(); };
                // Hide main window, and show admin window
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
        private Models.Admin GetAdmin (string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password)) return null;

            Models.Admin admin = null;

            using (var db = new Models.BibliophileContext())
            {
                var admins = (from user in db.Users
                              where user is Models.Admin
                              select user as Models.Admin).AsEnumerable();

                admin = admins.FirstOrDefault(a => a.UserName == username && Others.PasswordHasher.VerifyPassword(password, a.PassWord));
            }

            return admin;
        }
    }
}
