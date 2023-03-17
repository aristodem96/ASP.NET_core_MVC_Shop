using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FatherShopImperiyaOdyagu.Models
{
    public class Product
    {
        [Key] 
        public int Id { get; set; }
        public DateTime? CreatedDate { get; set; } = DateTime.Now;
        public string? Photo { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        [ForeignKey("FK_001")]
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
    }
}
