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
using Polomka.DataBase;

namespace Polomka
{
    /// <summary>
    /// Логика взаимодействия для ServieWindow.xaml
    /// </summary>
    public partial class ServieWindow : Window
    {
        Random random = new Random();
        public ServieWindow()
        {
            InitializeComponent();
        }

        private void BuyUslBtn_Click(object sender, RoutedEventArgs e)
        {
            var id = Convert.ToInt32(((Button)sender).CommandParameter);
            var a = DBPolomkaEntities.GetContext().Services.Where(x => x.Id_Service == id).FirstOrDefault();
            Order order = new Order();
            order.Id_Service = id;
            order.Id_Employee = random.Next(1,5);
            order.Id_Client = random.Next(5,7);
            order.Date = DateTime.Now;
            DBPolomkaEntities.GetContext().Order.Add(order);
            DBPolomkaEntities.GetContext().SaveChanges();
            MessageBox.Show("Успешно заказано!!");
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }
        private void Mouse_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                DBPolomkaEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(a => a.Reload());
                DGridServise.ItemsSource = DBPolomkaEntities.GetContext().Services.ToList();
            }
        }
    }
}
