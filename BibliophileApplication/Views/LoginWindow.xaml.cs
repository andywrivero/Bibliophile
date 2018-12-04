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
        public LoginWindow(LoginWindowViewModel viewmodel)
        {
            InitializeComponent();

            DataContext = viewmodel ?? throw new NullReferenceException();
        }

        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
