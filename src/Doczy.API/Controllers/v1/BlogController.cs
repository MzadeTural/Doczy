using System.Net;
using Doczy.Business.DTOs.BlogDtos;
using Doczy.Business.DTOs.Common;
using Doczy.Business.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Doczy.API.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private IBlogService _blogService;

        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }
        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        public async Task<IActionResult> GetFieldOfStudy(Guid id)
        {
            var response = await _blogService.GetBlogAsync(id);
            return Ok(response);
        }
        [HttpGet("")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Doctor,Admin,Patient")]
        public async Task<IActionResult> GetFieldOfStudy()
        {
            var response = await _blogService.GetAllBlogAsync();
            return Ok(response);
        }
        [HttpPost("")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        public async Task<IActionResult> CreateFieldOfStudy(CreateBlogDto createBlogDto)
        {
            var response = await _blogService.CreateBlogAsync(createBlogDto);
            return StatusCode((int)HttpStatusCode.Created, new ResponseDto(response.StatusCode, response.Message));
        }
        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        public async Task<IActionResult> UpdateFieldOfStudy(Guid id, UpdateBlogDto updateBlogDto)
        {
            var response = await _blogService.UpdateBlog(id, updateBlogDto);
            return StatusCode((int)HttpStatusCode.OK, new ResponseDto(response.StatusCode, response.Message));
        }
        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        public async Task<IActionResult> DeleteFieldOfStudy(Guid id)
        {
            var response = await _blogService.DeleteBlog(id);
            return StatusCode((int)HttpStatusCode.OK, new ResponseDto(response.StatusCode, response.Message));
        }
    }
}
