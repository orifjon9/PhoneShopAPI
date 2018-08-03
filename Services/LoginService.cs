
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using PhoneShopAPI.Models;
using PhoneShopAPI.Services.Interfaces;

namespace PhoneShopAPI.Services
{
    public class LoginService : ILoginService
    {
        public async Task<UserWithToken> AuthenticateAsync(LoginViewModel loginViewModel)
        {
            if ("admin".Equals(loginViewModel.UserName) &&
            "admin".Equals(loginViewModel.Password))
            {
                var task = Task.Run(() =>
                    new UserWithToken
                    {
                        User = new UserViewModel()
                        {
                            UserName = "Orifjon Narkulov",
                            Email = "orifjon9@gmail.com"
                        },
                        Token = BuildToken()
                    });
                return await task;
            }

            return null;
        }

        private string BuildToken()
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("veryVerySecretKey"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken("http://localhost:5000/", "http://localhost:5000/",
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}