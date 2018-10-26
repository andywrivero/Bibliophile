using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    public partial class UserCardInfoControl : UserControl
    {
        public Models.User User
        {
            get
            {
                return DataContext as Models.User;
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

        public UserCardInfoControl()
        {
            InitializeComponent();

            combobox.ItemsSource = Enumerable.Range(1, 120);
        }

        private void SetEditable(bool editOption)
        {
            // Get the list of texboxes
            List<Control> controls = new List<Control>();
            GetAllTextBoxes(maingrid, controls);

            // Enable or disable textboxes according to editOption
            foreach (var texBox in controls)
                texBox.IsEnabled = editOption;

            // Enable or disable combobox according to editOption
            combobox.IsEnabled = editOption;
        }

        private void GetAllTextBoxes(DependencyObject element, List<Control> controls)
        {
            if (element != null)
            {
                if (element is TextBox) // if the element is a textbox just add it to the list
                    controls.Add(element as TextBox);
                else // else visit each child of the element in search of a textbox
                    for (int i = 0; i < VisualTreeHelper.GetChildrenCount(element); i++)
                        GetAllTextBoxes(VisualTreeHelper.GetChild(element, i), controls);
            }
        }

        public bool ValidateInfo ()
        {
            if (string.IsNullOrWhiteSpace(cardidbox.Text) || !int.TryParse(cardidbox.Text, out int cardid))
            {
                MessageBox.Show("Enter a valid library card id", "Error", MessageBoxButton.OK);
                return false;
            }

            if (string.IsNullOrWhiteSpace(firstnamebox.Text) || string.IsNullOrWhiteSpace(lastnamebox.Text))
            {
                MessageBox.Show("Please enter valid first name and last name", "Error", MessageBoxButton.OK);
                return false;
            }

            if (string.IsNullOrWhiteSpace(addressbox.Text) || string.IsNullOrWhiteSpace(citybox.Text) ||
                string.IsNullOrWhiteSpace(statebox.Text) || string.IsNullOrWhiteSpace (zipbox.Text) ||
                !int.TryParse(zipbox.Text, out int zip))
            {
                MessageBox.Show("Enter some address", "Error", MessageBoxButton.OK);
                return false;
            }

            if (combobox.SelectedIndex == -1)
            {
                MessageBox.Show("Please enter age", "Error", MessageBoxButton.OK);
                return false;
            }

            return true;
        }
    }
}
