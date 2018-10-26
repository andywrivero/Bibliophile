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
using BibliophileApplication.ViewModels;
using BibliophileApplication.Models;

namespace BibliophileApplication.Views
{
    public partial class UserMainWindow : Window
    {
        // The User Main Window
        public UserMainWindow()
        {
            InitializeComponent();

            // Set the view model of this window
            DataContext = new UserMainWindowViewModel()
            {
                Books = GetBookList()
            };
        }

        // No Observable collection is used because users are not to modify data
        private List<Book> GetBookList ()
        {
            // The book list to be returned
            List<Book> bookList;

            // Load book collection from database
            using (var db = new BibliophileContext())
            {
                db.Books.Load();
                bookList = db.Books.Local.ToList ();
            }

            return bookList;
        }

        private void Request_Button_Click(object sender, RoutedEventArgs e)
        {
            // Check there's a book selection
            if (datagrid.SelectedIndex > -1) 
            {
                // get the user id and email 
                string strUserId = (DataContext as UserMainWindowViewModel).UserId;
                string email = (DataContext as UserMainWindowViewModel).Email;

                // Validate for request information
                if (string.IsNullOrWhiteSpace(strUserId) || string.IsNullOrWhiteSpace(email) || !int.TryParse(strUserId, out int userid))
                {
                    MessageBox.Show("Enter a valid User Card Id and associated Email", "Error", MessageBoxButton.OK);
                    return;
                }

                // get the selected book
                int bookid = (datagrid.SelectedItem as Book).BookId.Value;

                // Search the database for the user and book
                using (var db = new BibliophileContext())
                {
                    // Find user with requested user id and email
                    User user = db.Users.FirstOrDefault(u => u.UserId == userid && u.Email == email);

                    // Check the user exist with such userid and email
                    if (user == null)
                    {
                        MessageBox.Show("User not found!", "Error", MessageBoxButton.OK);
                        return;
                    }

                    // Check the number of book copies this user has is not more than what's allowed
                    if (user.Books.Count >= User.MAXCOPIES)
                    {
                        MessageBox.Show($"Users cannot checkout more than {User.MAXCOPIES} books", "Error", MessageBoxButton.OK);
                        return;
                    }

                    // Find the selected book in the databse
                    Book book = db.Books.Find(bookid);

                    // Check if there are enough copies of the book available
                    if (book.AvailableCopies > 0)
                    {
                        // Check out the book
                        user.Books.Add(book);
                        book.Users.Add(user);
                        book.AvailableCopies--;
                        db.SaveChanges();

                        MessageBox.Show("Your copy has been reserved", "Info", MessageBoxButton.OK);
                    }
                    else
                    {
                        // This is a pending feature that is not yet implemented
                        // There will be a list of pending request and users are notified by email when a copy of the book
                        // they want becomes available

                        MessageBox.Show("There are not enough copies of the book. Your request is pending", "Info", MessageBoxButton.OK);
                    } 
                }
            }
            else
            {
                MessageBox.Show("Please make a book selection", "Error", MessageBoxButton.OK);
                return;
            }
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                // We need to force the focus out of the texbox so the binded property can get its value
                (sender as TextBox).MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
                // call the button click event handler to process the request
                Request_Button_Click(sender, null);
            }
        }
    }
}
