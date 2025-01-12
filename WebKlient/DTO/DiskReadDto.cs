using System.ComponentModel.DataAnnotations;

namespace WebKlient.DTO
{
    public class DiskReadDto
    {
        public int DiskID { get; set; }
        public required string Type { get; set; }
        public int Capacity { get; set; }
        public required string Path { get; set; }
        public required string SerialNumber { get; set; }
        public required string Manufacturer { get; set; }
    }
}

