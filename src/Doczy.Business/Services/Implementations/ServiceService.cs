using AutoMapper;
using Doczy.Business.DTOs.Common;
using Doczy.Business.DTOs.ServiceDtos;
using Doczy.Business.Services.Interfaces;
using Doczy.Core.Entities;
using Doczy.DataAccess.Repositories.Implementations;
using Doczy.DataAccess.Repositories.Interfaces;
using Org.BouncyCastle.Asn1.Ocsp;
using System.Net;

namespace Doczy.Business.Services.Implementations
{
    public class ServiceService : IServiceService
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly IMapper _mapper;

        public ServiceService(ServiceRepository serviceRepository, IMapper mapper)
        {
            _serviceRepository = serviceRepository;
            _mapper = mapper;
        }

        public async Task<ResponseDto> CreateServiceAsync(CreateServiceDto model)
        {
            if (model.Price <= 0)
                throw new ArgumentOutOfRangeException("Price",
                        "The value must be greater than 0.0");
            var newService = _mapper.Map<Service>(model);
            var result = await _serviceRepository.CreateAsync(newService);
            await _serviceRepository.SaveAsync();

            return new ResponseDto(
                         StatusCode: result ? HttpStatusCode.Created : HttpStatusCode.BadRequest,
                         Message: result ? "Service successfully created" : "Something went wrong"
                         );
        }
    }
}
