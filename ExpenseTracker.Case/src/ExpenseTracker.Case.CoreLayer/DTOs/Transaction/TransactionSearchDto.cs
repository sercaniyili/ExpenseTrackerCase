using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Case.CoreLayer.DTOs.Transaction
{
    public class TransactionSearchDto
    {
        public float? MinAmount { get; set; }
        public float? MaxAmount { get; set; }
        public string Category { get; set; }
    }
}
