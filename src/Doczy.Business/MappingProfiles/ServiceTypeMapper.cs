using AutoMapper;
using Doczy.Business.DTOs.ServiceTypeDtos;
using Doczy.Core.Entities;
using static Org.BouncyCastle.Asn1.Cmp.Challenge;

namespace Doczy.Business.MappingProfiles
{
    public class ServiceTypeMapper :Profile
    {
        public ServiceTypeMapper()
        {
            CreateMap<CreateServiceTypeDto, ServiceType>().ForMember(s=>s.IconUrl, e => e.Ignore())
                .ReverseMap();
            CreateMap<ServiceType,GetServiceTypeDto>()
               .ReverseMap();
        }
    }
}
