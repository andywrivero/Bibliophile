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
    public partial class UserInfoWindow : Window
    {
        private enum MODE { NEWUSER, MODIFYUSER };
        private MODE mode;

        private ObservableCollection<User> users;
        private User user;

        // This contructor is to be called to modify an existing user
        public UserInfoWindow(User user)
        {
            this.user = user ?? throw new NullReferenceException("Invalid null reference user exception");

            InitializeComponent();

            // Set up the datacontext
            DataContext = new User()
            {
                UserId = user.UserId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Address = user.Address,
                City = user.City,
                State = user.State,
                ZipCode = user.ZipCode,
                Email = user.Email, 
                Age = user.Age
            };

            // window mode
            mode = MODE.MODIFYUSER;

            Title = "Modify User";
        }

        // This constructor is to be called to add a new user
        public UserInfoWindow(ObservableCollection<User> users)
        {
            // hold the list where we're adding a new user
            this.users = users ?? throw new NullReferenceException("Invalid null reference user list");

            InitializeComponent();

            // Initialize the user card
            DataContext = new User();

            // window mode
            mode = MODE.NEWUSER;

            Title = "New User";
        }

        private void Accept_Button_Click(object sender, RoutedEventArgs e)
        {
            // first validate the user card info
            if (!usercardinfocontrol.ValidateInfo()) return;

            if (mode == MODE.NEWUSER)
            { 
                if (users.FirstOrDefault(u => u.UserId == usercardinfocontrol.User.UserId) != null)
                {
                    MessageBox.Show("Enter a different card id", "Error", MessageBoxButton.OK);
                    return;
                }

                users.Add(usercardinfocontrol.User);
            } 
            else
            {
                if (user.UserId != usercardinfocontrol.User.UserId)
                {
                    MessageBox.Show("Card id cannot be modified", "Error", MessageBoxButton.OK);
                    return;
                }

                user.FirstName = usercardinfocontrol.User.FirstName;
                user.LastName = usercardinfocontrol.User.LastName;
                user.Address = usercardinfocontrol.User.Address;
                user.City = usercardinfocontrol.User.City;
                user.State = usercardinfocontrol.User.State;
                user.ZipCode = usercardinfocontrol.User.ZipCode;
                user.Email = usercardinfocontrol.User.Email;
                user.Age = usercardinfocontrol.User.Age;
            }

            Close();
        }

        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
