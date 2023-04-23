using ExpenseTracker.Case.CoreLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Case.CoreLayer.Interfaces.Repositories
{
    public interface ITransactionRepository : IBaseRepository<Transaction> 
    {
        public Task<Account> FindAccountById(int id);
        public Task<int> SaveTransactionChangesAsync();
    }

}
