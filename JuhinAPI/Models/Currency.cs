using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JuhinAPI.Models
{
    public class Currency
    {
        [Required]
        [Key]
        public int CurrencyId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Code { get; set; }
        [Required]
        public int ValuePLN { get; set; }


        //Currency 1-m Item
        public List<Item> Items { get; set; }
    }
}
