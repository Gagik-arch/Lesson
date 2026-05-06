using System.ComponentModel.DataAnnotations;

namespace Lesson.Data.DTO.Write
{
    public class ChangePasswordDTO
    {
        [Required]
        [EmailAddress]
        public required string Email { get; set; }

        [Required]
        [StringLength(8, MinimumLength = 8)]
        public required string NewPassword { get; set; }
    }
}
