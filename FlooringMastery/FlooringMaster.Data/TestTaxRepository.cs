using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringMastery.UI.Data;
using FlooringMastery.UI.Models;

namespace FlooringMaster.Data
{
    public class TestTaxRepository : ITaxRepository
    {
        private static List<Tax> _taxes = new List<Tax>();

        public TestTaxRepository()
        {
            if (!_taxes.Any())
            {
                _taxes.AddRange(new List<Tax>()
                {
                    new Tax()
                    {
                        StateAbbreviation = "OH",
                        StateName = "Ohio",
                        TaxRate = 6.25M
                    },
                    new Tax()
                    {
                        StateAbbreviation = "PA",
                        StateName = "Pennsylvania",
                        TaxRate = 6.75M
                    },
                    new Tax()
                    {
                        StateAbbreviation = "MI",
                        StateName = "Michigan",
                        TaxRate = 5.75M
                    },
                    new Tax()
                    {
                        StateAbbreviation = "IN",
                        StateName = "Indiana",
                        TaxRate = 6.00M
                    },
                });
            }
        }
        public IEnumerable<Tax> GetTaxes()
        {
            IEnumerable<Tax> results;
            results = Load();
            return results;
        }

        public Tax GetTaxesByState(string state)
        {
            Tax result = null;
            List<Tax> taxes = Load();
            result = taxes.FirstOrDefault(o => o.StateAbbreviation == state);
            if (result == null)
            {
                result = taxes.FirstOrDefault(o => o.StateName == state);
                if(result == null)
                {
                    throw new Exception("We do not ship to this state.\n");
                }
            }
            return result;
        }

        private List<Tax> Load()
        {
            List<Tax> taxes = new List<Tax>();
            taxes = _taxes;
            return taxes;
        }
    }
}
