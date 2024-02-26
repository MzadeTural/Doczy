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
    public class WorkPlaceService : IWorkPlaceService
    {
        private readonly IWorkPlaceRepository _workPlacerepository;
        private readonly IWebHostEnvironment _environment;
        private readonly IFileService _fileService;
        private readonly IMapper _mapper;

        public WorkPlaceService(IWorkPlaceRepository workPlacerepository, IWebHostEnvironment environment, IFileService fileService, IMapper mapper)
        {
            _workPlacerepository = workPlacerepository;
            _environment = environment;
            _fileService = fileService;
            _mapper = mapper;
        }

        public async Task<ResponseDto> CreateWorkPlaceAsync(CreateWorkPlaceDto model)
        {
            bool isExist = await _workPlacerepository.IsExistAsync(s => s.Name == model.Name);
            if (isExist)
                throw new ServiceTypeAlreadyExistExceptions("Service type already exist");

            string file = await _fileService.CreateFileAsync(model.Icon, _environment.WebRootPath + "/uploads/workplaceicons/");

            var newWP = _mapper.Map<WorkPlace>(model);
            newWP.IconUrl = file;
            var result = await _workPlacerepository.CreateAsync(newWP);
            await _workPlacerepository.SaveAsync();

            return new ResponseDto(
                         StatusCode: result ? HttpStatusCode.Created : HttpStatusCode.BadRequest,
                         Message: result ? "Work Place  successfully created" : "Something went wrong"
                         );
        }
    }
}
