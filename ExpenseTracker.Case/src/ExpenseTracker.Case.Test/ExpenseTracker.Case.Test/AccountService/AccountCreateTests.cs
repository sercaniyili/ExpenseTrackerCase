using AutoMapper;
using ExpenseTracker.Case.BusinessLayer.Managers;
using ExpenseTracker.Case.BusinessLayer.Validations.Account;
using ExpenseTracker.Case.CoreLayer.DTOs.Account;
using ExpenseTracker.Case.CoreLayer.Entities;
using ExpenseTracker.Case.CoreLayer.Entities.Enums;
using ExpenseTracker.Case.CoreLayer.Interfaces.Repositories;
using ExpenseTracker.Case.CoreLayer.Mappings;
using FluentAssertions;
using FluentValidation.TestHelper;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Case.Test.Account
{
    public class AccountCreateTests
    {
        private readonly Mock<IAccountRepository> _mockAccountRepository;
        private readonly Mock<ITransactionRepository> _mockTransationRepository;
        private readonly IMapper _mapper;

        public AccountCreateTests()
        {
            _mockAccountRepository = new();
            _mockTransationRepository = new();
            var mapperConfig = new MapperConfiguration(c => { c.AddProfile(new MappingProfiles()); });
            _mapper = mapperConfig.CreateMapper();
        }

        [Fact]
        public async Task CreateAccount_Validations_ReturnFails()
        {
            //Arrange
            var accountCreateDto = new AccountCreateDto
            {
                AppUserId = 0,
                Name = "a",
                Currency = 0,
                Description = "xpArXfqKOeShlNQVOwEcRrELRirIjrQpkwZfXEUjOXPwxdAJoBUjNMvarTNOKXtZMErmLiUjzBgBMJzlrvQ"
            };

            //Act
            AccountCreateDtoValidation validationRules = new AccountCreateDtoValidation();
            var result = validationRules.TestValidate(accountCreateDto);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.AppUserId);
            result.ShouldHaveValidationErrorFor(x => x.Name);
            result.ShouldHaveValidationErrorFor(x => x.Currency);
            result.ShouldHaveValidationErrorFor(x => x.Description);
        }

        [Fact]
        public async Task CreateAccount_CreateAsyncExecuted_ReturnsCreatedAccountDto()
        {

            //Arrange

            var accountCreateDto = new AccountCreateDto
            {
                AppUserId = 1,
                Name = "Lorem",
                Currency = CoreLayer.Entities.Enums.Currency.USD,
                Description = "Lorem ipsum"
            };

            var account = _mapper.Map<CoreLayer.Entities.Account>(accountCreateDto);

            _mockAccountRepository.Setup(x => x.CreateAsync(It.IsAny<CoreLayer.Entities.Account>()))
                .ReturnsAsync((CoreLayer.Entities.Account createdAccount) => createdAccount);

           var accountManager = new AccountManager(_mockAccountRepository.Object, _mockTransationRepository.Object, _mapper);

            //Act

            var result = await accountManager.CreateAccount(accountCreateDto);

            //Assert

            _mockAccountRepository.Verify(x => x.CreateAsync(It.IsAny<CoreLayer.Entities.Account>()), Times.Once);

            result.Should().NotBeNull();
            result.Should().BeOfType<AccountCreateDto>();
            result.Name.Should().Be(accountCreateDto.Name);
            result.Currency.Should().Be(accountCreateDto.Currency);
            result.Description.Should().Be(accountCreateDto.Description);
            result.AppUserId.Should().Be(accountCreateDto.AppUserId);   
        }

    }
}
