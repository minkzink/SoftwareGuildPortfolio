using FlooringMastery.UI.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FlooringMastery.UI.Data
{
    public class ProdTaxRepository : ITaxRepository
    {
        private string _filename;
        private static List<Tax> _taxes = new List<Tax>();

        public ProdTaxRepository(string filename)
        {
            _filename = filename;
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
                if (result == null)
                {
                    throw new Exception("We do not ship to this state.\n");
                }
            }
            return result;
        }

        private List<Tax> Load()
        {
            List<Tax> taxes = new List<Tax>();
            using (StreamReader sr = new StreamReader(_filename))
            {
                sr.ReadLine();
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    if (line.Contains("|"))
                    {
                        string[] fields = line.Split('|');
                        Tax tax = new Tax();
                        tax.StateAbbreviation = fields[0];
                        tax.StateName = fields[1];
                        tax.TaxRate = decimal.Parse(fields[2]);
                        taxes.Add(tax);
                    }
                    else
                    {
                        string[] fields = line.Split(',');
                        Tax tax = new Tax();
                        tax.StateAbbreviation = fields[0];
                        tax.StateName = fields[1];
                        tax.TaxRate = decimal.Parse(fields[2]);
                        taxes.Add(tax);
                    }

                }
            }
            _taxes = taxes;
            return taxes;
        }
    }
}
