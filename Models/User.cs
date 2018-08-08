
using System.Collections.Generic;

namespace PhoneShopAPI.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public virtual IEnumerable<Role> Roles { get; set; }
    }
}