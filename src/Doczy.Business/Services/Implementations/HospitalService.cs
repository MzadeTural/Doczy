using AutoMapper;
using AutoMapper.QueryableExtensions;
using Doczy.Business.DTOs.Common;
using Doczy.Business.DTOs.HospitalDtos;
using Doczy.Business.DTOs.WorkPlace;
using Doczy.Business.Exceptions.HospitalExceptions;
using Doczy.Business.Services.Interfaces;
using Doczy.Core.Entities;
using Doczy.DataAccess.Repositories.Implementations;
using Doczy.DataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
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

        public async Task<ResponseDto> CreateHospitalAsync(CreateHospitalDto model)
        {
            bool isExist = await _hospitalracerepository.IsExistAsync(s => s.Name == model.Name);
            if (isExist)
                throw new HospitalAlreadyExistExceptions("Hospital type already exist");

            string file = await _fileService.CreateFileAsync(model.Icon, _environment.WebRootPath + "/uploads/hospitalicons/");

            var newWP = _mapper.Map<Hospital>(model);
            newWP.IconUrl = file;
            var result = await _hospitalracerepository.CreateAsync(newWP);
            await _hospitalracerepository.SaveAsync();

            return new ResponseDto(
                         StatusCode: result ? HttpStatusCode.Created : HttpStatusCode.BadRequest,
                         Message: result ? "Hospital  successfully created" : "Something went wrong"
                         );
        }

        public async Task<List<GetHospitalDto>> GetHospitalAsync()
        {
            return await _hospitalracerepository.GetAll().ProjectTo<GetHospitalDto>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public async Task<ResponseDto> UpdateHospitalAsync(Guid hospitalId,UpdateHospitalDto model)
        {
            var dbHospital = await _hospitalracerepository.GetByIdAsync(hospitalId);
            if (dbHospital is null)
                throw new HospitalNotFoundException();
            if (model.Name is not null)
                dbHospital.Name = model.Name;
            if (model.Icon is not null)
            {
                string file = await _fileService.CreateFileAsync(model.Icon, _environment.WebRootPath + "/uploads/hospitalicons/");
                dbHospital.IconUrl = file;
            }
            var result = _hospitalracerepository.Update(dbHospital);
            await _hospitalracerepository.SaveAsync();
            return new ResponseDto(
                StatusCode: result ? HttpStatusCode.OK : HttpStatusCode.BadRequest,
                Message: result ? "Hospital successfully Updated" : "Something went wrong");

            
        }
    }
}
