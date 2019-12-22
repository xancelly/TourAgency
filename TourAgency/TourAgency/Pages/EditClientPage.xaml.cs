using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
    /// Логика взаимодействия для EditClientPage.xaml
    /// </summary>
    public partial class EditClientPage : Page
    {
        Client CurrentClient = null;
        public EditClientPage(Client client)
        {
            InitializeComponent();
            CurrentClient = client;
            if (CurrentClient != null)
            {
                SaveButton.Content = "Изменить";
                LoginTextBox.Text = AppData.Context.Users.Where(c => c.Id == CurrentClient.IdUser).Select(c => c.Login).FirstOrDefault();
                PasswordTextBox.Text = AppData.Context.Users.Where(c => c.Id == CurrentClient.IdUser).Select(c => c.Password).FirstOrDefault();
                LastNameTextBox.Text = CurrentClient.LastName;
                FirstNameTextBox.Text = CurrentClient.FirstName;
                MiddleNameTextBox.Text = CurrentClient.MiddleName;
                PhoneTextBox.Text = CurrentClient.Phone.Replace(@"+7", "");
                EmailTextBox.Text = CurrentClient.Email;
                DateOfBirthDatePicker.SelectedDate = CurrentClient.DateOfBirth;
                SeriasTextBox.Text = CurrentClient.Passport.Remove(4, 6);
                NumberTextBox.Text = CurrentClient.Passport.Remove(0, 4);
                AreaTextBox.Text = CurrentClient.Region;
                CityTextBox.Text = CurrentClient.City;
                StreetTextBox.Text = CurrentClient.Street;
                HouseTextBox.Text = CurrentClient.House;
                ApartmentTextBox.Text = CurrentClient.Apartment;
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            string letterList = "ABCDEFGHIJKLMNOPRSTUVWXYZabcdefghijklmnoprstuvwxyzАБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯабвгдеёжзийклмнопрстуфхцчшщъыьэюя";
            string numList = "1234567890";
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
                                                        if (CurrentClient == null)
                                                        {
                                                            if (AppData.Context.Users.Where(c => c.Login == LoginTextBox.Text).FirstOrDefault() == null)
                                                            {
                                                                Users CurrentUser = new Users()
                                                                {
                                                                    Login = LoginTextBox.Text,
                                                                    Password = PasswordTextBox.Text,
                                                                    IdRole = 1,
                                                                };
                                                                AppData.Context.Users.Add(CurrentUser);
                                                                CurrentClient = new Client()
                                                                {
                                                                    LastName = LastNameTextBox.Text,
                                                                    FirstName = FirstNameTextBox.Text,
                                                                    MiddleName = MiddleNameTextBox.Text,
                                                                    DateOfBirth = DateOfBirthDatePicker.SelectedDate.Value,
                                                                    Phone = RegionTextBox.Text + "" + PhoneTextBox.Text,
                                                                    Region = AreaTextBox.Text,
                                                                    City = CityTextBox.Text,
                                                                    Street = StreetTextBox.Text,
                                                                    House = HouseTextBox.Text,
                                                                    Apartment = ApartmentTextBox.Text,
                                                                    Email = EmailTextBox.Text,
                                                                    Passport = SeriasTextBox.Text + "" + NumberTextBox.Text,
                                                                    IdUser = CurrentUser.Id,
                                                                    IdTaxation = 1,
                                                                };
                                                                AppData.Context.Client.Add(CurrentClient);
                                                                MessageBox.Show("Клиент успешно зарегистрирован!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
                                                                AppData.Context.SaveChanges();
                                                                NavigationService.GoBack();
                                                            }
                                                            else
                                                            {
                                                                MessageBox.Show("Пользователь с таким логином уже существет!\nПридумайте другой логин.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                                                            }
                                                        }
                                                        else
                                                        {
                                                            Users CurrentUser = AppData.Context.Users.Where(c => c.Id == CurrentClient.IdUser).FirstOrDefault();
                                                            if (LoginTextBox.Text != CurrentUser.Login)
                                                            {
                                                                if (AppData.Context.Users.Where(c => c.Login == LoginTextBox.Text).FirstOrDefault() == null)
                                                                {
                                                                    CurrentUser.Login = LoginTextBox.Text;
                                                                    CurrentUser.Password = PasswordTextBox.Text;
                                                                    CurrentClient.LastName = LastNameTextBox.Text;
                                                                    CurrentClient.FirstName = FirstNameTextBox.Text;
                                                                    CurrentClient.MiddleName = MiddleNameTextBox.Text;
                                                                    CurrentClient.DateOfBirth = DateOfBirthDatePicker.SelectedDate.Value;
                                                                    CurrentClient.Phone = RegionTextBox.Text + "" + PhoneTextBox.Text;
                                                                    CurrentClient.Region = AreaTextBox.Text;
                                                                    CurrentClient.City = CityTextBox.Text;
                                                                    CurrentClient.Street = StreetTextBox.Text;
                                                                    CurrentClient.House = HouseTextBox.Text;
                                                                    CurrentClient.Apartment = ApartmentTextBox.Text;
                                                                    CurrentClient.Email = EmailTextBox.Text;
                                                                    CurrentClient.Passport = SeriasTextBox.Text + "" + NumberTextBox.Text;
                                                                    MessageBox.Show("Информация обновлена!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
                                                                    AppData.Context.SaveChanges();
                                                                    NavigationService.GoBack();
                                                                }
                                                                else
                                                                {
                                                                    MessageBox.Show("Пользователь с таким логином уже существет!\nПридумайте другой логин.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                                                                }
                                                            }
                                                            else
                                                            {
                                                                CurrentUser.Password = PasswordTextBox.Text;
                                                                CurrentClient.LastName = LastNameTextBox.Text;
                                                                CurrentClient.FirstName = FirstNameTextBox.Text;
                                                                CurrentClient.MiddleName = MiddleNameTextBox.Text;
                                                                CurrentClient.DateOfBirth = DateOfBirthDatePicker.SelectedDate.Value;
                                                                CurrentClient.Phone = RegionTextBox.Text + "" + PhoneTextBox.Text;
                                                                CurrentClient.Region = AreaTextBox.Text;
                                                                CurrentClient.City = CityTextBox.Text;
                                                                CurrentClient.Street = StreetTextBox.Text;
                                                                CurrentClient.House = HouseTextBox.Text;
                                                                CurrentClient.Apartment = ApartmentTextBox.Text;
                                                                CurrentClient.Email = EmailTextBox.Text;
                                                                CurrentClient.Passport = SeriasTextBox.Text + "" + NumberTextBox.Text;
                                                                MessageBox.Show("Информация обновлена!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
                                                                AppData.Context.SaveChanges();
                                                                NavigationService.GoBack();
                                                            }
                                                        }
                                                    }
                                                    else
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
    }
}
