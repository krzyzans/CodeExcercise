using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CodeExcercise.Common.Configuration.Extensions
{
    public static class ConfigurationExtensions
    {
        public static void AddSqlConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<SqlConnectionConfiguration>(options =>
            {
                configuration.GetSection(SqlConnectionConfiguration.SectionName).Bind(options);
            });
            
            services.Configure<TopLimitsConfiguration>(options =>
            {
                configuration.GetSection(TopLimitsConfiguration.SectionName).Bind(options);
            });
        }
    }
}