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
                
                var result = context.Meal.ToList().Find(meal => meal.MealId == id);
                if (result != null)
                {
                    return Ok(result);
                } else
                {
                    return NotFound();
                }
                
            }
        }

        [HttpPost("meal/{id}")]
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
                        context.Mealorder.Add(
                        new Mealorder
                        {
                            BuyerId = authState.User.BuyerId,
                            MealId = meal.MealId,
                            Quantity = auth.Request
                        });
                        return Ok();
                    }
                    return BadRequest();
                }
                else
                {
                    return NotFound();
                }

            }
        }

        [HttpGet("search{location}")]
        public dynamic Search(string location)
        {
            using (var context = new foodlikeContext())
            {

                List<Meal> meals = context.Meal.ToList();
                Dictionary<Meal, int> mealdistance = new Dictionary<Meal, int>();
                foreach (var meal in meals)
                {
                    Seller seller = context.Seller.Where(e => e.SellerId == meal.SellerId).First();
                    Address selleraddress = context.Address.Where(e => e.AddressId == seller.AddressId).First();
                    dynamic distancetomeal = client.GetAsync(string.Format("https://maps.googleapis.com/maps/api/distancematrix/json?destinations={0}&origins={1}&key=aizasyccdwwvhnejhbnscxgv1ik3boqdwtg0bt0", selleraddress, location));
                    mealdistance.Add(meal, distancetomeal.elements[0].distance.value);
                }
                return mealdistance;
                //return context.Meal.ToList();
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

    }
}
