using System;
using System.Collections.Generic;

namespace Accounting.Models
{
    public partial class TypeExpense
    {
        public TypeExpense()
        {
            Finances = new HashSet<Finance>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }

        public virtual ICollection<Finance> Finances { get; set; }
    }
}
