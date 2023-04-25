using ExpenseTracker.Case.BusinessLayer.Managers;
using ExpenseTracker.Case.CoreLayer.DTOs.Account;
using ExpenseTracker.Case.CoreLayer.DTOs.Transaction;
using ExpenseTracker.Case.CoreLayer.Interfaces.Services.Account;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.Case.WebAPI.Controllers
{
    [Route("api/transactions")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionManager;

        public TransactionController(ITransactionService transactionManager)
        {
            _transactionManager = transactionManager;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<TransactionListDto>>> GetAccounts()
        {
            try
            {
                var transactions = await _transactionManager.GetAllTransactions();
                return Ok(transactions);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<TransactionCreateDto>> CreateTransaction([FromBody] TransactionCreateDto transactionCreateDto)
        {
            try
            {
                var transactions = await _transactionManager.CreateTransaction(transactionCreateDto);
                return Ok(transactions);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransaciton(int id)
        {
            try
            {
                await _transactionManager.DeleteTransaction(id);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }


        [HttpGet("search")]
        public async Task<IActionResult> GetTransactions([FromQuery] TransactionSearchDto TransactionSearchDto)
        {
            try
            {
                var transactions = await _transactionManager.SearchTransactions(TransactionSearchDto);
                return Ok(transactions);

            }
            catch (ArgumentException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

    }
}
