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

            //using (var db = new BibliophileContext())
            //{
            //    db.Users.Add(new Admin()
            //    {
            //        UserId = 1,
            //        UserName = "s",
            //        FirstName = "s",
            //        LastName = "s",
            //        PassWord = Others.PasswordHasher.HashPassword("s")
            //    });

            //    db.SaveChanges();
            //}
        }

        private void Admin_Button_Click(object sender, RoutedEventArgs e)
        {
            // Create a login window 
            LoginWindowViewModel viewmodel = new LoginWindowViewModel();
            LoginWindow loginWindow = new LoginWindow(viewmodel) { Owner = this };
            loginWindow.ShowDialog();

            if (viewmodel.Admin == null) return;

            // If login was successful then create the adminwindow
            AdminMainWindow adminWindow = new AdminMainWindow(viewmodel.Admin.UserId.Value) { Owner = this };
            adminWindow.Closed += (sender2, e2) => { Show(); };

            Hide();
            adminWindow.Show();
        }

        private void User_Button_Click(object sender, RoutedEventArgs e)
        {
            // create user window
            UserMainWindow userWindow = new UserMainWindow();

            // handle the closed event of the user window to restore the main window
            userWindow.Closed += (sender2, e2) => { Show(); };

            Hide();
            userWindow.Show();
        }
    }
}
