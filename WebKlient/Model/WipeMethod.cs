using System.ComponentModel.DataAnnotations;

namespace WebKlient.Model
{
    public class WipeMethod
    {
        [Key]
        public int WipeMethodID { get; set; }  // Primary Key

        public string? Name { get; set; }   // Navn på slettemetoden

        public string? Description { get; set; }   // Beskrivelse af slettemetoden

        public int OverwritePass { get; set; }   // Antal overskrivninger

        public ICollection<WipeJob> WipeJobs { get; set; }
    }
}