using System;
using System.Collections.Generic;
using System.Text;

namespace Food_Like.Shared
{
    public class AuthState
    {
        public bool FoundUser;
        public Buyer? User;

        public AuthState(bool foundUser, Buyer user = null)
        {
            FoundUser = foundUser;
            if (user != null)
            {
                User = user;
            }
        }
        
    }

}
