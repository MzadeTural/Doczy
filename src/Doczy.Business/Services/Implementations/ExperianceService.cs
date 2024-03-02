using AutoMapper;
using AutoMapper.QueryableExtensions;
using Doczy.Business.DTOs.Common;
using Doczy.Business.DTOs.Experiance;
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
    public class ExperianceService : IExperianceService
    {
        private readonly IExperianceRepository _experianceRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<BaseAppUser> _userManager;
        public ExperianceService(IExperianceRepository experianceRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor, UserManager<BaseAppUser> userManager)
        {
            _experianceRepository = experianceRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

        public async Task<ResponseDto> CreateExperianceAsync(CreateExperianceDto model)
        {
            var doctorId = (await _userManager.GetUserAsync(_httpContextAccessor?.HttpContext?.User)).Id;
            Experiance newExperiance = _mapper.Map<Experiance>(model);
            newExperiance.DoctorId = doctorId;
            var result = await _experianceRepository.CreateAsync(newExperiance);
            await _experianceRepository.SaveAsync();

            return new ResponseDto(
                         StatusCode: result ? HttpStatusCode.Created : HttpStatusCode.BadRequest,
                         Message: result ? "Experiance  successfully created" : "Something went wrong"
                         );

        }

        public async Task<List<GetExperianceDto>> GetExperiancesAsync()
        {
            var doctorId = (await _userManager.GetUserAsync(_httpContextAccessor?.HttpContext?.User)).Id;
            ArgumentNullException.ThrowIfNull(doctorId);

            var experiances = await _experianceRepository.FindAll(c => c.DoctorId == doctorId, tracking: false, e => e.WorkPlace)
                                                            .ProjectTo<GetExperianceDto>(_mapper.ConfigurationProvider)
                                                             .ToListAsync();
            return experiances;

        }
    }
}
