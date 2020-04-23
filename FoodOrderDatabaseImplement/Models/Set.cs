using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodOrderDatabaseImplement.Models
{
    public class Set
    {
        public int Id { get; set; }
        [Required]
        public string SetName { get; set; }
        [Required]
        public decimal Price { get; set; }

        public virtual List<SetOfDish> SetOfDishes { get; set; }
        public virtual List<Order> Orders { set; get; }
    }
}
