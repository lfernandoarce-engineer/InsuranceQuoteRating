using InsuranceQuoteRating.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace InsuranceQuoteRating.Repositories
{
    public class BusinessFactorRepo : IRepository<BusinessFactor>
    {
        private readonly string _dataSource = "Repositories/BusinessFactor.json";
        private List<BusinessFactor> _businessFactors;

        public BusinessFactorRepo() {
            loadData();
        }

        public BusinessFactor GetFactor(string factorScale) {
            try {
                if (_businessFactors == null)
                    loadData();

                return _businessFactors.FirstOrDefault(bf => bf.Business == factorScale);
            }
            catch (Exception exception) {
                throw exception; //TODO: what is a good way to log this exception? Application Insights -> future
            };
        }

        private void loadData()
        {
            try
            {
                _businessFactors = JsonConvert.DeserializeObject<List<BusinessFactor>>(File.ReadAllText(_dataSource));
            }
            catch(Exception exception) {
                throw exception; //TODO: what is a good way to log this exception? Application Insights -> future
            }
        }
    }
}
