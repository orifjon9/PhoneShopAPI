
using System.Threading.Tasks;
using PhoneShopAPI.Models;

namespace PhoneShopAPI.Services.Interfaces
{ 
    public interface ILoginService
    {
        Task<UserWithToken> AuthenticateAsync(LoginViewModel loginViewModel);

        // add register logic
    }
}