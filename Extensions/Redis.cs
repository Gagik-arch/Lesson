using StackExchange.Redis;
using System.Diagnostics;

namespace Lesson.Extensions
{
    static public class Redis
    {

        public static void AddRedis(this IServiceCollection services, IConfiguration configuration) {
            string key = configuration["ConnectionStrings:RedisConnection"] ?? "";

            try
            {
                ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(key);

                services.AddSingleton<IConnectionMultiplexer>(redis);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error connecting to Redis: {ex.Message}");
            }

        }
    }
}
