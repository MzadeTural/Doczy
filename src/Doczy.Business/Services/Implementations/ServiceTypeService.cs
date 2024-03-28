using AutoMapper;
using AutoMapper.QueryableExtensions;
using Doczy.Business.DTOs.Common;
using Doczy.Business.DTOs.ServiceTypeDtos;
using Doczy.Business.Exceptions.ExperianceExceptions;
using Doczy.Business.Exceptions.FileExceptions;
using Doczy.Business.Exceptions.HospitalExceptions;
using Doczy.Business.Exceptions.ServiceTypeExceptions;
using Doczy.Business.Services.Interfaces;
using Doczy.Core.Entities;
using Doczy.DataAccess.Migrations;
using Doczy.DataAccess.Repositories.Implementations;
using Doczy.DataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System.Net;


namespace Doczy.Business.Services.Implementations
{
    public class ServiceTypeService : IServiceTypeService
    {
        private readonly IServiceTypeRepository _serviceTypeRepository;
        private readonly IFileService _fileService;
        private readonly IWebHostEnvironment _environment;
        private readonly IMapper _mapper;

        public ServiceTypeService(IServiceTypeRepository serviceTypeRepository, IFileService fileService, IWebHostEnvironment environment, IMapper mapper)
        {
            _serviceTypeRepository = serviceTypeRepository;
            _fileService = fileService;
            _environment = environment;
            _mapper = mapper;
        }

        public async Task<ResponseDto> CreateServiceTypeAsync(CreateServiceTypeDto model)
        {
            bool isExist = await _serviceTypeRepository.IsExistAsync(s => s.Name == model.Name);
            if (isExist)
                throw new ServiceTypeAlreadyExistExceptions("Service type already exist");

            string file = await _fileService.CreateFileAsync(model.Icon, _environment.WebRootPath + "/uploads/servicetypeicons/");

            var newServiceType = _mapper.Map<ServiceType>(model);
            newServiceType.IconUrl = file;
            var result = await _serviceTypeRepository.CreateAsync(newServiceType);
            await _serviceTypeRepository.SaveAsync();

            return new ResponseDto(
                         StatusCode: result ? HttpStatusCode.Created : HttpStatusCode.BadRequest,
                         Message: result ? "Service type successfully created" : "Something went wrong"
                         );
        }

        public async Task<List<GetServiceTypeDto>> GetServiceTypeAsync()
        {
            return await _serviceTypeRepository.FindAll(st=>!st.IsDeleted,tracking:false).ProjectTo<GetServiceTypeDto>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public async Task<ResponseDto> UpdateServiceTypeAsync(Guid id, UpdateServiceTypeDto model)
        {
            var dbType = await _serviceTypeRepository.GetSingleAysnc(e => e.Id == id && !e.IsDeleted);
            if (dbType is null)
                throw new ServiceTypeNotFoundException();
            dbType.Name = model.Name ?? dbType.Name;
            if (model.Icon is not null)
            {
                string file = await _fileService.CreateFileAsync(model.Icon, _environment.WebRootPath + "/uploads/servicetypeicons/");
                if (!string.IsNullOrEmpty(file))
                {
                    _fileService.DeteleFile(_environment.WebRootPath + $"/uploads/servicetypeicons/{dbType.IconUrl}");
                    dbType.IconUrl = file;
                }
                else
                {
                    throw new FileCreationFailureException("file creation failure");
                }
            }
            bool result = _serviceTypeRepository.Update(dbType);
           await _serviceTypeRepository.SaveAsync();

            return new ResponseDto(
                          StatusCode: result ? HttpStatusCode.NoContent : HttpStatusCode.BadRequest,
                          Message: result ? "Service type successfully updated" : "Something went wrong"
                          );
        }
    }
}
