
using System.ComponentModel.DataAnnotations;

namespace PhoneShopAPI.Models
{
    public class UserViewModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Email { get; set; }
    }
}