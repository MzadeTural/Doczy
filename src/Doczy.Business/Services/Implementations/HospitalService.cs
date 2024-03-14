using AutoMapper;
using Doczy.Business.DTOs.Common;
using Doczy.Business.DTOs.WorkPlace;
using Doczy.Business.Exceptions.ServiceTypeExceptions;
using Doczy.Business.Services.Interfaces;
using Doczy.Core.Entities;
using Doczy.DataAccess.Repositories.Implementations;
using Doczy.DataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Hosting;
using System.Net;

namespace Doczy.Business.Services.Implementations
{
    public class HospitalService : IHospitalService
    {
        private readonly IHospitalRepository _hospitalracerepository;
        private readonly IWebHostEnvironment _environment;
        private readonly IFileService _fileService;
        private readonly IMapper _mapper;

        public HospitalService(IHospitalRepository workPlacerepository, IWebHostEnvironment environment, IFileService fileService, IMapper mapper)
        {
            _hospitalracerepository = workPlacerepository;
            _environment = environment;
            _fileService = fileService;
            _mapper = mapper;
        }

        public async Task<ResponseDto> CreateWorkPlaceAsync(CreateHospitalDto model)
        {
            bool isExist = await _hospitalracerepository.IsExistAsync(s => s.Name == model.Name);
            if (isExist)
                throw new ServiceTypeAlreadyExistExceptions("Service type already exist");

            string file = await _fileService.CreateFileAsync(model.Icon, _environment.WebRootPath + "/uploads/workplaceicons/");

            var newWP = _mapper.Map<Hospital>(model);
            newWP.IconUrl = file;
            var result = await _hospitalracerepository.CreateAsync(newWP);
            await _hospitalracerepository.SaveAsync();

            return new ResponseDto(
                         StatusCode: result ? HttpStatusCode.Created : HttpStatusCode.BadRequest,
                         Message: result ? "Hospital  successfully created" : "Something went wrong"
                         );
        }
    }
}
