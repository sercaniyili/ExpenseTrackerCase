using AutoMapper;
using ExpenseTracker.Case.BusinessLayer.Managers;
using ExpenseTracker.Case.CoreLayer.DTOs.Account;
using ExpenseTracker.Case.CoreLayer.DTOs.Transaction;
using ExpenseTracker.Case.CoreLayer.Interfaces.Services.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace ExpenseTracker.Case.WebAPI.Controllers
{
    [Route("api/transactions")]
    [Authorize(Roles = "user")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionManager;
        private readonly IMapper _mapper;
        public TransactionController(ITransactionService transactionManager, IMapper mapper)
        {
            _transactionManager = transactionManager;
            _mapper = mapper;
        }


        /// <summary>
        ///  Tüm işlemleri listeler
        /// </summary>
        /// <returns>işlemlerin dto'da istenen bölümlerini</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TransactionListDto>>> GetTransactions()
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

        /// <summary>
        /// Belirtilen girdilere göre yeni işlem oluşturur
        /// </summary>
        /// <param name="transactionCreateDto">belirtilen girdiler</param>
        /// <returns>oluşturulan işlemin dto'da istenen bölümlerini</returns>
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

        /// <summary>
        /// id'si belirtilen işlemi siler
        /// </summary>
        /// <param name="id">belirtilen id</param>
        /// <returns></returns>
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

        /// <summary>
        /// Dto'da belirlilen arama kritelerine göre işlemleri listeler
        /// </summary>
        /// <param name="TransactionSearchDto">arama kriterleri</param>
        /// <returns>filtlenmiş işlemler</returns>
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
