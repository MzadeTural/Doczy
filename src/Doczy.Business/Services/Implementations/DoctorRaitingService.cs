using AutoMapper;
using Doczy.Business.DTOs.Common;
using Doczy.Business.DTOs.RaitingDtos;
using Doczy.Business.Services.Interfaces;
using Doczy.Core.Entities;
using Doczy.Core.Entities.Identities;
using Doczy.DataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Net;

namespace Doczy.Business.Services.Implementations
{
    public class DoctorRaitingService : IDoctorRaitingService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<BaseAppUser> _userManager;
        private readonly IDoctorRatingRepository _ratingRepository;
        private readonly IMapper _mapper;

        public DoctorRaitingService(IHttpContextAccessor httpContextAccessor, UserManager<BaseAppUser> userManager, IDoctorRatingRepository ratingRepository, IMapper mapper)
        {
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _ratingRepository = ratingRepository;
            _mapper = mapper;
        }

        public async Task<ResponseDto> CreateRaitingDoctor(CreateRaitingDto model)
        {
            Guid patientId = (await _userManager.GetUserAsync(_httpContextAccessor?.HttpContext?.User)).Id;
            ArgumentNullException.ThrowIfNull(patientId);
            DoctorRating raiting = _mapper.Map<DoctorRating>(model);
            raiting.PatientId = patientId;
            raiting.Date=DateTime.UtcNow;
            bool result = await _ratingRepository.CreateAsync(raiting);
            await _ratingRepository.SaveAsync();
            return new ResponseDto(
                       StatusCode: result ? HttpStatusCode.Created : HttpStatusCode.BadRequest,
                       Message: result ? "Raiting successfully created" : "Something went wrong"
                       );
        }
    }
}
