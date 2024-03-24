namespace Doczy.Core.Entities
{
	public class Payriff
	{
            public string Code { get; set; }
            public string Message { get; set; }
            public string Route { get; set; }
            public string RnternalMessage { get; set; }
            public string ResponseId { get; set; }
            public Payload payload { get; set; }

            public class Payload
            {
                public string OrderId { get; set; }
                public string SessionId { get; set; }
                public string PaymentUrl { get; set; }
                public int TransactionId { get; set; }
            }
        
	}
}

