using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lesson.Models
{
    public class Todo
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public required string Title { get; set; }

        public string? Description { get; set; }

        public required string Author { get; set; }

        public bool IsCompleted { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
