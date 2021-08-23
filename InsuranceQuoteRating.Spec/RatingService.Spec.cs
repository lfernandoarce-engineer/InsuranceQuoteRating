using InsuranceQuoteRating.Controllers;
using InsuranceQuoteRating.Models.DTOs;
using Microsoft.Extensions.Logging;
using Machine.Fakes;
using Machine.Specifications;
using Machine.Fakes.Sdk;
using InsuranceQuoteRating.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using InsuranceQuoteRating.Repositories;
using InsuranceQuoteRating.Models;

namespace InsuranceQuoteRating.Spec
{

    [Subject(typeof(RatingService))]
    class WhenCalculatingPremiumRating : WithSubject<RatingService>
    {
        static PremiumRequest premiumRequest;
        static RatingController rating;
        static ILogger<RatingController> logger;
        static IRepository<StateFactor> stateFactorRepo;
        static IRepository<BusinessFactor> businessFactorRepo;
        static double result;
        static PremiumResponse expectedResult;

        Establish context = () =>
        {
            premiumRequest = new PremiumRequest() { revenue = 6000000, state = "TX", business = "Plumber" };
            stateFactorRepo = The<IRepository<StateFactor>>();
            businessFactorRepo = The<IRepository<BusinessFactor>>();
            stateFactorRepo.WhenToldTo(r => r.GetFactor("TX")).Return(new StateFactor() { State = "TX", Factor = 0.943 });
            businessFactorRepo.WhenToldTo(r => r.GetFactor("Plumber")).Return(new BusinessFactor() { Business = "Plumber", Factor = 0.5 });
            Subject = new RatingService(stateFactorRepo, businessFactorRepo);
        };

        Because of = () => {
            result = Subject.CalculatePremiumRating(6000000, "TX", "Plumber");
        };

        It should_be_expected_premium_rate = () => result.ShouldEqual<double>(11316);
    }
}
