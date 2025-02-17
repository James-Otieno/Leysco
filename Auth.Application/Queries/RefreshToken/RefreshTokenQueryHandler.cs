using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Auth.Application.Config;
using Auth.Application.DTO;
using Auth.Application.Services;
using Auth.Domain.Entities;
using MediatR;
using Microsoft.AspNet.Identity;
using Microsoft.Extensions.Options;

namespace Auth.Application.Queries.RefreshToken
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

        public async  Task<RefreshTokenResponseDTO> Handle(RefreshTokenQuery request, CancellationToken cancellationToken)
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
