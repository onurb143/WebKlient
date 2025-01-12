using System.ComponentModel.DataAnnotations;

namespace WebKlient.DTO
{
    public class WipeMethodReadDto
    {
        [Key]
        public int WipeMethodID { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }
        public int OverwritePass { get; set; }
    }
}
