﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Application.DTO
{
  public class RefreshTokenResponseDTO
    {

        public bool Success { get; set; }
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
        public List<string> Errors { get; set; }
    }
}
