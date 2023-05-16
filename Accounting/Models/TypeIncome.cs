using System;
using System.Collections.Generic;

namespace Accounting.Models
{
    public partial class TypeIncome
    {
        public TypeIncome()
        {
            Finances = new HashSet<Finance>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Finance> Finances { get; set; }
    }
}
