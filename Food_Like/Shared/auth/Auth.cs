using System;
using System.Collections.Generic;
using System.Text;

namespace Food_Like.Shared
{
    public class Auth
    {
        public string EncryptedPassword { get; set; }
        public string Email { get; set; }
        public Seller Data { get; set; }


        //public Auth(string token, SetupSellerRequest data)
        //{
        //    string[] credentials = token.Split("-.-");
        //    Email = credentials[0];
        //    EncryptedPassword = credentials[1];
        //    Data = data;
        //}
    }

}
