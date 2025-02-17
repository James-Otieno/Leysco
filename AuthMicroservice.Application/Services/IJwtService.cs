using AuthMicroservice.Application.DTO;
using AuthMicroservice.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AuthService.Api.Services
{
    public interface IJwtService
    {
        JwtResponse GenerateJwtToken(User user, IList<string> roles);
        Task<TokenValidationResultDTO> ValidateRefreshToken(string refreshToken);
    }
}
