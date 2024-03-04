namespace Doczy.Business.DTOs.AuthDtos
{
    public class ResetPasswordDto
    {
        public string EmailorPhoneNumber { get; set; }
        public string OTP { get; set; }
        public string NewPassword { get; set; }
    }
}
