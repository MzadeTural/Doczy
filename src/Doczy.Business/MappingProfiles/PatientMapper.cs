using AutoMapper;
using Doczy.Business.DTOs.UserDtos;
using Doczy.Core.Entities.Identities;

namespace Doczy.Business.MappingProfiles
{
    public class PatientMapper:Profile
    {
        public PatientMapper()
        {
            CreateMap<CreatePatientDto, PatientAppUser>()
                .ReverseMap();
        }
    }
}
