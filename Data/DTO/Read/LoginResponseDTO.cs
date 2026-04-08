namespace Lesson.Data.DTO.Read
{
    public class LoginResponseDTO
    {
        public required string Token { get; set; }

        public required UserReadDTO User { get; set; }
    }
}
