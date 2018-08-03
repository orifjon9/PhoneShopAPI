
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PhoneShopAPI.Models;
using PhoneShopAPI.Services.Interfaces;

namespace PhoneShopAPI.Controllers
{
    [Route("api/auth")]
    public class AuthController
    {
        private readonly ILoginService _service;

        public AuthController(ILoginService service)
        {
            _service = service;
        }

        [HttpPost("sign-in")]
        [AllowAnonymous]
        public async Task<ActionResult<UserWithToken>> SignIn([FromBody] LoginViewModel loginViewModel)
        {
            return await _service.AuthenticateAsync(loginViewModel);
        }
    }
}