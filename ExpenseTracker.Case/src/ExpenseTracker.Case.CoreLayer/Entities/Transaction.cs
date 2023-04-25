using ExpenseTracker.Case.CoreLayer.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Case.CoreLayer.Entities
{
    public class Transaction : IBaseEntity
    {
        public int Id { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public float Amount { get; set; }
        public Category Category { get; set; }


        //nav
        public int AccountId { get; set; }
        public Account Account { get; set; }

    }
}
