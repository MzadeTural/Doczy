namespace Doczy.Business.DTOs.AuthDtos
{
    public class ConfirmOTPDto
    {
        public string EmailOrPhoneNumber { get; set; }
        public string OTP { get; set; }
    }
}
