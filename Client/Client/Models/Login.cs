using System.ComponentModel.DataAnnotations;

namespace Client.Models
{
    public class Login
    {
        [Required(ErrorMessage = "Email Can't Empty, Try Again !")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password Can't Empty, Try Again !")]
        [StringLength(25)]
        [MinLength(6, ErrorMessage = "Minimum Password Character is 6")]
        public string Password { get; set; }
    }
}
