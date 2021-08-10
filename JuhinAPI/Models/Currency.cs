using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JuhinAPI.Models
{
    public class Currency
    {
        public int CurrencyId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int ValuePLN { get; set; }


        //Item m-1 Currency
        public List<Item> Items { get; set; }
    }
}
