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

        private DateTime _createDate;
        public string CreateDate
        {
            get => _createDate.ToString("yyyy-MM-dd");
            set => _createDate = DateTime.Parse(value);
        }
        public string  UserName{ get; set; }

        public float Balance { get; set; }

    }
}
