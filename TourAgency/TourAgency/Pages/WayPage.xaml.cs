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
    /// Логика взаимодействия для WayPage.xaml
    /// </summary>
    public partial class WayPage : Page
    {
        Context Db = new Context();
        public WayPage()
        {
            InitializeComponent();
            WayDataGrid.ItemsSource = Db.Way.ToList();
        }

        private void UpdateWay()
        {
            var CurrentWay = Db.Way.ToList();
            WayDataGrid.ItemsSource = CurrentWay;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateWay();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new EditWayPage(null));
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            Way CurrentWay = WayDataGrid.SelectedItem as Way;
            if (CurrentWay != null)
            {
                NavigationService.Navigate(new EditWayPage(CurrentWay));
            } else
            {
                MessageBox.Show("Маршрут не выбран!\nВыберите маршрут и повторите процедуру.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            Way CurrentWay = WayDataGrid.SelectedItem as Way;
            if (CurrentWay != null)
            {
                if (MessageBox.Show("Вы действительно хотите удалить маршрут?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    Db.Way.Remove(CurrentWay);
                    Db.SaveChanges();
                    Page_Loaded(null, null);
                    MessageBox.Show("Маршрут был удалён!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            } else
            {
                MessageBox.Show("Маршрут не выбран!\nВыберите маршрут и повторите процедуру.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
