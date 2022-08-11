using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Client.Models
{
    public class Project
    {
        [Key]
        [JsonPropertyName("ProjectId")]
        public int ProjectId { get; set; }

        [Required(ErrorMessage = "Nama Project Harus Diisi !")]
        [StringLength(50)]
        [JsonPropertyName("Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Deskripsi Project Harus Diisi !")]
        [JsonPropertyName("Description")]
        public string Description { get; set; }
    }
}
