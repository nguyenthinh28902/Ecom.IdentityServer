namespace Ecom.IdentityServer.Models.Enums
{
    public enum ServiceAuth
    {
        APIGatewayWebService,
        IdentityServer,
        ecom_web_client
    }

    public enum ExpireTimeSpanSignIn
    {
        Short = 1, // 1 hour
        Medium = 8, // 8 hours
        Long = 24 // 24 hours
    }
}
