using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    public class User
    {
        public Employee Employee { get; set; }
        [Key]
        [ForeignKey("Employee")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Username Can't Empty, Try Again")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Email Can't Empty, Try Again")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password Can't Empty, Try Again")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
