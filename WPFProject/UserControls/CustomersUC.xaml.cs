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

namespace WPFProject.UserControls
{
    public partial class CustomersUC : UserControl
    {
        private GenericRepository<Customer> repository;
        private Customer selectedCustomer;

        public CustomersUC()
        {
            InitializeComponent();

            DatabaseHelper db = new DatabaseHelper();

            repository = new GenericRepository<Customer>(db.GetConnection());

            LoadCustomers();
        }

        private void LoadCustomers()
        {
            customersGrid.ItemsSource = null;
            customersGrid.ItemsSource = repository.GetAll();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            Customer customer = new Customer
            {
                UserId = int.TryParse(txtUserId.Text, out int uid) ? uid : 0,
                PhoneNumber = txtPhone.Text,
                Address = txtAddress.Text,
                City = txtCity.Text,
                Country = txtCountry.Text,
                BirthDate = DateTime.TryParse(txtBirthDate.Text, out DateTime bd) ? bd : DateTime.Now
            };

            repository.Add(customer);

            LoadCustomers();

            MessageBox.Show("Vásárló hozzáadva!");
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (selectedCustomer == null)
            {
                MessageBox.Show("Válassz ki egy vásárlót!");
                return;
            }

            repository.Delete(selectedCustomer);

            LoadCustomers();

            MessageBox.Show("Vásárló törölve!");
        }

        private void customersGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedCustomer = customersGrid.SelectedItem as Customer;

            if (selectedCustomer == null)
                return;

            txtUserId.Text = selectedCustomer.UserId.ToString();
            txtPhone.Text = selectedCustomer.PhoneNumber;
            txtAddress.Text = selectedCustomer.Address;
            txtCity.Text = selectedCustomer.City;
            txtCountry.Text = selectedCustomer.Country;
            txtBirthDate.Text = selectedCustomer.BirthDate.ToString("yyyy-MM-dd");
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (selectedCustomer == null)
            {
                MessageBox.Show("Válassz ki egy vásárlót!");
                return;
            }

            selectedCustomer.UserId = int.TryParse(txtUserId.Text, out int uid) ? uid : selectedCustomer.UserId;
            selectedCustomer.PhoneNumber = txtPhone.Text;
            selectedCustomer.Address = txtAddress.Text;
            selectedCustomer.City = txtCity.Text;
            selectedCustomer.Country = txtCountry.Text;
            selectedCustomer.BirthDate = DateTime.TryParse(txtBirthDate.Text, out DateTime bd) ? bd : selectedCustomer.BirthDate;

            repository.Update(selectedCustomer);

            LoadCustomers();

            MessageBox.Show("Vásárló módosítva!");
        }
    }
}

