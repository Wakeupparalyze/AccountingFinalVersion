using System;
using System.Collections.Generic;

namespace Accounting.Models
{
    public partial class Budget
    {
        public int Id { get; set; }
        public int IdUser { get; set; }
        public int IdFinance { get; set; }
        public decimal Total { get; set; }

        public virtual Finance IdFinanceNavigation { get; set; } = null!;
        public virtual User IdUserNavigation { get; set; } = null!;
    }
}
