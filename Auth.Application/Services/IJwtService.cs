using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Auth.Application.DTO;
using Auth.Domain.Entities;

namespace Auth.Application.Services
{
   public  interface IJwtService
    {
        JwtResponse GenerateJwtToken(User user, IList<string> roles);
        Task<TokenValidationResultDTO> ValidateRefreshToken(string refreshToken);

    }
}
