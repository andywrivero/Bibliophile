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
        private bool edit = false;
        private Models.Admin admin = null;
        private Models.User usercard = new Models.User()
        {
            UserId = 1212312,
            FirstName = "Pepe",
            LastName = "Toto",
            Age = 35,
            Address = "123 aa bb",
            City = "Tampa",
            State = "FL",
            ZipCode = 33061,
            Email = "abc@abc"
        };
        
        // A UserMainWindow for the Guest
        public UserMainWindow()
        {
            InitializeComponent();

            // In this case the Guest don't use the usercardcontrol so we hidde it by collapsing its container
            (usercardcontrol.Parent as GroupBox).Visibility = Visibility.Collapsed;
        }

        // A UserMainWindow for the Admin
        public UserMainWindow (Models.Admin admin)
        {
            InitializeComponent();

            // admin of this window
            this.admin = admin;

            // This is just to try it. We load a random user to in this control
            usercardcontrol.User = usercard;
            usercardcontrol.SetEditable(edit);

            // Change the title of the window
            Title = $"Logged in as {admin.UserName}";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            usercardcontrol.SetEditable(edit = !edit);
        }
    }
}
