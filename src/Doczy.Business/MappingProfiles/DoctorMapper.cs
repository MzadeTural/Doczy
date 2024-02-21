using AutoMapper;
using Doczy.Business.DTOs.UserDtos;
using Doczy.Core.Entities.Identities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doczy.Business.MappingProfiles
{
    public class DoctorMapper :Profile
    {
        public DoctorMapper()
        {
               
        CreateMap<CreateDoctorDto, DoctorAppUser>()
                .ForMember(usc => usc.DiplomaImageUrl, e => e.Ignore())
                .ForMember(usc => usc.IdCardImageUrl, e => e.Ignore())              
                .ReverseMap();
             }
    }
}
