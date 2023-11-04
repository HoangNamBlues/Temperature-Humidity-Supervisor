using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Core_Layer.DTOs
{
    public class TokenModelRequest
    {
        public string JwtToken { get; set; }
        public string RefreshToken { get; set; }
    }
}