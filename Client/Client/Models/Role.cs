using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Client.Models
{
    public class Role
    {
        [Key]
        [JsonPropertyName("RoleId")]
        public int RoleId { get; set; }

        [Required(ErrorMessage = "Role Name Can't Empty, Try Again")]
        [JsonPropertyName("Name")]
        public string Name { get; set; }
    }
}
