using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FoodOrderDatabaseImplement.Models
{
    public class StorageDish
    {
        public int Id { get; set; }
        public int StorageId { get; set; }
        public int DishId { get; set; }
        [Required]
        public int Count { get; set; }
        public virtual Dish Dish { get; set; }
        public virtual Storage Storage { get; set; }
    }
}
