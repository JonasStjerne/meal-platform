using Food_Like.Server.Models;
using Food_Like.Server.Services;
using Food_Like.Shared;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace Food_Like.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {


        //Get All userinformation
        [HttpGet]
        public IActionResult Get([FromHeader] string Auth)
        {
            using (var context = new foodlikeContext())
            {
                //Standard check for authorized access and make sure is seller
                var userService = new UserService(context);
                var authState = userService.GetUser(Auth);

                if (authState.FoundUser == false)
                {
                    return Unauthorized();
                }

                try
                {
                    if (userService.UserIsSeller(authState))
                    {
                   
                            return Ok(context.Buyer
                                .Where(buyer => buyer.BuyerId == authState.User.BuyerId)
                                .Include(e => e.Seller)
                                .First()
                            );
                    
                    } else
                    {
                    
                            return Ok(context.Buyer.Where(buyer => buyer.BuyerId == authState.User.BuyerId).First());
                
                    }
                } catch (Exception exp)
                {
                    return BadRequest(exp);
                }

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

        [HttpGet("{id}")]
        public async Task<ActionResult<Seller>> GetSeller(int id)
        {
            using (var context = new foodlikeContext())
            {
                try
                {
                    return context.Seller
                        .Where(s => s.SellerId == id).Include(s => s.Address).Include(s => s.SellerNavigation).First();
                }
                catch (Exception exp)
                {
                    return BadRequest(exp);
                }

            }
        }

        [HttpGet("{id}/ratings")]
        public async Task<ActionResult<List<Review>>> GetRatings(int id)
        {
            using (var context = new foodlikeContext())
            {
                try
                {
                    return context.Review
                        .Where(e => e.Meal.SellerId == id).Include(r => r.Buyer).ToList();
                }
                catch (Exception exp)
                {
                    return BadRequest(exp);
                }

            }
        }

        [HttpGet("mymeals")]
        public async Task<ActionResult<List<Meal>>> GetMyMeals([FromHeader] string Auth)
        {
            using (var context = new foodlikeContext())
            {
                //Standard check for authorized access and make sure is seller
                var userService = new UserService(context);
                var authState = userService.GetUser(Auth);

                if (authState.FoundUser == false || !userService.UserIsSeller(authState))
                {
                    return Unauthorized();
                }

                try
                {
                    //Map relevant data to hide sensitive information
                    var response = context.Meal
                        .Where(e => e.SellerId == authState.User.BuyerId)
                        .Select(e => new Meal 
                        {
                            Titel = e.Titel,
                            Portions = e.Portions,
                            PortionPrice = e.PortionPrice,
                            PickupFrom = e.PickupFrom,
                            PickupTo = e.PickupTo,
                            MealPicture = e.MealPicture,
                            Mealorder = e.Mealorder,
                            Seller = new Seller
                            {
                                Address = e.Seller.Address
                            }
                        })
                        .ToList();

                    if (response == null)
                    {
                        return NotFound();
                    } else
                    {
                        return Ok(response);
                    }
                }
                catch (Exception exp)
                {
                    return BadRequest(exp);
                }

            }
        }

        [HttpGet("myreservations")]
        public async Task<ActionResult<List<Meal>>> GetMyReservations([FromHeader] string Auth)
        {
            using (var context = new foodlikeContext())
            {
                //Standard check for authorized access and make sure is seller
                var userService = new UserService(context);
                var authState = userService.GetUser(Auth);

                if (authState.FoundUser == false || !userService.UserIsSeller(authState))
                {
                    return Unauthorized();
                }

                try
                {
                    //Map relevant data to hide sensitive information
                    var response = context.Mealorder
                        .Include(e => e.Meal)
                        .Where(e => e.BuyerId == authState.User.BuyerId)
                        .Select(e => new Meal
                        {
                            Titel = e.Meal.Titel,
                            Portions = e.Meal.Portions,
                            PortionPrice = e.Meal.PortionPrice,
                            PickupFrom = e.Meal.PickupFrom,
                            PickupTo = e.Meal.PickupTo,
                            MealPicture = e.Meal.MealPicture,
                            Seller = new Seller
                            {
                                Address = e.Meal.Seller.Address,
                                SellerNavigation = new Buyer
                                {
                                    BuyerId = e.Meal.Seller.SellerNavigation.BuyerId,
                                    PhoneNumber = e.Meal.Seller.SellerNavigation.PhoneNumber,
                                    ProfilePicture = e.Meal.Seller.SellerNavigation.ProfilePicture
                                }
                            }
                        })
                        .ToList();

                    if (response == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        return Ok(response);
                    }
                }
                catch (Exception exp)
                {
                    return BadRequest(exp);
                }

            }
        }
    }
}
