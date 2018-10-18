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
    public partial class UserCardControl : UserControl
    {
        // The object User is the ViewModel of this control. 
        // When it changes then the DataContext of this control is updated accordingly
        private Models.User _user;
        public Models.User User
        {
            get => _user;

            set
            {
                DataContext = _user = value;
            }
        }

        public UserCardControl()
        {
            InitializeComponent();

            // set the item source of the Age combobox to a list of integers 1..120
            combobox.ItemsSource = Enumerable.Range(1, 120);
        }

        public void SetEditable(bool editOption)
        {
            // Get the list of texboxes
            List<Control> controls = new List<Control> ();
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
    }
}
