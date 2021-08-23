using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InsuranceQuoteRating.Models.DTOs;
using InsuranceQuoteRating.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace InsuranceQuoteRating.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RatingController : Controller
    {
        private readonly ILogger<RatingController> _logger;
        private readonly IRatingService _ratingService;

        public RatingController(ILogger<RatingController> logger, IRatingService ratingService)
        {
            _logger = logger;
            _ratingService = ratingService;
        }

        [Route("premium")]
        [HttpPost]
        public IActionResult Premium([FromBody] PremiumRequest request)
        {
            if (request == null) return BadRequest("Payload has a wrong format or it is empty");
            if (string.IsNullOrEmpty(request.state)) return BadRequest("State is required to calculate premium rating");
            if (string.IsNullOrEmpty(request.business)) return BadRequest("Business type is required to calculate premium rating");
            if (request.revenue < 0) return BadRequest("Revenue can not be a negative number");

            //TODO: Validated if we got a valid result from service?
            //TODO: Add more log everywhere
            //TODO: Use more general/generic name for the solution

            var premiumRate = _ratingService.CalculatePremiumRating((ulong)request.revenue, request.state, request.business);
            _logger.LogInformation("Is the log working correctly?");

            return Ok(new PremiumResponse() { premium = premiumRate }); //TODO: response with an IAction or similar instead? What good practices indicates???
        }

    }
}
