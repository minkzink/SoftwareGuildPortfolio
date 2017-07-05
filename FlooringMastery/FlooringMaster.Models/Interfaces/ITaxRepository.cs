using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringMastery.UI.Models;

namespace FlooringMastery.UI.Data
{
    public interface ITaxRepository
    {
        IEnumerable<Tax> GetTaxes();
        Tax GetTaxesByState(string state);
    }
}
