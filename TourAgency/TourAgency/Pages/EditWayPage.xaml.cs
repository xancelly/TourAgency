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
        Way CurrentWay = null;
        public EditWayPage(Way way)
        {
            InitializeComponent();
            StartPointComboBox.ItemsSource = AppData.Context.Country.ToList();
            FinalPointComboBox.ItemsSource = AppData.Context.Country.ToList();
            CurrentWay = way;
            if (CurrentWay != null)
            {
                StartPointComboBox.SelectedItem = CurrentWay.Country;
                FinalPointComboBox.SelectedItem = CurrentWay.Country1;
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
                    string letterList = "ABCDEFGHIJKLMNOPRSTUVWXYZabcdefghijklmnoprstuvwxyzАБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯабвгдеёжзийклмнопрстуфхцчшщъыьэюя";
                    if (PriceTextBox.Text.IndexOfAny(letterList.ToCharArray()) <= -1)
                    {
                        CurrentWay = new Way()
                        {
                            Country1 = StartPointComboBox.SelectedItem as Country,
                            Country = FinalPointComboBox.SelectedItem as Country,
                            Price = decimal.Parse(PriceTextBox.Text),
                        };
                        AppData.Context.Way.Add(CurrentWay);
                        MessageBox.Show("Маршрут был добавлен!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
                        AppData.Context.SaveChanges();
                        NavigationService.GoBack();
                    } else
                    {
                        MessageBox.Show("Стоимость введена некорректно!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    string letterList = "ABCDEFGHIJKLMNOPRSTUVWXYZabcdefghijklmnoprstuvwxyzАБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯабвгдеёжзийклмнопрстуфхцчшщъыьэюя";
                    if (PriceTextBox.Text.IndexOfAny(letterList.ToCharArray()) <= -1)
                    {
                        CurrentWay.Country1 = StartPointComboBox.SelectedItem as Country;
                        CurrentWay.Country = FinalPointComboBox.SelectedItem as Country;
                        CurrentWay.Price = decimal.Parse(PriceTextBox.Text);
                        MessageBox.Show("Информация обновлена!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
                        AppData.Context.SaveChanges();
                        NavigationService.GoBack();
                    }
                    else
                    {
                        MessageBox.Show("Стоимость введена некорректно!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }

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
