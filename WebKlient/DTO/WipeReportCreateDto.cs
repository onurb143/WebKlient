using System.ComponentModel.DataAnnotations;

namespace WebKlient.DTO
{
    public class WipeReportCreateDto
    {
        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public DateTime EndTime { get; set; }

        [MaxLength(100)]
        public string? Status { get; set; }

        [Required]
        [MaxLength(50)]
        public string SerialNumber { get; set; }

        [MaxLength(50)]
        public string? Manufacturer { get; set; }

        [Required]
        [MaxLength(50)]
        public string WipeMethodName { get; set; }

        [Range(1, 10)]
        public int OverwritePasses { get; set; }

        [Required]
        [MaxLength(50)]
        public string PerformedBy { get; set; }
    }
}
