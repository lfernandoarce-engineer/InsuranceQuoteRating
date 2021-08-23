using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InsuranceQuoteRating.Models.DTOs
{
    public class PremiumRequest
    {
        public ulong revenue { set; get; }
        public string state { set; get; }
        public string business { set; get; }

    }
}
