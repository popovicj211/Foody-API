using Application.DataTransfer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Helpers
{
    public class AuthResponse
    {
        public string Token { get; set; }
        public UserDTO User { get; set; }
    }
}
