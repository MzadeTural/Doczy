using AutoMapper;
using AutoMapper.QueryableExtensions;
using Doczy.Business.DTOs.Common;
using Doczy.Business.DTOs.HospitalDtos;
using Doczy.Business.DTOs.WorkPlace;
using Doczy.Business.Exceptions.HospitalExceptions;
using Doczy.Business.Exceptions.UnivercityExceptions;
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
        private readonly IHospitalRepository _hospitalrepository;
        private readonly IWebHostEnvironment _environment;
        private readonly IFileService _fileService;
        private readonly IMapper _mapper;

        public HospitalService(IHospitalRepository workPlacerepository, IWebHostEnvironment environment, IFileService fileService, IMapper mapper)
        {
            _hospitalrepository = workPlacerepository;
            _environment = environment;
            _fileService = fileService;
            _mapper = mapper;
        }

        public async Task<ResponseDto> CreateHospitalAsync(CreateHospitalDto model)
        {
            bool isExist = await _hospitalrepository.IsExistAsync(s => s.Name.ToLower().Trim() == model.Name.ToLower().Trim());
            if (isExist)
                throw new HospitalAlreadyExistExceptions("Hospital  already exist");

            string file = await _fileService.CreateFileAsync(model.Icon, _environment.WebRootPath + "/uploads/hospitalicons/");

            var newWP = _mapper.Map<Hospital>(model);
            newWP.IconUrl = file;
            var result = await _hospitalrepository.CreateAsync(newWP);
            await _hospitalrepository.SaveAsync();

            return new ResponseDto(
                         StatusCode: result ? HttpStatusCode.Created : HttpStatusCode.BadRequest,
                         Message: result ? "Hospital  successfully created" : "Something went wrong"
            );
        }

        public async Task<ResponseDto> DeleteHospitalAsync(Guid hospitalId)
        {
            var db = await _hospitalrepository.GetSingleAysnc(x => x.Id == hospitalId && !x.IsDeleted);
            if (db is null) throw new HospitalNotFoundByIdException(hospitalId);
            _hospitalrepository.SoftDelete(db);
            var result = _hospitalrepository.Update(db);
            await _hospitalrepository.SaveAsync();
            return new ResponseDto(
                         StatusCode: result ? HttpStatusCode.OK : HttpStatusCode.BadRequest,
                         Message: result ? "Hospital successfully deleted" : "Something went wrong"
                         );
            
        }

        public async Task<List<GetHospitalDto>> GetHospitalAsync()
        {
            return await _hospitalrepository.GetAll().ProjectTo<GetHospitalDto>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public async Task<GetHospitalDto> GetHospitalByIdAsync(Guid hospitalId)
        {
            var isExist = await _hospitalrepository.IsExistAsync(h => h.Id == hospitalId && !h.IsDeleted);
            if (!isExist)
                throw new HospitalNotFoundByIdException(hospitalId);

            
             var dbHospital= await _hospitalrepository.GetByIdAsync(hospitalId);
            var hospital=_mapper.Map<GetHospitalDto>(dbHospital);
            return hospital;
        }

        public async Task<ResponseDto> UpdateHospitalAsync(Guid hospitalId, UpdateHospitalDto model)
        {
            var dbHospital = await _hospitalrepository.GetSingleAysnc(h => h.Id == hospitalId && !h.IsDeleted);
            if (dbHospital is null)
                throw new HospitalNotFoundException();
            bool isExist = await _hospitalrepository.IsExistAsync(s => s.Name.ToLower().Trim() == model.Name.ToLower().Trim());
            if (isExist)
                throw new HospitalAlreadyExistExceptions("Hospital  already exist");
            if (model.Name is not null)
                dbHospital.Name = model.Name;
            if (model.Icon is not null)
            {
                string file = await _fileService.CreateFileAsync(model.Icon, _environment.WebRootPath + "/uploads/hospitalicons/");
                _fileService.DeteleFile(_environment.WebRootPath + $"/uploads/hospitalicons/{dbHospital.IconUrl}");
                dbHospital.IconUrl = file;
            }
            var result = _hospitalrepository.Update(dbHospital);
            await _hospitalrepository.SaveAsync();
            return new ResponseDto(
                StatusCode: result ? HttpStatusCode.NoContent : HttpStatusCode.BadRequest,
                Message: result ? "Hospital successfully Updated" : "Something went wrong");


        }
    }
}
