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

namespace BibliophileApplication
{
    public partial class LoginWindow : Window
    {
        internal ViewModels.LoginWindowViewModel viewModel; 

        public LoginWindow(ViewModels.LoginWindowViewModel viewModel)
        {
            InitializeComponent();

            DataContext = this.viewModel = viewModel;
        }

        private void Accept_Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace (usernamebox.Text) ||
                string.IsNullOrWhiteSpace (passwordbox.Password))
            {
                MessageBox.Show("Enter username/password", "Error", MessageBoxButton.OK);
                return;
            }

            viewModel.Password = passwordbox.Password;

            Close();
        }

        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            viewModel.UserName = viewModel.Password = null;

            Close();
        }

        private void usernamebox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                Accept_Button_Click(sender, null);
            }
        }

        private void passwordbox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                Accept_Button_Click(sender, null);
            }
        }
    }
}
