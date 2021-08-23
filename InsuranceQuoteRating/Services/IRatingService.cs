using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InsuranceQuoteRating.Services
{
    public interface IRatingService
    {
        double CalculatePremiumRating(long revenue, string state, string business);
    }
}
