using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Логика взаимодействия для AboutProgramPage.xaml
    /// </summary>
    public partial class AboutProgramPage : Page
    {
        public AboutProgramPage()
        {
            InitializeComponent();
            NameProductTextBox.Text = "Spider Tour";
            VersionTextBox.Text = "1.0";
            AuthorTextBox.Text = "Чугунов А.В.";
            DescriptionTextBox.Text = "Данное ПО было создано для\nоблегчения поиска, создания и редактирования заказа,\nа так же создания и редактирования туров.";

        }

        private void DictionaryButton_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("TourAgency.html");
        }
    }
}
