using System.ComponentModel.DataAnnotations;

namespace E_Commerce.ViewModel
{
    public class LoginVM
    {
        [EmailAddress]
        public string? Email { get; set; }

        public string? Password { get; set; }
    }
}
