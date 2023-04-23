using ExpenseTracker.Case.CoreLayer.DTOs.Account;
using ExpenseTracker.Case.CoreLayer.Entities;
using ExpenseTracker.Case.CoreLayer.Interfaces.Services.Account;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.Case.WebAPI.Controllers
{
    [Route("api/accounts")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountManager;
        public AccountController(IAccountService accountManager)
        {
            _accountManager = accountManager;
        }

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
