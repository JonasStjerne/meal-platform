using Food_Like.Server.Models;
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
    public class OrderController : ControllerBase
    {
        [HttpGet("{id}")]
        public IActionResult GetOrder(int id)
        {
            using (var context = new foodlikeContext())
            {

                Mealorder order = context.Mealorder
                    .Include(o => o.Meal)
                        .ThenInclude(m => m.Seller)
                            .ThenInclude(s => s.SellerNavigation)
                        .ThenInclude(m => m.Seller)
                            .ThenInclude(s => s.Address)
                    .Include(o => o.Buyer)
                    .First(o => o.OrderId == id);
                if (order != null)
                {
                    return Ok(order);
                }
                else
                {
                    return NotFound();
                }

            }
        }


    }
}
