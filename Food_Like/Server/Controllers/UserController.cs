using Food_Like.Server.Models;
using Food_Like.Server.Services;
using Food_Like.Shared;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Food_Like.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {

        [HttpGet]
        public IEnumerable<Buyer> Get()
        {
            using (var context = new foodlikeContext())
            {
                return context.Buyer.ToList();
            }
        }

        [HttpPost("setupseller")]
        public dynamic SetupSeller(Auth auth)
        {
            using (var context = new foodlikeContext())
            {
                var userService = new UserService(context);
                AuthState authState = userService.GetUser(auth);
                if (authState.FoundUser == true && !userService.UserIsSeller(auth))
                {
                    Buyer user = authState.User;
                    var address = new Address()
                    {
                        City = auth.Request.Address.City,
                        Line1 = auth.Request.Address.Line1,
                        Line2 = auth.Request.Address.Line2,
                    };

                    auth.Request.Address = address;
                    context.Seller.Add(auth.Request);
                    context.SaveChanges();

                    return context.Buyer.Where(e => e.BuyerId == user.BuyerId);
                }
                else
                {
                    return "naah";
                }
               
            }
        }

    }
}
