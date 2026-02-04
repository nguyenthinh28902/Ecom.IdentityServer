namespace Ecom.IdentityServer.Models.DTOs.SignIn
{
    public class SignInResponseDto
    {
        public SignInResponseDto() { }
        // Sử dụng kiểu string cho Id nếu bạn có ý định dùng GUID sau này, 
        // hoặc giữ int nhưng nên nhất quán với DB.
        public int CustomerId { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }

        // Kỹ thuật đúng: Một User có thể có nhiều Role. 
        // Khởi tạo sẵn List trống để tránh lỗi NullReferenceException khi dùng.

    }
}
