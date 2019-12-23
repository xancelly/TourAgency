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
    /// Логика взаимодействия для TourPage.xaml
    /// </summary>
    public partial class TourPage : Page
    {
        public TourPage()
        {
            InitializeComponent();
            TripDataGrid.ItemsSource = AppData.Context.Trip.ToList();
            if (Properties.Settings.Default.IdRole == 1)
            {
                AddButton.Visibility = Visibility.Hidden;
                EditButton.Visibility = Visibility.Hidden;
                DeleteButton.Visibility = Visibility.Hidden;
            } else if (Properties.Settings.Default.IdRole == 2)
            {
                OrderButton.Visibility = Visibility.Hidden;
            }
        }

        private void UpdateTrip()
        {
            var CurrentTrip = AppData.Context.Trip.ToList();
            CurrentTrip = CurrentTrip.Where(c => c.Name.ToLower().Contains(SearchTextBox.Text.ToLower())).ToList();
            TripDataGrid.ItemsSource = CurrentTrip;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new EditTourPage(null));
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            Trip CurrentTrip = TripDataGrid.SelectedItem as Trip;
            if (CurrentTrip != null)
            {
                NavigationService.Navigate(new EditTourPage(CurrentTrip));
            }
            else
            {
                MessageBox.Show("Тур не выбран!\nВыберите тур и повторите процедуру.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            Trip CurrentTrip = TripDataGrid.SelectedItem as Trip;
            if (CurrentTrip != null)
            {
                if (MessageBox.Show("Вы действительно хотите удалить тур?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    AppData.Context.Trip.Remove(CurrentTrip);
                    AppData.Context.TripImage.RemoveRange(AppData.Context.TripImage.ToList().Where(c => c.IdTrip == CurrentTrip.Id));
                    AppData.Context.SaveChanges();
                    Page_Loaded(null, null);
                    MessageBox.Show("Тур был удалён!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                MessageBox.Show("Тур не выбран!\nВыберите тур и повторите процедуру.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateTrip();
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateTrip();
        }

        private void OrderButton_Click(object sender, RoutedEventArgs e)
        {
            Trip CurrentTrip = TripDataGrid.SelectedItem as Trip;
            if (CurrentTrip != null)
            {
                NavigationService.Navigate(new EditOrderPage(CurrentTrip));
            }
            else
            {
                MessageBox.Show("Выберите тур!");
            }
        }
    }
}
