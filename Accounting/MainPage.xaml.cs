using Accounting.DB;
using Accounting.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page, INotifyPropertyChanged
    {
        private User User;
        private decimal sum;
        private IncomeOrExpense selectedIncomeOrExpense;

        public event PropertyChangedEventHandler? PropertyChanged;
        void Signal([CallerMemberName] string prop = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
        public decimal Sum
        {
            get => sum;
            set
            {
                sum = value;
                Signal();
            }
        }
        public string FIO { get; set; }
        public List<IncomeOrExpense> IncomeOrExpenses { get; set; }
        public IncomeOrExpense SelectedIncomeOrExpense 
        {
            get => selectedIncomeOrExpense;
            set
            {
                selectedIncomeOrExpense = value;
                Signal();
            }
        }
        public List<TypeIncome> TypeIncomes { get; set; }
        public TypeIncome SelectedTypeIncome { get; set; }
        public List<TypeExpense> TypeExpenses { get; set; }
        public TypeExpense SelectedTypeExpense { get; set; }
        public Finance Finance { get; set; }
        public Budget Budget { get; set; }
        public MainPage(Models.User user)
        {
            InitializeComponent();
            User = user;
            DataContext = this;
            FIO = user.FIO;
            IncomeOrExpenses = accountingContext.Instance().IncomeOrExpenses.ToList();
            TypeIncomes = accountingContext.Instance().TypeIncomes.ToList();
            TypeExpenses = accountingContext.Instance().TypeExpenses.ToList();
            Vanish1.Visibility = Visibility.Hidden;
            Vanish2.Visibility = Visibility.Hidden;
        }

        private void Select(object sender, RoutedEventArgs e)
        {
            var firstFinance = accountingContext.Instance().Finances.FirstOrDefault(s => s.IdUser == User.Id);
            if (firstFinance != null)
            {
                if (SelectedIncomeOrExpense?.Id == 1)
                {
                    Vanish2.Visibility = Visibility.Hidden;
                    Vanish1.Visibility = Visibility.Visible;
                }
                else if (SelectedIncomeOrExpense?.Id == 2)
                {
                    Vanish1.Visibility = Visibility.Hidden;
                    Vanish2.Visibility = Visibility.Visible;
                }
            }
            else
            {
                SelectedIncomeOrExpense = accountingContext.Instance().IncomeOrExpenses.FirstOrDefault( s => s.Id == 1);
                Signal(nameof(SelectedIncomeOrExpense));
                Vanish1.Visibility = Visibility.Visible;
                MessageBox.Show("Первый раз укажите доход");
                return;
            }
        }

        private void Add(object sender, RoutedEventArgs e)
        {
            if (SelectedIncomeOrExpense?.Id == 1)
            {
                SelectedTypeExpense = null;
                if (SelectedTypeIncome != null && Sum != 0)
                {
                    SaveFinance();
                    SaveBudget();
                    MessageBox.Show("Запись сохранена");
                }
                else
                {
                    MessageBox.Show("Не все поля заполнены");
                    return;
                }
            }
            else if (SelectedIncomeOrExpense?.Id == 2)
            {
                SelectedTypeIncome = null;
                if (SelectedTypeExpense != null && Sum != 0)
                {
                    SaveFinance();
                    SaveBudget();
                    MessageBox.Show("Запись сохранена");
                }
                else
                {
                    MessageBox.Show("Не все поля заполнены");
                    return;
                }
            }
            else
            {
                MessageBox.Show("Не все поля заполнены");
                return;
            }
            SelectedIncomeOrExpense = null;
            SelectedTypeIncome = null;
            SelectedTypeExpense = null;
            Sum = 0;
            Signal(nameof(SelectedIncomeOrExpense));
            Signal(nameof(SelectedTypeIncome));
            Signal(nameof(SelectedTypeExpense));
            Signal(nameof(Sum));
            Vanish1.Visibility = Visibility.Hidden;
            Vanish2.Visibility = Visibility.Hidden;
            return;
        }
        private void SaveFinance()
        {
            try
            {
                Finance = new Finance() { IdUser = User.Id, IdIncomeOrExpenses = SelectedIncomeOrExpense.Id, IdTypeIncome = SelectedTypeIncome?.Id, IdTypeExpenses = SelectedTypeExpense?.Id, Date = DateTime.Now, Sum = Sum };
                accountingContext.Instance().Finances.Add(Finance);
                accountingContext.Instance().SaveChanges();
            }
            catch
            {
                MessageBox.Show("Ошибка связи с БД");
                return;
            }

        }
        private void SaveBudget()
        {
            try
            {
                var lastFinance = accountingContext.Instance().Finances.ToList().LastOrDefault();
                var lastBudget = accountingContext.Instance().Budgets.ToList().LastOrDefault(s => s.IdUser == User.Id);
                if (lastBudget == null)
                {
                    if (lastFinance.Sum < 0)
                    {
                        lastFinance.Sum = lastFinance.Sum * (-1);
                        
                    }
                    Budget = new Budget() { IdUser = User.Id, IdFinance = lastFinance.Id, Total = lastFinance.Sum };
                    accountingContext.Instance().Budgets.Add(Budget);
                    accountingContext.Instance().SaveChanges();
                    MessageBox.Show($"Ваш баланс: {lastFinance.Sum} ₽");
                    return;

                }
                else
                {
                    if (lastFinance.Sum < 0)
                    {
                        lastFinance.Sum = lastFinance.Sum * (-1);
                        
                    }
                    if (SelectedIncomeOrExpense.Id == 1)
                    {
                        lastBudget.Total = lastFinance.Sum + lastBudget.Total;
                        accountingContext.Instance().Budgets.Update(lastBudget);
                        accountingContext.Instance().SaveChanges();
                        MessageBox.Show($"Ваш баланс: {lastBudget.Total} ₽");
                        return;
                    }
                    else
                    {
                        lastBudget.Total = lastBudget.Total - lastFinance.Sum;
                        accountingContext.Instance().Budgets.Update(lastBudget);
                        accountingContext.Instance().SaveChanges();
                        MessageBox.Show($"Ваш баланс: {lastBudget.Total} ₽");
                        return;
                    }

                }
            }
            catch
            {
                MessageBox.Show("Ошибка!");
            }

        }

        private void SignOut(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new SignInPage());
        }

        private void ToStatisticsPage(object sender, RoutedEventArgs e)
        {
            var win = new StatisticsWin(User);
            win.ShowDialog();
        }
    }
}
