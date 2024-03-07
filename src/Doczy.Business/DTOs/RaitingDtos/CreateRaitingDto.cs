using Doczy.Core.Entities.Identities;

namespace Doczy.Business.DTOs.RaitingDtos
{
    public record CreateRaitingDto
    (
         int Rating,// Rating out of 5
         string Review ,
         Guid DoctorId 
       
        
    );
}
