using System;
using Doczy.Business.DTOs.Common;
using Doczy.Business.DTOs.BlogDtos;

namespace Doczy.Business.Services.Interfaces
{
	public interface IBlogService
	{
        Task<ResponseDto> CreateBlogAsync(CreateBlogDto createBlog);
        Task<ResponseDto> UpdateBlog(Guid id, UpdateBlogDto updateBlog);
        Task<ResponseDto> DeleteBlog(Guid id);
        Task<List<GetBlogDto>> GetAllBlogAsync();
        Task<GetBlogDto> GetBlogAsync(Guid id);
    }
}

