using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Application.DTO
{
   public class TokenValidationResultDTO
    {

        public bool IsValid { get; set; }
        public string UserId { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
    }
}
