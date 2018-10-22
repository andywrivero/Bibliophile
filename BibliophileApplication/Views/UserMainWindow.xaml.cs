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

namespace BibliophileApplication.Views
{
    public partial class UserMainWindow : Window
    {
        // A UserMainWindow for the Guest
        public UserMainWindow()
        {
            InitializeComponent();

            LoadBooks();

            bookcardcontrol.SetEditable(false);

            // Bind the request control
            requestgroupbox.DataContext = new ViewModels.RequestViewModel();
        }

        private void LoadBooks ()
        {
            // Load book collection from database
            using (Models.BibliophileContext db = new Models.BibliophileContext())
            {
                db.Books.Load();
                datagrid.ItemsSource = db.Books.Local;
            }
        }

        private void Request_Button_Click(object sender, RoutedEventArgs e)
        {
            // Check there's a book selection
            if (datagrid.SelectedItem is Models.Book book) 
            {
                // Obtain the request information
                ViewModels.RequestViewModel request = requestgroupbox.DataContext as ViewModels.RequestViewModel;
                
                // Validate for nullity
                if (string.IsNullOrWhiteSpace(request.UserId) || string.IsNullOrWhiteSpace(request.Email))
                {
                    MessageBox.Show("Enter a valid User Card Id and associated Email", "Error", MessageBoxButton.OK);
                    return;
                }

                // Validate for user id is an integer
                if (!int.TryParse (request.UserId, out int userid))
                {
                    MessageBox.Show("Enter a valid User Card Id", "Error", MessageBoxButton.OK);
                    return;
                }

                // Search the database for the user and book
                using (Models.BibliophileContext db = new Models.BibliophileContext())
                {
                    // Find user with requested user id and email
                    Models.User user = db.Users.FirstOrDefault(u => u.UserId == userid && u.Email == request.Email);

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
    }
}
