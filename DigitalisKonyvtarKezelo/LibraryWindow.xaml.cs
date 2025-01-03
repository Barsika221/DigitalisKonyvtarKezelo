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

namespace DigitalisKonyvtarKezelo
{
    /// <summary>
    /// Interaction logic for LibraryWondow.xaml
    /// </summary>
    public partial class LibraryWindow : Window
    {
        private List<Book> books;
        private User currentUser;

        public LibraryWindow(List<Book> books, User user)
        {
            InitializeComponent();
            this.books = books;
            this.currentUser = user;
            UpdateBookList();
        }

        private void UpdateBookList()
        {
            BooksDataGrid.Items.Clear();
            foreach (Book book in books)
            {
                BooksDataGrid.Items.Add(book);
            }
        }

        private void CategoryComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedCategory = CategoryComboBox.SelectedItem.ToString();
            List<Book> filteredBooks = books.Where(b => b.Category == selectedCategory).ToList();
            BooksDataGrid.Items.Clear();
            foreach (Book book in filteredBooks)
            {
                BooksDataGrid.Items.Add(book);
            }
        }

        private void AddBookButton_Click(object sender, RoutedEventArgs e)
        {
            AddBookWindow addBookWindow = new AddBookWindow(books);
            addBookWindow.ShowDialog();
            UpdateBookList();
        }

        private void EditBookButton_Click(object sender, RoutedEventArgs e)
        {
            Book selectedBook = (Book)BooksDataGrid.SelectedItem;
            if (selectedBook != null)
            {
                BookEditWindow editBookWindow = new BookEditWindow(selectedBook, books);
                editBookWindow.ShowDialog();
                UpdateBookList();
            }
            else
            {
                MessageBox.Show("Kérlek, válassz ki egy könyvet a szerkesztéshez!", "Figyelmeztetés", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }


        private void DeleteBookButton_Click(object sender, RoutedEventArgs e)
        {
            Book selectedBook = (Book)BooksDataGrid.SelectedItem;
            books.Remove(selectedBook);
            UpdateBookList();
        }
    }

}
