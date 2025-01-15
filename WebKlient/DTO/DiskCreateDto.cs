using System.ComponentModel.DataAnnotations;

namespace WebKlient.DTO
{
    public class DiskCreateDto
    {
        [Required(ErrorMessage = "Type is required.")]
        [StringLength(50, ErrorMessage = "Type cannot exceed 50 characters.")]
        public string Type { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Capacity must be greater than 0.")]
        public int Capacity { get; set; }

        [Required(ErrorMessage = "Path is required.")]
        [MaxLength(8)]
        public string Path { get; set; }

        [Required(ErrorMessage = "Serial number is required.")]
        [MaxLength(18)]
        public string SerialNumber { get; set; }

        [Required(ErrorMessage = "Manufacturer is required.")]
        [MaxLength(24)]
        public string Manufacturer { get; set; }
    }

}
