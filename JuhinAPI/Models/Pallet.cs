using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JuhinAPI.Models
{
    public class Pallet
    {
        [Key]
        [Required]
        public int PalletId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Xdimension { get; set; }
        [Required]
        public int Ydimension { get; set; }

        //Item m-1 Pallet
        public List<Item> Items { get; set; }
    }                               
}
