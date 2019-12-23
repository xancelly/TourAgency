using Microsoft.Win32;
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

namespace TourAgency.Pages
{
    /// <summary>
    /// Логика взаимодействия для EditTourPage.xaml
    /// </summary>
    public partial class EditTourPage : Page
    {
        List<byte[]> imageList = new List<byte[]>();
        Trip CurrentTrip = null;
        public EditTourPage(Trip trip)
        {
            InitializeComponent();
            WayComboBox.ItemsSource = AppData.Context.Way.ToList();
            CurrentTrip = trip;
            if (CurrentTrip != null)
            {
                NameTextBox.Text = CurrentTrip.Name;
                DescriptionTextBox.Text = CurrentTrip.Description;
                WayComboBox.SelectedItem = CurrentTrip.Way;
                PriceTextBox.Text = CurrentTrip.Price.Value.ToString();
                ImageListView.ItemsSource = CurrentTrip.TripImage.ToList().Select(c => c.Source).ToList();
                AddButton.Content = "Изменить";
                if (CurrentTrip.ImagePreview == null)
                {
                    PreviewImage.DataContext = "/TourAgency;component/Source/noimage.jpg";
                }
                else
                {
                    PreviewImage.DataContext = CurrentTrip.ImagePreview;
                }
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            string letterList = "ABCDEFGHIJKLMNOPRSTUVWXYZabcdefghijklmnoprstuvwxyzАБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯабвгдеёжзийклмнопрстуфхцчшщъыьэюя";
            bool isValid = true;
            if (String.IsNullOrEmpty(NameTextBox.Text) || String.IsNullOrEmpty(DescriptionTextBox.Text) || PriceTextBox.Text.IndexOfAny(letterList.ToCharArray()) > -1 || WayComboBox.SelectedItem == null)
            {
                isValid = false;
            }
            if (isValid == true)
            {
                if (CurrentTrip == null)
                {
                    CurrentTrip = new Trip()
                    {
                        Name = NameTextBox.Text,
                        Description = DescriptionTextBox.Text,
                        Way = WayComboBox.SelectedItem as Way,
                        Price = decimal.Parse(PriceTextBox.Text),
                    };
                    foreach (var image in imageList)
                    {
                        CurrentTrip.TripImage.Add(new TripImage
                        {
                            Source = image
                        });
                    }
                    AppData.Context.Trip.Add(CurrentTrip);
                    AppData.Context.SaveChanges();
                    MessageBox.Show("Тур был добавлен!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    CurrentTrip.Name = NameTextBox.Text;
                    CurrentTrip.Description = DescriptionTextBox.Text;
                    CurrentTrip.Way = WayComboBox.SelectedItem as Way;
                    CurrentTrip.Price = decimal.Parse(PriceTextBox.Text);
                    if (imageList.Count() > 0)
                    {
                        AppData.Context.TripImage.RemoveRange(AppData.Context.TripImage.ToList().Where(c => c.IdTrip == CurrentTrip.Id));
                        foreach (var image in imageList)
                        {
                            CurrentTrip.TripImage.Add(new TripImage
                            {
                                Source = image
                            });
                        }
                    }
                    MessageBox.Show("Информация обновлена!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                if (ImageListView.Items.Count > 0)
                {
                    CurrentTrip.ImagePreview = PreviewImage.DataContext as byte[];
                }
                AppData.Context.SaveChanges();
                NavigationService.GoBack();
            }
            else
            {
                MessageBox.Show("Не все поля корретно заполнены!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragDrop.DoDragDrop(sender as Image, (sender as Image).DataContext as Byte[], DragDropEffects.Copy);
        }

        private void PreviewImage_Drop(object sender, DragEventArgs e)
        {
            PreviewImage.DataContext = e.Data.GetData(typeof(byte[]));
        }


        private void AddImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image | *.jpg;*.png;";
            ofd.Multiselect = true;
            if (ofd.ShowDialog() == true)
            {
                ImageListView.ItemsSource = null;
                imageList.Clear();
                foreach (var path in ofd.FileNames)
                {
                    imageList.Add(File.ReadAllBytes(path));
                }
                PreviewImage.DataContext = imageList[0];
                ImageListView.ItemsSource = imageList;
            }
        }

        private void Image_Drop(object sender, DragEventArgs e)
        {
            PreviewImage.DataContext = e.Data.GetData(typeof(byte[]));
        }
    }
}
