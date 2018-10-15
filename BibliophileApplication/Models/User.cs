using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;     // re: DatabaseGeneratedOption
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliophileApplication.Models
{
    public class User : INotifyPropertyChanged
    {
        private int? _userId;
        private string _firstName;
        private string _lastName;
        private int _age;
        private string _address;
        private string _city;
        private string _state;
        private int _zipCode;
        private string _email;

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int? UserId
        {
            get => _userId;

            set
            {
                if (_userId == null) // ID can only be set once
                {
                    _userId = value;
                    NotifyPropertyChanged("UserId");
                }
            }
        }
        public string FirstName
        {
            get => _firstName;

            set
            {
                _firstName = value;
                NotifyPropertyChanged("FirstName");
            }
        }
        public string LastName
        {
            get => _lastName;

            set
            {
                _lastName = value;
                NotifyPropertyChanged("LastName");
            }
        }
        public int Age
        {
            get => _age;

            set
            {
                _age = value;
                NotifyPropertyChanged("Age");
            }
        }
        public string Address
        {
            get => _address;

            set
            {
                _address = value;
                NotifyPropertyChanged("Address");
            }
        }
        public string City
        {
            get => _city;

            set
            {
                _city = value;
                NotifyPropertyChanged("City");
            }
        }
        public string State
        {
            get => _state;

            set
            {
                _state = value;
                NotifyPropertyChanged("State");
            }
        }
        public int ZipCode
        {
            get => _zipCode;

            set
            {
                _zipCode = value;
                NotifyPropertyChanged("ZipCode");
            }
        }
        public string Email
        {
            get => _email;

            set
            {
                _email = value;
                NotifyPropertyChanged("Email");
            }
        }
        public virtual ObservableCollection<Book> Books { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is User user)
                return UserId == user.UserId && FirstName == user.FirstName && LastName == user.LastName;
            else
                return false;
        }

        public static bool operator ==(User lhs, User rhs)
        {
            if (lhs is null) return rhs is null;

            return lhs.Equals(rhs);
        }

        public static bool operator !=(User lhs, User rhs)
        {
            return !(lhs == rhs);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
