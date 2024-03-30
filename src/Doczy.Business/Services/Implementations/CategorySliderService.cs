using AutoMapper;
using AutoMapper.QueryableExtensions;
using Doczy.Business.DTOs.CategorySliderDtos;
using Doczy.Business.DTOs.Common;
using Doczy.Business.DTOs.GenderDtos;
using Doczy.Business.Exceptions.DoctorCategoryExceptions;
using Doczy.Business.Exceptions.GenderExceptions;
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
    public class CategorySliderService : ICategorySliderService
    {
        private readonly ICategorySliderRepository _categorySliderRepository;
        private readonly IMapper _mapper;
        private readonly IDoctorCategoryRepository _doctorCategoryRepository;
        private readonly IFileService _fileService;
        private readonly IWebHostEnvironment _environment;

        public CategorySliderService(ICategorySliderRepository categorySliderRepository, IMapper mapper, IDoctorCategoryRepository doctorCategoryRepository, IFileService fileService, IWebHostEnvironment environment)
        {
            _categorySliderRepository = categorySliderRepository;
            _mapper = mapper;
            _doctorCategoryRepository = doctorCategoryRepository;
            _fileService = fileService;
            _environment = environment;
        }

        public async Task<ResponseDto> CreateAsync(CreateCategorySliderDto model)
        {
            var isExist = await _categorySliderRepository.IsExistAsync(g => g.Title == model.Title);
            var isExistCat = await _doctorCategoryRepository.IsExistAsync(g => g.Id == model.CategoryId);
            if (!isExistCat)
                throw new CategoryNotFoundByIdException(model.CategoryId);
            if (isExist)
                throw new GenderAlreadyExistException($"{model.Title} already exist");
            string file = await _fileService.CreateFileAsync(model.IconUrl, _environment.WebRootPath + "/uploads/categoryslideicon/");
            var categorySlider = _mapper.Map<CategorySlider>(model);
            categorySlider.IconUrl = file;
            var result = await _categorySliderRepository.CreateAsync(categorySlider);
            await _categorySliderRepository.SaveAsync();
            return new ResponseDto(
                        StatusCode: result ? HttpStatusCode.OK : HttpStatusCode.BadRequest,
                        Message: result ? "Category slider successfully created" : "Something went wrong"
                        );
        }

        public async Task<ResponseDto> DeleteAsync(Guid id)
        {
            var db = await _categorySliderRepository.GetSingleAysnc(x => x.Id == id && !x.IsDeleted);
            if (db is null) throw new CategoryNotFoundByIdException(id);
            _categorySliderRepository.SoftDelete(db);
            var result = _categorySliderRepository.Update(db);
            await _categorySliderRepository.SaveAsync();
            return new ResponseDto(
                         StatusCode: result ? HttpStatusCode.OK : HttpStatusCode.BadRequest,
                         Message: result ? "Category slider successfully deleted" : "Something went wrong"
                         );
        }

        public async Task<List<GetCategorySliderDto>> GetAsync()
        {
            var slides = await _categorySliderRepository
                                         .FindAll(cs => !cs.IsDeleted, tracking: false)
                                         .ProjectTo<GetCategorySliderDto>(_mapper.ConfigurationProvider)
                                         .ToListAsync();
            return slides;
        }

        public async Task<GetCategorySliderDto> GetByIdAsync(Guid id)
        {
            var slide = await _categorySliderRepository
                                         .FindAll(cs => !cs.IsDeleted && cs.Id == id, tracking: false)
                                         .ProjectTo<GetCategorySliderDto>(_mapper.ConfigurationProvider)
                                         .FirstOrDefaultAsync();
            return slide;
        }
    }
}
