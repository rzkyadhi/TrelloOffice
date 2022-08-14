using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Client.Models
{
    public class RoleUser
    {
        [Key]
        [JsonPropertyName("RoleUserId")]
        public int RoleUserId { get; set; }

        public User User { get; set; }
        [ForeignKey("User")]
        [JsonPropertyName("UserId")]
        public int UserId { get; set; }

        public Role Role { get; set; }
        [ForeignKey("Role")]
        [JsonPropertyName("RoleId")]
        public int RoleId { get; set; }
    }
}
