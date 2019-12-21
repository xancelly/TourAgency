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
    /// Логика взаимодействия для AgentPage.xaml
    /// </summary>
    public partial class AgentPage : Page
    {
        public AgentPage()
        {
            InitializeComponent();
            AgentDataGrid.ItemsSource = AppData.Context.Agent.ToList();
        }

        public void UpdateAgents()
        {
            var CurrentAgent = AppData.Context.Agent.ToList();
            CurrentAgent = CurrentAgent.Where(c => c.LastName.ToLower().Contains(SearchTextBox.Text.ToLower()) && c.FirstName.ToLower().Contains(SearchTextBox.Text.ToLower()) && c.MiddleName.ToLower().Contains(SearchTextBox.Text.ToLower())).ToList();
            AgentDataGrid.ItemsSource = CurrentAgent;
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateAgents();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new EditAgentPage(null));
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            Agent CurrentAgent = AgentDataGrid.SelectedItem as Agent;
            if (CurrentAgent != null)
            {
                NavigationService.Navigate(new EditAgentPage(CurrentAgent));
            }
            else
            {
                MessageBox.Show("Выберите агента!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            Agent CurrentAgent = AgentDataGrid.SelectedItem as Agent;
            if (CurrentAgent != null)
            {
                if (MessageBox.Show("Вы действительно хотите удалить агента?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    AppData.Context.Agent.Remove(CurrentAgent);
                    AppData.Context.SaveChanges();
                    Page_Loaded(null, null);
                    MessageBox.Show("Агент был удалён!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                MessageBox.Show("Выберите агента!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateAgents();
        }
    }
}
