using AutoMapper;
using Doczy.Business.DTOs.Common;
using Doczy.Business.DTOs.Language;
using Doczy.Business.Exceptions.LanguageExceptions;
using Doczy.Business.Exceptions.ServiceTypeExceptions;
using Doczy.Business.Services.Interfaces;
using Doczy.Core.Entities;
using Doczy.DataAccess.Repositories.Interfaces;
using System.Net;

namespace Doczy.Business.Services.Implementations
{
    public class LanguageService : ILanguageService
    {
        private readonly ILanguageRepository _languageRepository;
        private readonly IMapper _mapper;

        public LanguageService(ILanguageRepository languageRepository, IMapper mapper)
        {
            _languageRepository = languageRepository;
            _mapper = mapper;
        }

        public async Task<ResponseDto> CreateLanguageAsync(CreateLanguageDto model)
        {
            bool isExist = await _languageRepository.IsExistAsync(s => s.Name == model.Name);
            if (isExist)
                throw new LanguageAlreadyExistExceptions("Service type already exist");
            var newLanguage = _mapper.Map<Language>(model);
         
            var result = await _languageRepository.CreateAsync(newLanguage);
            await _languageRepository.SaveAsync();

            return new ResponseDto(
                         StatusCode: result ? HttpStatusCode.Created : HttpStatusCode.BadRequest,
                         Message: result ? "Language successfully created" : "Something went wrong"
                         );
        }
    }
}
