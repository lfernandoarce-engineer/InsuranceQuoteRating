using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InsuranceQuoteRating.Exceptions;
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
        public ActionResult<PremiumResponse> Premium([FromBody] PremiumRequest request)
        {
            _logger.LogInformation("Validating premium request");

            var errorMessage = ValidatePremiumRequest(request);
            if (!string.IsNullOrEmpty(errorMessage))
            {
                _logger.LogError(errorMessage);
                return BadRequest(errorMessage);
            }

            try {
                _logger.LogInformation("Calculating Premium Rating");

                var premiumRate = _ratingService.CalculatePremiumRating((ulong)request.revenue, request.state, request.business);

                _logger.LogInformation($"Premium rating calculated: {premiumRate}");

                return Ok(new PremiumResponse() { premium = premiumRate });
            }
            catch (FactorNotLocatedException exception) {
                _logger.LogError(exception.Message);
                return NotFound(exception.Message);
            }
            catch (Exception exception) {
                _logger.LogError(exception.Message);
                return StatusCode(500);
            }         
        }

        private string ValidatePremiumRequest(PremiumRequest request) {

            if (request == null) return "Payload has a wrong format or it is empty";
            if (string.IsNullOrEmpty(request.state)) return "State is required to calculate premium rating";
            if (string.IsNullOrEmpty(request.business)) return "Business type is required to calculate premium rating";
            if (request.revenue < 0) return "Revenue can not be a negative number";

            return string.Empty;
        }

    }
}
