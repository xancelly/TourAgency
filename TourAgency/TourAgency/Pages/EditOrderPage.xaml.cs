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
        public EditOrderPage(Trip trip)
        {
            InitializeComponent();
            TypeTripComboBox.ItemsSource = AppData.Context.TypeTrip.ToList();
            DayCountTextBox.Text = "1";
            decimal CurrentTaxation = Convert.ToDecimal(AppData.Context.Taxation.Select(c => c.Tax).FirstOrDefault());
            NameTextBox.Text = trip.Name;
            DescriptionTextBox.Text = trip.Description;
            StartPointTextBox.Text = trip.Way.Country1.Name;
            FinalPointTextBox.Text = trip.Way.Country.Name;
            DateOrderDatePicker.SelectedDate = DateTime.Today;
            var FullnameClient = AppData.Context.Client.Where(c => c.IdUser == Properties.Settings.Default.IdUser).FirstOrDefault();
            ClientTextBox.Text = FullnameClient.FullnameClient;
            TotalPriceTextBox.Text = Convert.ToString(((trip.Price * decimal.Parse(DayCountTextBox.Text)) + trip.Way.Price) + ((trip.Price + trip.Way.Price) / 100 * CurrentTaxation));
            CurrentTrip = trip;
        }

        private void OrderButton_Click(object sender, RoutedEventArgs e)
        {
            string letterList = "ABCDEFGHIJKLMNOPRSTUVWXYZabcdefghijklmnoprstuvwxyzАБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯабвгдеёжзийклмнопрстуфхцчшщъыьэюя";
            if (!String.IsNullOrWhiteSpace(NameTextBox.Text) && !String.IsNullOrWhiteSpace(DescriptionTextBox.Text) && !String.IsNullOrWhiteSpace(StartPointTextBox.Text) && !String.IsNullOrWhiteSpace(ClientTextBox.Text) && !String.IsNullOrWhiteSpace(TotalPriceTextBox.Text) && TotalPriceTextBox.Text.IndexOfAny(letterList.ToCharArray()) <= -1 && TypeTripComboBox.SelectedItem != null)
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
                }
                else
                {
                    OrderButton.Content = "Изменить";
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
            TotalPriceTextBox.TextChanged += DayCountTextBox_TextChanged;
        }
    }
}
