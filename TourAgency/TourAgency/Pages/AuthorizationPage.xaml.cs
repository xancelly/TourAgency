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
    /// Логика взаимодействия для AuthorizationPage.xaml
    /// </summary>
    public partial class AuthorizationPage : Page
    {
        Context Db = new Context();
        public AuthorizationPage()
        {
            InitializeComponent();
        }

        private void RegistrationButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RegistrationPage());

        }

        private void EnterButton_Click(object sender, RoutedEventArgs e)
        {
            Users LoginUser = Db.Users.Where(c => c.Login == LoginTextBox.Text && c.Password == PasswordTextBox.Text && c.IsDeleted == false).FirstOrDefault();
            if (LoginUser != null)
            {
                LoginTextBox.Text = "";
                PasswordTextBox.Text = "";
                Properties.Settings.Default.IdUser = LoginUser.Id;
                Properties.Settings.Default.IdRole = Convert.ToInt32(LoginUser.IdRole);
                NavigationService.Navigate(new MainMenuPage());
            }
            else
            {
                MessageBox.Show("Такого пользователя не существует \nили введены неверные данные.\nПовторите попытку или зарегистрируйтесь!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
