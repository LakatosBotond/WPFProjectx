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
using System.Linq;
using System.Windows;
using WPFProject.Database;
using WPFProject.Models;
using WPFProject.Repositories;

namespace WPFProject
{
    public partial class LoginWindow : Window
    {
        private GenericRepository<User> userRepo;

        public LoginWindow()
        {
            InitializeComponent();

            DatabaseHelper db = new DatabaseHelper();
            userRepo = new GenericRepository<User>(db.GetConnection());
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Password;

            string hashedPassword = HashPassword(password);

            var user = userRepo.GetAll()
                .FirstOrDefault(u => u.Username == username && u.PasswordHash == hashedPassword);

            if (user != null)
            {
                MainWindow main = new MainWindow();
                main.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Hibás felhasználónév vagy jelszó!");
            }
        }

        private string HashPassword(string password)
        {
            using (System.Security.Cryptography.SHA256 sha256 = System.Security.Cryptography.SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

                return string.Concat(bytes.Select(b => b.ToString("x2")));
            }
        }
    }
}

