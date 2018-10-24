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
    public partial class UserInfoWindow : Window
    {
        private enum MODE { NEWUSER, MODIFYUSER };
        private MODE mode;

        private ObservableCollection<Models.User> users;
        private Models.User user;

        // This contructor is to be called to modify an existing user
        public UserInfoWindow(Models.User user)
        {
            this.user = user ?? throw new NullReferenceException("Invalid / null reference user exception");

            InitializeComponent();

            // Set up the user card
            usercardinfocontrol.User = new Models.User()
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

            // save the mode
            this.mode = MODE.MODIFYUSER;
        }

        // This constructor is to be called to add a new user
        public UserInfoWindow(ObservableCollection<Models.User> users)
        {
            // hold the list where we're adding a new user
            this.users = users ?? throw new NullReferenceException("Invalid user list");

            InitializeComponent();

            // Initialize the user card
            usercardinfocontrol.User = new Models.User();

            // save the mode
            this.mode = MODE.NEWUSER;
        }

        private void Accept_Button_Click(object sender, RoutedEventArgs e)
        {
            // remember to validate

            if (mode == MODE.NEWUSER)
            {
                // Add the new user
                users.Add(usercardinfocontrol.User);
            }
            else
            {
                user.UserId = usercardinfocontrol.User.UserId;
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
