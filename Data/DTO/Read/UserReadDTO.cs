namespace Lesson.Data.DTO.Read
{
    public class UserReadDTO
    {
        public required string Email { get; set; }

        public required string Id { get; set; }

        public required DateTime CreatedAt { get; set; }
    }
}
