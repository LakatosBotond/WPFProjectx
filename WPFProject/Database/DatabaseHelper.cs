using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using SQLite;
using WPFProject.Models;
using System.Security.Cryptography;

namespace WPFProject.Database
{
    public class DatabaseHelper
    {
        private readonly SQLiteConnection database;

        public DatabaseHelper()
        {
            string dbPath = Path.Combine(
                Directory.GetCurrentDirectory(),
                "shopmanager.db");

            database = new SQLiteConnection(dbPath);

            database.CreateTable<User>();
            database.CreateTable<Customer>();
            database.CreateTable<Product>();

            SeedAdmin();
        }

        public SQLiteConnection GetConnection()
        {
            return database;
        }

        private void SeedAdmin()
        {
            var users = database.Table<User>().ToList();

            if (users.Count == 0)
            {
                database.Insert(new User
                {
                    Username = "admin",
                    PasswordHash = HashPassword("admin"),
                    FirstName = "Admin",
                    LastName = "User",
                    Email = "admin@test.com"
                });
            }
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
    }
}
