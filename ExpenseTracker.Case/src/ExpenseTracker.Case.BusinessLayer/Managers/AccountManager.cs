using AutoMapper;
using ExpenseTracker.Case.CoreLayer.DTOs.Account;
using ExpenseTracker.Case.CoreLayer.Entities;
using ExpenseTracker.Case.CoreLayer.Interfaces.Repositories;
using ExpenseTracker.Case.CoreLayer.Interfaces.Services.Account;
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
        private readonly IMapper _mapper;

        public AccountManager(IAccountRepository accountRepository, IMapper mapper)
        {
            _accountRepository = accountRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AccountListDto>> GetAllAccounts()
        {
            var accounts = _accountRepository.GetAllAsyncQueryable()
                .Include(c => c.AppUser);
            var mappedAccounts = _mapper.Map<IEnumerable<AccountListDto>>(accounts);
            return mappedAccounts;
        }
        public async Task<AccountCreateDto> CreateAccount(AccountCreateDto accountCreateDto)
        {
            var account = _mapper.Map<Account>(accountCreateDto);
            var mappedAccount = await _accountRepository.CreateAsync(account);
            var result = _mapper.Map<AccountCreateDto>(mappedAccount);
            return result;
        }

        public async Task<AccountEditDto> EditAccount(int id, AccountEditDto accountEditDto)
        {
            var account = await _accountRepository.GetByIdAsync(id);
            if (account == null)
                throw new ArgumentException($"{id} numaralı hesap bulunmamaktadır.");

            _mapper.Map(accountEditDto, account);
            await _accountRepository.UpdateAsync(account);

            var updatedAccountDto = _mapper.Map<AccountEditDto>(account);
            return updatedAccountDto;
        }

        public async Task DeleteAccount(int id)
        {
            var account = await _accountRepository.GetByIdAsync(id);
            if (account == null)
                throw new ArgumentException($"Account with id {id} does not exist.");

            await _accountRepository.DeleteAsync(id);
        }
    }
}
