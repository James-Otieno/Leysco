using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Auth.Application.DTO;
using MediatR;

namespace Auth.Application.Queries.RefreshToken
{
    public class RefreshTokenQuery : IRequest<RefreshTokenResponseDTO>
    {
        public string RefreshToken { get; set; }


    }
}
