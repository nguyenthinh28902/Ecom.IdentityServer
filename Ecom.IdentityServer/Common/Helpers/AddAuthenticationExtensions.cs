using Ecom.IdentityServer.Models.DTOs.SignIn;
using Ecom.IdentityServer.Models.Enums;
using Ecom.IdentityServer.Models.Settings;
using Microsoft.AspNetCore.Authentication;

namespace Ecom.IdentityServer.Common.Helpers
{
    public static class AuthenticationExtensions
    {
        public static IServiceCollection AddAuthenticationExtensions(this IServiceCollection services, IConfiguration configuration)
        {

            var googleConfig = configuration.GetSection("GoogleAuthentication").Get<GoogleAuthentication>();
            // 1. Kiểm tra cấu hình rõ ràng hơn
            var jwtSettings = configuration.GetSection("JwtSettings").Get<JwtSettings>()
                ?? throw new InvalidOperationException("Config Error: 'JwtSettings' is missing in appsettings.json");
            var gatewayUrl = ConfigBaseUrl.GetEcomApiGatewayBaseUrl(configuration);

            services.AddAuthentication(options =>
            {
                // Scheme mặc định để kiểm tra đăng nhập
                options.DefaultScheme = CookieName.Cookies.ToString();
                // Scheme dùng để thách thức (Challenge) khi chưa đăng nhập
                options.DefaultChallengeScheme = CookieName.Google.ToString();
                // Scheme tạm thời để lưu kết quả từ Google
                options.DefaultSignInScheme = CookieName.External.ToString();
            })
            .AddCookie(CookieName.Cookies.ToString()) // Cookie chính cho ứng dụng
            .AddCookie(CookieName.External.ToString()) // Cookie tạm cho Google
            .AddGoogle(CookieName.Google.ToString(), options =>
            {
                if (googleConfig != null)
                {
                    options.ClientId = googleConfig.client_id;
                    options.ClientSecret = googleConfig.client_secret;
                    options.SaveTokens = true;
                    // Chỉ định nơi lưu kết quả tạm thời sau khi Google đăng nhập xong
                    options.SignInScheme = CookieName.External.ToString();
                    options.Scope.Add("profile");
                    options.ClaimActions.MapJsonKey(ClaimTypeCustoms.image_url.ToString(), "picture");
                }
            });

            services.AddCors(options =>
            {
                options.AddPolicy(AuthEnum.AllowEcomWeb.ToString(), policy =>
                {
                    var ecomWebBaseUrl = configuration["EcommerceWeb:BaseUrl"];
                    if (!string.IsNullOrEmpty(ecomWebBaseUrl))
                    {
                        policy.WithOrigins(ecomWebBaseUrl)
                              .AllowAnyHeader()
                              .AllowAnyMethod()
                              .AllowCredentials();
                    }
                });
            });
            var hours = (int)ExpireTimeSpanSignIn.Long; // hours = 8
            // 3. Cấu hình Cookie (Để IdentityServer tương tác tốt với trình duyệt hiện đại)
            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.Domain = gatewayUrl;
                options.Cookie.Name = "identity_auth_session";
                options.Cookie.SameSite = SameSiteMode.None; // Cho phép Cross-site (Nuxt gọi Identity)
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always; // Bắt buộc HTTPS
                options.ExpireTimeSpan = TimeSpan.FromHours(hours); // Phiên đăng nhập sống trong 8 giờ
                options.SlidingExpiration = true; // Tự động gia hạn nếu user còn hoạt động
            });
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.MinimumSameSitePolicy = SameSiteMode.None;
                options.Secure = CookieSecurePolicy.Always; // Ép buộc Cookie luôn đi kèm HTTPS
            });
            return services;
        }
    }
}
