using System.ComponentModel.DataAnnotations;

namespace WebKlient.DTO
{
    public class DiskReadDto
    {
        public int DiskID { get; set; }

        [Required]
        public string Type { get; set; }

        public int Capacity { get; set; }

        [Required]
        public string Path { get; set; }

        [Required]
        public string SerialNumber { get; set; }

        [Required]
        public string Manufacturer { get; set; }
    }
}
