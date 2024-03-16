using AutoMapper;
using AutoMapper.QueryableExtensions;
using Doczy.Business.DTOs.Common;
using Doczy.Business.DTOs.ServiceDtos;
using Doczy.Business.Exceptions.UserExceprions;
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
    public class ServiceService : IServiceService
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<BaseAppUser> _userManager;

        public ServiceService(IServiceRepository serviceRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor, UserManager<BaseAppUser> userManager)
        {
            _serviceRepository = serviceRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

        public async Task<ResponseDto> CreateServiceAsync(CreateServiceDto model)
        {

            var doctorId = (await _userManager.GetUserAsync(_httpContextAccessor?.HttpContext?.User)).Id;
            if (model.Price <= 0)
                throw new ArgumentOutOfRangeException("Price",
                        "The value must be greater than 0.0");
            var newService = _mapper.Map<Service>(model);
            newService.DoctorId = doctorId;


            var result = await _serviceRepository.CreateAsync(newService);
            await _serviceRepository.SaveAsync();

            return new ResponseDto(
                         StatusCode: result ? HttpStatusCode.Created : HttpStatusCode.BadRequest,
                         Message: result ? "Service successfully created" : "Something went wrong"
                         );
        }

        public async Task<List<GetServiceDto>> GetServiceAsync(Guid doctorId)
        {
            var isExist=_userManager.FindByIdAsync(doctorId.ToString());    
            if(isExist is null)
                throw new UserNotFoundException("DoctorId", $"{doctorId}");
            ArgumentNullException.ThrowIfNull(doctorId);
            var services = await _serviceRepository.FindAll(s => s.DoctorId == doctorId,
                                                     tracking: true, s => s.ServiceType)
                                                      .ProjectTo<GetServiceDto>(_mapper.ConfigurationProvider).ToListAsync();

            return services;
        }
    }
}
