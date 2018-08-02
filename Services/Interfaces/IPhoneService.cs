
using System.Collections.Generic;
using System.Threading.Tasks;
using PhoneShopAPI.Models;
using PhoneShopAPI.ViewModels;

namespace PhoneShopAPI.Services.Interfaces {
    
    public interface IPhoneService
    {
        IEnumerable<PhoneViewModel> ListPhones();
        Task<PhoneViewModel> GetPhoneAsync(int id);

        Task<bool> CreatePhoneItemAsync(PhoneViewModel phoneToCreate);
        Task<bool> UpdatePhoneItem(int id, PhoneViewModel phoneToUpdate);
        Task<bool> DeletePhoneItem(PhoneViewModel phoneToDelete);
    }
}