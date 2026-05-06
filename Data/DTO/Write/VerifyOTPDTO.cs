using System.ComponentModel.DataAnnotations;

namespace Lesson.Data.DTO.Write
{
    public class VerifyOTPDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [StringLength(6, MinimumLength = 6, ErrorMessage = "OTP code must be 6 characters long.")]
        public string Code { get; set; } = string.Empty;
    }
}
