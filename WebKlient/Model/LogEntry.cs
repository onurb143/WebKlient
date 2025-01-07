using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebKlient.Model
{

    public class LogEntry
    {
        [Key]
        public int LogID { get; set; }

        public DateTime Timestamp { get; set; }

        public string? Message { get; set; }

        [ForeignKey("WipeJob")]
        public int WipeJobId { get; set; } // Fremmednøgle til WipeJob

        public WipeJob WipeJob { get; set; }
    }
}
