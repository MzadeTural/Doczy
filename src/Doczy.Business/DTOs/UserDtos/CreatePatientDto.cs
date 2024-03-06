namespace Doczy.Business.DTOs.UserDtos
{
    public record CreatePatientDto
    (
         string? FirstName,
         string? LastName,
         string Email,
         string? Password
    );
}

