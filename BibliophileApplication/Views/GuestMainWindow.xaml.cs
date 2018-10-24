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

namespace BibliophileApplication.Views
{
    public partial class GuestMainWindow : Window
    {
        // A UserMainWindow for the Guest
        public GuestMainWindow()
        {
            InitializeComponent();

            // Set the view model of this window
            DataContext = new ViewModels.UserMainWindowViewModel()
            {
                Books = GetBookList()
            };
        }

        private List<Models.Book> GetBookList ()
        {
            // The book list to be returned
            List<Models.Book> bookList;

            // Load book collection from database
            using (Models.BibliophileContext db = new Models.BibliophileContext())
            {
                db.Books.Load();
                bookList = db.Books.Local.ToList ();
            }

            return bookList;
        }

        private void Request_Button_Click(object sender, RoutedEventArgs e)
        {
            // Check there's a book selection
            if (datagrid.SelectedItem is Models.Book book) 
            {
                // get datacontext viewmodel
                ViewModels.UserMainWindowViewModel viewmodel = DataContext as ViewModels.UserMainWindowViewModel;

                // get the user id and email from this viewmodel
                string strUserId = viewmodel.UserId;
                string email = viewmodel.Email;

                // Validate for nullity
                if (string.IsNullOrWhiteSpace(strUserId) || string.IsNullOrWhiteSpace(email))
                {
                    MessageBox.Show("Enter a valid User Card Id and associated Email", "Error", MessageBoxButton.OK);
                    return;
                }

                // Validate user id is an integer
                if (!int.TryParse (strUserId, out int userid))
                {
                    MessageBox.Show("Enter a valid User Card Id", "Error", MessageBoxButton.OK);
                    return;
                }

                // Search the database for the user and book
                using (Models.BibliophileContext db = new Models.BibliophileContext())
                {
                    // Find user with requested user id and email
                    Models.User user = db.Users.FirstOrDefault(u => u.UserId == userid && u.Email == email);

                    // If the user is not found print a message
                    if (user == null)
                    {
                        MessageBox.Show("User not found!", "Error", MessageBoxButton.OK);
                        return;
                    }

                    // Check the number of book copies this user has
                    if (user.Books.Count > Models.User.MAXCOPIES)
                    {
                        MessageBox.Show($"Users cannot checkout more than {Models.User.MAXCOPIES} books", "Error", MessageBoxButton.OK);
                        return;
                    }

                    // Insert code here.
                    // The code below should place a user request in the Request List in the Database. 
                    // This way a copy can be held at the library, or in case no copy is availble the user can be notified by email
                    // later on when one becomes available based on the user current position in the Request List

                    // Notice the message below is not really doing anything yet
                    MessageBox.Show("Your request has been submitted successfully!", "Info", MessageBoxButton.OK);
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
