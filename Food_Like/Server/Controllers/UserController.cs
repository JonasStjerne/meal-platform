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
        public dynamic SetupSeller(Auth<SetupSellerRequest> request)
        {
            using (var context = new foodlikeContext())
            {
                try
                {

                    //Makes an new userService instance to access methods
                    var userService = new UserService(context);

                    //Gets the auth state for the user, still also containing the request
                    AuthState authState = userService.GetUser(request);

                    //If the user exist and is not a seller, then create seller
                    if (authState.FoundUser == true && authState.User.Seller == null)
                    {
                        //Declare the user from the auth
                        Buyer user = authState.User;

                        //Declatre the seller object
                        Seller sellerInformation = new Seller()
                        {
                            SellerId = user.BuyerId,
                            KitchenPicture = request.Request.KitchenPicture,
                            Address = request.Request.Address
                        };

                        context.Seller.Add(sellerInformation);
                        context.SaveChanges();

                        return new SetupSellerResponse()
                        {
                            Success = true
                        };
                    }
                    else
                    {
                        return new SetupSellerResponse()
                        {
                            Success = false
                        };
                    }
                } catch (Exception)
                {
                    return new SetupSellerResponse()
                    {
                        Success = false
                    };
                }
               
            }
        }

        [HttpGet("{id}/ratings")]
        public IActionResult getRatings(int id, Auth<dynamic> request)
        {
            using (var context = new foodlikeContext())
            {
                //Standard check for authorized access and make sure is seller
                var userService = new UserService(context);
                var authState = userService.GetUser(request);

                if (authState.FoundUser == false)
                {
                    return Unauthorized();
                }

                var reviews = context.Review.Where(review => review.Meal.SellerId == id).ToList();
                if (reviews == null)
                {
                    return NotFound();
                }
                return Ok(reviews);
            }
        }

    }
}
