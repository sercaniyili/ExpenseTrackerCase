using ExpenseTracker.Case.CoreLayer.Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Case.CoreLayer.DTOs.Transaction
{
    public class TransactionListDto
    {
        public int Id { get; set; }

        private DateTime _createDate;
        public string CreateDate
        {
            get => _createDate.ToString("yyyy-MM-dd");
            set => _createDate = DateTime.Parse(value);
        }
        public string AmountWithCurrency { get; set; }
        public string Category { get; set; }

    }
}
