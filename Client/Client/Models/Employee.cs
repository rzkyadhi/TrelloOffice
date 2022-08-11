using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Client.Models
{
    public class Employee
    {
        [Key]
        [JsonPropertyName("EmployeeID")]
        public int EmployeeID { get; set; }
        [Required]
        [JsonPropertyName("Name")]
        public string Name { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        [JsonPropertyName("BirthDate")]
        public string BirthDate { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        [JsonPropertyName("HireDate")]
        public string HireDate { get; set; }
    }
}
