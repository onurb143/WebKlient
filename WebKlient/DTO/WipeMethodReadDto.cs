using System.ComponentModel.DataAnnotations;

namespace WebKlient.DTO
{
    public class WipeMethodReadDto
    {
        [Key]
        public int WipeMethodID { get; set; }

        // Optional property - only assign if provided
        [MaxLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
        public string? Name { get; set; }

        // Optional property with max length validation
        [MaxLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string? Description { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "OverwritePass must be greater than 0.")]
        public int OverwritePass { get; set; }
    }
}
