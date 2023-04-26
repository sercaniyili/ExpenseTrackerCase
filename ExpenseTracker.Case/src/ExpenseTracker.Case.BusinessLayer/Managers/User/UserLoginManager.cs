using AutoMapper;
using ExpenseTracker.Case.CoreLayer.DTOs.User;
using ExpenseTracker.Case.CoreLayer.Entities.Identity;
using ExpenseTracker.Case.CoreLayer.Interfaces.Services.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Case.BusinessLayer.Managers.User
{
    public class UserLoginManager : IUserLoginService
    {
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly IConfiguration _configuration;

        public UserLoginManager(IMapper mapper, UserManager<AppUser> userManager, IConfiguration configuration)
        {
            _mapper = mapper;
            _userManager = userManager;
             _configuration = configuration;
        }

        /// <summary>
        /// Verilen kullanıcı bilgileri kullancı girişi sağlanır
        /// </summary>
        /// <param name="userLoginDto">kullanıcı bilgileri</param>
        /// <returns>Kullancının giriş yaptığını gösteren bir JWT ve kullanıcı ad-eposta içeren dto</returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task<UserLoginResponseDto> LoginAsync(UserLoginDto userLoginDto)
        {
            var user = await _userManager.FindByEmailAsync(userLoginDto.Email);

            if (user == null || !await _userManager.CheckPasswordAsync(user, userLoginDto.Password))
                throw new ArgumentException("Email adresi veya şifre hatalı.");

            var token = await GenerateJwtTokenAsync(user);

           return new UserLoginResponseDto
            {
                Token = token,
                Username = user.UserName,
                Email = user.Email
            };          
        }

        /// <summary>
        /// Verilen kullanıcı için bir jwt token oluşturulur
        /// </summary>
        /// <param name="user">jwt oluşturulacak kullanıcı </param>
        /// <returns>oluşturulan JWT</returns>
        private async Task<string> GenerateJwtTokenAsync(AppUser user)
        {
            var claims = new List<Claim>
              {
                 new Claim(ClaimTypes.Name,user.UserName),
                 new Claim(ClaimTypes.Email,user.Email),
                 new Claim(JwtRegisteredClaimNames.Sub,user.Id.ToString()),
                 new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
             };

            var roles =  await _userManager.GetRolesAsync(user);

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
     

            var token = new JwtSecurityToken(
                issuer :  _configuration["Jwt:ValidIssuer"],
                audience :  _configuration["Jwt:ValidAudience"],
                claims:claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


    }
}
