using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AbstractShopDatabaseImplement.Models
{
    public class Set
    {
        public int Id { get; set; }
        [Required]
        public string SetName { get; set; }
        [ForeignKey("SetId")]

        [Required]
        public decimal Price { get; set; }
        public virtual Dish Dish { get; set; }

        public virtual List<Order> Orders { set; get; }
    }
}
