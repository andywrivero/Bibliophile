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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BibliophileApplication.MyControls
{
    public partial class AdminInfoControl : UserControl
    {
        public Models.Admin Admin
        {
            get
            {
                return DataContext as Models.Admin;
            }

            set
            {
                DataContext = value;
            }
        }
        // Set controls to the edit value;
        public bool Editable
        {
            set
            {
                SetEditable(value);
            }
        }

        public AdminInfoControl()
        {
            InitializeComponent();
        }

        private void SetEditable(bool editOption)
        {
            usernametb.IsEnabled = editOption;
            hireddp.IsEnabled = editOption;
            usercardcontrol.Editable = editOption;
        }

        public bool ValidateInfo ()
        {
            if (!usercardcontrol.ValidateInfo()) return false;

            if (string.IsNullOrWhiteSpace (usernametb.Text))
            {
                MessageBox.Show("Enter a valid UserName", "Error", MessageBoxButton.OK);
                return false;
            }

            if (!hireddp.SelectedDate.HasValue || hireddp.SelectedDate.Value.Year < (DateTime.Now.Year - 150))
            {
                MessageBox.Show("Enter a valid hire date", "Error", MessageBoxButton.OK);
                return false;
            }

            return true;
        }
    }
}
