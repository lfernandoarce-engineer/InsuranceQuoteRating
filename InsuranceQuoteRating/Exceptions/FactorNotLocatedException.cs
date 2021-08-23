using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InsuranceQuoteRating.Exceptions
{
    public class FactorNotLocatedException : Exception
    {
        public FactorNotLocatedException(string errorMessage) : base(errorMessage) { }
    }
}
