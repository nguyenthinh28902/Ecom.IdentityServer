using Ecom.IdentityServer.Models;
using Ecom.IdentityServer.Models.DTOs.SignIn;
using Ecom.IdentityServer.Models.ViewModels.Accounts;

namespace Ecom.IdentityServer.Services.Interfaces
{
    public interface IAuthService
    {
        /// <summary>
        /// Xác thực thông tin đăng nhập với CustomerService thông qua System Token.
        /// </summary>
        /// <param name="signInViewModel">Thông tin UserID và Password từ người dùng.</param>
        /// <returns>Thông tin User đã được xác thực dưới dạng SignInResponseDto.</returns>
        Task<SignInResponseDto?> AuthenticateInternal(SignInViewModel signInViewModel);

        /// <summary>
        /// Xác thực thông tin đăng nhập từ nhà cung cấp bên thứ ba (ví dụ: Google, Facebook).
        /// </summary>
        /// <param name="userInfoSinginDto">thông tin nhận được khi login thành công với bên thứ 3</param>
        /// <param name="providerName">tên nhà cung cấp</param>
        /// <returns></returns>
        public Task<bool> AuthenticateInternal(UserInfoSinginDto userInfoSinginDto, string providerName);

        /// <summary>
        /// Thực hiện lưu trữ phiên đăng nhập vào IdentityServer Cookie.
        /// Thiết lập các Claims quan trọng như sub, email, wid và roles.
        /// </summary>
        /// <param name="user">Dữ liệu người dùng từ hệ thống nội bộ.</param>
        Task SignInIdentityUserAsync(SignInResponseDto user);

        /// <summary>
        /// Đổi mã Authorization Code lấy Token thực thi bên ngoài (External Token).
        /// Sử dụng ExchangeRequest chứa Code và CodeVerifier cho luồng PKCE.
        /// </summary>
        /// <param name="exchangeRequest">Model chứa Code, CodeVerifier và các thông tin liên quan.</param>
        /// <returns>Result bao gồm TokenResponseDto nếu thành công.</returns>
        Task<Result<TokenResponseDto?>> ExchangeCodeForExternalToken(ExchangeRequest exchangeRequest);
    }
}
