using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Case.CoreLayer.DTOs.User
{
    public class UserLoginResponseDto
    {
        public string Email { get; set; }
        public string Username { get; set; }
        public string Token { get; set; }
    }
}

