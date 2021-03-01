using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class PrimesController : ControllerBase
    {
        private readonly IPrimesSearcher _primesSearcher;
        private readonly ILogger<PrimesController> _logger;

        public PrimesController(IPrimesSearcher primesSearcher, ILoggerFactory loggerFactory)
        {
            _primesSearcher = primesSearcher;
            _logger = loggerFactory.CreateLogger<PrimesController>();
        }

        [HttpGet]
        [Route("primes/{number:int}")]
        public IActionResult IsPrime([FromRoute] int number)
        {
            //if (number == default) return NotFound();

            var isPrime = _primesSearcher.IsPrime(number);

            if (isPrime)
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet]
        [Route("primes")]
        public Task<IActionResult> GetPrimes([FromQuery] string from, [FromQuery] string to)
        {
            return Task.Run<IActionResult>(async () =>
            {
                if (int.TryParse(from, out var nfrom) && int.TryParse(to, out var nto))
                {
                    _logger.LogInformation($"Get request with url {Request.Path} parameters are valid!");
                    var primes = await _primesSearcher.FindPrimesAsync(nfrom, nto);
                    _logger.LogInformation($"Get request with url {Request.Path} and parameters from: {from}; to: {to} was performed successfully!");
                    return Ok(primes);
                }
                else
                {
                    _logger.LogInformation($"Get request with url {Request.Path} parameters are invalid!");
                    return BadRequest();
                }
            });
        }
    }   
}
