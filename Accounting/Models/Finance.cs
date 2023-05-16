using System;
using System.Collections.Generic;

namespace Accounting.Models
{
    public partial class Finance
    {
        public Finance()
        {
            Budgets = new HashSet<Budget>();
        }

        public int Id { get; set; }
        public int IdUser { get; set; }
        public int IdIncomeOrExpenses { get; set; }
        public int? IdTypeIncome { get; set; }
        public int? IdTypeExpenses { get; set; }
        public DateTime Date { get; set; }
        public decimal Sum { get; set; }

        public virtual IncomeOrExpense IdIncomeOrExpensesNavigation { get; set; } = null!;
        public virtual TypeExpense? IdTypeExpensesNavigation { get; set; }
        public virtual TypeIncome? IdTypeIncomeNavigation { get; set; }
        public virtual User IdUserNavigation { get; set; } = null!;
        public virtual ICollection<Budget> Budgets { get; set; }
    }
}
