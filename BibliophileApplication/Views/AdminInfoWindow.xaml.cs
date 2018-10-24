using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;

namespace BibliophileApplication.Views
{
    /// <summary>
    /// Interaction logic for AdminInfoWindow.xaml
    /// </summary>
    public partial class AdminInfoWindow : Window
    {
        private enum MODE { NEWADMIN, MODIFYADMIN };
        private MODE mode;

        private ObservableCollection<Models.User> admins;
        private Models.Admin admin;

        public AdminInfoWindow(ObservableCollection<Models.User> admins)
        {
            this.admins = admins ?? throw new NullReferenceException("Invalid reference to an admin list");

            InitializeComponent();

            DataContext = new Models.Admin();

            mode = MODE.NEWADMIN;
        }
        public AdminInfoWindow(Models.Admin admin)
        {
            this.admin = admin ?? throw new NullReferenceException("Invalid reference to an admin");

            InitializeComponent();

            DataContext = new Models.Admin()
            {
                UserId = admin.UserId,
                FirstName = admin.FirstName,
                LastName = admin.LastName,
                Address = admin.Address,
                City = admin.City,
                State = admin.State,
                ZipCode = admin.ZipCode,
                Email = admin.Email,
                Age = admin.Age,
                UserName = admin.UserName,
                PassWord = admin.PassWord
            };

            mode = MODE.MODIFYADMIN;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Validate window information

            if (mode == MODE.NEWADMIN)
            {
                string password = "admin1234"; // Default

                if (!string.IsNullOrWhiteSpace(passwordbox.Password))
                    password = passwordbox.Password;

                Models.Admin newAdmin = DataContext as Models.Admin;
                newAdmin.PassWord = Others.PasswordHasher.HashPassword(password);

                admins.Add(newAdmin);

                Close();
            }
            else
            {
                Models.Admin modAdmin = DataContext as Models.Admin;

                admin.UserId = modAdmin.UserId;
                admin.FirstName = modAdmin.FirstName;
                admin.LastName = modAdmin.LastName;
                admin.Address = modAdmin.Address;
                admin.City = modAdmin.City;
                admin.State = modAdmin.State;
                admin.ZipCode = modAdmin.ZipCode;
                admin.Email = modAdmin.Email;
                admin.Age = modAdmin.Age;
                admin.UserName = modAdmin.UserName;

                if (!string.IsNullOrWhiteSpace(passwordbox.Password))
                    admin.PassWord = Others.PasswordHasher.HashPassword(passwordbox.Password);

                Close();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
