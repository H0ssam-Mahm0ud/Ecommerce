using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commerce.Models
{
    public class Product
    {
        [Key]
        public int ProId { get; set; }

        [Required]
        public string? ProName { get; set; }

        [Required]
        public string? Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        public string ProImage { get; set; }

        [NotMapped]
        public IFormFile File { get; set; }

        public int CatId { get; set; }
        [ForeignKey(nameof(CatId))]

        public virtual Category Category { get; set; }
    }
}
