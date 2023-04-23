using ExpenseTracker.Case.CoreLayer.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Case.CoreLayer.DTOs.Account
{
    public class AccountListDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Currency { get; set; }
        public string Description { get; set; }
        public DateTime CreateDate { get; set; }
        public string  UserName{ get; set; }

    }
}
