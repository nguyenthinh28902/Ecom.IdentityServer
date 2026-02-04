using Ecom.IdentityServer.Models.DTOs.SignIn;
using Ecom.IdentityServer.Models.Settings;

namespace Ecom.IdentityServer.Common.Helpers
{
    public static class ConfigAppSetting
    {
        public static IServiceCollection AddConfigAppSetting(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JwtSettings>(
             configuration.GetSection("JwtSettings"));
            services.Configure<GoogleAuthentication>(
             configuration.GetSection("GoogleAuthentication"));

            services.Configure<ServiceAuthOptions>(
            configuration.GetSection("IdentityServerService"));
            return services;
        }
    }
}
