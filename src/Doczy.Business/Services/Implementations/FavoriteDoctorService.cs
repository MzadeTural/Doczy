using AutoMapper;
using Doczy.Business.DTOs.Common;
using Doczy.Business.Exceptions.AuthExceptions;
using Doczy.Business.Exceptions.FavoriteDoctorExceptions;
using Doczy.Business.Exceptions.UserExceprions;
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

        public FavoriteDoctorService(IFavoriteDoctorRepository favoriteDoctorRepository, UserManager<BaseAppUser> userManager, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _favoriteDoctorRepository = favoriteDoctorRepository;
            _userManager = userManager;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ResponseDto> CreateFavoriteDoctorAsync(Guid doctorId)
        {
            var user = _httpContextAccessor.HttpContext.User.Identity;
            if (!user.IsAuthenticated)
                throw new AuthorizationException("Please login to add favourite any doctor", HttpStatusCode.Unauthorized);

            Guid patientId = (await _userManager.GetUserAsync(_httpContextAccessor?.HttpContext?.User)).Id;
            ArgumentNullException.ThrowIfNull(patientId);
           
            var isExistDoctor = await _userManager.FindByIdAsync(doctorId.ToString());
            if (isExistDoctor is null)
                throw new UserNotFoundException("Id", doctorId.ToString());

            FavoriteDoctor favourit = new()
            {
                DoctorId = doctorId,
                PatientId = patientId
            };
            bool result = await _favoriteDoctorRepository.CreateAsync(favourit);
            await _favoriteDoctorRepository.SaveAsync();
            return new ResponseDto(
                       StatusCode: result ? HttpStatusCode.Created : HttpStatusCode.BadRequest,
                       Message: result ? "Favourite successfully created" : "Something went wrong"
                       );
        }

        public async Task<ResponseDto> RemoveFavoriteDoctorAsync(Guid doctorId)
        {
            var user = _httpContextAccessor.HttpContext.User.Identity;
            if (!user.IsAuthenticated)
                throw new AuthorizationException("Please login to add favourite any doctor", HttpStatusCode.Unauthorized);
            var isExistDoctor = await _userManager.FindByEmailAsync(doctorId.ToString());
            if (isExistDoctor is null)
                throw new UserNotFoundException("Id", doctorId.ToString());
            Guid patientId = (await _userManager.GetUserAsync(_httpContextAccessor?.HttpContext?.User)).Id;
            ArgumentNullException.ThrowIfNull(patientId);
            bool IsExist = await _favoriteDoctorRepository.IsExistAsync(fd => fd.DoctorId == doctorId && fd.PatientId == patientId);
            if (!IsExist)
                throw new FavoriteDoctorNotFoundException();
            var favouriteDoctor = await _favoriteDoctorRepository.GetSingleAysnc(fd => fd.DoctorId == doctorId && fd.PatientId == patientId);
              var result= _favoriteDoctorRepository.Delete(favouriteDoctor);
            await _favoriteDoctorRepository.SaveAsync();

            return new ResponseDto(
                       StatusCode: result ? HttpStatusCode.OK : HttpStatusCode.BadRequest,
                       Message: result ? "Favourite successfully removed" : "Something went wrong"
                       );
        }
    }
}
