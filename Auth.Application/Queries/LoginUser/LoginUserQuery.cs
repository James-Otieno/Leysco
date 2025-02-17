using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Auth.Application.DTO;
using MediatR;

namespace Auth.Application.Queries.LoginUser
{
   public class LoginUserQuery : IRequest<LoginResponseDTO>
    {
        public string Email { get; set; }
        public string Password { get; set; }


    }
}
