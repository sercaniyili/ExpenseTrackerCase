using ExpenseTracker.Case.CoreLayer.Entities.Enums;
using ExpenseTracker.Case.CoreLayer.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Case.CoreLayer.Entities
{
    public class Account : IBaseEntity
    {
        public int Id { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public string Name { get; set; }
        public Currency Currency { get; set; }
        public string Description { get; set; }


        //nav
        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public ICollection<Transaction> Transactions { get; set; }

    }
}
