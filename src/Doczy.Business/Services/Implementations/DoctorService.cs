using AutoMapper;
using AutoMapper.QueryableExtensions;
using Doczy.Business.DTOs.Common;
using Doczy.Business.DTOs.DoctorDtos;
using Doczy.Business.DTOs.Language;
using Doczy.Business.DTOs.RaitingDtos;
using Doczy.Business.Exceptions.DoctorCategoryExceptions;
using Doczy.Business.Exceptions.LanguageExceptions;
using Doczy.Business.Exceptions.UserExceprions;
using Doczy.Business.Services.Interfaces;
using Doczy.Core.Entities;
using Doczy.Core.Entities.Identities;
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
        private readonly IDoctorCategoryRepository _categoryRepository;
        public DoctorService(UserManager<BaseAppUser> userManager, IWorkPlaceRepository workPlaceRepository, IDoctorRepository doctorRepository, ILanguageRepository languageRepository, IDoctorLanguageRepository doctorLanguageRepository, IHttpContextAccessor httpContextAccessor, IMapper mapper, IDoctorCategoryRepository categoryRepository)
        {
            _userManager = userManager;
            _workPlaceRepository = workPlaceRepository;
            _doctorRepository = doctorRepository;
            _languageRepository = languageRepository;
            _doctorLanguageRepository = doctorLanguageRepository;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            _categoryRepository = categoryRepository;
        }

        public async Task<ResponseDto> AddLanguageAsync(Guid languageId)
        {
            var doctorId = (await _userManager.GetUserAsync(_httpContextAccessor?.HttpContext?.User)).Id;
            ArgumentNullException.ThrowIfNull(languageId);
            var languages = await _doctorLanguageRepository.FindAll(dl => dl.DoctorId == doctorId).ToListAsync();
            var check = languages.Any(dl => dl.LanguageId == languageId);
            if (check)
                throw new LanguageAlreadyAddedExceptions("The language  is already available for this user");
            var language = await _languageRepository.GetByIdAsync(languageId);
            if (language is null) throw new LanguageNotFoundException("Lnguage Not Faound");
            var userLanguage = new DoctorLanguage()
            {
                DoctorId = doctorId,
                LanguageId = languageId
            };
            await _doctorLanguageRepository.CreateAsync(userLanguage);
            await _doctorLanguageRepository.SaveAsync();
            return new ResponseDto(
                                     StatusCode: HttpStatusCode.OK,
                                     Message: "Language  successfully added"
                                     );
        }
        public async Task<List<GetLanguageDto>> GetLanguageAsync()
        {
            var doctorId = (await _userManager.GetUserAsync(_httpContextAccessor?.HttpContext?.User)).Id;
            ArgumentNullException.ThrowIfNull(doctorId);

            var languages = await _doctorLanguageRepository.FindAll(c => c.DoctorId == doctorId, tracking: false)
                                                            .Select(dl => dl.Language)
                                                            .ProjectTo<GetLanguageDto>(_mapper.ConfigurationProvider)
                                                             .ToListAsync();
            return languages;
        }
        public Task<List<GetDoctorAppointmentsDto>> GetDoctorAppointments()
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseDto> UpdatePhoneNumberAsync(UserPhoneUpdateDto model)
        {
            var doctorId = (await _userManager.GetUserAsync(_httpContextAccessor?.HttpContext?.User)).Id;
            ArgumentNullException.ThrowIfNull(doctorId);

            var doct = await _userManager.FindByIdAsync(doctorId.ToString());
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
            //ArgumentNullException.ThrowIfNull(id);
            //ArgumentNullException.ThrowIfNull(worpPlaceId);
            //var doct = await _doctorRepository.GetByIdAsync(id);
            //var workPlace = await _workPlaceRepository.GetByIdAsync(worpPlaceId);
            //if (doct is null) throw new UserNotFoundException("Doctor Not Found");
            //if (workPlace is null) throw new WokrPlaceNotFoundException("Work Place Not Found");
            //doct.WorkPlaceId = worpPlaceId;
            await _doctorRepository.SaveAsync();
            return new ResponseDto(
                                     StatusCode: HttpStatusCode.OK,
                                     Message: "Work place successfully modified"
                                     );
        }

        public async Task<ResponseDto> UpdateCategoryAsync(Guid categoryId)
        {
            var doctorId = (await _userManager.GetUserAsync(_httpContextAccessor?.HttpContext?.User)).Id;
            ArgumentNullException.ThrowIfNull(categoryId);
            var doct = await _doctorRepository.GetByIdAsync(doctorId);
            if (doct is null) throw new UserNotFoundException("Doctor Not Found");
            var catg = _categoryRepository.GetByIdAsync(categoryId);
            if (catg is null) throw new CategoryNotFoundException("Category Not Found");
            doct.DoctorCategoryId = categoryId;
            await _doctorRepository.SaveAsync();
            return new ResponseDto(
                                    StatusCode: HttpStatusCode.OK,
                                    Message: "Category successfully modified"
                                    );

        }

        public async Task<List<GetDoctorsDto>> GetFilterDoctors(GetDoctorFilterDto model)
        {
            var doctors = await _doctorRepository.FindAll(u => u.DoctorCategoryId == model.CategoryId
                                                           && u.Services.Any(s => s.ServiceTypeId == model.ServiceTypeId
                                                           && u.Experiances.Any(e => e.currentlyWorking && e.WorkPlaceId == model.workPlaceId)
                                                           && s.Price >= model.MinPrice && s.Price <= model.MinPrice),
                                                           tracking: false,
                                                           d => d.Services,
                                                           d => d.FavoriteDoctors,
                                                           d => d.Ratings,
                                                           d => d.Experiances,
                                                           d => d.DoctorCategory
                                                           ).ProjectTo<GetDoctorsDto>(_mapper.ConfigurationProvider)
                                                           .ToListAsync();
            return doctors;
        }

        public Task<ResponseDto> RaitingDoctor(CreateRaitingDto model)
        {
            throw new NotImplementedException();
        }
    }
}
