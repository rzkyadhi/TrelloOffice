using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    public class RoleUser
    {
        [Key]
        public int RoleUserId { get; set; }
        public User User { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }

        public Role Role { get; set; }
        [ForeignKey("Role")]
        public int RoleId { get; set; }
    }
}
