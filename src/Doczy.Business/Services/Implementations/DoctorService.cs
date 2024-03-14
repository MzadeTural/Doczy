using AutoMapper;
using AutoMapper.QueryableExtensions;
using Doczy.Business.DTOs.Common;
using Doczy.Business.DTOs.DoctorDtos;
using Doczy.Business.DTOs.Language;
using Doczy.Business.DTOs.RaitingDtos;
using Doczy.Business.Enums;
using Doczy.Business.Exceptions.DoctorCategoryExceptions;
using Doczy.Business.Exceptions.LanguageExceptions;
using Doczy.Business.Exceptions.UserExceprions;
using Doczy.Business.Services.Interfaces;
using Doczy.Core.Entities;
using Doczy.Core.Entities.Identities;
using Doczy.DataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Doczy.Business.Services.Implementations
{
    public class DoctorService : IDoctorService
    {
        private readonly UserManager<BaseAppUser> _userManager;
        private readonly IDoctorRepository _doctorRepository;
        private readonly ILanguageRepository _languageRepository;
        private readonly IDoctorLanguageRepository _doctorLanguageRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        private readonly IDoctorCategoryRepository _categoryRepository;
       
        public DoctorService(UserManager<BaseAppUser> userManager, IDoctorRepository doctorRepository, ILanguageRepository languageRepository, IDoctorLanguageRepository doctorLanguageRepository, IHttpContextAccessor httpContextAccessor, IMapper mapper, IDoctorCategoryRepository categoryRepository)
        {
            _userManager = userManager;
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
                                    StatusCode: HttpStatusCode.NoContent,
                                    Message: "Category successfully modified"
                                    );

        }

        public async Task<List<GetDoctorsDto>> GetFilterDoctors(GetDoctorFilterDto model)
        {
            var doctorsQuery = _doctorRepository.FindAll(u => true, // Initial condition to include all doctors
         tracking: false,
         d => d.Services,
         d => d.FavoriteDoctors,
         d => d.Ratings,
         d => d.Experiances,
         d => d.DoctorCategory
     );

            if (model.CategoryId != null)
            {
                doctorsQuery = doctorsQuery.Where(u => u.DoctorCategoryId == model.CategoryId);
            }

            if (!string.IsNullOrEmpty(model.FullName))
            {
                doctorsQuery = doctorsQuery.Where(u => u.FirstName.Contains(model.FullName) || u.LastName.Contains(model.FullName));
            }

            if (model.ServiceTypeId != null)
            {
                doctorsQuery = doctorsQuery.Where(u => u.Services.Any(s => s.ServiceTypeId == model.ServiceTypeId));
            }

            if (model.HospitalId != null)
            {
                doctorsQuery = doctorsQuery.Where(u => u.Experiances.Any(e => e.currentlyWorking && e.HospitalId == model.HospitalId));
            }

            if (model.MinPrice != null)
            {
                doctorsQuery = doctorsQuery.Where(u => u.Services.Any(s => s.Price >= model.MinPrice));
            }

            if (model.MaxPrice != null)
            {
                doctorsQuery = doctorsQuery.Where(u => u.Services.Any(s => s.Price <= model.MaxPrice));
            }

            var doctors = await doctorsQuery.ProjectTo<GetDoctorsDto>(_mapper.ConfigurationProvider).ToListAsync();

            return doctors;
        }

        public async Task<List<GetDoctorsDto>> GetDoctors()
        {
            var doctors = await _doctorRepository.GetAll(  tracking: false,
                                                           d => d.FavoriteDoctors,
                                                           d => d.Ratings,
                                                           d => d.DoctorCategory
                                                           ).ProjectTo<GetDoctorsDto>(_mapper.ConfigurationProvider)
                                                           .ToListAsync();
            return doctors;
        }
    }
}
