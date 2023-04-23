using ExpenseTracker.Case.CoreLayer.DTOs.Account;
using ExpenseTracker.Case.CoreLayer.DTOs.Transaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Case.CoreLayer.Interfaces.Services.Account
{
    public interface ITransactionService
    {
        Task<IEnumerable<TransactionListDto>> GetAllTransactions();

        Task<TransactionCreateDto> CreateTransaction(TransactionCreateDto  transactionCreateDto);

        Task DeleteTransaction(int id);
    }
}
