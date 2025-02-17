using AuthMicroservice.Application.DTO;
using AuthMicroservice.Application.Queries.LoginUser;
using AuthMicroservice.Application.Services;
using AuthMicroservice.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AuthMicroservice.Application.Queries.LoginUser
{
    public class LoginUserQueryHandler : IRequestHandler<LoginUserQuery, LoginResponseDTO>
    {
        private readonly UserManager<User> _userManager;

        private readonly SignInManager<User> _signInManager;

        private readonly IJwtService _jwtService;


        public LoginUserQueryHandler(UserManager<User> userManager, SignInManager<User> signInManager, IJwtService jwtService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtService = jwtService;
        }

        public async Task<LoginResponseDTO> Handle(LoginUserQuery request, CancellationToken cancellationToken)
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
