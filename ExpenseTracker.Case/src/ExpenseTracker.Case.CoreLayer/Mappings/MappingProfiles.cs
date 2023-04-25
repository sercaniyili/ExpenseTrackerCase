using AutoMapper;
using ExpenseTracker.Case.CoreLayer.DTOs.Account;
using ExpenseTracker.Case.CoreLayer.DTOs.Transaction;
using ExpenseTracker.Case.CoreLayer.DTOs.User;
using ExpenseTracker.Case.CoreLayer.Entities;
using ExpenseTracker.Case.CoreLayer.Entities.Enums;
using ExpenseTracker.Case.CoreLayer.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Case.CoreLayer.Mappings
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            //user
            CreateMap<AppUser, UserRegisterDto>().ReverseMap();
            CreateMap<AppUser, UserLoginDto>().ReverseMap();

            //account
            CreateMap<Account, AccountListDto>()
                .ForMember(dest => dest.Currency, opt => opt.MapFrom(src => src.Currency.ToString()))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.AppUser.UserName))
                .ReverseMap();

            CreateMap<Account, AccountCreateDto>().ReverseMap();
            CreateMap<Account, AccountEditDto>().ReverseMap();

            //transaction
            CreateMap<Transaction, TransactionListDto>()
                  .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.ToString()))
                   .ForMember(dest => dest.AmountWithCurrency, opt => opt.MapFrom(src => $"{src.Amount} {src.Account.Currency}"));

            CreateMap<Transaction, TransactionCreateDto>()
                .ReverseMap();

            CreateMap<TransactionSearchDto, Transaction>()
                 .ForMember(dest => dest.Amount, opt => opt.Ignore())
                 .ForMember(dest => dest.Category, opt => opt.MapFrom(src => Enum.Parse<Category>(src.Category)));



        }
    }
}
