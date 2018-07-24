using Microsoft.EntityFrameworkCore;

namespace PhoneShopAPI.Models
{
    public class PhoneContext : DbContext
    {
        public PhoneContext(DbContextOptions<PhoneContext> options) : base(options)
        {
        }

        public DbSet<Phone> PhoneItems { get; set; }
    }
}