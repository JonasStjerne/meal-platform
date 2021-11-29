using System;
using System.Collections.Generic;
using System.Text;

namespace Food_Like.Shared
{
    public class CreateRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Firstname { get; set; }
        public string Surname { get; set; }
        public string Mobile { get; set; }
        public string ProfilePicture { get; set; }

    }
}
