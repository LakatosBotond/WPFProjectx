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
using WPFProject.UserControls;
using WPFProject.Database;

namespace WPFProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DatabaseHelper databaseHelper;

        public MainWindow()
        {
            InitializeComponent();

            databaseHelper = new DatabaseHelper();

            ContentGrid.Children.Clear();
            ContentGrid.Children.Add(new DashboardUC());
        }

        private void Dashboard_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ContentGrid.Children.Clear();
            ContentGrid.Children.Add(new DashboardUC());
        }

        private void Users_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ContentGrid.Children.Clear();
            ContentGrid.Children.Add(new UsersUC());
        }

        private void Costumers_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ContentGrid.Children.Clear();
            ContentGrid.Children.Add(new CustomersUC());
        }

        private void Products_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ContentGrid.Children.Clear();
            ContentGrid.Children.Add(new ProductsUC());
        }

        private void Settings_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void kilepesMenu_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }

}

