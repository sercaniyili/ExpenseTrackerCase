using AutoMapper;
using ExpenseTracker.Case.BusinessLayer.Managers;
using ExpenseTracker.Case.BusinessLayer.Validations.Account;
using ExpenseTracker.Case.CoreLayer.DTOs.Account;
using ExpenseTracker.Case.CoreLayer.Interfaces.Repositories;
using ExpenseTracker.Case.CoreLayer.Mappings;
using FluentAssertions;
using FluentValidation.TestHelper;
using Moq;

namespace ExpenseTracker.Case.Test.AccountService
{
    public class AccountEditTests
    {
        private readonly Mock<IAccountRepository> _mockAccountRepository;
        private readonly Mock<ITransactionRepository>  _mockTransationRepository;
        private readonly IMapper _mapper;

        public AccountEditTests()
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
            var accountEditDto = new AccountEditDto
            {
                Name = "a",
                Currency = 0,
                Description = "xpArXfqKOeShlNQVOwEcRrELRirIjrQpkwZfXEUjOXPwxdAJoBUjNMvarTNOKXtZMErmLiUjzBgBMJzlrvQ"
            };

            //Act
            AccountEditDtoValidation validationRules = new AccountEditDtoValidation();
            var result = validationRules.TestValidate(accountEditDto);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.Name);
            result.ShouldHaveValidationErrorFor(x => x.Currency);
            result.ShouldHaveValidationErrorFor(x => x.Description);
        }




        [Fact]
        public async Task EditAccount_AccountIdDoesNotExist_ReturnsArgumentException()
        {
            //Arrange

            int nonExistentId = 12345;

            var accountEditDto = new AccountEditDto
            {
                Name = "Lorem",
                Currency = CoreLayer.Entities.Enums.Currency.USD,
                Description = "Lorem ipsum"
            };

            _mockAccountRepository.Setup(x => x.GetByIdAsync(nonExistentId)).ReturnsAsync((CoreLayer.Entities.Account)null);

            var accountManager = new AccountManager(_mockAccountRepository.Object, _mockTransationRepository.Object, _mapper);

            //Act
            //Assert

            await accountManager
                      .Invoking(x => x.EditAccount(nonExistentId, accountEditDto))
                      .Should()
                      .ThrowAsync<ArgumentException>()
                      .WithMessage($"{nonExistentId} numaralı hesap bulunmamaktadır.");
        }


        [Fact]
        public async Task EditAccount_UpdateAsyncExecuted_ReturnsEditedAccountDto()
        {

            //Arrange

            int id = 1;

            var accountEditDto = new AccountEditDto
            {
                Name = "Lorem",
                Currency = CoreLayer.Entities.Enums.Currency.USD,
                Description = "Lorem ipsum"
            };

            var account = _mapper.Map<CoreLayer.Entities.Account>(accountEditDto);

            _mockAccountRepository.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(account);

            _mockAccountRepository.Setup(x => x.UpdateAsync(It.IsAny<CoreLayer.Entities.Account>()))
               .ReturnsAsync((CoreLayer.Entities.Account editedAccount) => editedAccount);

            var accountManager = new AccountManager(_mockAccountRepository.Object, _mockTransationRepository.Object, _mapper);

            //Act

            var result = await accountManager.EditAccount(id, accountEditDto);


            //Assert

            _mockAccountRepository.Verify(x => x.UpdateAsync(It.IsAny<CoreLayer.Entities.Account>()), Times.Once);

            result.Should().NotBeNull();
            result.Should().BeOfType<AccountEditDto>();
            result.Name.Should().Be(accountEditDto.Name);
            result.Currency.Should().Be(accountEditDto.Currency);
            result.Description.Should().Be(accountEditDto.Description);
        }

    }
}
