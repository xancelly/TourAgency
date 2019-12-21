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
    /// Логика взаимодействия для EditWayPage.xaml
    /// </summary>
    public partial class EditWayPage : Page
    {
        Context Db = new Context();
        Way CurrentWay = null;
        public EditWayPage(Way way)
        {
            InitializeComponent();
            StartPointComboBox.ItemsSource = Db.Country.ToList();
            FinalPointComboBox.ItemsSource = Db.Country.ToList();
            CurrentWay = way;
            if (CurrentWay != null)
            {
                StartPointComboBox.SelectedItem = CurrentWay.StartPoint;
                FinalPointComboBox.SelectedItem = CurrentWay.FinalPoint;
                PriceTextBox.Text = CurrentWay.Price.Value.ToString("N2");
                SaveButton.Content = "Изменить";
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            bool isValid = true;
            if (StartPointComboBox.SelectedItem == null || FinalPointComboBox.SelectedItem == null || String.IsNullOrWhiteSpace(PriceTextBox.Text))
            {
                isValid = false;
            }
            if (isValid == true)
            {
                if (CurrentWay == null)
                {
                    CurrentWay = new Way()
                    {
                        StartPoint = StartPointComboBox.SelectedItem as Country,
                        FinalPoint = FinalPointComboBox.SelectedItem as Country,
                        Price = decimal.Parse(PriceTextBox.Text),
                    };
                    Db.Way.Add(CurrentWay);
                    MessageBox.Show("Маршрут был добавлен!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    CurrentWay.StartPoint = StartPointComboBox.SelectedItem as Country;
                    CurrentWay.FinalPoint = FinalPointComboBox.SelectedItem as Country;
                    CurrentWay.Price = decimal.Parse(PriceTextBox.Text);
                    MessageBox.Show("Информация обновлена!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                Db.SaveChanges();
                NavigationService.GoBack();

            }
            else
            {
                MessageBox.Show("Не все поля заполнены!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
