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
using System.Linq;
using System.Windows.Controls;
using WPFProject.Database;
using WPFProject.Models;
using WPFProject.Repositories;

namespace WPFProject.UserControls
{
    public partial class DashboardUC : UserControl
    {
        private GenericRepository<User> userRepo;
        private GenericRepository<Customer> customerRepo;
        private GenericRepository<Product> productRepo;

        public DashboardUC()
        {
            InitializeComponent();

            DatabaseHelper db = new DatabaseHelper();

            var conn = db.GetConnection();

            userRepo = new GenericRepository<User>(conn);
            customerRepo = new GenericRepository<Customer>(conn);
            productRepo = new GenericRepository<Product>(conn);

            LoadStats();
        }

        private void LoadStats()
        {
            txtUsersCount.Text = userRepo.GetAll().Count.ToString();
            txtCustomersCount.Text = customerRepo.GetAll().Count.ToString();
            txtProductsCount.Text = productRepo.GetAll().Count.ToString();
        }
    }
}

