using AutoMapper;
using Doczy.Business.DTOs.FavoriteDoctorDtos;
using Doczy.Core.Entities;

namespace Doczy.Business.MappingProfiles
{
    public class FavoriteDoctorMapper :Profile
    {
        public FavoriteDoctorMapper()
        {
            CreateMap<CreateFavoriteDoctorDto ,FavoriteDoctor>().ReverseMap();
            CreateMap<FavoriteDoctor,GetFavouriteDoctorDto>()
                .ReverseMap();
        }
    }
}
