using AutoMapper;
using Doczy.Business.DTOs.RaitingDtos;
using Doczy.Core.Entities;

namespace Doczy.Business.MappingProfiles
{
    public class RaitingDoctorMapper:Profile
    {
        public RaitingDoctorMapper()
        {
            CreateMap<DoctorRating,CreateRaitingDto>().ReverseMap();
        }
    }
}
