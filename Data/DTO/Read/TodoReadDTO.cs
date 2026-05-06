using System.ComponentModel.DataAnnotations.Schema;

namespace Lesson.Data.DTO.Read
{
    public class TodoReadDTO
    {
        public Guid Id { get; set; }

        public required string Title { get; set; }

        public string? Description { get; set; }

        public required string Author { get; set; }

        public required bool IsCompleted { get; set; }

        public required DateTime CreatedAt { get; set; }
        public string? Image { get; set; }
    }
}
