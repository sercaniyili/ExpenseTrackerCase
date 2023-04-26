using ExpenseTracker.Case.CoreLayer.DTOs.Account;
using ExpenseTracker.Case.CoreLayer.DTOs.Transaction;
using ExpenseTracker.Case.CoreLayer.Entities;
using ExpenseTracker.Case.CoreLayer.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Case.CoreLayer.Interfaces.Services.Account
{
    public interface IAccountService
    {
        Task<IEnumerable<AccountListDto>> GetAllAccounts();

        Task<AccountCreateDto> CreateAccount(AccountCreateDto accountCreateDto);

        Task<AccountEditDto> EditAccount(int id, AccountEditDto accountEditDto);

        Task DeleteAccount(int id);
        Task<IEnumerable<TransactionListDto>> GetTransactionsByAccountIdAsync(int id);

    }
}


