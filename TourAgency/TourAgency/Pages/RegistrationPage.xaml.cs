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
using System.ComponentModel.DataAnnotations;

namespace TourAgency.Pages
{
    /// <summary>
    /// Логика взаимодействия для RegistrationPage.xaml
    /// </summary>
    public partial class RegistrationPage : Page
    {
        Context Db = new Context();
        public RegistrationPage()
        {
            InitializeComponent();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void RegistrationButton_Click(object sender, RoutedEventArgs e)
        {
            string letterList = "ABCDEFGHIJKLMNOPRSTUVWXYZabcdefghijklmnoprstuvwxyzАБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯабвгдеёжзийклмнопрстуфхцчшщъыьэюя";
            string numList = "1234567890";
            if (Db.Users.Where(c => c.Login == LoginTextBox.Text).FirstOrDefault() == null)
            {
                if (!String.IsNullOrWhiteSpace(PasswordTextBox.Text) && !String.IsNullOrWhiteSpace(LastNameTextBox.Text) && !String.IsNullOrWhiteSpace(FirstNameTextBox.Text) && !String.IsNullOrWhiteSpace(PhoneTextBox.Text) && !String.IsNullOrWhiteSpace(EmailTextBox.Text) && !String.IsNullOrWhiteSpace(SeriasTextBox.Text) && !String.IsNullOrWhiteSpace(NumberTextBox.Text) && DateOfBirthDatePicker.SelectedDate != null && !String.IsNullOrWhiteSpace(AreaTextBox.Text) && !String.IsNullOrWhiteSpace(CityTextBox.Text) && !String.IsNullOrWhiteSpace(StreetTextBox.Text) && !String.IsNullOrWhiteSpace(HouseTextBox.Text))
                {
                    if (LastNameTextBox.Text.IndexOfAny(numList.ToCharArray()) <= -1)
                    {
                        if (FirstNameTextBox.Text.IndexOfAny(numList.ToCharArray()) <= -1)
                        {
                            if (MiddleNameTextBox.Text.IndexOfAny(numList.ToCharArray()) <= -1)
                            {
                                if (PhoneTextBox.Text.Length == 10 && (PhoneTextBox.Text.IndexOfAny(letterList.ToCharArray()) <= -1))
                                {
                                    if (new EmailAddressAttribute().IsValid(EmailTextBox.Text))
                                    {
                                        if (DateOfBirthDatePicker.SelectedDate < DateTime.Today)
                                        {
                                            if (SeriasTextBox.Text.Length == 4 && (SeriasTextBox.Text.IndexOfAny(letterList.ToCharArray()) <= -1))
                                            {
                                                if (NumberTextBox.Text.Length == 6 && (NumberTextBox.Text.IndexOfAny(letterList.ToCharArray()) <= -1))
                                                {
                                                    if (AreaTextBox.Text.IndexOfAny(numList.ToCharArray()) <= -1)
                                                    {
                                                        if (CityTextBox.Text.IndexOfAny(numList.ToCharArray()) <= -1)
                                                        {
                                                            Users NewUser = new Users()
                                                            {
                                                                Login = LoginTextBox.Text,
                                                                Password = PasswordTextBox.Text,
                                                                IdRole = 1,
                                                            };
                                                            Db.Users.Add(NewUser);
                                                            Properties.Settings.Default.IdUser = NewUser.Id;
                                                            if (!String.IsNullOrWhiteSpace(ApartmentTextBox.Text))
                                                            {
                                                                Client NewClient = new Client()
                                                                {
                                                                    LastName = LastNameTextBox.Text,
                                                                    FirstName = FirstNameTextBox.Text,
                                                                    MiddleName = MiddleNameTextBox.Text,
                                                                    Phone = RegionTextBox.Text + "" + PhoneTextBox.Text,
                                                                    Address = "обл. " + AreaTextBox.Text + ", г. " + CityTextBox.Text + ", ул. " + StreetTextBox.Text + ", д. " + HouseTextBox.Text + ", кв. " + ApartmentTextBox.Text,
                                                                    Email = EmailTextBox.Text,
                                                                    Passport = SeriasTextBox.Text + "" + NumberTextBox.Text,
                                                                    IdUser = Properties.Settings.Default.IdUser,
                                                                    IdTaxation = 1,
                                                                };
                                                                Db.Client.Add(NewClient);
                                                                Db.SaveChanges();
                                                                MessageBox.Show("Пользователь успешно зарегистрирован!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
                                                                NavigationService.GoBack();
                                                            } else
                                                            {
                                                                Client NewClient = new Client()
                                                                {
                                                                    LastName = LastNameTextBox.Text,
                                                                    FirstName = FirstNameTextBox.Text,
                                                                    MiddleName = MiddleNameTextBox.Text,
                                                                    Phone = RegionTextBox.Text + "" + PhoneTextBox.Text,
                                                                    Address = "обл. " + AreaTextBox.Text + ", г. " + CityTextBox.Text + ", ул. " + StreetTextBox.Text + ", д. " + HouseTextBox.Text,
                                                                    Email = EmailTextBox.Text,
                                                                    Passport = SeriasTextBox.Text + "" + NumberTextBox.Text,
                                                                    IdUser = Properties.Settings.Default.IdUser,
                                                                    IdTaxation = 1,
                                                                };
                                                                Db.Client.Add(NewClient);
                                                                Db.SaveChanges();
                                                                MessageBox.Show("Пользователь успешно зарегистрирован!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
                                                                NavigationService.GoBack();
                                                            }
                                                        } else
                                                        {
                                                            MessageBox.Show("Город указан некорректно!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                                                            CityTextBox.Focus();
                                                        }
                                                    } 
                                                    else
                                                    {
                                                        MessageBox.Show("Область указана некорректно", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                                                        AreaTextBox.Focus();
                                                    }
                                                }
                                                else
                                                {
                                                    MessageBox.Show("Номер паспорта указана некорректно!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                                                    NumberTextBox.Focus();
                                                }
                                            }
                                            else
                                            {
                                                MessageBox.Show("Серия паспорта указана некорректно!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                                                SeriasTextBox.Focus();
                                            }
                                        }
                                        else
                                        {
                                            MessageBox.Show("Дата рождения указана некорректно!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                                            DateOfBirthDatePicker.Focus();
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("E-Mail введён некорректно!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                                        EmailTextBox.Focus();
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Телефон введён некорректно!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                                    PhoneTextBox.Focus();
                                }
                            }
                            else
                            {
                                MessageBox.Show("Отчество указано некорректно!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                                MiddleNameTextBox.Focus();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Имя указано некорректно!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                            FirstNameTextBox.Focus();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Фамилия указана некорректно!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        LastNameTextBox.Focus();
                    }
                }
                else
                {
                    MessageBox.Show("Не все поля заполнены!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Пользователь с таким логином уже существет!\nПридумайте другой логин.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
