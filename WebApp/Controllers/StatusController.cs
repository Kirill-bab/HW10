using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StatusController : ControllerBase
    {
       [HttpGet]
       [Route("")]
       public IActionResult Info()
        {
            return Ok("Created by Kirill Babich\nThis program i" +
                "s a web service for performing simple actions with primes");
        }
    }
}
