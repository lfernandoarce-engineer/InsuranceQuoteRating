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
        public PremiumResponse Premium([FromBody] PremiumRequest request)
        {
            //TODO: Validated payload data (return bad request in case invalid??) - also revenue should be long - return 400 bad request
            //TODO: Validated if we got a valid result from service?
            //TODO: Add more log everywhere
            //TODO: Use more general/generic name for the solution

            var premiumRate = _ratingService.CalculatePremiumRating((long)request.revenue, request.state, request.business);
            _logger.LogInformation("Is the log working correctly?");

            return new PremiumResponse() { premium = premiumRate }; //TODO: response with an IAction or similar instead? What good practices indicates???
        }

    }
}
