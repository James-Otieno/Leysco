using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Auth.Application.DTO;
using Auth.Domain.Entities;
using MediatR;
using Microsoft.AspNet.Identity;

namespace Auth.Application.Commands.RegisterUser
{
   public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, RegisterResponseDTO>
    {
        private readonly UserManager<User> _userManager;

        public RegisterUserCommandHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<RegisterResponseDTO> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {

            try
            {
                var existingUser = await _userManager.FindByEmailAsync(request.Email);

                if (existingUser != null)
                {
                    return new RegisterResponseDTO
                    {
                        Success = false,
                        Message = "User with this email already exists.",
                        Errors = new List<string> { "Email is already in use." }
                    };
                }
                var user = new User
                {
                    UserName = request.Email,
                    Email = request.Email,
                    FirstName = request.FirstName,
                    LastName = request.LastName
                };

                var result = await _userManager.CreateAsync(user, request.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, request.Role);
                }

                var response = new RegisterResponseDTO
                {
                    Success = result.Succeeded,
                    Message = result.Succeeded ? "User registered successfully!" : "User registration failed.",
                    Errors = result.Errors?.Select(e => e.Description).ToList()
                };

                return response;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
