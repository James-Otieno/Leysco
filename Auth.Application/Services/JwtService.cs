using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Auth.Application.Config;
using Auth.Application.DTO;
using Auth.Domain.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Auth.Application.Services
{
    public class JwtService : IJwtService
    {

        private readonly JwtConfig _jwtConfig;

        public JwtService(IOptions<JwtConfig> jwtConfig)
        {
            _jwtConfig = jwtConfig.Value;
        }

        public JwtResponse GenerateJwtToken(User user, IList<string> roles)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("FirstName", user.FirstName),
                new Claim("LastName", user.LastName)
            };

            // Add roles to the claims
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfig.Secret));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwtConfig.Issuer,
                audience: _jwtConfig.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtConfig.ExpirationInMinutes),
                signingCredentials: credentials
            );

            return new JwtResponse
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = token.ValidTo
            };
        }

        public async Task<TokenValidationResultDTO> ValidateRefreshToken(string refreshToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_jwtConfig.Secret);

            try
            {
                var validationParams = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidIssuer = _jwtConfig.Issuer,
                    ValidateAudience = true,
                    ValidAudience = _jwtConfig.Audience,
                    ValidateLifetime = false // Token expiration handled separately
                };

                var principal = tokenHandler.ValidateToken(refreshToken, validationParams, out var validatedToken);

                if (validatedToken is JwtSecurityToken jwtSecurityToken &&
                    jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                {
                    var expiryClaim = principal.FindFirstValue(JwtRegisteredClaimNames.Exp);

                    if (expiryClaim != null && long.TryParse(expiryClaim, out var expiryTime))
                    {
                        var expiryDate = DateTimeOffset.FromUnixTimeSeconds(expiryTime).UtcDateTime;

                        if (expiryDate < DateTime.UtcNow)
                        {
                            return new TokenValidationResultDTO
                            {
                                IsValid = false,
                                Errors = new List<string> { "Token expired." }
                            };
                        }
                    }

                    var userId = principal.FindFirstValue(JwtRegisteredClaimNames.Sub);

                    return new TokenValidationResultDTO
                    {
                        IsValid = true,
                        UserId = userId
                    };
                }
            }
            catch
            {
                return new TokenValidationResultDTO
                {
                    IsValid = false,
                    Errors = new List<string> { "Invalid token." }
                };
            }

            return new TokenValidationResultDTO
            {
                IsValid = false,
                Errors = new List<string> { "Unknown error occurred." }
            };
        }
    }
}
