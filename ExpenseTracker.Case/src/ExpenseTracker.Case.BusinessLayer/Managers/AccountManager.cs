using AutoMapper;
using ExpenseTracker.Case.BusinessLayer.Validations.Account;
using ExpenseTracker.Case.CoreLayer.DTOs.Account;
using ExpenseTracker.Case.CoreLayer.DTOs.Transaction;
using ExpenseTracker.Case.CoreLayer.Entities;
using ExpenseTracker.Case.CoreLayer.Interfaces.Repositories;
using ExpenseTracker.Case.CoreLayer.Interfaces.Services.Account;
using ExpenseTracker.Case.DataAccessLayer.Repositories;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Case.BusinessLayer.Managers
{
    public class AccountManager : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ITransactionRepository  _transactionRepository;
        private readonly IMapper _mapper;

        public AccountManager(IAccountRepository accountRepository, ITransactionRepository transactionRepository, IMapper mapper)
        {
            _accountRepository = accountRepository;
            _transactionRepository = transactionRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Tüm hesapları ve ilgili kullanıcı bilgilerini listeler
        /// </summary>
        /// <returns>hesaplar</returns>
        public async Task<IEnumerable<AccountListDto>> GetAllAccounts()
        {
            var accounts = _accountRepository.GetAllAsyncQueryable()
                .Include(c => c.AppUser);
            var mappedAccounts = _mapper.Map<IEnumerable<AccountListDto>>(accounts);
            return mappedAccounts;
        }

        /// <summary>
        /// Verilen hesap id'sine göre işlemleri listeler
        /// </summary>
        /// <param name="id">hesap id'si</param>
        /// <returns>id'si verilen hesap ile ilişkili işlemler</returns>
        public async Task<IEnumerable<TransactionListDto>> GetTransactionsByAccountIdAsync(int id)
        {

            var transactions = await _transactionRepository.GetAllAsyncQueryable()
                .Where(x => x.AccountId == id)
                 .Include(c => c.Account)
                 .ToListAsync();
            var mappedTransactions = _mapper.Map<IEnumerable<TransactionListDto>>(transactions);
            var result = _mapper.Map<IEnumerable<TransactionListDto>>(mappedTransactions);
            return result;
        }

        /// <summary>
        /// Hesap oluşturulur, validasyonlar yapılır
        /// </summary>
        /// <param name="accountCreateDto">hesap oluşturmak için istenen girdiler</param>
        /// <returns>oluşturulan hesap</returns>
        /// validasyon olursa hata fırlatır
        public async Task<AccountCreateDto> CreateAccount(AccountCreateDto accountCreateDto)
        {
            var account = _mapper.Map<Account>(accountCreateDto);

            AccountCreateDtoValidation validationRules = new AccountCreateDtoValidation();
            validationRules.ValidateAndThrow(accountCreateDto);

            var mappedAccount = await _accountRepository.CreateAsync(account);
            var result = _mapper.Map<AccountCreateDto>(mappedAccount);
            return result;
        }

        /// <summary>
        /// Belirtilen id'ye sahip hesabın, AccountEditDto'da istenen özelliklerini günceller
        /// </summary>
        /// <param name="id">Güncellenecek hesabın id'si</param>
        /// <param name="accountEditDto">Güncellenecek hesap girdileri</param>
        /// <returns>Güncellenmiş hesap bilgileri</returns>
        /// <exception cref="ArgumentException">belirtilen id'ye ait hep bulunamdığında</exception>
        public async Task<AccountEditDto> EditAccount(int id, AccountEditDto accountEditDto)
        {
            var account = await _accountRepository.GetByIdAsync(id);
            if (account == null)
                throw new ArgumentException($"{id} numaralı hesap bulunmamaktadır.");

            _mapper.Map(accountEditDto, account);

            AccountEditDtoValidation validationRules = new AccountEditDtoValidation();
            validationRules.ValidateAndThrow(accountEditDto);

            await _accountRepository.UpdateAsync(account);

            var updatedAccountDto = _mapper.Map<AccountEditDto>(account);
            return updatedAccountDto;
        }

        /// <summary>
        ///  Belirtilen id'ye sahip hesabı siler
        /// </summary>
        /// <param name="id">silinecek hesap id'si</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">belirtilen id'ye sahip hesap yoksa<</exception>
        public async Task DeleteAccount(int id)
        {
            var account = await _accountRepository.GetByIdAsync(id);
            if (account == null)
                throw new ArgumentException($"{id} numaralı hesap bulunmamaktadır.");

            await _accountRepository.DeleteAsync(id);
        }

    }
}
