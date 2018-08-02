
using System.ComponentModel.DataAnnotations;

namespace PhoneShopAPI.ViewModels {
    public class PhoneViewModel
    {
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; }
        
        [Required]
        public string Description { get; set; }

        public override string ToString()
        {
            return $"Id: {this.Id}, Name: {this.Name}, Description: {this.Description}";
        }
    }
}