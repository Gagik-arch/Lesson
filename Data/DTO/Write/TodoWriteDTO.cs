using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lesson.Data.DTO.Write
{
    public class TodoWriteDTO
    {
        [Required]
        public required string Title { get; set; }

        public string? Description { get; set; }
        public IFormFile? Image { get; set; }
    }
}
