using AuthMicroservice.Domain.Entities;

namespace AuthService.Api.Services
{
    public interface IJwtService
    {
        string GenerateJwtToken(User user, IList<string> roles);
    }
}
