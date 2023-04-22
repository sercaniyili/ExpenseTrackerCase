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
    public class AccountRepository: BaseRepository<Account>, IAccountRepository
    {
        public AccountRepository(AppDbContext appDbContext) : base(appDbContext) { }
    }
}
