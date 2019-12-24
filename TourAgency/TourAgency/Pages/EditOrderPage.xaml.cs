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
using System.Windows.Navigation;
using System.Windows.Shapes;
using TourAgency.Entities;
using Word = Microsoft.Office.Interop.Word;

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
        decimal TypeTrip = 1;
        decimal VisaPrice = 1;
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
            PriceTextBox.Visibility = Visibility.Hidden;
        }

        public EditOrderPage(Order order)
        {
            InitializeComponent();
            if (Properties.Settings.Default.IdRole == 1)
            {
                OrderButton.Visibility = Visibility.Hidden;
                DayCountTextBox.IsReadOnly = true;
                TypeTripComboBox.IsEnabled = false;
                IsActualCheckBox.IsEnabled = false;
                DateOrderDatePicker.IsEnabled = false;
            }
            var FullnameClient = AppData.Context.Client.Where(c => c.IdUser == Properties.Settings.Default.IdUser).FirstOrDefault();
            var FullnameAgent = AppData.Context.Agent.Where(c => c.IdUser == Properties.Settings.Default.IdUser).FirstOrDefault();
            var typeTrip = AppData.Context.TypeTrip.ToList();
            WayPrice = Convert.ToDecimal(order.Trip.Way.Price);
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
            TotalPriceTextBox.Text = order.TotalPrice;
            CurrentOrder = order;
            IsActualCheckBox.Visibility = Visibility.Visible;
            IsActualCheckBox.IsChecked = order.IsActual;
            loadForm = true;
            if (IsActualCheckBox.IsChecked == false)
            {
                PrintButton.Visibility = Visibility.Hidden;
            }
            OrderButton.Content = "Изменить";
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
                        IdAgent = 1,
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

        private void PrintButton_Click(object sender, RoutedEventArgs e)
        {
            Word.Document wDoc = null;
            Word.Application wApp = new Word.Application();
            try
            {
                wApp.Visible = true;
                string src = "";
                src = $@"{Directory.GetCurrentDirectory().ToString()}\Dogovor";
                DateTime dateTime = CurrentOrder.Date.Value;
                dateTime.AddDays(Convert.ToDouble(CurrentOrder.DayCount));
                wDoc = wApp.Documents.Open(src);
                wDoc.Activate();
                Word.Bookmarks wMarks = wDoc.Bookmarks;
                Word.Range wRange;
                wMarks["CurrentDate"].Range.Text = Convert.ToString(DateTime.Today);
                wMarks["AgentFullname"].Range.Text = CurrentOrder.Agent.FullnameAgent;
                wMarks["AgentFullname1"].Range.Text = CurrentOrder.Agent.FullnameAgent;
                wMarks["ClientFullname"].Range.Text = CurrentOrder.Client.FullnameClient;
                wMarks["DayCount"].Range.Text = CurrentOrder.DayCount.ToString();
                wMarks["EmailAgent"].Range.Text = CurrentOrder.Agent.Email;
                wMarks["EmailClient"].Range.Text = CurrentOrder.Client.Email;
                wMarks["EndDate"].Range.Text = dateTime.ToString();
                wMarks["EndDate1"].Range.Text = dateTime.ToString();
                wMarks["FinalCountry"].Range.Text = CurrentOrder.Trip.Way.Country.Name;
                wMarks["FinalCountry1"].Range.Text = CurrentOrder.Trip.Way.Country.Name;
                wMarks["FirstNameAgent"].Range.Text = CurrentOrder.Agent.FirstName;
                wMarks["LastNameAgent"].Range.Text = CurrentOrder.Agent.LastName;
                wMarks["MiddleNameAgent"].Range.Text = CurrentOrder.Agent.MiddleName;
                wMarks["FirstNameClient"].Range.Text = CurrentOrder.Client.FirstName;
                wMarks["LastNameClient"].Range.Text = CurrentOrder.Client.LastName;
                wMarks["MiddleNameClient"].Range.Text = CurrentOrder.Client.MiddleName;
                wMarks["LivePrice"].Range.Text = Convert.ToString(CurrentOrder.DayCount * CurrentOrder.Trip.Price);
                wMarks["NumberOrder"].Range.Text = CurrentOrder.Id.ToString();
                wMarks["PassportAgent"].Range.Text = CurrentOrder.Agent.Passport;
                wMarks["PassportClient"].Range.Text = CurrentOrder.Client.Passport;
                wMarks["PhoneAgent"].Range.Text = CurrentOrder.Agent.Phone;
                wMarks["PhoneClient"].Range.Text = CurrentOrder.Client.Phone;
                wMarks["StartDate"].Range.Text = CurrentOrder.Date.ToString();
                wMarks["StartDate1"].Range.Text = CurrentOrder.Date.ToString();
                wMarks["StartFinalCountry"].Range.Text = Convert.ToString(CurrentOrder.Trip.Way.Country1.Name + " - " + CurrentOrder.Trip.Way.Country.Name);
                wMarks["TotalPrice"].Range.Text = CurrentOrder.TotalPrice;
                wMarks["TotalPrice1"].Range.Text = CurrentOrder.TotalPrice;
                wMarks["TypeTourPrice"].Range.Text = TypeTrip.ToString();
                wMarks["VisaPrice"].Range.Text = VisaPrice.ToString();
                wMarks["MiddleNameClient"].Range.Text = CurrentOrder.Client.MiddleName;
                wMarks["WayPrice"].Range.Text = CurrentOrder.Trip.Way.Price.ToString();
                wDoc.SaveAs2
                        ($@"{Directory.GetCurrentDirectory().ToString()}\Dogovor_1.docx");
                wDoc.Close();
                wDoc = null;
                wApp.Quit();
            } catch (Exception ex)
            {
                wDoc = null;
            }
        }
    }
}
