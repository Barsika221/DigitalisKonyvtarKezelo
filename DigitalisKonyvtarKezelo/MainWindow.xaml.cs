using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DigitalisKonyvtarKezelo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<User> users;
        private List<Book> books;

        public MainWindow()
        {
            InitializeComponent();
            users = FileManager.LoadUsers() ?? new List<User>();
            books = FileManager.LoadBooks() ?? new List<Book>();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = PasswordBox.Password;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Kérlek, töltsd ki a felhasználónevet és jelszót!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            User user = users.FirstOrDefault(u => u.Username == username && u.Password == password);

            if (user == null)
            {
                MessageBox.Show("Hibás felhasználónév vagy jelszó!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            LibraryWindow libraryWindow = new LibraryWindow(books, user);
            libraryWindow.Show();
            this.Close();
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            RegistrationWindow registrationWindow = new RegistrationWindow(users);
            registrationWindow.ShowDialog();

            users = FileManager.LoadUsers();
        }
    }

}