using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Case.CoreLayer.Entities.Identity
{
    public class AppUser : IdentityUser<int>, IBaseEntity
    {
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public string Name { get; set; }
        public string Surname { get; set; }


        //nav
        public ICollection<Account> Accounts { get; set; }
    }
}
