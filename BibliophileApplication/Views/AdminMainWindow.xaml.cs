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
using System.Windows.Shapes;
using System.Data.Entity;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace BibliophileApplication.Views
{
    public partial class AdminMainWindow : Window
    {
        private ViewModels.AdminWindowViewModel viewmodel;
        private Models.BibliophileContext db;

        public AdminMainWindow()
        {
            InitializeComponent();

            // Create the databse context of this window
            db = new Models.BibliophileContext();

            // When the admin window closes save changes and dispose the context
            Closed += (sender, e) =>
            {
                db.SaveChanges();
                db.Dispose();
            };

            // Initialize the viewmodel of this window
            viewmodel = new ViewModels.AdminWindowViewModel();

            // Load the data sets
            db.Users.Load ();
            db.Books.Load ();

            // Set the viewmodel
            viewmodel.Users = db.Users.Local;
            viewmodel.Books = db.Books.Local;
            viewmodel.Admins = new ObservableCollection<Models.Admin>(from u in viewmodel.Users
                                                                      where u is Models.Admin
                                                                      select u as Models.Admin);

            viewmodel.Users.CollectionChanged += Users_CollectionChanged;

            DataContext = viewmodel;
        }

        private void Users_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (var user in e.NewItems)
                    if (user is Models.Admin admin)
                        viewmodel.Admins.Add(admin);
            }
        }

        private void AddUser_MenuItem_Click(object sender, RoutedEventArgs e)
        {
            new UserInfoWindow(viewmodel.Users).ShowDialog();
        }

        private void ModifyUser_MenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (usergrid.SelectedIndex > 0)
            {
                new UserInfoWindow(usergrid.SelectedItem as Models.User).ShowDialog();
            }
        }

        private void RemoveUser_MenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (usergrid.SelectedIndex > 0)
            {
                viewmodel.Users.RemoveAt(usergrid.SelectedIndex);
            }
        }

        private void AddEmployee_MenuItem_Click(object sender, RoutedEventArgs e)
        {
            new AdminInfoWindow(viewmodel.Users).ShowDialog();
        }

        private void ModifyEmployee_MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            if (employeegrid.SelectedIndex > 0)
            {
                new AdminInfoWindow(employeegrid.SelectedItem as Models.Admin).ShowDialog();
            }
        }

        private void RemoveEmployee_MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            if (employeegrid.SelectedIndex > 0)
            {
                viewmodel.Users.Remove(employeegrid.SelectedItem as Models.User);
                viewmodel.Admins.RemoveAt(employeegrid.SelectedIndex);
            }
        }
    }
}
