using InsuranceQuoteRating.Controllers;
using InsuranceQuoteRating.Models.DTOs;
using Microsoft.Extensions.Logging;
using Machine.Fakes;
using Machine.Specifications;
using Machine.Fakes.Sdk;
using InsuranceQuoteRating.Services;

namespace InsuranceQuoteRating.Spec
{

    [Subject(typeof(RatingController))]
    class WhenRequestOnPremiumRating : WithSubject<RatingController>
    {
        static PremiumRequest premiumRequest;
        static RatingController rating;
        static ILogger<RatingController> logger;
        static IRatingService ratingService;
        static PremiumResponse result;
        static PremiumResponse expectedResult;

        Establish context = () =>
        {
            premiumRequest = new PremiumRequest() { revenue = 6000000, state = "TX", business = "Plumber" };
            ratingService = The<IRatingService>();
            ratingService.WhenToldTo(r => r.CalculatePremiumRating(6000000, "TX", "Plumber")).Return(11316);
            logger = An<ILogger<RatingController>>();
            Subject = new RatingController(logger, ratingService);
        };

        Because of = () => result = Subject.Premium(premiumRequest);

        It should_expected_premium_rate = () => result.premium.ShouldEqual(11316);
    }
}
