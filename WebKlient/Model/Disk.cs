using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebKlient.Model
{
    public class Disk
    {
        public int DiskID { get; set; }

        [Required(ErrorMessage = "Type is required.")]
        [StringLength(50, ErrorMessage = "Type cannot exceed 50 characters.")]
        public string Type { get; set; } = string.Empty;

        [Range(1, int.MaxValue, ErrorMessage = "Capacity must be greater than 0.")]
        public int Capacity { get; set; }

        [StringLength(5)]
        public string? Path { get; set; }

        [StringLength(18)]
        public string? SerialNumber { get; set; }

        [StringLength(50)]
        public string? Manufacturer { get; set; }

        [JsonIgnore] // Undgå serialization af WipeJobs i JSON
        public ICollection<WipeJob> WipeJobs { get; set; }

    }
}
