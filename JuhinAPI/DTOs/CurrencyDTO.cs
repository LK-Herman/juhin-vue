using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JuhinAPI.DTOs
{
    public class CurrencyDTO
    {
        public int CurrencyId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int ValuePLN { get; set; }
    }
}
