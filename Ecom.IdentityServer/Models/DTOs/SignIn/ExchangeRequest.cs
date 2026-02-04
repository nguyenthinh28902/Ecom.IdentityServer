using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

namespace Ecom.IdentityServer.Models.DTOs.SignIn
{
    public class ExchangeRequest
    {
        [JsonPropertyName("code")]
        public string Code { get; set; } = string.Empty;

        // Code Verifier để kiểm tra tính hợp lệ của Code (PKCE)
        [FromForm(Name = "code_verifier")]
        public string CodeVerifier { get; set; } = string.Empty;
        [FromForm(Name = "redirect_uri")]
        public string RedirectUri { get; set; } = string.Empty;
    }
}
