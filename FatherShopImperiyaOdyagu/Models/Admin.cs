using System.ComponentModel.DataAnnotations;

namespace FatherShopImperiyaOdyagu.Models
{
    public class Admin
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}
