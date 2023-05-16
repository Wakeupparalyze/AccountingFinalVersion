using Accounting.DB;
using Accounting.Models;
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

namespace Accounting
{
    /// <summary>
    /// Логика взаимодействия для RegistrationPage.xaml
    /// </summary>
    public partial class RegistrationPage : Page
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Patronymic { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }

        public User User { get; set; }
        public RegistrationPage()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void Registration(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrEmpty(LastName) || string.IsNullOrEmpty(FirstName) || string.IsNullOrEmpty(Login) || string.IsNullOrEmpty(Password))
            {
                MessageBox.Show("Не все поля заполнены");
                return;
            }
            else
            {
                try
                {
                    var user = accountingContext.Instance().Users.FirstOrDefault(s => s.Login == Login);
                    if (user != null)
                    {
                        MessageBox.Show("Пользователь с таким логином уже существует");
                        return;
                    }
                    else
                    {
                        User = new User() { Lastname = LastName, Name = FirstName, Patronymic = Patronymic, Login = Login, Password = Password };
                        accountingContext.Instance().Users.Add(User);
                        accountingContext.Instance().SaveChanges();
                        MessageBox.Show("Вы успешно зарегистрировались!");
                        NavigationService.Navigate(new SignInPage());
                    }
                }
                catch
                {
                    MessageBox.Show("Ошибка связи с БД");
                    return;
                }
            }
        }

        private void ToSignInPage(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new SignInPage());
        }
    }
}
