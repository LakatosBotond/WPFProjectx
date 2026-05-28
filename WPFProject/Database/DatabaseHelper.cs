using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using System.IO;
using WPFProject.Models;

namespace WPFProject.Database
{
    public class DatabaseHelper { private readonly SQLiteConnection database; public DatabaseHelper() { string dbPath = Path.Combine(Directory.GetCurrentDirectory(), "shopmanager.db"); database = new SQLiteConnection(dbPath); database.CreateTable<User>(); database.CreateTable<Customer>(); database.CreateTable<Product>(); } public SQLiteConnection GetConnection() { return database; } }
}
