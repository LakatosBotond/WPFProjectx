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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPFProject.Database;
using WPFProject.Models;
using WPFProject.Repositories;
using System.Security.Cryptography;
using System.Text;


namespace WPFProject.UserControls
{
    public partial class UsersUC : UserControl
    {
        private GenericRepository<User> repository;
        private User selectedUser;

        public UsersUC()
        {
            InitializeComponent();

            DatabaseHelper db = new DatabaseHelper();

            repository = new GenericRepository<User>(db.GetConnection());

            LoadUsers();
        }

        private void LoadUsers()
        {
            usersGrid.ItemsSource = null;
            usersGrid.ItemsSource = repository.GetAll();
        }

        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));

                StringBuilder builder = new StringBuilder();

                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }

                return builder.ToString();
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            User user = new User
            {
                Username = txtUsername.Text,
                Email = txtEmail.Text,
                PasswordHash = HashPassword(txtPassword.Text),
                FirstName = txtFirstName.Text,
                LastName = txtLastName.Text
            };

            repository.Add(user);

            LoadUsers();

            txtUsername.Clear();
            txtEmail.Clear();
            txtPassword.Clear();
            txtFirstName.Clear();
            txtLastName.Clear();

            MessageBox.Show("Felhasználó hozzáadva!");
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (selectedUser == null)
            {
                MessageBox.Show("Válassz ki egy felhasználót!");
                return;
            }

            repository.Delete(selectedUser);

            LoadUsers();

            MessageBox.Show("Felhasználó törölve!");
        }

        private void usersGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedUser = usersGrid.SelectedItem as User;

            if (selectedUser == null)
                return;

            txtUsername.Text = selectedUser.Username;
            txtEmail.Text = selectedUser.Email;
            txtPassword.Text = "";
            txtFirstName.Text = selectedUser.FirstName;
            txtLastName.Text = selectedUser.LastName;
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (selectedUser == null)
            {
                MessageBox.Show("Válassz ki egy felhasználót!");
                return;
            }

            selectedUser.Username = txtUsername.Text;
            selectedUser.Email = txtEmail.Text;
            selectedUser.FirstName = txtFirstName.Text;
            selectedUser.LastName = txtLastName.Text;

            if (!string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                selectedUser.PasswordHash = HashPassword(txtPassword.Text);
            }

            repository.Update(selectedUser);

            LoadUsers();

            MessageBox.Show("Felhasználó módosítva!");
        }
    }
}
