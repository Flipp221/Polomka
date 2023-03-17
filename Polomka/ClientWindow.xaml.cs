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
    /// Логика взаимодействия для ClientWindow.xaml
    /// </summary>
    public partial class ClientWindow : Window
    {
        public static DBPolomkaEntities db = new DBPolomkaEntities();
        public List<Clients> clients = new List<Clients>();
        public ClientWindow()
        {
            InitializeComponent();
            RefreshComboBox();
            UpdateTovar();
            RefreshFilter();
        }
        private void CBNumberWrite_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            pageSize = Convert.ToInt32(CBNumberWrite.SelectedItem.ToString());
            RefreshPagination();
        }
        private void BLeft_Click(object sender, RoutedEventArgs e)
        {
            if (pageNumber == 0)
                return;
            pageNumber--;
            RefreshPagination();
        }

        private void BRight_Click(object sender, RoutedEventArgs e)
        {
            if (clientens.Count % pageSize == 0)
            {
                if (pageNumber == (clientens.Count / pageSize) - 1)
                    return;
            }
            else
            {

                if (pageNumber == (clientens.Count / pageSize))
                    return;
            }
            pageNumber++;
            RefreshPagination();
        }

        int pageSize;
        int pageNumber;
        List<Clients> clientens = db.Clients.ToList();

        private void RefreshPagination()
        {
            ClientList.ItemsSource = null;
            ClientList.ItemsSource = clientens.Skip(pageNumber * pageSize).Take(pageSize).ToList();
        }

        private void RefreshComboBox()
        {
            CBNumberWrite.Items.Add("10");
            SortCB.Items.Add("По имени");
            SortCB.Items.Add("По номеру машины");
        }
        private void RefreshFilter()
        {
            foreach (var item in db.Car_Brands)
                FilterCB.Items.Add(item.Name_Brand);
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            pageNumber = Convert.ToInt32(button.Content) - 1;
            RefreshPagination();
        }
        private void Poisk_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateTovar();
        }
        private void UpdateTovar()
        {
            var currentKeyboard = DBPolomkaEntities.GetContext().Clients.ToList();

            currentKeyboard = currentKeyboard.Where(p => p.Name.ToLower().Contains(Poisk.Text.ToLower())).ToList();

            ClientList.ItemsSource = currentKeyboard.OrderBy(p => p.Name).ToList();
        }
        private void Mouse_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                DBPolomkaEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(a => a.Reload());
                ClientList.ItemsSource = DBPolomkaEntities.GetContext().Clients.ToList();
            }
        }
        private void Red_Click(object sender, RoutedEventArgs e)
        {
            addClientsWindow page = new addClientsWindow((sender as Button).DataContext as Clients);
            page.Show();
            this.Close();
        }

        private void SortCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(SortCB.SelectedIndex == 0)
            {
                ClientList.ItemsSource = DBPolomkaEntities.GetContext().Clients.OrderBy(z => z.Name).ToList();
            }
            if (SortCB.SelectedIndex == 1)
            {
                ClientList.ItemsSource = DBPolomkaEntities.GetContext().Clients.OrderBy(z => z.Car_Number).ToList();
            }
        }

        private void FilterCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox combobox = (ComboBox)sender;
            string item = Convert.ToString(combobox.SelectedItem);
            if (item == "Фильтрация")
            {
                ClientList.ItemsSource = clientens;
                return;
            }
            clientens = db.Clients.Where(z => z.Car_Brands.Name_Brand == item).ToList();
            ClientList.ItemsSource = clientens;
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            var ClientsForRemoving = ClientList.SelectedItems.Cast<Clients>().ToList();

            if (MessageBox.Show($"Вы точно хотите удалить сдедующие{ClientsForRemoving.Count()} элементов?", "Внимение",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    DBPolomkaEntities.GetContext().Clients.RemoveRange(ClientsForRemoving);
                    DBPolomkaEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены");

                    ClientList.ItemsSource = DBPolomkaEntities.GetContext().Clients.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            addClientsWindow addClients = new addClientsWindow(null);
            addClients.Show();
            this.Close();
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainPage = new MainWindow();
            mainPage.Show();
            this.Close();
        }
    }
}
