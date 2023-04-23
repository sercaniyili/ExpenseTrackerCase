using ExpenseTracker.Case.CoreLayer.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Case.CoreLayer.DTOs.Account
{
    public class AccountCreateDto
    {
        public string Name { get; set; }
        public Currency Currency { get; set; }
        public string Description { get; set; }
        public int AppUserId { get; set; }
    }
}
