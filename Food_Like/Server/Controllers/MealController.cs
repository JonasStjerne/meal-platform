using Food_Like.Server.Models;
using Food_Like.Server.Services;
using Food_Like.Shared;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Food_Like.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MealController : ControllerBase
    {
        private static readonly HttpClient client = new HttpClient();

        [HttpGet("{id}")]
        public IActionResult GetMeal(int id)
        {
            using (var context = new foodlikeContext())
            {

                var result = context.Meal
                  .Include(m => m.Seller)
                      .ThenInclude(s => s.SellerNavigation)
                  .Include(m => m.Seller)
                      .ThenInclude(s => s.Address)
                  .Include(s => s.Mealorder)
                  .Include(m => m.Review)
                  .Include(m => m.Mealcategory)
                      .ThenInclude(c => c.Category)
                  .First(m => m.MealId == id);


                if (result != null)
                {
                    return Ok(result);
                } else
                {
                    return NotFound();
                }

            }
        }

        [HttpPost("create")]
        public IActionResult CreateMeal(Auth<Meal> request)
        {
            using (var context = new foodlikeContext())
            {
                //Standard check for authorized access and make sure is seller
                var userService = new UserService(context);
                var authState = userService.GetUser(request);

                if (authState.FoundUser == false || !userService.UserIsSeller(authState))
                {
                    return Unauthorized();
                }
                try
                {
                    //Validate if pickup is in the future and that the minimum pickup interval is 30 minutes
                    if (!(request.Request.PickupFrom > DateTime.Now.AddMinutes(-5)) || !(request.Request.PickupFrom.AddMinutes(30) < request.Request.PickupTo))
                    {
                        throw new Exception();
                    }

                    request.Request.SellerId = authState.User.BuyerId;

                    //var meal = new Meal()
                    //{
                    //    SellerId = authState.User.BuyerId,
                    //    Portions = request.Request.Portions,
                    //    PortionPrice = request.Request.PortionPrice,
                    //    MealDescription = request.Request.MealDescription,
                    //    Ingridients = request.Request.Ingridients,
                    //    PickupFrom = request.Request.PickupFrom,
                    //    PickupTo = request.Request.PickupTo,
                    //    MealPicture = request.Request.MealPicture
                    //};

                    //var testdata = new Meal()
                    //{
                    //    SellerId = 2,
                    //    Portions = 4,
                    //    PortionPrice = 20,
                    //    MealDescription = "Description af meal",
                    //    Ingridients = "Ingridients list",
                    //    PickupFrom = DateTime.Now,
                    //    PickupTo = DateTime.Now.AddHours(1),
                    //    MealPicture = new byte[0]
                    //};

                    context.Add(request.Request);

                    context.SaveChanges();
                    return Ok();
                }
                catch (Exception exep)
                {
                    return BadRequest(exep);
                }

            }
        }

        [HttpPost("{id}")]
        public IActionResult BuyMeal(int id, Auth<sbyte> auth)
        {
            using (var context = new foodlikeContext())
            {
                var userService = new UserService(context);
                var authState = userService.GetUser(auth);

                if (authState.FoundUser == false)
                {
                    return Unauthorized();
                }

                var meal = context.Meal.Include(e => e.Mealorder).ToList().Find(meal => meal.MealId == id);
                if (meal != null)
                {
                    if (meal.Portions - meal.Mealorder.Count(e => e.MealId == id) >= auth.Request)
                    {
                        Mealorder order = new Mealorder
                        {
                            BuyerId = authState.User.BuyerId,
                            MealId = meal.MealId,
                            Quantity = auth.Request
                        };
                        context.Mealorder.Add(order);
                        context.SaveChanges();
                        var response = new BuyMealResponse
                        {
                            Success = true,
                            OrderId = order.OrderId
                        };
                        return Ok(response);
                    }
                    return BadRequest();
                }
                else
                {
                    return NotFound();
                }

            }
        }

        //Not ready
        [HttpGet("search/{location}")]
        public async Task<List<Meal>> Search(string location)
        {
            using (var context = new foodlikeContext())
            {
                List<Meal> meals = context.Meal.Where(e => e.PickupTo > DateTime.Now.AddMinutes(-30)).ToList();
                List<Meal> mealdistance = new List<Meal>();
                List<dynamic> test = new List<dynamic>();
                foreach (var meal in meals)
                {
                    Seller seller = context.Seller.AsNoTracking().Include(e => e.SellerNavigation).Where(e => e.SellerId == meal.SellerId).First();
                    Address selleraddress = context.Address.AsNoTracking().Where(e => e.AddressId == seller.AddressId).First();
                    string response = await client.GetStringAsync(string.Format("https://maps.googleapis.com/maps/api/distancematrix/json?destinations={0}&origins={1}&key=AIzaSyCcdwwvHneJhBnSCxGv1Ik3BOqDWTG0BT0", selleraddress.Line1+selleraddress.Line2+selleraddress.City, location));
                    var reponseDeserialized = JsonConvert.DeserializeObject<dynamic>(response);
                    mealdistance.Add(
                        new Meal {
                            DistanceValue = reponseDeserialized.rows[0].elements[0].distance.value,
                            Distance = reponseDeserialized.rows[0].elements[0].distance.text,
                            MealId = meal.MealId,
                            MealPicture = meal.MealPicture,
                            Seller = new Seller 
                            { 
                                SellerId = meal.SellerId,
                                SellerNavigation = new Buyer
                                {
                                    ProfilePicture = seller.SellerNavigation.ProfilePicture
                                }
                            },
                            Titel = meal.Titel,
                            PickupFrom = meal.PickupFrom,
                            PickupTo = meal.PickupTo,
                            PortionPrice = meal.PortionPrice,
                        }
                    );
                }
                List<Meal> mealDistanceSorted = mealdistance.OrderBy(e => e.DistanceValue).ToList();
                return mealDistanceSorted;
            }
        }

        [HttpGet("topmeals")]
        public IEnumerable<Meal> GetTopMeals()
        {
            using (var context = new foodlikeContext())
            {
                return context.Meal.ToList().OrderBy(e => e.Rating).Take(12);
            }
        }

        [HttpPost("review/{id}")]
        public IActionResult ReviewMeal(int id, Auth<CreateReviewRequest> auth)
        {
            using (var context = new foodlikeContext())
            {
                var userService = new UserService(context);
                var authState = userService.GetUser(auth);

                if (authState.FoundUser == false || !userService.BoughtMeal(authState, id) || userService.HasMadeReview(authState, id))
                {
                    return Unauthorized();
                }

                try
                {
                    Review review = new Review()
                    {
                        //Set reviewer as the authorized user
                        BuyerId = authState.User.BuyerId,
                        MealId = id,
                        Rating = auth.Request.Rating,
                        ReviewDescription = auth.Request.ReviewDescription

                    };
                    //Set reviewer as the authorized user
                    //auth.Request.BuyerId = authState.User.BuyerId;
                    //Set the meal id as the one from the uri
                    //auth.Request.MealId = id;
                    context.Review.Add(review);
                    context.SaveChanges();
                    return Ok();
                } catch (Exception)
                {
                    return StatusCode(500);
                }
            }
        }
    }
}
