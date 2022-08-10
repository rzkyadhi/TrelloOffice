using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    public class RoleUserTask
    {
        [Key]
        public int RoleUserTaskId { get; set; }
        public RoleUser RoleUser { get; set; }
        [ForeignKey("RoleUser")]
        public int RoleUserId { get; set; }
    }
}
