namespace Ecom.IdentityServer.Models.DTOs.SignIn
{
    public class ConfirmCustomerInforResponse
    {
        public ConfirmCustomerInforResponse() { }
        public int CustomerId { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

    }
}
