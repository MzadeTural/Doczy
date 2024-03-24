using System.Net;
using AutoMapper;
using Doczy.Business.DTOs.Common;
using Doczy.Business.DTOs.UnivercityDtos;
using Doczy.Business.Exceptions.UnivercityExceptions;
using Doczy.Business.Services.Interfaces;
using Doczy.Core.Entities;
using Doczy.DataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace Doczy.Business.Services.Implementations
{
    public class UnivercityService : IUnivercityService
	{
        private IMapper _mapper;
        private IUnivercityRepository _univercityRepository;
        private readonly IFileService _fileService;
        private readonly IWebHostEnvironment _environment;

        public UnivercityService(IMapper mapper, IUnivercityRepository univercityRepository, IFileService fileService, IWebHostEnvironment environment)
        {
            _mapper = mapper;
            _univercityRepository = univercityRepository;
            _fileService = fileService;
            _environment = environment;
        }

        public async Task<List<GetUnivercityDto>> GetUnivercitiesAsync()
        {
            var dbUnivercities = await _univercityRepository.FindAll(x=>!x.IsDeleted).ToListAsync();
            List<GetUnivercityDto> model = _mapper.Map<List<GetUnivercityDto>>(dbUnivercities);
            return model;
        }

        public async Task<GetUnivercityDto> GetUnivercityAsync(Guid id)
        {
            var db = await _univercityRepository.GetSingleAysnc(x => x.Id == id && !x.IsDeleted);
            if (db is null) throw new UnivercityAlreadyExistExceptions("Univercity is not found");
            GetUnivercityDto model = _mapper.Map<GetUnivercityDto>(db);
            return model;
        }

        public async Task<ResponseDto> CreateUnivercityAsync(CreateUnivercityDto model)
        {
            Univercity newUnivercity = _mapper.Map<Univercity>(model);

            var isExist = await _univercityRepository.GetSingleAysnc(x => x.Name
                                                              .Trim()
                                                              .ToLower() ==
                                                              newUnivercity.Name.
                                                              Trim()
                                                              .ToLower()
                                                              && !x.IsDeleted);
                                                             

            if (isExist != null)
                throw new UnivercityAlreadyExistExceptions("Univercity name is exist");

            var result = await _univercityRepository.CreateAsync(newUnivercity);
                await _univercityRepository.SaveAsync();
                return new ResponseDto(
                         StatusCode: result ? HttpStatusCode.Created : HttpStatusCode.BadRequest,
                         Message: result ? "Univercity successfully created" : "Something went wrong"
                         );
            
        }

        public async Task<ResponseDto> UpdateUnivercity(Guid id, UpdateUnivercityDto model)
        {
            var db = await _univercityRepository.GetSingleAysnc(x => x.Id == id && !x.IsDeleted);
            if(db is null) throw new UnivercityAlreadyExistExceptions("Univercity is not found");
            
            Univercity univercity = _mapper.Map<Univercity>(model);
            if(univercity.Name != null)
            {
                db.Name = univercity.Name;
            }
            if (univercity.IconUrl != null)
            {
                db.IconUrl = univercity.IconUrl;
            }
            var result = _univercityRepository.Update(db);
            await _univercityRepository.SaveAsync();
            return new ResponseDto(
                         StatusCode: result ? HttpStatusCode.OK : HttpStatusCode.BadRequest,
                         Message: result ? "Univercity successfully updated" : "Something went wrong"
                         );
        }

        public async Task<ResponseDto> DeleteUnivercity(Guid id)
        {
            var db = await _univercityRepository.GetSingleAysnc(x => x.Id == id && !x.IsDeleted);
            if (db is null) throw new UnivercityAlreadyExistExceptions("Univercity is not found");
            _univercityRepository.SoftDelete(db);
            var result = _univercityRepository.Update(db);
            await _univercityRepository.SaveAsync();
            return new ResponseDto(
                         StatusCode: result ? HttpStatusCode.OK : HttpStatusCode.BadRequest,
                         Message: result ? "Univercity successfully deleted" : "Something went wrong"
                         );
        }

        
    }
}

