using Lesson.Data.DTO.Read;
using Lesson.Data.DTO.Write;
using Lesson.Extensions;
using Lesson.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Lesson.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _service;
        private readonly IOTPService _OTPService;

        public AuthController(IUserService service, IOTPService OTPService)
        {
            _service = service;
            _OTPService = OTPService;

        }

        [HttpPost("signup", Name = "Signup")]
        public async Task<IActionResult> SignUp([FromBody] UserWriteDTO user)
        {
            UserReadDTO result = await _service.Create(user);

            return CreatedAtRoute(
                "Signup",
                new { email = user.Email },
                new
                {
                    Email = result.Email,
                    Message = "User created succesfully"
                }
             );
        }

        [HttpPost("login", Name = "Login")]
        public async Task<IActionResult> Login([FromBody] UserWriteDTO user)
        {
            LoginResponseDTO response = await _service.Login(user);

            return Ok(response);
        }

        [HttpPost("forgot-password", Name = "ForgotPassword")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDTO forgotPassword)
        {
            UserReadDTO user = await _service.Me(forgotPassword.Email);

            bool otpCodeStatus = await _OTPService.SendOTPAsync(user.Email);

            return Ok(new { status = otpCodeStatus });
        }

        [HttpPost("verify-otp", Name = "VerifyOTP")]
        public async Task<IActionResult> VerifyOTP([FromBody] VerifyOTPDTO verifyOTP)
        {
            await _OTPService.VerifyOtpAsync(verifyOTP.Email, verifyOTP.Code);

            return Ok(new { Message = "OTP verified successfully" });
        }

        [HttpPost("change-password", Name = "ChangePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDTO newDetails)
        {
            await _service.ChangePassword(newDetails);

            return Ok(new { Message = "OTP verified successfully" });
        }
    }
}
