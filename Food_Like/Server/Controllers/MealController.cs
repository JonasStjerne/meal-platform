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

        [HttpGet("{id}")]
        public GenericResponse<dynamic> GetMeal(int id)
        {
            using (var context = new foodlikeContext())
            {
                try
                {
                    return new GenericResponse<dynamic>()
                    {
                        Data = context.Meal.ToList().Find(meal => meal.MealId == id),
                        Sucess = context.Meal.ToList().Any(meal => meal.MealId == id)
                    };
                }
                catch
                {
                    return new GenericResponse<dynamic>()
                    {
                        Sucess = false
                    };
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
