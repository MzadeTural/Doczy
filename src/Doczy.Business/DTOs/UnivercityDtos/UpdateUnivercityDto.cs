using Microsoft.AspNetCore.Http;

namespace Doczy.Business.DTOs.UnivercityDtos
{
    public record UpdateUnivercityDto(
     string? Name,
     string? IconUrl
  );
}
