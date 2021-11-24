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
        public dynamic SetupSeller(Auth request)
        {
            using (var context = new foodlikeContext())
            {
                //Makes an new userService instance to access methods
                var userService = new UserService(context);

                //Gets the auth state for the user, still also containing the request
                AuthState authState = userService.GetUser(request);

                //If the user exist and is not a seller, then create seller
                if (authState.FoundUser == true && !userService.UserIsSeller(request))
                {
                    //Declare the user from the auth
                    Buyer user = authState.User;


                    //var address = new Address()
                    //{
                    //    City = auth.Request.Address.City,
                    //    Line1 = auth.Request.Address.Line1,
                    //    Line2 = auth.Request.Address.Line2,
                    //};

                    //Set the address 
                    //request.Data.SellerId = user.BuyerId;
                    context.Address.Add(request.Data.Address);
                    context.SaveChanges();
                    request.Data.AddressId = request.Data.Address.AddressId;
                    request.Data.SellerId = user.BuyerId;
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
