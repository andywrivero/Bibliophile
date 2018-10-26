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
using BibliophileApplication.ViewModels;
using BibliophileApplication.Models;

namespace BibliophileApplication.Views
{
    public partial class BookCheckoutWindow : Window
    {
        private ObservableCollection<User> users;
        private CheckoutViewModel viewmodel;

        public BookCheckoutWindow(ObservableCollection<User> users, Book book)
        {
            this.users = users ?? throw new NullReferenceException("Users null reference exception in BookCheckoutWindow");

            InitializeComponent();

            viewmodel = new CheckoutViewModel()
            {
                Book = book ?? throw new NullReferenceException ("Book null reference exception in BookCheckoutWindow")
            };

            DataContext = viewmodel;
        }

        private void Request_Button_Click(object sender, RoutedEventArgs e)
        {
            // Validation goes here

            var user = users.FirstOrDefault(u => u.UserId == viewmodel.UserId && u?.Email == viewmodel.Email);

            if (user == null)
            {
                MessageBox.Show("User not found", "Error", MessageBoxButton.OK);
            }
            else if (user?.Books.Count >= User.MAXCOPIES)
            {
                MessageBox.Show("User cannot not rent more books", "Error", MessageBoxButton.OK);
            }
            else
            {
                user.Books.Add(viewmodel.Book);
                viewmodel.Book.Users.Add(user);
                viewmodel.Book.AvailableCopies--;

                Close();
            }
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                Request_Button_Click(sender, null);
            }
        }
    }
}
