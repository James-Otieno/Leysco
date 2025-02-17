using AuthMicroservice.Application.DTO;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;
using AuthMicroservice.Domain.Entities;
using AuthMicroservice.Application.Query.RefreshToken;
using AuthService.Api.Services;
using Microsoft.Extensions.Options;
using AuthService.Api.Config;

namespace AuthMicroservice.Application.Queries.RefreshToken
{
    public class RefreshTokenQueryHandler : IRequestHandler<RefreshTokenQuery, RefreshTokenResponseDTO>
    {
        private readonly UserManager<User> _userManager;
        private readonly IJwtService _jwtService;
        private readonly JwtConfig _jwtConfig;

        public RefreshTokenQueryHandler(UserManager<User> userManager, IJwtService jwtService, IOptions<JwtConfig> jwtConfig)
        {
            _userManager = userManager;
            _jwtService = jwtService;
            _jwtConfig = jwtConfig.Value;
        }

        public async Task<RefreshTokenResponseDTO> Handle(RefreshTokenQuery request, CancellationToken cancellationToken)
        {
            var validationResult = await _jwtService.ValidateRefreshToken(request.RefreshToken);

            if (!validationResult.IsValid)
            {
                return new RefreshTokenResponseDTO
                {
                    Success = false,
                    Errors = validationResult.Errors
                };
            }

            var user = await _userManager.FindByIdAsync(validationResult.UserId);
            if (user == null)
            {
                return new RefreshTokenResponseDTO
                {
                    Success = false,
                    Errors = new List<string> { "User not found." }
                };
            }

            var roles = await _userManager.GetRolesAsync(user);

            var newTokenResponse = _jwtService.GenerateJwtToken(user, roles);

            return new RefreshTokenResponseDTO
            {
                Success = true,
                Token = newTokenResponse.Token,
                Expiration = newTokenResponse.Expiration
            };
        }
    }
}
