using System.ComponentModel.DataAnnotations;

namespace E_Commerce.Models
{
    public class Contact
    {
        [Key]
        public int CoId { get; set; }

        public string? Name { get; set; }

        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        public string? Subject { get; set; }

        public string? Message { get; set; }
    }
}
