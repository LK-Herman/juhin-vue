using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JuhinAPI.Models
{
    public class Unit
    {
        [Key]
        [Required]
        public int UnitId { get; set; }
        [Required]
        [StringLength(20)]
        public string Name { get; set; }
        [StringLength(6)]
        public string ShortName { get; set; }

        //Unit 1-m Item
        public List<Item> Items { get; set; }
    }
}
