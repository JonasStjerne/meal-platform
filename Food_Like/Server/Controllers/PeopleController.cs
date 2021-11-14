using Food_Like.Server.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Food_Like.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PeopleController : ControllerBase
    {
        [HttpGet]
        public async Task<List<People>> getAllPeople()
        {
            DbContext context = HttpContext.RequestServices.GetService(typeof(Food_Like.Server.Models.DbContext)) as DbContext;
            return context.getAllPeople();
        }
    }
}
