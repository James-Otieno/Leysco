using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthMicroservice.Application.DTO
{
    public class LoginResponseDTO
    {
        public bool Success { get; set; } 

        public string Token { get; set; } 

        public DateTime Expiration { get; set; } 

        public List<string> Errors { get; set; } = new List<string>(); 
    }
}
