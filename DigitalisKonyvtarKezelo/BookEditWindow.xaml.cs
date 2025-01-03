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
    /// Interaction logic for BookEditWindow.xaml
    /// </summary>
    public partial class BookEditWindow : Window
    {
        private Book book;
        private List<Book> books;

        public BookEditWindow(Book bookToEdit, List<Book> allBooks)
        {
            InitializeComponent();
            book = bookToEdit;
            books = allBooks;

            if (book != null)
            {
                TitleTextBox.Text = book.Title;
                AuthorTextBox.Text = book.Author;
                YearTextBox.Text = book.PublicationYear.ToString();
                CategoryComboBox.SelectedItem = CategoryComboBox.Items.Cast<ComboBoxItem>().FirstOrDefault(item => item.Content.ToString() == book.Category);
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            string title = TitleTextBox.Text;
            string author = AuthorTextBox.Text;
            string yearText = YearTextBox.Text;
            string category = (CategoryComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();

            if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(author) || string.IsNullOrWhiteSpace(yearText) || string.IsNullOrWhiteSpace(category))
            {
                MessageBox.Show("Minden mező kitöltése kötelező!");
                return;
            }

            if (!int.TryParse(yearText, out int year) || !ValidationHelper.ValidatePublicationYear(year))
            {
                MessageBox.Show("Érvénytelen kiadási év!");
                return;
            }

            if (book == null)
            {
                book = new Book(title, author, year, category);
                books.Add(book);
            }

            book.Title = title;
            book.Author = author;
            book.PublicationYear = year;
            book.Category = category;

            FileManager.SaveBooks(books);

            MessageBox.Show("Könyv sikeresen mentve!");
            this.Close();
        }
    }

}
