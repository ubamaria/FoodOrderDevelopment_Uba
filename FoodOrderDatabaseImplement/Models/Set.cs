using FoodOrderDatabaseImplement.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FoodOrderImplement.Models
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
