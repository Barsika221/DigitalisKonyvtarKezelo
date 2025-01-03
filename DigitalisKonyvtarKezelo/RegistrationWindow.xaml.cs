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
    /// Interaction logic for RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        private List<User> users;

        public RegistrationWindow(List<User> existingUsers)
        {
            InitializeComponent();
            users = existingUsers;
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = PasswordBox.Password;
            string email = EmailTextBox.Text;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(email))
            {
                MessageBox.Show("Minden mező kitöltése kötelező!");
                return;
            }

            if (users.Any(u => u.Username == username))
            {
                MessageBox.Show("Ez a felhasználónév már foglalt!");
                return;
            }

            if (!ValidationHelper.ValidateEmail(email))
            {
                MessageBox.Show("Érvénytelen e-mail cím!");
                return;
            }

            if (!ValidationHelper.ValidatePassword(password))
            {
                MessageBox.Show("A jelszónak legalább 8 karakterből kell állnia, és tartalmaznia kell nagybetűt, kisbetűt és számot!");
                return;
            }

            User newUser = new User(username, password, email, false);

            users.Add(newUser);
            FileManager.SaveUsers(users);

            MessageBox.Show("Sikeres regisztráció!");
            this.Close();
        }
    }
}