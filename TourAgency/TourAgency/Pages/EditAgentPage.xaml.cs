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
    /// Логика взаимодействия для EditAgentPage.xaml
    /// </summary>
    public partial class EditAgentPage : Page
    {
        Agent CurrentAgent = null;
        public EditAgentPage(Agent agent)
        {
            InitializeComponent();
            CurrentAgent = agent;
            if (CurrentAgent != null)
            {
                LoginTextBox.Text = AppData.Context.Users.Where(c => c.Id == CurrentAgent.IdUser).Select(c => c.Login).FirstOrDefault();
                PasswordTextBox.Text = AppData.Context.Users.Where(c => c.Id == CurrentAgent.IdUser).Select(c => c.Password).FirstOrDefault();
                LastNameTextBox.Text = CurrentAgent.LastName;
                FirstNameTextBox.Text = CurrentAgent.FirstName;
                MiddleNameTextBox.Text = CurrentAgent.MiddleName;
                PhoneTextBox.Text = CurrentAgent.Phone.Replace(@"+7", "");
                EmailTextBox.Text = CurrentAgent.Email;
                DateOfBirthDatePicker.SelectedDate = CurrentAgent.DateOfBirth;
                SeriasTextBox.Text = CurrentAgent.Passport.Remove(4, 6);
                NumberTextBox.Text = CurrentAgent.Passport.Remove(0, 4);
            }

        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
