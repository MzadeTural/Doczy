using System;
using AutoMapper;
using Doczy.Business.DTOs.FieldOfStudyDtos;
using Doczy.Business.DTOs.PaymentDtos;
using Doczy.Core.Entities;

namespace Doczy.Business.MappingProfiles
{
	public class PayriffPaymentMapper:Profile
	{
		public PayriffPaymentMapper()
		{
            CreateMap<Payload, PayriffPayments>();
        }
	}
}

