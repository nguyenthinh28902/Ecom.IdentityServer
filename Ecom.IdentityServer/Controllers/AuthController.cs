using Duende.IdentityServer.Services;
using Ecom.IdentityServer.Models.DTOs.SignIn;
using Ecom.IdentityServer.Models.ViewModels.Accounts;
using Ecom.IdentityServer.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Ecom.IdentityServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IIdentityServerInteractionService _interaction;
        private readonly IAuthService _authService;
        private readonly ILogger<AuthController> _logger;
        public AuthController(IAuthService authService, IIdentityServerInteractionService interaction, ILogger<AuthController> logger)
        {
            _authService = authService;
            _interaction = interaction;
            _logger = logger;
        }

        [HttpPost("dang-nhap")]
        public async Task<IActionResult> SignIn([FromForm] SignInViewModel signInViewModel)
        {
            var result = await _authService.AuthenticateInternal(signInViewModel);
            var context = await _interaction.GetAuthorizationContextAsync(signInViewModel.ReturnUrl);
            var link = Url.Action(controller: "Login", action: "Error", values: signInViewModel.ReturnUrl ?? "/");
            if (result == null)
            {
                return Redirect(link);
            }
            await _authService.SignInIdentityUserAsync(result);
            if (context != null)
            {
                return Redirect(signInViewModel.ReturnUrl);
            }

            return Redirect(link);
        }


        [HttpPost("trao-doi-token")]
        public async Task<IActionResult> Exchange([FromForm] ExchangeRequest request)
        {
            _logger.LogInformation($"data client change token: {JsonSerializer.Serialize(request)}");

            var resultToken = await _authService.ExchangeCodeForExternalToken(request);

            return Ok(resultToken);
        }



        [HttpPost("dang-xuat-he-thong")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmLogout([FromForm] LogoutViewModel model)
        {
            // Xóa Cookie của Identity Server
            await HttpContext.SignOutAsync();

            // Lấy thông tin ngữ cảnh để quay lại đúng CMS
            var logoutContext = await _interaction.GetLogoutContextAsync(model.LogoutId);

            if (logoutContext != null && !string.IsNullOrEmpty(logoutContext.PostLogoutRedirectUri))
            {
                // Quay về CMS (thông qua Gateway 7145)
                return Redirect(logoutContext.PostLogoutRedirectUri);
            }

            // Nếu không có thông tin quay về, đưa về trang chủ mặc định
            return Redirect("/");
        }
    }
}


