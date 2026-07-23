using Lesson.Exceptions;
using Lesson.Extensions;
using Lesson.Models;
using Lesson.Services.Interfaces;
using StackExchange.Redis;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;

namespace Lesson.Services.Implementations
{
    public class OTPService : IOTPService
    {
        private readonly IDatabase _redis;

        public OTPService(IConnectionMultiplexer redis)
        {
            _redis = redis.GetDatabase();
        }

        public async Task<bool> SendOTPAsync(string email)
        {
            string otp = new Random().Next(100000, 999999).ToString();

            string hash = Hash(otp);
            try
            {
                await _redis.StringSetAsync(
                    $"otp:{email}",
                    hash,
                    TimeSpan.FromMinutes(5)
                );

                SMTP smtpService = new SMTP(
                    from: "gagik.chilingaryan2020@gmail.com",
                    subject: "Lesson App OTP",
                    body: $"<h1>Your OTP code is: {otp}</h1>"
                );

                smtpService.Add(email);
                await smtpService.SendAsync();

                return true;

            }
            catch
            {
                return false;
            }
        }

        private string Hash(string input)
        {
            using SHA256 sha256 = SHA256.Create();
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));

            return Convert.ToBase64String(bytes);
        }

        public async Task VerifyOtpAsync(string email, string inputOtp)
        {
            try
            {
                string? storedHash = await _redis.StringGetAsync($"otp:{email}");

                string hash = Hash(inputOtp);

                if (storedHash is null || storedHash != hash)
                {
                    throw new NotFoundException("Invalid OTP");
                }

                 await _redis.KeyDeleteAsync($"otp:{email}");
            }
            catch{
                throw new BadRequestException("Invalid OTP");
            }
        }
    }
}
