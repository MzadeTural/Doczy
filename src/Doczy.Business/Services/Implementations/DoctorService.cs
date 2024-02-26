using AutoMapper;
using AutoMapper.QueryableExtensions;
using Doczy.Business.DTOs.Common;
using Doczy.Business.DTOs.DoctorDtos;
using Doczy.Business.DTOs.Language;
using Doczy.Business.Exceptions.AuthExceptions;
using Doczy.Business.Exceptions.LanguageExceptions;
using Doczy.Business.Exceptions.UserExceprions;
using Doczy.Business.Exceptions.WorkPlaceExceptions;
using Doczy.Business.Services.Interfaces;
using Doczy.Core.Entities;
using Doczy.Core.Entities.Identities;
using Doczy.DataAccess.Repositories.Implementations;
using Doczy.DataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Doczy.Business.Services.Implementations
{
    public class DoctorService : IDoctorService
    {
        private readonly UserManager<BaseAppUser> _userManager;

        private readonly IWorkPlaceRepository _workPlaceRepository;
        private readonly IDoctorRepository _doctorRepository;
        private readonly ILanguageRepository _languageRepository;
        private readonly IDoctorLanguageRepository _doctorLanguageRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        public DoctorService(UserManager<BaseAppUser> userManager, IWorkPlaceRepository workPlaceRepository, IDoctorRepository doctorRepository, ILanguageRepository languageRepository, IDoctorLanguageRepository doctorLanguageRepository, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _userManager = userManager;
            _workPlaceRepository = workPlaceRepository;
            _doctorRepository = doctorRepository;
            _languageRepository = languageRepository;
            _doctorLanguageRepository = doctorLanguageRepository;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }

        public async Task<ResponseDto> AddLanguageAsync(Guid id, Guid languageId)
        {

            ArgumentNullException.ThrowIfNull(id);
            ArgumentNullException.ThrowIfNull(languageId);
            var doct = await _doctorRepository.GetByIdAsync(id);
            var language = await _languageRepository.GetByIdAsync(languageId);
            if (doct is null) throw new UserNotFoundException("Doctor Not Found");
            if (language is null) throw new LanguageNotFoundException("Language Not Found");
            var userLanguage = new DoctorLanguage()
            {
                DoctorId = id,
                LanguageId = languageId
            };
            await _doctorLanguageRepository.CreateAsync(userLanguage);
            await _doctorLanguageRepository.SaveAsync();
            return new ResponseDto(
                                     StatusCode: HttpStatusCode.OK,
                                     Message: "Language  successfully added"
                                     );
        }
        public async Task<List<GetLanguageDto>> GetLanguageAsync(Guid userId)
        {
            ArgumentNullException.ThrowIfNull(userId);
            //var user = _httpContextAccessor?.HttpContext?.User?.Identity;
            //if (user?.IsAuthenticated == false)
            //    throw new AuthorizationException("Get Languages");
            //var userLanguagesDTO = _mapper.Map<List<LanguageDTO>>(user.Languages.Select(dl => dl.Language));

            var languages = await _doctorLanguageRepository.FindAll(c => c.DoctorId == userId ,tracking: false)
                                                            .Select(dl => dl.Language)
                                                            .ProjectTo<GetLanguageDto>(_mapper.ConfigurationProvider)
                                                             .ToListAsync();  
            return languages;
        }
            public Task<List<GetDoctorAppointmentsDto>> GetDoctorAppointments(Guid userId)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseDto> UpdatePhoneNumberAsync(Guid id, UserPhoneUpdateDto model)
        {
            ArgumentNullException.ThrowIfNull(id);
            var doct = await _userManager.FindByIdAsync(id.ToString());
            if (doct is null) throw new UserNotFoundException("Doctor Not Found");
            doct.PhoneNumber = model.PhoneNumber;
            await _doctorRepository.SaveAsync();
            return new ResponseDto(
                                    StatusCode: HttpStatusCode.OK,
                                    Message: "Phone number successfully modified"
                                    );
        }

        public async Task<ResponseDto> UpdateWorkPlaceAsync(Guid id, Guid worpPlaceId)
        {
            ArgumentNullException.ThrowIfNull(id);
            ArgumentNullException.ThrowIfNull(worpPlaceId);
            var doct = await _doctorRepository.GetByIdAsync(id);
            var workPlace = await _workPlaceRepository.GetByIdAsync(worpPlaceId);
            if (doct is null) throw new UserNotFoundException("Doctor Not Found");
            if (workPlace is null) throw new WokrPlaceNotFoundException("Work Place Not Found");
            doct.WorkPlaceId = worpPlaceId;
            await _doctorRepository.SaveAsync();
            return new ResponseDto(
                                     StatusCode: HttpStatusCode.OK,
                                     Message: "Work place successfully modified"
                                     );
        }
    }
}
