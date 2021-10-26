using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JuhinAPI.DTOs
{
    public class PalletDTO
    {
        public int PalletId { get; set; }
        public string Name { get; set; }
        public int Xdimension { get; set; }
        public int Ydimension { get; set; }
    }
}
