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
    public partial class BookCardControl : UserControl
    {
        // The Datacontext of this control is set when a value is assign to this property
        private Models.Book _book;
        public Models.Book Book
        {
            get => _book;

            set
            {
                DataContext = _book = value;
            }
        }

        public BookCardControl()
        {
            InitializeComponent();

            yearcombobox.ItemsSource = Enumerable.Range(1900, DateTime.Now.Year);
        }

        public void SetEditable(bool editOption)
        {
            // Get the list of texboxes
            List<Control> controls = new List<Control>();
            GetAllTextBoxes(maingrid, controls);

            // Enable or disable textboxes according to editOption
            foreach (var texBox in controls)
                texBox.IsEnabled = editOption;

            // Enable or disable combobox according to editOption
            yearcombobox.IsEnabled = editOption;
            genrecombobox.IsEnabled = editOption;
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
