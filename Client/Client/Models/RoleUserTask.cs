using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Client.Models
{
    public class RoleUserTask
    {
        [Key]
        [JsonPropertyName("RoleUserTaskId")]
        public int RoleUserTaskId { get; set; }
        public RoleUser RoleUser { get; set; }
        [ForeignKey("RoleUser")]
        [JsonPropertyName("RoleUserId")]
        public int RoleUserId { get; set; }
    }
}
