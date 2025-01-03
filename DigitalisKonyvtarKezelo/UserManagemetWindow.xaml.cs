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
    /// Interaction logic for UserManagemetWindow.xaml
    /// </summary>
    public partial class UserManagementWindow : Window
    {
        private List<User> users;
        private User currentUser;

        public UserManagementWindow(List<User> allUsers, User adminUser)
        {
            InitializeComponent();
            users = allUsers;
            currentUser = adminUser;
            UpdateUserList();
        }

        private void UpdateUserList()
        {
            UsersDataGrid.ItemsSource = null;
            UsersDataGrid.ItemsSource = users.Where(u => u != currentUser);
        }

        private void DeleteUserButton_Click(object sender, RoutedEventArgs e)
        {
            if (UsersDataGrid.SelectedItem is User selectedUser)
            {
                if (selectedUser == currentUser)
                {
                    MessageBox.Show("Nem törölheted saját magad!");
                    return;
                }

                MessageBoxResult result = MessageBox.Show($"Biztosan törölni szeretnéd a következő felhasználót: {selectedUser.Username}?", "Felhasználó törlése", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    users.Remove(selectedUser);
                    FileManager.SaveUsers(users);
                    UpdateUserList();
                    MessageBox.Show("Felhasználó sikeresen törölve!");
                }
            }
            else
            {
                MessageBox.Show("Kérlek, válassz ki egy felhasználót a törléshez!");
            }
        }

        private void AddUserButton_Click(object sender, RoutedEventArgs e)
        {
            RegistrationWindow registrationWindow = new RegistrationWindow(users);
            registrationWindow.ShowDialog();

            users = FileManager.LoadUsers();
            UpdateUserList();
        }

    }

}
