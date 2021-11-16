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
                var user = new UserService(context).GetUser(request);
                if (user != null)
                {
                    return new LoginResponse
                    {
                        Sucess = true,
                        Token = String.Format("{0}-.-{1}", user.Email, user.Password),
                        User = user
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

        [HttpPost("create")]
        public LoginResponse Create(Buyer request)
        {
            using (var context = new FoodLikeContext())
            {
                if (!new UserService(context).UserExists(request))
                {
                    try
                    {
                        context.Buyer.Add(request);
                        context.SaveChanges();
                        return new LoginResponse
                        {
                            Sucess = true,
                            Token = String.Format("{0}-.-{1}", request.Email, request.Password),
                            User = request
                        };
                    } 
                    catch
                    {
                        return new LoginResponse
                        {
                            Sucess = false,
                        };
                    }
                    
                } else
                {
                    return new LoginResponse
                    {
                        Sucess = false,
                    };
                }
                
                
            }

        }

        //Only needed if we hash information to localstorage and need to verify that

        //[HttpPost("authorize")]
        //public AuthorizeResponse Authorize(AuthorizeRequest request)
        //{
        //    using (var context = new FoodLikeContext()) 
        //    {

        //    }
        //}
    }
}
