using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doczy.Business.DTOs.AuthDtos
{
    public record LoginResponseDto
    
        ( 
        TokenResponseDto? TokenResponse 
        );
    
}
