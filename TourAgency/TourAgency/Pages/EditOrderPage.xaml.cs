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
using TourAgency.Entities;

namespace TourAgency.Pages
{
    /// <summary>
    /// Логика взаимодействия для EditOrderPage.xaml
    /// </summary>
    public partial class EditOrderPage : Page
    {
        Order CurrentOrder = null;
        Trip CurrentTrip = null;
        decimal CurrentTaxation = 1;
        decimal DaysPrice = 1;
        decimal WayPrice = 1;
        decimal TotalPrice = 1;
        bool loadForm = false;

        public EditOrderPage(Trip trip)
        {  
            InitializeComponent();
            TypeTripComboBox.ItemsSource = AppData.Context.TypeTrip.ToList();
            CurrentTaxation = Convert.ToDecimal(AppData.Context.Taxation.Select(c => c.Tax).FirstOrDefault());
            var FullnameClient = AppData.Context.Client.Where(c => c.IdUser == Properties.Settings.Default.IdUser).FirstOrDefault();
            var FullnameAgent = AppData.Context.Agent.Where(c => c.IdUser == Properties.Settings.Default.IdUser).FirstOrDefault();
            WayPrice = Convert.ToDecimal(trip.Way.Price);
            NameTextBox.Text = trip.Name;
            DescriptionTextBox.Text = trip.Description;
            StartPointTextBox.Text = trip.Way.Country1.Name;
            FinalPointTextBox.Text = trip.Way.Country.Name;
            PriceTextBox.Text = trip.Price.ToString();
            DateOrderDatePicker.SelectedDate = DateTime.Today;
            ClientTextBox.Text = FullnameClient.FullnameClient;
            TotalPriceTextBox.Text = TotalPrice.ToString();
            CurrentTrip = trip;
            loadForm = true;
        }

        public EditOrderPage(Order order)
        {
            InitializeComponent();
            if (Properties.Settings.Default.IdRole == 1)
            {
                OrderButton.Visibility = Visibility.Visible;
            }
            var FullnameClient = AppData.Context.Client.Where(c => c.IdUser == Properties.Settings.Default.IdUser).FirstOrDefault();
            var FullnameAgent = AppData.Context.Agent.Where(c => c.IdUser == Properties.Settings.Default.IdUser).FirstOrDefault();
            var typeTrip = AppData.Context.TypeTrip.ToList();
            TypeTripComboBox.ItemsSource = typeTrip;
            CurrentTaxation = Convert.ToDecimal(AppData.Context.Taxation.Select(c => c.Tax).FirstOrDefault());
            NameTextBox.Text = order.Trip.Name;
            DescriptionTextBox.Text = order.Trip.Description;
            StartPointTextBox.Text = order.Trip.Way.Country1.Name;
            FinalPointTextBox.Text = order.Trip.Way.Country.Name;
            ClientTextBox.Text = order.FullnameClient;
            TypeTripComboBox.SelectedItem = order.TypeTrip;
            AgentTextBox.Text = order.FullnameAgent;
            PriceTextBox.Text = Convert.ToString(order.Trip.Price);
            DateOrderDatePicker.SelectedDate = order.Date;
            DayCountTextBox.Text = Convert.ToString(order.DayCount);
            TotalPriceTextBox.Text = TotalPrice.ToString();
            CurrentOrder = order;
            IsActualCheckBox.Visibility = Visibility.Visible;
            IsActualCheckBox.IsChecked = order.IsActual;
            loadForm = true;
        }

        private void OrderButton_Click(object sender, RoutedEventArgs e)
        {
            string letterList = "ABCDEFGHIJKLMNOPRSTUVWXYZabcdefghijklmnoprstuvwxyzАБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯабвгдеёжзийклмнопрстуфхцчшщъыьэюя";
            if (!String.IsNullOrWhiteSpace(NameTextBox.Text) && !String.IsNullOrWhiteSpace(DescriptionTextBox.Text) && !String.IsNullOrWhiteSpace(StartPointTextBox.Text) && !String.IsNullOrWhiteSpace(ClientTextBox.Text) && !String.IsNullOrWhiteSpace(TotalPriceTextBox.Text) && TotalPriceTextBox.Text.IndexOfAny(letterList.ToCharArray()) <= -1 && TypeTripComboBox.SelectedItem != null && DateOrderDatePicker.SelectedDate != null)
            {
                if (CurrentOrder == null)
                {
                    CurrentOrder = new Order()
                    {
                        IdTrip = CurrentTrip.Id,
                        DayCount = int.Parse(DayCountTextBox.Text),
                        Date = DateOrderDatePicker.SelectedDate,
                        TypeTrip = TypeTripComboBox.SelectedItem as TypeTrip,
                        IdClient = AppData.Context.Client.Where(c => c.IdUser == Properties.Settings.Default.IdUser).Select(c => c.Id).FirstOrDefault(),
                        IdAgent = 2,
                        IsActual = false,
                    };
                    AppData.Context.Order.Add(CurrentOrder);
                    AppData.Context.SaveChanges();
                    NavigationService.GoBack();
                    MessageBox.Show("Заказ добавлен в обработку!\nОжидайте подтверждения заказа.", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    CurrentOrder.DayCount = int.Parse(DayCountTextBox.Text);
                    CurrentOrder.TypeTrip = TypeTripComboBox.SelectedItem as TypeTrip;
                    CurrentOrder.Date = DateOrderDatePicker.SelectedDate;
                    OrderButton.Content = "Изменить";
                    CurrentOrder.Agent = AppData.Context.Agent.Where(c => c.IdUser == Properties.Settings.Default.IdUser).FirstOrDefault();
                    if (Convert.ToBoolean(IsActualCheckBox.IsChecked))
                    {
                        CurrentOrder.IsActual = true;
                    }
                    else
                    {
                        CurrentOrder.IsActual = false;
                    }
                    AppData.Context.SaveChanges();
                    NavigationService.GoBack();
                    MessageBox.Show("Заказ изменён!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                MessageBox.Show("Не все поля заполнены корректно!");
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void DayCountTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string letterList = "ABCDEFGHIJKLMNOPRSTUVWXYZabcdefghijklmnoprstuvwxyzАБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯабвгдеёжзийклмнопрстуфхцчшщъыьэюя";
            if (loadForm)
            {
                if (DayCountTextBox.Text != "" && DayCountTextBox.Text.IndexOfAny(letterList.ToCharArray()) <= -1)
                {
                    DaysPrice = Convert.ToDecimal(PriceTextBox.Text) * Convert.ToDecimal(DayCountTextBox.Text);
                    TotalPrice = (DaysPrice + WayPrice) + ((DaysPrice + WayPrice) / 100 * CurrentTaxation);
                    TotalPriceTextBox.Text = TotalPrice.ToString("N2");
                }
            }
        }
    }
}
