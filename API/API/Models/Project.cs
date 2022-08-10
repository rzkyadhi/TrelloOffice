using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class Project
    {
        [Key]
        public int ProjectId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
