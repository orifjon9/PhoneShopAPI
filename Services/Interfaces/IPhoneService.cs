
using System.Collections.Generic;
using System.Threading.Tasks;
using PhoneShopAPI.Models;

namespace PhoneShopAPI.Services.Interfaces {
    
    public interface IPhoneService
    {
        IEnumerable<Phone> ListPhones();
        Task<Phone> GetPhoneAsync(int id);

        Task<bool> CreatePhoneItemAsync(Phone phoneToCreate);
        Task<bool> UpdatePhoneItem(Phone srcPhone, Phone phoneToUpdate);
        Task<bool> DeletePhoneItem(Phone phoneToDelete);

        bool ValidatePhone(Phone phoneToValidate);
    }
}