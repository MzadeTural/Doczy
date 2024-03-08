using AutoMapper;
using Doczy.Business.DTOs.Common;
using Doczy.Business.DTOs.Experiance;
using Doczy.Business.DTOs.FavoriteDoctorDtos;
using Doczy.Business.Services.Interfaces;
using Doczy.Core.Entities;
using Doczy.Core.Entities.Identities;
using Doczy.DataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Net;

namespace Doczy.Business.Services.Implementations
{
    public class FavoriteDoctorService : IFavoriteDoctorService
    {
        private readonly IFavoriteDoctorRepository _favoriteDoctorRepository;
        private readonly UserManager<BaseAppUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public FavoriteDoctorService(IFavoriteDoctorRepository favoriteDoctorRepository, UserManager<BaseAppUser> userManager, IMapper mapper)
        {
            _favoriteDoctorRepository = favoriteDoctorRepository;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<ResponseDto> CreateFavoriteDoctorAsync(CreateFavoriteDoctorDto model)
        {
            Guid patientId = (await _userManager.GetUserAsync(_httpContextAccessor?.HttpContext?.User)).Id;
            ArgumentNullException.ThrowIfNull(patientId);
            FavoriteDoctor favourit = _mapper.Map<FavoriteDoctor>(model);
            favourit.PatientId = patientId;          
            bool result = await _favoriteDoctorRepository.CreateAsync(favourit);
            await _favoriteDoctorRepository.SaveAsync();
            return new ResponseDto(
                       StatusCode: result ? HttpStatusCode.Created : HttpStatusCode.BadRequest,
                       Message: result ? "Favourite successfully created" : "Something went wrong"
                       );
        }
    }
}
