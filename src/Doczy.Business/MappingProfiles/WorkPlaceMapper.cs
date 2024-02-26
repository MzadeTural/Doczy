using AutoMapper;
using Doczy.Business.DTOs.WorkPlace;
using Doczy.Core.Entities;

namespace Doczy.Business.MappingProfiles
{
    public class WorkPlaceMapper : Profile
    {
        public WorkPlaceMapper()
        {
            CreateMap<CreateWorkPlaceDto, WorkPlace>().ForMember(s => s.IconUrl, e => e.Ignore())
                .ReverseMap();
        }
    }
}
