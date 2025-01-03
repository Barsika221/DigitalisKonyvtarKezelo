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
    /// Interaction logic for AddBookWindow.xaml
    /// </summary>
    public partial class AddBookWindow : Window
    {
        private List<Book> books;

        public AddBookWindow(List<Book> existingBooks)
        {
            InitializeComponent();
            books = existingBooks;
        }

        private void AddBookButton_Click(object sender, RoutedEventArgs e)
        {
            string title = TitleTextBox.Text;
            string author = AuthorTextBox.Text;
            string yearText = YearTextBox.Text;
            string category = (CategoryComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();

            if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(author) ||
                string.IsNullOrWhiteSpace(yearText) || string.IsNullOrWhiteSpace(category))
            {
                MessageBox.Show("Minden mező kitöltése kötelező!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!int.TryParse(yearText, out int year) || !ValidationHelper.ValidatePublicationYear(year))
            {
                MessageBox.Show("Érvénytelen kiadási év!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            Book newBook = new Book(title, author, year, category);
            books.Add(newBook);
            FileManager.SaveBooks(books);

            MessageBox.Show("Könyv sikeresen hozzáadva!", "Siker", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();
        }
    }

}
