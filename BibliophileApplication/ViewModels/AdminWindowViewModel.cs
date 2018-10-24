using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BibliophileApplication.Models;

namespace BibliophileApplication.ViewModels
{
    public class AdminWindowViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Admin> _admins;
        private ObservableCollection<User> _users;
        private ObservableCollection<Book> _books;

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<Admin> Admins
        {
            get => _admins;

            set
            {
                _admins = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Admins"));
            }
        }
        public ObservableCollection<Models.User> Users
        {
            get => _users;

            set
            {
                _users = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Users"));
            }
        }
        public ObservableCollection<Models.Book> Books
        {
            get => _books;

            set
            {
                _books = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Books"));
            }
        }

    }
}
