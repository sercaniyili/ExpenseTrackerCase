using ExpenseTracker.Case.CoreLayer.Entities;
using ExpenseTracker.Case.CoreLayer.Interfaces.Repositories;
using ExpenseTracker.Case.DataAccessLayer.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Case.DataAccessLayer.Repositories
{
    public class TransactionRepository : BaseRepository<Transaction>,ITransactionRepository
    {
        public TransactionRepository(AppDbContext appDbContext) : base(appDbContext) { }
    }
}
