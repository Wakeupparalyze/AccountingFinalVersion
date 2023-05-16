using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Accounting.Models
{
    public partial class User
    {
        public User()
        {
            Budgets = new HashSet<Budget>();
            Finances = new HashSet<Finance>();
        }

        public int Id { get; set; }
        public string Lastname { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Patronymic { get; set; }
        public string Login { get; set; } = null!;
        public string Password { get; set; } = null!;

        public virtual ICollection<Budget> Budgets { get; set; }
        public virtual ICollection<Finance> Finances { get; set; }

        [NotMapped]
        public string FIO { get => Lastname + " " + Name + " " + Patronymic; }
    }
}
