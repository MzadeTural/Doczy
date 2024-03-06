using System;
using System.Net;
using AutoMapper;
using Doczy.Business.DTOs.Common;
using Doczy.Business.DTOs.UnivercityDtos;
using Doczy.Business.Exceptions.LanguageExceptions;
using Doczy.Business.Exceptions.UnivercityExceptions;
using Doczy.Business.Services.Interfaces;
using Doczy.Core.Entities;
using Doczy.DataAccess.Repositories.Interfaces;

namespace Doczy.Business.Services.Implementations
{
	public class UnivercityService:IUnivercityService
	{
        private IMapper _mapper;
        private IUnivercityRepository _univercityRepository;

        public UnivercityService(IMapper mapper,IUnivercityRepository univercityRepository)
        {
            _mapper = mapper;
            _univercityRepository = univercityRepository;
        }

        public async Task<ResponseDto> CreateUnivercityAsync(CreateUnivercityDto model)
        {
            Univercity newUnivercity = _mapper.Map<Univercity>(model);
            var isExist = _univercityRepository.FindAll(x => x.Name == newUnivercity.Name).FirstOrDefault();

            if(isExist == null)
            {
                var result = await _univercityRepository.CreateAsync(newUnivercity);
                await _univercityRepository.SaveAsync();
                return new ResponseDto(
                         StatusCode: result ? HttpStatusCode.Created : HttpStatusCode.BadRequest,
                         Message: result ? "Univercity successfully created" : "Something went wrong"
                         );
            }
            else throw new UnivercityAlreadyExistExceptions("Univercity name is exist");
        }

        public async Task<ResponseDto> UpdateUnivercityAsync(Guid id, UpdateUnivercityDto model)
        {
            var db = await _univercityRepository.GetSingleAysnc(x => x.Id == id);
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
                         StatusCode: result ? HttpStatusCode.Created : HttpStatusCode.BadRequest,
                         Message: result ? "Univercity successfully updated" : "Something went wrong"
                         );
        }

        public async Task<ResponseDto> DeleteUnivercityAsync(Guid id)
        {
            var db = await _univercityRepository.GetSingleAysnc(x => x.Id == id);
            if (db is null) throw new UnivercityAlreadyExistExceptions("Univercity is not found");
            _univercityRepository.SoftDelete(db);
            var result = _univercityRepository.Update(db);
            await _univercityRepository.SaveAsync();
            return new ResponseDto(
                         StatusCode: result ? HttpStatusCode.Created : HttpStatusCode.BadRequest,
                         Message: result ? "Univercity successfully deleted" : "Something went wrong"
                         );
        }

        
    }
}

