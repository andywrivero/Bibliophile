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
using BibliophileApplication.Models;
using BibliophileApplication.ViewModels;

namespace BibliophileApplication.Views
{

    public partial class AdminInfoWindow : Window
    {
        private enum MODE { NEWADMIN, MODIFYADMIN };
        private MODE mode;

        private ObservableCollection<User> users;
        private Admin admin;

        // This constructor is to construct a new admin employee 
        public AdminInfoWindow(ObservableCollection<User> users)
        {
            this.users = users ?? throw new NullReferenceException("Invalid reference to an admin list");

            InitializeComponent();

            DataContext = new Admin();

            mode = MODE.NEWADMIN;
            Title = "New Admin Employee";
        }

        // This constructor for this window is to modify an existing admin employee
        public AdminInfoWindow(Admin admin)
        {
            this.admin = admin ?? throw new NullReferenceException("Invalid reference to an admin");

            InitializeComponent();

            DataContext = new Admin()
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
                HireDate = admin.HireDate,
                UserName = admin.UserName,
                PassWord = admin.PassWord
            };

            mode = MODE.MODIFYADMIN;
            Title = "Modify Admin Employee";
        }

        private void Accept_Button_Click(object sender, RoutedEventArgs e)
        {
            // Validate window information
            if (!admincardcontrol.ValidateInfo()) return;

            if (mode == MODE.NEWADMIN)
            {
                Admin newAdmin = DataContext as Admin;

                if (users.FirstOrDefault (u => u.UserId == newAdmin.UserId) != null)
                {
                    MessageBox.Show("Enter a different card id", "Error", MessageBoxButton.OK);
                    return;
                }

                if (string.IsNullOrWhiteSpace(passwordbox.Password))
                    newAdmin.PassWord = Others.PasswordHasher.HashPassword("1234"); // default password
                else
                    newAdmin.PassWord = Others.PasswordHasher.HashPassword(passwordbox.Password);

                users.Add(newAdmin);

                Close();
            }
            else
            {
                Admin modAdmin = DataContext as Admin;

                if (modAdmin.UserId != admin.UserId)
                {
                    MessageBox.Show("Card id cannot be modified", "Error", MessageBoxButton.OK);
                    return;
                }

                admin.FirstName = modAdmin.FirstName;
                admin.LastName = modAdmin.LastName;
                admin.Address = modAdmin.Address;
                admin.City = modAdmin.City;
                admin.State = modAdmin.State;
                admin.ZipCode = modAdmin.ZipCode;
                admin.Email = modAdmin.Email;
                admin.Age = modAdmin.Age;
                admin.HireDate = modAdmin.HireDate;
                admin.UserName = modAdmin.UserName;

                // if the user types a new password then change it
                if (!string.IsNullOrWhiteSpace(passwordbox.Password))
                    admin.PassWord = Others.PasswordHasher.HashPassword(passwordbox.Password);

                Close();
            }
        }

        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
