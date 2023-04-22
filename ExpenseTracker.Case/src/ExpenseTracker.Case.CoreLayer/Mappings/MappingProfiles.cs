using AutoMapper;
using ExpenseTracker.Case.CoreLayer.DTOs.User;
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
            CreateMap<AppUser, UserRegisterDto>().ReverseMap();
            CreateMap<AppUser, UserLoginDto>().ReverseMap();

        }
    }
}
