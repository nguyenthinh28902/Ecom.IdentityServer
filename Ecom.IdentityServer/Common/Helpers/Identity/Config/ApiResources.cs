using Duende.IdentityServer.Models;

namespace Ecom.IdentityServer.Common.Helpers.Identity.Config
{
    public static class ApiResources
    {
        public static IEnumerable<ApiResource> Get()
        {
            return new[]
            {
                    // Resource mới dành riêng cho Customer Service
                    // 1. Identity/User Service Resource
                new ApiResource("customer.api", "Identity CMS Service API")
                {
                    Scopes = { "customer.internal", "customer.read", "customer.write" },
                    // UserClaims giúp đính kèm thêm thông tin vào Access Token khi gọi API này
                    UserClaims = { "sub" },
                },
              };
        }
    }
}
