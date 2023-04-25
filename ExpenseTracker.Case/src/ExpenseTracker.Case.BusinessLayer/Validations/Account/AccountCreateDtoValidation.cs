using ExpenseTracker.Case.CoreLayer.DTOs.Account;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Case.BusinessLayer.Validations.Account
{
    public class AccountCreateDtoValidation : AbstractValidator<AccountCreateDto>
    {
        public AccountCreateDtoValidation()
        {
            RuleFor(x => x.Name)
                .Length(2, 30).WithMessage("Başlık geçerli uzunlukta değil")
                .NotEmpty().WithMessage("Başlık boş geçilemez");
            RuleFor(x => x.Description)
                .MaximumLength(50).WithMessage("Açıklama 50 karakterden uzun olamaz");
            RuleFor(x => x.AppUserId)
                .NotEmpty().WithMessage("Kullanıcı Id boş geçilemez");
            RuleFor(x => x.Currency)
                .NotEmpty().WithMessage("Birim boş geçilemez");
        }
    }
}
