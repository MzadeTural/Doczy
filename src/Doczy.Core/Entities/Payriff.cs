namespace Doczy.Core.Entities
{
	public class Payriff
	{
            public string code { get; set; }
            public string message { get; set; }
            public string route { get; set; }
            public string internalMessage { get; set; }
            public string responseId { get; set; }
            public Payload payload { get; set; }

            public class Payload
            {
                public string orderId { get; set; }
                public string sessionId { get; set; }
                public string paymentUrl { get; set; }
                public int transactionId { get; set; }
            }
        
	}
}

