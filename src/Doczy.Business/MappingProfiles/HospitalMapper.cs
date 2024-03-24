using AutoMapper;
using Doczy.Business.DTOs.HospitalDtos;
using Doczy.Business.DTOs.WorkPlace;
using Doczy.Core.Entities;

namespace Doczy.Business.MappingProfiles
{
    public class HospitalMapper : Profile
    {
        public HospitalMapper()
        {
            CreateMap<CreateHospitalDto, Hospital>().ForMember(s => s.IconUrl, e => e.Ignore())
                .ReverseMap();
            CreateMap<Hospital, GetHospitalDto>();
        }
    }
}
