using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Client.Models
{
    public class User
    {
        public Employee Employee { get; set; }
        [Key]
        [ForeignKey("Employee")]
        [JsonPropertyName("UserId")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Username Can't Empty, Try Again")]
        [JsonPropertyName("Username")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Email Can't Empty, Try Again")]
        [EmailAddress]
        [JsonPropertyName("Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password Can't Empty, Try Again")]
        [DataType(DataType.Password)]
        [JsonPropertyName("Password")]
        public string Password { get; set; }
    }
}
