using AutoMapper;
using AutoMapper.QueryableExtensions;
using Doczy.Business.DTOs.Common;
using Doczy.Business.DTOs.GenderDtos;
using Doczy.Business.DTOs.SliderDtos;
using Doczy.Business.Exceptions.EducationExceptions;
using Doczy.Business.Exceptions.SliderExceptions;
using Doczy.Business.Services.Interfaces;
using Doczy.Core.Entities;
using Doczy.DataAccess.Repositories.Implementations;
using Doczy.DataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Doczy.Business.Services.Implementations
{
    public class SliderService : ISliderService
    {
        private readonly ISliderRepository _sliderRepository;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;
        private readonly IWebHostEnvironment _environment;
        public SliderService(ISliderRepository sliderRepository, IMapper mapper, IFileService fileService, IWebHostEnvironment environment)
        {
            _sliderRepository = sliderRepository;
            _mapper = mapper;
            _fileService = fileService;
            _environment = environment;
        }

        public async Task<ResponseDto> CreateAsync(CreateSliderDto model)
        {
           
            string file = await _fileService.CreateFileAsync(model.ImageUrls, _environment.WebRootPath + "/uploads/slides/");
            var slide = new Slider()
            {
                ImageUrl = file
            };
            var result = await _sliderRepository.CreateAsync(slide);
            await _sliderRepository.SaveAsync();
            return new ResponseDto(
                        StatusCode: result ? HttpStatusCode.OK : HttpStatusCode.BadRequest,
                        Message: result ? "Slider successfully created" : "Something went wrong"
                        );
        }

        public async Task<ResponseDto> Delete(Guid id)
        {
            var dbSlide = await _sliderRepository.GetSingleAysnc(x => x.Id == id && !x.IsDeleted);
            if (dbSlide is null) throw new SlideNotFoundByIdException(id);
            var result = _sliderRepository.SoftDelete(dbSlide);
            await _sliderRepository.SaveAsync();
            return new ResponseDto(
                StatusCode: result ? HttpStatusCode.OK : HttpStatusCode.BadRequest,
                Message: result ? "Slide successfully Deleted" : "Something went wrong");
        }

        public async Task<List<GetSliderDto>> GetAsync()
        {
            var slides = await _sliderRepository
                                       .FindAll(s => !s.IsDeleted, tracking: false)
                                       .ProjectTo<GetSliderDto>(_mapper.ConfigurationProvider)
                                       .ToListAsync();
            return slides;
        }
    }
}
