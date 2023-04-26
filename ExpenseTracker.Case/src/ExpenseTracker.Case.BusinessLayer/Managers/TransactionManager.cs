using AutoMapper;
using ExpenseTracker.Case.BusinessLayer.Validations.Account;
using ExpenseTracker.Case.BusinessLayer.Validations.Transaction;
using ExpenseTracker.Case.CoreLayer.DTOs.Account;
using ExpenseTracker.Case.CoreLayer.DTOs.Transaction;
using ExpenseTracker.Case.CoreLayer.Entities;
using ExpenseTracker.Case.CoreLayer.Entities.Enums;
using ExpenseTracker.Case.CoreLayer.Interfaces.Repositories;
using ExpenseTracker.Case.CoreLayer.Interfaces.Services.Account;
using ExpenseTracker.Case.DataAccessLayer.Repositories;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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

        /// <summary>
        /// Tüm işlemleri ilgili hesap verileri ile listeler
        /// </summary>
        /// <returns>işlemler</returns>
        public async Task<IEnumerable<TransactionListDto>> GetAllTransactions()
        {
            var transactions = await _transactionRepository.GetAllAsyncQueryable()
           .Include(c => c.Account)
           .ToListAsync();
            var mappedTransactions = _mapper.Map<IEnumerable<TransactionListDto>>(transactions);
            var result = _mapper.Map<IEnumerable<TransactionListDto>>(mappedTransactions);
            return result;
        }

        /// <summary>
        /// İşlem oluşturulur, validasyonlar yapılır, işlemde yer alan hesap bakiyesi güncellenir
        /// </summary>
        /// <param name="transactionCreateDto">işlem oluşturmak için kullanılan girdiler</param>
        /// <returns>oluşturulan işlem</returns>
        /// validasyon hatası olursa hata fırlatılır
        public async Task<TransactionCreateDto> CreateTransaction(TransactionCreateDto transactionCreateDto)
        {
            var transactions = _mapper.Map<Transaction>(transactionCreateDto);

            TransactionCreateDtoValidation validationRules = new TransactionCreateDtoValidation();
            validationRules.ValidateAndThrow(transactionCreateDto);

            await UpdateAccountBalance(transactionCreateDto);

            var mappedTransaction = await _transactionRepository.CreateAsync(transactions);
            var result = _mapper.Map<TransactionCreateDto>(mappedTransaction);
            return result;
        }

        /// <summary>
        /// Belirtilen id'ye sahip işlemi siler
        /// </summary>
        /// <param name="id">silinecek işlem id'si</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">belirtilen id'ye sahip işlem yoksa</exception>
        public async Task DeleteTransaction(int id)
        {
            var transactions = await _transactionRepository.GetByIdAsync(id);
            if (transactions == null)
                throw new ArgumentException($"{id} numaralı işlem bulunmamaktadır.");

            await _transactionRepository.DeleteAsync(id);
        }

        /// <summary>
        /// işlemleri kriterlere göre listeler
        /// </summary>
        /// <param name="searchDto">aranacak işlem kriterlerini içeren dto</param>
        /// <returns>arama sonuçlarını içeren IEnumerable TransactionSearchDto nesnesi</returns>
        public async Task<IEnumerable<TransactionListDto>> SearchTransactions(TransactionSearchDto searchDto)
        {
            var query = await _transactionRepository.GetAllAsyncQueryable()
             .Include(c => c.Account)
             .ToListAsync();

            if (searchDto.MinAmount.HasValue)
            {
                query = query.Where(t => t.Amount >= searchDto.MinAmount.Value).ToList();
            }

            if (searchDto.MaxAmount.HasValue)
            {
                query = query.Where(t => t.Amount <= searchDto.MaxAmount.Value).ToList();
            }

            if (!string.IsNullOrWhiteSpace(searchDto.Category))
            {
                query = query.Where(t => t.Category.ToString().ToLower() == searchDto.Category.ToLower()).ToList();
            }


            var mappedTransactions = _mapper.Map<IEnumerable<TransactionListDto>>(query);
            var result =_mapper.Map<IEnumerable<TransactionListDto>>(mappedTransactions);
            return result;
        }

        /// <summary>
        /// işlem yapılan hesabın bakiyesi güncellenir
        /// </summary>
        /// <param name="transactionCreateDto">yeni işlem yapılacak hesap verileri</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">işlem tutarı 0 ise geçersiz işlem</exception>
        private async Task UpdateAccountBalance(TransactionCreateDto transactionCreateDto)
        {
            var account = await _transactionRepository.FindAccountById(transactionCreateDto.AccountId);

            if (transactionCreateDto.Amount > 0)           
                account.Balance += transactionCreateDto.Amount;
            else if (transactionCreateDto.Amount < 0)
                account.Balance += transactionCreateDto.Amount;
            else
                throw new ArgumentException($"0 deeğerinde bir işlem yapamazsınız");

            await _transactionRepository.SaveTransactionChangesAsync();
        }

   
    }
}
