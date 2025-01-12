using System.ComponentModel.DataAnnotations;

namespace WebKlient.DTO
{
    public class DiskReadDto
    {
        // DiskID bruges kun til at vise data og ikke til input, så det er ikke nødvendigt at gøre det required
        public int DiskID { get; set; }

        // Type, Path, SerialNumber og Manufacturer bør være required, da de er vigtige informationer.
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
