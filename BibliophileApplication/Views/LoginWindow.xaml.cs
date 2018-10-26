using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
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

namespace BibliophileApplication.Views
{
    public partial class LoginWindow : Window
    {
        public LoginWindow(LoginWindowViewModel viewModel)
        {
            InitializeComponent();

            DataContext = viewModel;
        }

        private void Accept_Button_Click(object sender, RoutedEventArgs e)
        {
            // Check the username and password is not null or empty
            if (string.IsNullOrWhiteSpace (usernamebox.Text) ||
                string.IsNullOrWhiteSpace (passwordbox.Password))
            {
                MessageBox.Show("Invalid username/password", "Error", MessageBoxButton.OK);
                return;
            }

            // Before closing set the password of the view model
            (DataContext as LoginWindowViewModel).PassWord = passwordbox.Password;

            Close();
        }

        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            // Handle the Return Key on the textboxes as the Accept Button
            if (e.Key == Key.Return)
            {
                Accept_Button_Click(sender, null);
            }
        }
    }
}
