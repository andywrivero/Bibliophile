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
using BibliophileApplication.ViewModels;
using BibliophileApplication.Models;

namespace BibliophileApplication.Views
{
    public partial class AdminMainWindow : Window
    {
        // private fields viewmode, and the database context
        private AdminWindowViewModel viewmodel;
        private BibliophileContext db;
        private Admin windowAdmin;

        public AdminMainWindow(int adminId)
        {
            InitializeComponent();

            // Create the databse context of this window
            db = new BibliophileContext();

            // When the window closes save any pending changes and dispose of the context
            Closed += (sender, e) =>
            {
                db.SaveChanges();
                db.Dispose();
            };

            // Initialize the viewmodel of this window
            viewmodel = new AdminWindowViewModel();

            // Load the data sets
            db.Users.Load ();
            db.Books.Load ();

            // Select the two main lists to be use for the view model of the window
            viewmodel.Users = db.Users.Local;
            viewmodel.Books = db.Books.Local;

            // Select the two custom queries of the lists. One for the Admins, the other one for the checkouts
            viewmodel.Admins = new ObservableCollection<Admin>(from u in viewmodel.Users
                                                                      where u is Admin
                                                                      select u as Admin);

            viewmodel.Checkouts = new ObservableCollection<Tuple<User, Book>>(from u in viewmodel.Users
                                                                                            from b in u.Books
                                                                                            select Tuple.Create(u, b));

            // When the user collection changes we check if there was an admin employee and we add it to the query
            viewmodel.Users.CollectionChanged += (sender, e) =>
            {
                if (e.Action == NotifyCollectionChangedAction.Add)
                {
                    foreach (var user in e.NewItems)
                        if (user is Admin admin)
                            viewmodel.Admins.Add(admin);
                }
            };

            DataContext = viewmodel;

            // Find the admin owner of this window by Id
            windowAdmin = viewmodel.Admins.FirstOrDefault(a => a.UserId == adminId);

            // Check such admin exist
            if (windowAdmin != null)
                Title = $"Logged in as {windowAdmin.UserName}";
            else
            {
                throw new Exception("Exception. Only employees can open the admin window");
            }
        }

        private void AddUser_MenuItem_Click(object sender, RoutedEventArgs e)
        {
            // create a new user window and pass the list of users to be modified
            new UserInfoWindow(viewmodel.Users).ShowDialog();
            db.SaveChanges();
        }

        private void ModifyUser_MenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (usergrid.SelectedIndex > -1)
            {
                // create a modifying user window and pass the reference of the user that needs to be modified
                new UserInfoWindow(usergrid.SelectedItem as User).ShowDialog();
                db.SaveChanges();
            }
        }

        private void RemoveUser_MenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (usergrid.SelectedIndex > -1)
            {
                if (viewmodel.Users[usergrid.SelectedIndex] is Admin)
                {
                    MessageBox.Show("Delete admin employee in the Employee tab", "Error", MessageBoxButton.OK);
                }
                else if (viewmodel.Users[usergrid.SelectedIndex].Books.Count > 0)
                {
                    MessageBox.Show("User cannot be removed", "Error", MessageBoxButton.OK);
                }
                else
                {
                    // remove the selected user
                    viewmodel.Users.RemoveAt(usergrid.SelectedIndex);
                    db.SaveChanges();
                }
            }
        }

        private void AddEmployee_MenuItem_Click(object sender, RoutedEventArgs e)
        {
            new AdminInfoWindow(viewmodel.Users).ShowDialog();
            db.SaveChanges();
        }

        private void ModifyEmployee_MenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (employeegrid.SelectedIndex > -1)
            {
                new AdminInfoWindow(employeegrid.SelectedItem as Admin).ShowDialog();
                db.SaveChanges();
            }
        }

        private void RemoveEmployee_MenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (viewmodel.Admins.Count == 1)
            {
                MessageBox.Show("Admin employee cannot be removed");
                return;
            }

            if (employeegrid.SelectedIndex > -1)
            {
                User employee = employeegrid.SelectedItem as User;

                if (employee.Books.Count > 0)
                {
                    MessageBox.Show("Admin employee cannot be removed", "Error", MessageBoxButton.OK);
                }
                else
                {
                    viewmodel.Users.Remove(employee);
                    viewmodel.Admins.RemoveAt(employeegrid.SelectedIndex);
                    db.SaveChanges();
                }
            }
        }

        private void AddBook_MenuItem_Click(object sender, RoutedEventArgs e)
        {
            new BookInfoWindow(viewmodel.Books).ShowDialog();
            db.SaveChanges();
        }

        private void ModifyBook_MenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (bookgrid.SelectedIndex > -1)
            {
                new BookInfoWindow(bookgrid.SelectedItem as Book).ShowDialog();
                db.SaveChanges();
            }
        }

        private void RemoveBook_MenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (bookgrid.SelectedIndex > -1)
            {
                if (viewmodel.Books[bookgrid.SelectedIndex].TotalCopies != viewmodel.Books[bookgrid.SelectedIndex].AvailableCopies)
                {
                    MessageBox.Show("Book cannot be removed", "Error", MessageBoxButton.OK);
                }
                else
                {
                    viewmodel.Books.RemoveAt(bookgrid.SelectedIndex);
                    db.SaveChanges();
                }
            }
        }

        private void RemoveCheckout_MenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (checkoutgrid.SelectedIndex > -1)
            {
                var user = (checkoutgrid.SelectedItem as Tuple<User, Book>).Item1;
                var book = (checkoutgrid.SelectedItem as Tuple<User, Book>).Item2;

                user.Books.Remove(book);

                book.Users.Remove(user);
                book.AvailableCopies++;

                viewmodel.Checkouts.RemoveAt(checkoutgrid.SelectedIndex);

                db.SaveChanges();
            }
        }

        private void CheckoutBook_MenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (bookgrid.SelectedIndex > -1)
            {
                Book book = bookgrid.SelectedItem as Book;

                if (book.AvailableCopies <= 0)
                {
                    MessageBox.Show("No more copies of this book can be checked out", "Error", MessageBoxButton.OK);
                    return;
                }

                new BookCheckoutWindow(viewmodel.Users, book).ShowDialog();

                // Update the checkout list
                viewmodel.Checkouts = new ObservableCollection<Tuple<User, Book>>(from u in viewmodel.Users
                                                                                  from b in u.Books
                                                                                  select Tuple.Create(u, b));

                db.SaveChanges();
            }
        }
    }
}
