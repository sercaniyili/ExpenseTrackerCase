using ExpenseTracker.Case.CoreLayer.DTOs.User;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Case.CoreLayer.Interfaces.Services.User
{
    public interface IUserRegisterService
    {
        Task<IdentityResult> RegisterAsync(UserRegisterDto userRegisterDto);
    }
}
