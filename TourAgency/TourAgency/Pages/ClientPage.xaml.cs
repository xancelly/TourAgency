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
    /// Логика взаимодействия для ClientPage.xaml
    /// </summary>
    public partial class ClientPage : Page
    {
        public ClientPage()
        {
            InitializeComponent();
            ClientDataGrid.ItemsSource = AppData.Context.Client.ToList();
        }

        public void UpdateClient()
        {
            var CurrentClient = AppData.Context.Client.ToList();
            CurrentClient = CurrentClient.Where(c => c.LastName.ToLower().Contains(SearchTextBox.Text.ToLower()) || c.FirstName.ToLower().Contains(SearchTextBox.Text.ToLower()) || c.MiddleName.ToLower().Contains(SearchTextBox.Text.ToLower())).ToList();
            ClientDataGrid.ItemsSource = CurrentClient;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateClient();
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateClient();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new EditClientPage(null));
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            Client CurrentClient = ClientDataGrid.SelectedItem as Client;
            if (CurrentClient != null)
            {
                NavigationService.Navigate(new EditClientPage(CurrentClient));
            }
            else
            {
                MessageBox.Show("Выберите клиента!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            Client CurrentClient = ClientDataGrid.SelectedItem as Client;
            if (CurrentClient != null)
            {
                if (MessageBox.Show("Вы действительно хотите удалить клиента?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    AppData.Context.Client.Remove(CurrentClient);
                    AppData.Context.SaveChanges();
                    Page_Loaded(null, null);
                    MessageBox.Show("Клиент был удалён!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                MessageBox.Show("Выберите клиента!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
