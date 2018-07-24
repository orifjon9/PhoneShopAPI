
using System.ComponentModel.DataAnnotations;

namespace PhoneShopAPI.Models
{
    public class Phone
    {
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; }
        
        public string Description { get; set; }

        public override string ToString()
        {
            return $"Id: {this.Id}, Name: {this.Name}, Description: {this.Description}";
        }
    }
}