namespace Ecom.IdentityServer.Services.Interfaces
{
    public interface IClientService
    {
        Task<string> GetAllowedScopesAsync(string clientId);
    }
}
