using System;
using System.Collections.Generic;
using System.Text;

namespace Food_Like.Shared
{
    public class Auth
    {
        public string EncryptedPassword;
        public string Email;
        public Seller Request;

        public Auth(string token, Seller request)
        {
            string[] credentials = token.Split("-.-");
            Email = credentials[0];
            EncryptedPassword = credentials[1];
            Request = request;
        }
        
    }

}
