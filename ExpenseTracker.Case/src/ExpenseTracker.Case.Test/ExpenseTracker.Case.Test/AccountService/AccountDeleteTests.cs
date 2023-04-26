using AutoMapper;
using ExpenseTracker.Case.BusinessLayer.Managers;
using ExpenseTracker.Case.CoreLayer.DTOs.Account;
using ExpenseTracker.Case.CoreLayer.Entities;
using ExpenseTracker.Case.CoreLayer.Interfaces.Repositories;
using ExpenseTracker.Case.CoreLayer.Mappings;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Case.Test.AccountService
{
    public class AccountDeleteTests
    {
        private readonly Mock<IAccountRepository> _mockAccountRepository;
        private readonly Mock<ITransactionRepository> _mockTransationRepository;
        private readonly IMapper _mapper;

        public AccountDeleteTests()
        {
            _mockAccountRepository = new();
            _mockTransationRepository = new();
            var mapperConfig = new MapperConfiguration(c => { c.AddProfile(new MappingProfiles()); });
            _mapper = mapperConfig.CreateMapper();
        }
 

        [Fact]
        public async Task DeleteAccount_DeleteAsyncExecutedWithSucces_ReturnNothing()
        {
            //Arrange

            int id = 1;

            _mockAccountRepository.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(new CoreLayer.Entities.Account
            {
                Id = id
            });

            var accountManager = new AccountManager(_mockAccountRepository.Object, _mockTransationRepository.Object, _mapper);

            //Act

            var result =  accountManager.DeleteAccount(id);

            //Assert

            _mockAccountRepository.Verify(x => x.DeleteAsync(id), Times.Once);
        }

        [Fact]
        public async Task DeleteAccount_AccountIdDoesNotExist_ReturnsArgumentException()
        {
            //Arrange

            int nonExistentId = 12345;

            var accountCreateDto = new AccountCreateDto
            {
                AppUserId = 1,
                Name = "Lorem",
                Currency = CoreLayer.Entities.Enums.Currency.USD,
                Description = "Lorem ipsum"
            };

            _mockAccountRepository.Setup(x => x.GetByIdAsync(nonExistentId)).ReturnsAsync((CoreLayer.Entities.Account)null);

            var accountManager = new AccountManager(_mockAccountRepository.Object, _mockTransationRepository.Object, _mapper);

            //Act
            //Assert

            await accountManager
                      .Invoking(x => x.DeleteAccount(nonExistentId))
                      .Should()
                      .ThrowAsync<ArgumentException>()
                      .WithMessage($"{nonExistentId} numaralı hesap bulunmamaktadır.");
        }
    }
}
