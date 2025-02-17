using Auth.Domain.Entities;

namespace AuthMicroservice.API.Services
{
    public interface IJwtService
    {
        string GenerateJwtToken(User user, IList<string> roles);
    }
}
