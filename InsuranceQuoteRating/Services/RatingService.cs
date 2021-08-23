using InsuranceQuoteRating.Exceptions;
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

        public double CalculatePremiumRating(ulong revenue, string state, string business) {

            var stateFactor = _stateFactorRepo.GetFactor(state);
            if (stateFactor == null) throw new FactorNotLocatedException($"Could not locate a valid factor for state: {state}");

            var businessFactor = _businessFactorRepo.GetFactor(business);
            if (businessFactor == null) throw new FactorNotLocatedException($"Could not locate a valid factor for business: {business}");

            double basePremiumReal = revenue / _basePremiumDivider;
            var basePremium = Math.Ceiling(basePremiumReal);

            return stateFactor.Factor * businessFactor.Factor * basePremium * _hazardFactor;
        }
    }
}
