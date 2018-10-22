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
        }

        private void LoadBooks ()
        {
            using (Models.BibliophileContext db = new Models.BibliophileContext())
            {
                datagrid.ItemsSource = (from book in db.Books select book).ToList();
            }
        }

        private void Request_Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
