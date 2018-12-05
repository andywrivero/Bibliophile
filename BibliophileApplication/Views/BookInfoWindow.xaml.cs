using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public partial class BookInfoWindow : Window
    {
        private enum MODE { NEWBOOK, MODIFYBOOK };
        private MODE mode;

        private ObservableCollection<Models.Book> books;
        private Models.Book book;

        public BookInfoWindow(ObservableCollection<Models.Book> books)
        {
            this.books = books ?? throw new NullReferenceException("Books reference null exception");

            InitializeComponent();

            DataContext = new Models.Book();

            bookcard.IsNewBook = true;

            mode = MODE.NEWBOOK;
        }

        public BookInfoWindow(Models.Book book)
        {
            this.book = book ?? throw new NullReferenceException("Book reference null exception");

            InitializeComponent();

            DataContext = new Models.Book()
            {
                BookId = book.BookId,
                Title = book.Title,
                Author = book.Author,
                PublicationYear = book.PublicationYear,
                Genre = book.Genre,
                TotalCopies = book.TotalCopies,
                AvailableCopies = book.AvailableCopies
            };

            mode = MODE.MODIFYBOOK;
        }

        private void Accept_Button_Click(object sender, RoutedEventArgs e)
        {
            if (!bookcard.ValidateInfo()) return;

            if (mode == MODE.NEWBOOK)
            {
                var newBook = DataContext as Models.Book;

                if (books.FirstOrDefault (b => b.BookId == newBook.BookId) != null)
                {
                    MessageBox.Show("A book with such ISBN already exist", "Error", MessageBoxButton.OK);
                    return;
                }

                newBook.AvailableCopies = newBook.TotalCopies;

                books.Add(newBook);
            }
            else
            {
                Models.Book modBook = DataContext as Models.Book;

                if (book.BookId != modBook.BookId)
                {
                    MessageBox.Show("ISBN cannot be changed", "Error", MessageBoxButton.OK);
                    return;
                }

                book.Title = modBook.Title;
                book.Author = modBook.Author;
                book.PublicationYear = modBook.PublicationYear;
                book.Genre = modBook.Genre;
                book.TotalCopies = modBook.TotalCopies;
                book.AvailableCopies = modBook.AvailableCopies;
            }

            Close();
        }

        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
