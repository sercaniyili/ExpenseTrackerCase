using AutoMapper;
using ExpenseTracker.Case.CoreLayer.DTOs.Account;
using ExpenseTracker.Case.CoreLayer.DTOs.Transaction;
using ExpenseTracker.Case.CoreLayer.Entities;
using ExpenseTracker.Case.CoreLayer.Entities.Enums;
using ExpenseTracker.Case.CoreLayer.Interfaces.Repositories;
using ExpenseTracker.Case.CoreLayer.Interfaces.Services.Account;
using ExpenseTracker.Case.DataAccessLayer.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Case.BusinessLayer.Managers
{
    public class TransactionManager:ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IMapper _mapper;

        public TransactionManager(ITransactionRepository transactionRepository, IMapper mapper)
        {
            _transactionRepository = transactionRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TransactionListDto>> GetAllTransactions()
        {
            var transactions = await _transactionRepository.GetAllAsyncQueryable()
           .Include(c => c.Account)
           .ToListAsync();
            var mappedTransactions = _mapper.Map<IEnumerable<TransactionListDto>>(transactions);
            return _mapper.Map<IEnumerable<TransactionListDto>>(mappedTransactions);

        }

        public async Task<TransactionCreateDto> CreateTransaction(TransactionCreateDto transactionCreateDto)
        {
            var transactions = _mapper.Map<Transaction>(transactionCreateDto);
            await UpdateAccountBalance(transactionCreateDto);
            var mappedTransaction = await _transactionRepository.CreateAsync(transactions);
            var result = _mapper.Map<TransactionCreateDto>(mappedTransaction);
            return result;
        }

        public async Task DeleteTransaction(int id)
        {
            var transactions = await _transactionRepository.GetByIdAsync(id);
            if (transactions == null)
                throw new ArgumentException($"Transaction with id {id} does not exist.");

            await _transactionRepository.DeleteAsync(id);
        }


        //privateUpdateAccountBalanceMethod
        private async Task UpdateAccountBalance(TransactionCreateDto transactionCreateDto)
        {
            var account = await _transactionRepository.FindAccountById(transactionCreateDto.AccountId);

            if (transactionCreateDto.Amount > 0)
            {
             //   transactionCreateDto.TransactionType = TransactionType.Input;
                account.Balance += transactionCreateDto.Amount;
            }
            else if (transactionCreateDto.Amount < 0)
            {
               // transactionCreateDto.TransactionType = TransactionType.Output;
                account.Balance += transactionCreateDto.Amount;
            }
            else
                throw new ArgumentException($"0 deeğerinde bir işlem yapamazsınız");

            await _transactionRepository.SaveTransactionChangesAsync();
        }



    }
}
