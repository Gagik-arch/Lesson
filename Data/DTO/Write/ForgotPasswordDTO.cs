using System.ComponentModel.DataAnnotations;

namespace Lesson.Data.DTO.Write
{
    public class ForgotPasswordDTO
    {
        [Required]
        [EmailAddress]
        public required string Email { get; set; }
    }
}
