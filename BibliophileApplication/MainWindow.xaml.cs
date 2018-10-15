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

namespace BibliophileApplication
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //Models.Admin user = new Models.Admin()
            //{
            //    UserId = 1,
            //    FirstName = "Andy",
            //    LastName = "Rivero",
            //    Age = 35,
            //    Email = "andyrivero@mail.usf.edu",
            //    UserName = "andyrivero",
            //    PassWord = Others.PasswordHasher.HashPassword ("admin1234"),
            //    HireDate = DateTime.Now
            //};

            //using (var db = new Models.BibliophileContext ())
            //{
            //    db.Users.Add(user);
            //    db.SaveChanges();
            //}
        }

        private void Admin_Button_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.PasswordWindowViewModel passwordWindowViewModel = new ViewModel.PasswordWindowViewModel();
            PasswordWindow passwordWindow = new PasswordWindow(passwordWindowViewModel);

            passwordWindow.Closed += (sender2, e2) =>
            {
                if (passwordWindowViewModel.UserName != null && passwordWindowViewModel.Password != null)
                {
                    Models.Admin admin = null;
                    string username = passwordWindowViewModel.UserName;
                    string password = passwordWindowViewModel.Password;

                    using (var db = new Models.BibliophileContext())
                    {
                        var admins = (from user in db.Users
                                     where user is Models.Admin
                                     select user as Models.Admin).AsEnumerable ();

                        admin = admins.FirstOrDefault(a => a.UserName == username &&
                                                           Others.PasswordHasher.VerifyPassword (password, a.PassWord));

                        if (admin == null)
                        {
                            MessageBox.Show("UserName/Password mismatch. Try again", "Error", MessageBoxButton.OK);
                        }
                        else
                        {
                            MessageBox.Show($"Welcome back {admin.FirstName} {admin.LastName}");
                        }
                    }
                }
            };

            passwordWindow.ShowDialog();
        }

        private void User_Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
