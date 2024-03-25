using AutoMapper;
using AutoMapper.QueryableExtensions;
using Doczy.Business.DTOs.Common;
using Doczy.Business.DTOs.DoctorCategoryDtos;
using Doczy.Business.DTOs.Experiance;
using Doczy.Business.Services.Interfaces;
using Doczy.Core.Entities;
using Doczy.DataAccess.Repositories.Implementations;
using Doczy.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Doczy.Business.Services.Implementations
{
    public class DoctorCategoryService : IDoctorCategoryService
    {
        private readonly IDoctorCategoryRepository _doctorCategoryRepository;
        private readonly IMapper _mapper;


        public DoctorCategoryService(IDoctorCategoryRepository doctorCategoryRepository, IMapper mapper)
        {
            _doctorCategoryRepository = doctorCategoryRepository;
            _mapper = mapper;
        }

        public async Task<ResponseDto> CreateCategoryAsync(CreateDoctorCategoryDto model)
        {
            DoctorCategory newCategory=_mapper.Map<DoctorCategory>(model);
            var result = await _doctorCategoryRepository.CreateAsync(newCategory);
           await _doctorCategoryRepository.SaveAsync();

            return new ResponseDto(
                         StatusCode: result ? HttpStatusCode.Created : HttpStatusCode.BadRequest,
                         Message: result ? "Category  successfully created" : "Something went wrong"
                         );
        }

        public async Task<List<GetDoctorCategoryDto>> GetCategoryAsync()
        {
            var categories = await _doctorCategoryRepository.FindAll(dc=>!dc.IsDeleted)
                                                           .ProjectTo<GetDoctorCategoryDto>(_mapper.ConfigurationProvider)
                                                            .ToListAsync();
            return categories; 
        }
    }
}
