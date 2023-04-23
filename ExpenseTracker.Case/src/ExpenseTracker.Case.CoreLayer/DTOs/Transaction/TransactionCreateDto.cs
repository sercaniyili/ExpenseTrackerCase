using ExpenseTracker.Case.CoreLayer.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Case.CoreLayer.DTOs.Transaction
{
    public class TransactionCreateDto
    {
        public Category Category { get; set; }
        public float Amount { get; set; }
       // public TransactionType TransactionType { get; set; }
        public int AccountId { get; set; }
    }
}
