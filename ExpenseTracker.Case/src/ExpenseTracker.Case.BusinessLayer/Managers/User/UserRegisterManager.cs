using AutoMapper;
using ExpenseTracker.Case.CoreLayer.DTOs.User;
using ExpenseTracker.Case.CoreLayer.Entities.Identity;
using ExpenseTracker.Case.CoreLayer.Interfaces.Services.User;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Case.BusinessLayer.Managers.User
{
    public class UserRegisterManager : IUserRegisterService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole<int>> _roleManager;
        private readonly IMapper _mapper;

        public UserRegisterManager(UserManager<AppUser> userManager, IMapper mapper, RoleManager<IdentityRole<int>> roleManager)
        {
            _userManager = userManager;
            _mapper = mapper;
            _roleManager = roleManager;
        }

        public async Task<IdentityResult> RegisterAsync(UserRegisterDto userRegisterDto)
        {
            var user =await _userManager.FindByEmailAsync(userRegisterDto.Email);
            if (user is not null)
                throw new InvalidOperationException("Kullanıcı zaten kayıtlı.");

            var newUser = _mapper.Map<AppUser>(userRegisterDto);

            var result = await _userManager.CreateAsync(newUser, userRegisterDto.Password);
            if(!result.Succeeded)
                throw new InvalidOperationException("Kullanıcı oluşturulurken bir hata oluştu.");

            if(!await _roleManager.RoleExistsAsync("user"))
            {
                var role = new IdentityRole<int>("user");
                await _roleManager.CreateAsync(role);
            }

            await _userManager.AddToRoleAsync(newUser, "user");

            return result;
        }
    }
}
