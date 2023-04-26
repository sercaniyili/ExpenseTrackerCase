using AutoMapper;
using ExpenseTracker.Case.CoreLayer.DTOs.Account;
using ExpenseTracker.Case.CoreLayer.DTOs.Transaction;
using ExpenseTracker.Case.CoreLayer.Entities;
using ExpenseTracker.Case.CoreLayer.Interfaces.Services.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace ExpenseTracker.Case.WebAPI.Controllers
{
    [Route("api/accounts")]
    //[Authorize(Roles = "user")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountManager;
        private readonly IMapper _mapper;
        public AccountController(IAccountService accountManager, IMapper mapper)
        {
            _accountManager = accountManager;
            _mapper = mapper;
        }

        /// <summary>
        ///  Tüm hesapları listeler
        /// </summary>
        /// <returns>hesapların dto'da istenen bölümlerini</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AccountListDto>>> GetAccounts()
        {
            try
            {
                var accounts = await _accountManager.GetAllAccounts();
                return Ok(accounts);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Belirtilen hesap id'sine ait işlemleri
        /// </summary>
        /// <param name="id">hesap id'si</param>
        /// <returns>işlemlerin dto'da istenen bölümlerini</returns>
        [HttpGet("{id}/transactions")]
        public async Task<ActionResult<IEnumerable<TransactionListDto>>> GetTransactionsByAccountId(int id)
        {
            try
            {
                var transactions = await _accountManager.GetTransactionsByAccountIdAsync(id);
                var transactionDtos = _mapper.Map<IEnumerable<TransactionListDto>>(transactions);
                return Ok(transactionDtos);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Belirtilen girdilere göre yeni hesap oluşturur
        /// </summary>
        /// <param name="accountCreateDto">belirtilen girdiler</param>
        /// <returns>oluşturulan hesabın dto'da istenen bölümlerini</returns>
        [HttpPost]
        public async Task<ActionResult<AccountCreateDto>> CreateAccount([FromBody]AccountCreateDto  accountCreateDto)
        {
            try
            {
                var account = await _accountManager.CreateAccount(accountCreateDto);
                return Ok(account);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Belirtilen id'ye sahip hesabın dto'da belirtilen bölümlerini günceller
        /// </summary>
        /// <param name="id">belirtilen hesap id'si</param>
        /// <param name="accountEditDto">güncellecek hesap bölümleri</param>
        /// <returns>güncellenen hesabın dto'da istenen bölümlerini</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<AccountEditDto>> UpdateAccount(int id, [FromBody] AccountEditDto  accountEditDto)
        {
            try
            {
                var updatedAccountDto = await _accountManager.EditAccount(id, accountEditDto);
                return Ok(updatedAccountDto);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// id'si belirtilen hesabı siler
        /// </summary>
        /// <param name="id">belirtilen id</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccount(int id)
        {
            try
            {
                await _accountManager.DeleteAccount(id);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }

    }
}
