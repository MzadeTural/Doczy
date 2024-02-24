using AutoMapper;
using Doczy.Business.DTOs.ServiceDtos;
using Doczy.Core.Entities;

namespace Doczy.Business.MappingProfiles
{
    public class ServiceMapper:Profile
    {
        public ServiceMapper()
        {
            CreateMap<CreateServiceDto, Service>().ReverseMap();
        }
    }
}
