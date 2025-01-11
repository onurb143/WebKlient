using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace WebKlient.Model
{
    // Repræsenterer en rapport om en udført datasletning.
    public class WipeReport
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        // Primærnøgle for WipeReport. Fremmed nøgle til WipeJob.
        public int? WipeJobId { get; set; }

        // Tidsstempel for hvornår sletningsprocessen startede.
        public DateTime StartTime { get; set; }

        // Tidsstempel for hvornår sletningsprocessen sluttede.
        public DateTime EndTime { get; set; }

        // Status for slettejobbet, fx "Completed" eller "Failed".
        public string? Status { get; set; }

        // Disktypen, fx "SSD" eller "HDD".
        public string? DiskType { get; set; }

        // Diskens kapacitet i GB.
        public int Capacity { get; set; }

        // Diskens serienummer, bruges til unik identifikation.
        public string? SerialNumber { get; set; }

        // Navnet på producenten af disken, fx "Samsung" eller "Western Digital".
        public string? Manufacturer { get; set; }

        // Navnet på den anvendte slettemetode, fx "Secure Erase" eller "Zero Fill".
        public string WipeMethodName { get; set; }

        // Antallet af overskrivningspasser, der blev udført som en del af sletningsmetoden.
        public int OverwritePasses { get; set; }

        // Brugeren, der udførte sletningen.
        public string? PerformedBy { get; set; }

        [ForeignKey("WipeJobId")]
        [JsonIgnore]
        // Navigation property tilknyttet WipeJob.
        public  virtual WipeJob WipeJob { get; set; }
    }
}
