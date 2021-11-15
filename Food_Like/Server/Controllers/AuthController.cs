using Food_Like.Server.Models;
using Food_Like.Server.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Food_Like.Shared;

namespace Food_Like.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {

        [HttpPost("login")]
        public LoginResponse Login(LoginRequest request)
        {
            using (var context = new FoodLikeContext())
            {
                var user = context.Buyer.ToList().Find(user => user.Email == request.Email);
                if (user != null && user.Password == request.Password)
                {
                    return new LoginResponse
                    {
                        Sucess = true,
                        Token = String.Format("{0}.{1}", user.Email, user.Password),
                        User = user as Buyer
                    };
                } else
                {
                    return new LoginResponse
                    {
                        Sucess = false
                    };
                }
            }
                
        }
    }
}
