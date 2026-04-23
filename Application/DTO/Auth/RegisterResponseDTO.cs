using Application.DTO.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.Auth
{
    public record RegisterResponseDTO(
       string Message,
       UserResponseDto User
   );

}
