using ExpenseTracker.Case.CoreLayer.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Case.CoreLayer.Interfaces.Services.User
{
    public interface IUserLoginService
    {
        Task<UserLoginResponseDto> LoginAsync(UserLoginDto userLoginDto);

    }
}
