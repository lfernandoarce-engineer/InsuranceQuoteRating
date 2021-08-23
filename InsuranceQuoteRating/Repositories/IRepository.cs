using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InsuranceQuoteRating.Repositories
{
    public interface IRepository<T> where T:class
    {
        T GetFactor(string factorScale);
    }
}
