namespace Doczy.Business.DTOs.PaymentDtos
{
    public class CreatePaymentDto
    {
        public string CardNumber { get; set; }
        public string ExpiryDate { get; set; }
        public string CVV { get; set; }

    }
}
