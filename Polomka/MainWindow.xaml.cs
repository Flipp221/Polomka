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

namespace Polomka
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnClient_Click(object sender, RoutedEventArgs e)
        {
            ClientWindow clientWindow = new ClientWindow();
            clientWindow.Show();
            Close();
        }

        private void btnEmployee_Click(object sender, RoutedEventArgs e)
        {
            EmployeeWibndow empWibndow = new EmployeeWibndow();
            empWibndow.Show();
            Close();
        }

        private void btnOtder_Click(object sender, RoutedEventArgs e)
        {
            StockWindow stockWindow = new StockWindow();
            stockWindow.Show();
            Close();
        }

        private void btnUslugi_Click(object sender, RoutedEventArgs e)
        {
            ServieWindow servieWindow = new ServieWindow();
            servieWindow.Show();
            Close();
        }
    }
}
