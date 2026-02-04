using Duende.IdentityServer.Models;

namespace Ecom.IdentityServer.Common.Helpers.Identity.Config
{
    public static class ApiScopes
    {
        public static IEnumerable<ApiScope> Get()
        {
            return new[]
            {
                // --- PRODUCT SERVICE ---
                new ApiScope("product.internal", "Full access to Product Service"),
                new ApiScope("product.read", "Read product information"),

                // --- ORDER SERVICE ---
                new ApiScope("order.internal", "Full access to Order Service"),
                new ApiScope("order.read", "View order history"),
                new ApiScope("order.write", "Place or Modify orders"),

                // --- PAYMENT SERVICE ---
                new ApiScope("payment.internal", "Full access to Payment Service"),
                new ApiScope("payment.read", "View payment transactions"),
                new ApiScope("payment.write", "Process payments or refunds"),
                 // --- CUSOMER SERVICE ---
                 new ApiScope("customer.internal", "Full access to customer Service"),
                 new ApiScope("customer.read", "view infor"),
                 new ApiScope("customer.write", "Update infor"),
            };
        }
    }
}
