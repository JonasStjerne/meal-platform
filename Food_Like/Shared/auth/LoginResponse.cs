using System;
using System.Collections.Generic;
using System.Text;

namespace Food_Like.Shared
{
    public class LoginResponse
    {
        public bool Sucess { get; set; }
        public string Token { get; set; }
        public Buyer User { get; set; }

    }
}
