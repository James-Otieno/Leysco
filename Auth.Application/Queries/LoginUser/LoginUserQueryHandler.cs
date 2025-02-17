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

namespace Auth.Application.Queries.LoginUser
{
   public  class LoginUserQueryHandler : IRequestHandler<LoginUserQuery, LoginResponseDTO>
    {
        private readonly UserManager<User> _userManager;

        private readonly SignInManager<User> _signInManager;

        private readonly IJwtService _jwtService;

        private readonly JwtConfig _jwtConfig;

        public LoginUserQueryHandler(UserManager<User> userManager, SignInManager<User> signInManager, IJwtService jwtService, IOptions<JwtConfig> jwtConfig)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtService = jwtService;
            _jwtConfig = jwtConfig.Value;
        }

        public  async Task<LoginResponseDTO> Handle(LoginUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                return new LoginResponseDTO
                {
                    Success = false,
                    Errors = new() { "Invalid credentials!" }
                };
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
            if (!result.Succeeded)
            {
                return new LoginResponseDTO
                {
                    Success = false,
                    Errors = new() { "Invalid credentials!" }
                };
            }

            var roles = await _userManager.GetRolesAsync(user);

            var generateTokenResponse = _jwtService.GenerateJwtToken(user, roles);

            return new LoginResponseDTO
            {
                Success = true,
                Token = generateTokenResponse.Token,
                Expiration = generateTokenResponse.Expiration
            };
        }
    }
}
