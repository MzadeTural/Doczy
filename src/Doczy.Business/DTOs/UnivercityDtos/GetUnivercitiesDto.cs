using System;
namespace Doczy.Business.DTOs.UnivercityDtos
{
    public record GetUnivercityDto(
         Guid Id ,
    string? Name,
     string? IconUrl
  );
}

