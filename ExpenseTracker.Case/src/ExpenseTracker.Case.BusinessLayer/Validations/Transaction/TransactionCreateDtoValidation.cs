using ExpenseTracker.Case.CoreLayer.DTOs.Account;
using ExpenseTracker.Case.CoreLayer.DTOs.Transaction;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Case.BusinessLayer.Validations.Transaction
{
    public class TransactionCreateDtoValidation : AbstractValidator<TransactionCreateDto>
    {
        public TransactionCreateDtoValidation()
        {
            RuleFor(x => x.Amount)
             .NotEmpty().WithMessage("Miktar boş geçilemez");
            RuleFor(x => x.AccountId)
                 .NotEmpty().WithMessage("Hesap Id boş geçilemez");
            RuleFor(x => x.Category)
                .NotEmpty().WithMessage("Kategori boş geçilemez");
        }
    }
}
