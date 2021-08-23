using InsuranceQuoteRating.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace InsuranceQuoteRating.Repositories
{
    public class StateFactorRepo : IRepository<StateFactor>
    {
        private readonly string _dataSource = "Repositories/StateFactor.json";
        private List<StateFactor> _stateFactors;

        public StateFactorRepo()
        {
            loadData();
        }

        public StateFactor GetFactor(string factorScale)
        {
            try
            {
                if (_stateFactors == null)
                    loadData();

                return _stateFactors.FirstOrDefault(bf => bf.State == factorScale);
            }
            catch (Exception exception)
            {
                throw exception;
            };
        }

        private void loadData()
        {
            try
            {
                _stateFactors = JsonConvert.DeserializeObject<List<StateFactor>>(File.ReadAllText(_dataSource));
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

    }
}
