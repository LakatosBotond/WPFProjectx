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
    public partial class ProductsUC : UserControl
    {
        private GenericRepository<Product> repository;
        private Product selectedProduct;
        public ProductsUC()
        {
            InitializeComponent();

            DatabaseHelper db = new DatabaseHelper();

            repository = new GenericRepository<Product>(db.GetConnection());

            LoadProducts();
        }

        private void LoadProducts()
        {
            productsGrid.ItemsSource = null;
            productsGrid.ItemsSource = repository.GetAll();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            Product product = new Product
            {
                Name = txtName.Text,
                Description = txtDescription.Text,
                Category = txtCategory.Text,
                Brand = txtBrand.Text
            };

            decimal.TryParse(txtPrice.Text, out decimal price);
            product.Price = price;

            double.TryParse(txtWeight.Text, out double weight);
            product.Weight = weight;

            repository.Add(product);

            LoadProducts();

            txtName.Clear();
            txtDescription.Clear();
            txtPrice.Clear();
            txtCategory.Clear();
            txtBrand.Clear();
            txtWeight.Clear();

            MessageBox.Show("Termék sikeresen hozzáadva!");
        }

private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            Product selectedProduct = productsGrid.SelectedItem as Product;

            if (selectedProduct == null)
            {
                MessageBox.Show("Válassz ki egy terméket!");
                return;
            }

            repository.Delete(selectedProduct);

            LoadProducts();

            MessageBox.Show("Termék törölve!");
        }
private void productsGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedProduct = productsGrid.SelectedItem as Product;

            if (selectedProduct == null)
                return;

            txtName.Text = selectedProduct.Name;
            txtDescription.Text = selectedProduct.Description;
            txtPrice.Text = selectedProduct.Price.ToString();
            txtCategory.Text = selectedProduct.Category;
            txtBrand.Text = selectedProduct.Brand;
            txtWeight.Text = selectedProduct.Weight.ToString();
        }


private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (selectedProduct == null)
            {
                MessageBox.Show("Válassz ki egy terméket!");
                return;
            }

            selectedProduct.Name = txtName.Text;
            selectedProduct.Description = txtDescription.Text;
            selectedProduct.Category = txtCategory.Text;
            selectedProduct.Brand = txtBrand.Text;

            decimal.TryParse(txtPrice.Text, out decimal price);
            selectedProduct.Price = price;

            double.TryParse(txtWeight.Text, out double weight);
            selectedProduct.Weight = weight;

            repository.Update(selectedProduct);

            LoadProducts();

            MessageBox.Show("Termék módosítva!");
        }

    }
}

