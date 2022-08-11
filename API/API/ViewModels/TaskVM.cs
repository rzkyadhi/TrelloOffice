using System;
using System.ComponentModel.DataAnnotations;

namespace API.ViewModels
{
    public class TaskVM
    {
        [Required]
        public int TaskId { get; set; }
        [Required]
        public int RoleUserTaskId { get; set; }
        [Required]
        public int ProjectId { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime DueDate { get; set; }
        [Required]
        public bool IsCompleted { get; set; }
    }
}
