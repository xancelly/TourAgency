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

namespace TourAgency.Pages
{
    /// <summary>
    /// Логика взаимодействия для MainMenuPage.xaml
    /// </summary>
    public partial class MainMenuPage : Page
    {
        public MainMenuPage()
        {
            InitializeComponent();
            if (Properties.Settings.Default.IdRole == 1)
            {
                WayButton.Visibility = Visibility.Hidden;
                AgentButton.Visibility = Visibility.Hidden;
                ClientButton.Visibility = Visibility.Hidden;
            } else if (Properties.Settings.Default.IdRole == 2)
            {
                AgentButton.Visibility = Visibility.Hidden;
            }
        }

        private void TourButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new TourPage());
        }

        private void WayButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new WayPage());
        }

        private void AgentButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AgentPage());
        }

        private void OrderButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new OrderPage());
        }

        private void ClientButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ClientPage());
        }
    }
}
