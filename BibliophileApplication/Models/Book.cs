using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliophileApplication.Models
{
    public class Book : INotifyPropertyChanged
    {
        private int? _bookId;
        private string _title;
        private string _author;
        private int _publicationYear;
        private string _genre;
        private int _totalCopies;
        private int _availableCopies;

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int? BookId
        {
            get => _bookId;

            set
            {
                if (_bookId == null) // Set only once
                {
                    _bookId = value;
                    NotifyPropertyChanged("BookId");
                }
            }
        }
        public string Title
        {
            get => _title;

            set
            {
                _title = value;
                NotifyPropertyChanged("Title");
            }
        }
        public string Author
        {
            get => _author;

            set
            {
                _author = value;
                NotifyPropertyChanged("Author");
            }
        }
        public int PublicationYear
        {
            get => _publicationYear;

            set
            {
                _publicationYear = value;
                NotifyPropertyChanged("PublicationYear");
            }
        }
        public string Genre
        {
            get => _genre;

            set
            {
                _genre = value;
                NotifyPropertyChanged("Genre");
            }
        }
        public int TotalCopies
        {
            get => _totalCopies;

            set
            {
                _totalCopies = value;
                NotifyPropertyChanged("TotalCopies");
            }
        }
        public int AvailableCopies
        {
            get => _availableCopies;

            set
            {
                _availableCopies = value;
                NotifyPropertyChanged("AvailableCopies");
            }
        }
        public virtual ObservableCollection<User> Users { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is Book book)
                return BookId == book.BookId;
            else
                return false;
        }

        public static bool operator ==(Book lhs, Book rhs)
        {
            if (lhs == null) return false;

            return lhs.Equals(rhs);
        }

        public static bool operator !=(Book lhs, Book rhs)
        {
            return !(lhs == rhs);
        }

        public void IncrementBookCopiesBy(int increment)
        {
            TotalCopies += increment;
            AvailableCopies += increment;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
