using System;
using System.Collections.Generic;
using System.IO;
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
using Microsoft.Win32;
using Polomka.DataBase;

namespace Polomka
{
    /// <summary>
    /// Логика взаимодействия для addClientsWindow.xaml
    /// </summary>
    public partial class addClientsWindow : Window
    {
        OpenFileDialog ofdImage1 = new OpenFileDialog();
        private  Clients _clients = new Clients();
        public addClientsWindow(Clients selected)
        {
            InitializeComponent();
            if (selected != null)
                _clients = selected;

            DataContext = _clients;
            markaCB.ItemsSource = DBPolomkaEntities.GetContext().Car_Brands.ToList();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(NameTB.Text))
                errors.AppendLine("укажите имя");
            if (string.IsNullOrWhiteSpace(SurnameTB.Text))
                errors.AppendLine("укажите фамилию");
            if (string.IsNullOrWhiteSpace(PatronimycTB.Text))
                errors.AppendLine("укажите отчество");
            if (string.IsNullOrWhiteSpace(PhoneTB.Text))
                errors.AppendLine("укажите телефон");
            if (string.IsNullOrWhiteSpace(Car_NumberTB.Text))
                errors.AppendLine("укажите номер");
            if (_clients.Car_Brands == null)
                errors.AppendLine("Укажите марку");



            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            if (_clients.Id_Client == 0)
            {
                _clients.Picture = File.ReadAllBytes(ofdImage1.FileName);
                DBPolomkaEntities.GetContext().Clients.Add(_clients);
            }
            try
            {
                DBPolomkaEntities.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена");
                ClientWindow client = new ClientWindow();
                client.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofdImage = new OpenFileDialog();
            ofdImage.Filter = "Image files|*.bmp;*.jpg;*.png|All files|*.*";
            ofdImage.FilterIndex = 1;
            if (ofdImage.ShowDialog() == true)
            {
                BitmapImage image = new BitmapImage();
                image.BeginInit();
                image.UriSource = new Uri(ofdImage.FileName);
                image.EndInit();
                ofdImage1 = ofdImage;
                img.Source = image;
            }
        }
    }
}
