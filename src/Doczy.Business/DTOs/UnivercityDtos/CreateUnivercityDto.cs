using Microsoft.AspNetCore.Http;

namespace Doczy.Business.DTOs.UnivercityDtos
{
    public record CreateUnivercityDto(
     string? Name,
     string? IconUrl
  );
}
