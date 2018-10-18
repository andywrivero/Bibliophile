using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliophileApplication.ViewModels
{
    public class UserCardViewModel : INotifyPropertyChanged
    {
        private Models.User _user;
        public Models.User User
        {
            get => _user;

            set
            {
                _user = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("User"));
            }
        }

        private bool _isEditable;
        public bool IsEditable
        {
            get => _isEditable;

            set
            {
                _isEditable = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsEdtiable"));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
