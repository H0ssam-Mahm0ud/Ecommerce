using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commerce.Models
{
    public class ShoppingCart
    {
        [Key]
        public int CartId { get; set; }

        public int ProId { get; set; }
        [ForeignKey("ProId")]
        public virtual Product Product { get; set; }

        [Range(0, int.MaxValue)]
        public int Quantity { get; set; }

        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
