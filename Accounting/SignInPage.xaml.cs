using Accounting.DB;
using Accounting.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
    /// Логика взаимодействия для SignInPage.xaml
    /// </summary>
    public partial class SignInPage : Page, INotifyPropertyChanged
    {
        public string Login { get; set; }
        public string Password { get; set; }
        
        public SignInPage()
        {
            InitializeComponent();
            DataContext = this;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void SignalChanged([CallerMemberName] string prop = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        private void SignIn(object sender, RoutedEventArgs e)
        {
            try
            {
                var user = accountingContext.Instance().Users.FirstOrDefault(s => s.Login == Login && s.Password == Password);
                if (user != null)
                {
                    NavigationService.Navigate(new MainPage(user));
                }
                else
                {
                    MessageBox.Show("Неверный логин или пароль");
                    return;
                }
            }
            catch
            {
                MessageBox.Show("Ошибка связи с БД");
                return;
            }
            
        }

        private void ToRegistrationPage(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RegistrationPage());
        }
    }
}
