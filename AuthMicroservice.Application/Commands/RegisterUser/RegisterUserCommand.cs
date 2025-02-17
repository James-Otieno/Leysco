using AuthMicroservice.Application.DTO;
using MediatR;

namespace AuthMicroservice.Application.Commands.RegisterUser
{
    public class RegisterUserCommand : IRequest<RegisterResponseDTO>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
