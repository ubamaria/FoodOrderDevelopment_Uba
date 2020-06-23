using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodOrderDatabaseImplement.Models
{
    public class Dish
    {
        public int Id { get; set; }

        [Required] public string DishName { get; set; }

        [ForeignKey("DishId")] 
        public virtual List<SetOfDish> SetOfDishes { get; set; }
        public virtual List<StorageDish> StorageDishes { get; set; }
    }
}
