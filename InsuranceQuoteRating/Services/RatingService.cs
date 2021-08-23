using InsuranceQuoteRating.Models;
using InsuranceQuoteRating.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InsuranceQuoteRating.Services
{
    public class RatingService : IRatingService
    {
        private readonly IRepository<StateFactor> _stateFactorRepo;
        private readonly IRepository<BusinessFactor> _businessFactorRepo;
        private const int _basePremiumDivider = 1000;
        private const int _hazardFactor = 4;

        public RatingService(IRepository<StateFactor> stateFactorRepo, IRepository<BusinessFactor> businessFactorRepo) {
            _stateFactorRepo = stateFactorRepo;
            _businessFactorRepo = businessFactorRepo;
        }

        public double CalculatePremiumRating(long revenue, string state, string business) {

            //TODO: Throw exception when it was no able to get state or business factors?  404 not found???
            var stateFactor = _stateFactorRepo.GetFactor(state);
            var businessFactor = _businessFactorRepo.GetFactor(business);

            double basePremiumReal = revenue / _basePremiumDivider;
            var basePremium = Math.Ceiling(basePremiumReal);

            return stateFactor.Factor * businessFactor.Factor * basePremium * _hazardFactor;
        }
    }
}
