namespace Lesson.Services.Interfaces
{
    public interface IOTPService
    {

        Task<bool> SendOTPAsync(string key);
        Task VerifyOtpAsync(string key, string inputOtp);
    }
}
