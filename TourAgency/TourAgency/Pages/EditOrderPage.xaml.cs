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
        public EditOrderPage(Order order)
        {
            InitializeComponent();
            TypeTripComboBox.ItemsSource = AppData.Context.TypeTrip.ToList();
            CurrentOrder = order;
            if (CurrentOrder != null)
            {
                Trip CurrentTrip = AppData.Context.Trip.Where(c => c.Id == CurrentOrder.IdTrip).FirstOrDefault();
                Client CurrentClient = AppData.Context.Client.Where(c => c.Id == CurrentOrder.IdClient).FirstOrDefault();
                Agent CurrentAgent = AppData.Context.Agent.Where(c => c.Id == CurrentOrder.IdClient).FirstOrDefault();
                NameTextBox.Text = CurrentOrder.Trip.Name;
                DescriptionTextBox.Text = CurrentOrder.Trip.Description;
                StartPointTextBox.Text = CurrentOrder.Trip.Way.Country1.ToString();
                FinalPointTextBox.Text = CurrentOrder.Trip.Way.Country.ToString();
                ClientTextBox.Text = CurrentOrder.Client.FullnameClient;
                AgentTextBox.Text = CurrentOrder.Agent.FullnameAgent;
                DayCountTextBox.Text = CurrentOrder.DayCount.Value.ToString();
                TypeTripComboBox.SelectedItem = CurrentOrder.IdTypeTrip;

            }
        }

        private void OrderButton_Click(object sender, RoutedEventArgs e)
        {
            string letterList = "ABCDEFGHIJKLMNOPRSTUVWXYZabcdefghijklmnoprstuvwxyzАБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯабвгдеёжзийклмнопрстуфхцчшщъыьэюя";
            if (!String.IsNullOrWhiteSpace(NameTextBox.Text) && !String.IsNullOrWhiteSpace(DescriptionTextBox.Text) && !String.IsNullOrWhiteSpace(StartPointTextBox.Text) && !String.IsNullOrWhiteSpace(ClientTextBox.Text) && !String.IsNullOrWhiteSpace(AgentTextBox.Text) && !String.IsNullOrWhiteSpace(TotalPriceTextBox.Text) && TotalPriceTextBox.Text.IndexOfAny(letterList.ToCharArray()) <= -1 &&  TypeTripComboBox.SelectedItem != null)
            {
                if (CurrentOrder != null)
                {
                    IsActualCheckBox.Visibility = Visibility.Visible;
                    CurrentOrder.DayCount = int.Parse(DayCountTextBox.Text);
                    CurrentOrder.IdAgent = 
                    if (IsActualCheckBox.IsChecked.Value)
                    {
                        CurrentOrder.IsActual = true;
                    }
                    else
                    {
                        CurrentOrder.IsActual = false;
                    }

                }
            }
            else
            {
                MessageBox.Show("Не все поля заполнены корректно!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
