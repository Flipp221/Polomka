﻿using System;
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
    /// Логика взаимодействия для EmployeeWibndow.xaml
    /// </summary>
    public partial class EmployeeWibndow : Window
    {
        public EmployeeWibndow()
        {
            InitializeComponent();
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
                employeeList.ItemsSource = DBPolomkaEntities.GetContext().Employee.ToList();
            }
        }
    }
}
