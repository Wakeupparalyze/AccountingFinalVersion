using Accounting.DB;
using Accounting.Models;
using Microsoft.EntityFrameworkCore;
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
using System.Windows.Shapes;

namespace Accounting
{
    /// <summary>
    /// Логика взаимодействия для StatisticsWin.xaml
    /// </summary>
    public partial class StatisticsWin : Window
    {
        public User User { get; set; }
        public List<Finance> Finances { get; set; } = new List<Finance>();
        public Budget Budget { get; set; }

        public List<Finance> DayExpenses { get; set; }
        public List<Finance> MonthExpenses { get; set; }
        public List<Finance> YearExpenses { get; set; }
        public List<Finance> Expenses { get; set; }
        public Decimal AllExpensesToday { get; set; }
        public Decimal AllExpensesMonth { get; set; }
        public Decimal AllExpensesYear { get; set; }
        public Decimal AllExpenses { get; set; }

        public List<Finance> DayIncome { get; set; }
        public List<Finance> MonthIncome { get; set; }
        public List<Finance> YearIncome { get; set; }
        public List<Finance> Income { get; set; }
        public Decimal AllIncomeToday { get; set; }
        public Decimal AllIncomeMonth { get; set; }
        public Decimal AllIncomeYear { get; set; }
        public Decimal AllIncome { get; set; }

        public StatisticsWin(Models.User user)
        {
            InitializeComponent();
            DataContext = this;
            User = user;
            Finances = accountingContext.Instance().Finances.Where( s => s.IdUser == user.Id).ToList();
            Budget = accountingContext.Instance().Budgets.FirstOrDefault(s => s.IdUser == user.Id);
            //Статистика расходов
            DayExpenses = accountingContext.Instance().Finances.Where(s => s.IdUser == user.Id && s.IdIncomeOrExpenses == 2 && s.Date.Day == DateTime.Now.Day && s.Date.Month == DateTime.Now.Month && s.Date.Year == DateTime.Now.Year).ToList();
            foreach (var num in DayExpenses)
            {
                AllExpensesToday += num.Sum;
            }

            MonthExpenses = accountingContext.Instance().Finances.Where(s => s.IdUser == user.Id && s.IdIncomeOrExpenses == 2 && s.Date.Month == DateTime.Now.Month && s.Date.Year == DateTime.Now.Year).ToList();
            foreach (var num in MonthExpenses)
            {
                AllExpensesMonth += num.Sum;
            }

            YearExpenses = accountingContext.Instance().Finances.Where(s => s.IdUser == user.Id && s.IdIncomeOrExpenses == 2 && s.Date.Year == DateTime.Now.Year).ToList();
            foreach (var num in YearExpenses)
            {
                AllExpensesYear += num.Sum;
            }

            Expenses = accountingContext.Instance().Finances.Where(s => s.IdUser == user.Id && s.IdIncomeOrExpenses == 2).ToList();
            foreach (var num in Expenses)
            {
                AllExpenses += num.Sum;
            }
            //Статистика доходов
            DayIncome = accountingContext.Instance().Finances.Where(s => s.IdUser == user.Id && s.IdIncomeOrExpenses == 1 && s.Date.Day == DateTime.Now.Day && s.Date.Month == DateTime.Now.Month && s.Date.Year == DateTime.Now.Year).ToList();
            foreach (var num in DayIncome)
            {
                AllIncomeToday += num.Sum;
            }

            MonthIncome = accountingContext.Instance().Finances.Where(s => s.IdUser == user.Id && s.IdIncomeOrExpenses == 1 && s.Date.Month == DateTime.Now.Month && s.Date.Year == DateTime.Now.Year).ToList();
            foreach (var num in MonthIncome)
            {
                AllIncomeMonth += num.Sum;
            }

            YearIncome = accountingContext.Instance().Finances.Where(s => s.IdUser == user.Id && s.IdIncomeOrExpenses == 1 && s.Date.Year == DateTime.Now.Year).ToList();
            foreach (var num in YearIncome)
            {
                AllIncomeYear += num.Sum;
            }

            Income = accountingContext.Instance().Finances.Where(s => s.IdUser == user.Id && s.IdIncomeOrExpenses == 1).ToList();
            foreach (var num in Income)
            {
                AllIncome += num.Sum;
            }
        }
    }
}
