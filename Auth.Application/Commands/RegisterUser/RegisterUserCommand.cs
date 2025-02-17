using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Auth.Application.DTO;
using MediatR;

namespace Auth.Application.Commands.RegisterUser
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
