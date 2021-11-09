using Food_Like.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Food_Like.Server.Services;

namespace Food_Like.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {

        [HttpGet]
        public string Get()
        {
            return "Api works!";
        }

        [HttpPost("login")]
        public async Task<dynamic> getUser([FromBody] dynamic request)
        {
            return Services.AccountService.getUser(request);
        }
    }
}
