using Food_Like.Server.Models;
using Food_Like.Server.Services;
using Food_Like.Shared;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;

namespace Food_Like.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MealController : ControllerBase
    {
        private static readonly HttpClient client = new HttpClient();

        [HttpGet("search")]
        public List<Meal> Search(string location)
        {
            using (var context = new foodlikeContext())
            {

                //List<Meal> meals = context.Meal.ToList();
                //Dictionary<Meal, int> mealDistance = new Dictionary<Meal, int>();
                //foreach (var meal in meals)
                //{
                //    Seller seller = context.Seller.Where(e => e.SellerId == meal.SellerId).First();
                //    Address sellerAddress = context.Address.Where(e => e.AddressId == seller.AddressId).First();
                //    dynamic distanceToMeal = client.GetAsync(String.Format("https://maps.googleapis.com/maps/api/distancematrix/json?destinations={0}&origins={1}&key=AIzaSyBlObeK6EyBueLnbUSnlf1QO45xaO0wxro", sellerAddress, location));
                //    mealDistance.Add(meal, distanceToMeal.elements[0].distance.value);
                //}
                //return mealDistance;
                return context.Meal.ToList();
            }
        }

        [HttpGet("topMeals")]
        public IEnumerable<Meal> GetTopMeals()
        {
            using (var context = new foodlikeContext())
            {
                return context.Meal.ToList().OrderBy(e => e.Rating).Take(10);
            }
        }

    }
}
