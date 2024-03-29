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
using System.Windows.Navigation;
using System.Windows.Shapes;
using TourAgency.Entities;

namespace TourAgency.Pages
{
    /// <summary>
    /// Логика взаимодействия для OrderPage.xaml
    /// </summary>
    public partial class OrderPage : Page
    {
        public OrderPage()
        {
            InitializeComponent();
            if (Properties.Settings.Default.IdRole == 1)
            {
                OrderDataGrid.ItemsSource = AppData.Context.Order.Where(c => c.Client.IdUser == Properties.Settings.Default.IdUser).ToList();
                EditButton.Visibility = Visibility.Hidden;
                DeleteButton.Visibility = Visibility.Hidden;
            } else if (Properties.Settings.Default.IdRole == 2)
            {
                OrderDataGrid.ItemsSource = AppData.Context.Order.ToList();
                ShowButton.Visibility = Visibility.Hidden;
            }
        }

        private void UpdateOrder()
        {
            var CurrentOrder = AppData.Context.Order.ToList();
            if (Properties.Settings.Default.IdRole == 1)
            {
                CurrentOrder = AppData.Context.Order.Where(c => c.Client.IdUser == Properties.Settings.Default.IdUser).ToList();
            }
            else if (Properties.Settings.Default.IdRole == 2)
            {
                CurrentOrder = AppData.Context.Order.ToList();
            }
            CurrentOrder = CurrentOrder.Where(c => c.Client.FullnameClient.ToLower().Contains(SearchTextBox.Text.ToLower())).ToList();
            OrderDataGrid.ItemsSource = CurrentOrder;
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateOrder();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            Order CurrentOrder = OrderDataGrid.SelectedItem as Order;
            if (CurrentOrder != null)
            {
                NavigationService.Navigate(new EditOrderPage(CurrentOrder));
            }
            else
            {
                MessageBox.Show("Заказ не выбран!\nВыберите заказ и повторите процедуру.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            Order CurrentOrder = OrderDataGrid.SelectedItem as Order;
            if (CurrentOrder != null)
            {
                if (MessageBox.Show("Вы действительно хотите удалить заказ?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    AppData.Context.Order.Remove(CurrentOrder);
                    AppData.Context.SaveChanges();
                    Page_Loaded(null, null);
                    MessageBox.Show("Заказ был удалён!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                MessageBox.Show("Выберите заказ!");
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateOrder();
        }

        private void ShowButton_Click(object sender, RoutedEventArgs e)
        {
            Order CurrentOrder = OrderDataGrid.SelectedItem as Order;
            if (CurrentOrder != null)
            {
                NavigationService.Navigate(new EditOrderPage(CurrentOrder));
            }
            else
            {
                MessageBox.Show("Заказ не выбран!\nВыберите заказ и повторите процедуру.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}

