using System.ComponentModel.DataAnnotations;

namespace WebKlient.DTO
{
    public class WipeReportReadDto
    {
        public int WipeJobId { get; set; }

        public DateTime StartTime { get; set; }

        // Nullable EndTime - add logic in API to handle the cases
        public DateTime? EndTime { get; set; }

        // Optional status
        [MaxLength(50, ErrorMessage = "Status cannot exceed 50 characters.")]
        public string? Status { get; set; }

        // Nullable fields
        [MaxLength(50, ErrorMessage = "DiskType cannot exceed 50 characters.")]
        public string? DiskType { get; set; }

        public int Capacity { get; set; }

        [MaxLength(18, ErrorMessage = "SerialNumber cannot exceed 18 characters.")]
        public string? SerialNumber { get; set; }

        [MaxLength(24, ErrorMessage = "Manufacturer cannot exceed 24 characters.")]
        public string? Manufacturer { get; set; }

        [MaxLength(100, ErrorMessage = "WipeMethodName cannot exceed 100 characters.")]
        public string? WipeMethodName { get; set; }

        [Range(1, 100, ErrorMessage = "OverwritePasses must be between 1 and 100.")]
        public int OverwritePasses { get; set; }

        [MaxLength(50, ErrorMessage = "PerformedBy cannot exceed 50 characters.")]
        public string? PerformedBy { get; set; }
    }
}
