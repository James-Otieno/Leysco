using AuthMicroservice.Application.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthMicroservice.Application.Queries.RefreshToken
{
    public class RefreshTokenQuery : IRequest<RefreshTokenResponseDTO>
    {
        public string RefreshToken { get; set; }
    }
}
