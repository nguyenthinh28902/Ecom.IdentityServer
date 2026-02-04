namespace Ecom.IdentityServer.Models.Settings
{
    public static class ConfigBaseUrl
    {
        public static string GetEcommerceWebBaseUrl(IConfiguration configuration)
        {
            return configuration["EcommerceWeb:BaseUrl"] ?? "https://localhost:3000";
        }
        public static string GetCustomerServiceBaseUrl(IConfiguration configuration)
        {
            return configuration["CustomerService:BaseUrl"] ?? "https://localhost:3000";
        }
        public static string GetEcomApiGatewayBaseUrl(IConfiguration configuration)
        {
            return configuration["EcomApiGateway:BaseUrl"] ?? "https://localhost:3001";
        }
    }
}
