using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        public string BirthDate { get; set; }
        [Required]
        public Gender Gender { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        public string HireDate { get; set; }
    }
    public enum Gender
    {
        Male,
        Female
    }
}
