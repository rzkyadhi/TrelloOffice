using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    public class TaskUser
    {
        [Key]
        public int TaskUserId { get; set; }

        public Task Task { get; set; }
        [Required]
        [ForeignKey("TaskId")]
        public int TaskId { get; set; }

        public User User { get; set; }
        [Required]
        [ForeignKey("UserId")]
        public int UserId { get; set; }

    }
}
