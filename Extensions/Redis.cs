using StackExchange.Redis;

namespace Lesson.Extensions
{
    static public class Redis
    {

        public static void AddRedis(this IServiceCollection services, IConfiguration configuration) {
            string key = configuration["ConnectionStrings:RedisConnection"] ?? "";

            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(key);

            services.AddSingleton<IConnectionMultiplexer>(redis);
        }
    }
}
