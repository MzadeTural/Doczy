using System.Net;
using AutoMapper;
using Doczy.Business.DTOs.BlogDtos;
using Doczy.Business.DTOs.Common;
using Doczy.Business.Exceptions.BlogExceptions;
using Doczy.Business.Services.Interfaces;
using Doczy.Core.Entities;
using Doczy.Core.Entities.Identities;
using Doczy.DataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Doczy.Business.Services.Implementations
{
	public class BlogService:IBlogService
	{
        private IMapper _mapper;
        private IBlogRepository _blogRepository;
        private readonly UserManager<BaseAppUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public BlogService(IMapper mapper, IBlogRepository blogRepository, UserManager<BaseAppUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper;
            _blogRepository = blogRepository;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<List<GetBlogDto>> GetAllBlogAsync()
        {
            var dbFields = await _blogRepository.FindAll(x => !x.IsDeleted).ToListAsync();
            return _mapper.Map<List<GetBlogDto>>(dbFields);
        }

        public async Task<GetBlogDto> GetBlogAsync(Guid id)
        {
            var field = await _blogRepository.GetSingleAysnc(x => x.Id == id && !x.IsDeleted);
            if (field is null) throw new BlogNotFoundExceptions("Field is not found");
            var model = _mapper.Map<GetBlogDto>(field);
            return model;
        }

        public async Task<ResponseDto> CreateBlogAsync(CreateBlogDto createBlog)
        {
            var userId = (await _userManager.GetUserAsync(_httpContextAccessor?.HttpContext?.User)).Id;
            var newBlod = _mapper.Map<Blog>(createBlog);
            newBlod.DoctorId=userId;
            var result = await _blogRepository.CreateAsync(newBlod);
            await _blogRepository.SaveAsync();
            return new ResponseDto(
                StatusCode: result ? HttpStatusCode.Created : HttpStatusCode.BadRequest,
                Message: result ? "Blog successfully created" : "Something went wrong");
        }

        public async Task<ResponseDto> UpdateBlog(Guid id, UpdateBlogDto updateBlog)
        {
            var dbField = await _blogRepository.GetSingleAysnc(x => x.Id == id && !x.IsDeleted);
            if (dbField is null) throw new BlogNotFoundExceptions("Field is not found");

            if (updateBlog.Title != null)
            {
                dbField.Title = updateBlog.Title;
            }
            if (updateBlog.Content != null)
            {
                dbField.Content = updateBlog.Content;
            }
            if (updateBlog.Subtitle != null)
            {
                dbField.Subtitle = updateBlog.Subtitle;
            }
            if (updateBlog.DoctorId != null)
            {
                dbField.DoctorId = updateBlog.DoctorId;
            }
            var result = _blogRepository.Update(dbField);
            await _blogRepository.SaveAsync();
            return new ResponseDto(
                         StatusCode: result ? HttpStatusCode.OK : HttpStatusCode.BadRequest,
                         Message: result ? "Blog successfully updated" : "Something went wrong"
                         );
        }

        public async Task<ResponseDto> DeleteBlog(Guid id)
        {
            var dbField = await _blogRepository.GetSingleAysnc(x => x.Id == id && !x.IsDeleted);
            if (dbField is null) throw new BlogNotFoundExceptions("Blog is not found");
            var result = _blogRepository.SoftDelete(dbField);
            await _blogRepository.SaveAsync();
            return new ResponseDto(
                         StatusCode: result ? HttpStatusCode.OK : HttpStatusCode.BadRequest,
                         Message: result ? "Blog successfully deleted" : "Something went wrong"
                         );
        }
    }
}

