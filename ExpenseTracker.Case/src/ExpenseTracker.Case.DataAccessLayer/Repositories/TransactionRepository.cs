﻿using AutoMapper;
using ExpenseTracker.Case.CoreLayer.DTOs.Transaction;
using ExpenseTracker.Case.CoreLayer.Entities;
using ExpenseTracker.Case.CoreLayer.Interfaces.Repositories;
using ExpenseTracker.Case.DataAccessLayer.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Case.DataAccessLayer.Repositories
{
    public class TransactionRepository : BaseRepository<Transaction>,ITransactionRepository
    {
        protected readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;
        public TransactionRepository(AppDbContext appDbContext, IMapper mapper) : base(appDbContext)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<Account> FindAccountById(int id)
        {        
            return await _appDbContext.Accounts.FindAsync(id);
        }

        public async Task<int> SaveTransactionChangesAsync()
        {
            return await _appDbContext.SaveChangesAsync();
        }

    }
}
