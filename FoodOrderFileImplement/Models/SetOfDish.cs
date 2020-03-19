using System;
using System.Collections.Generic;
using System.Text;

namespace FoodOrderDatabaseImplement.Models
{
    public class SetOfDish
    {
        public int Id { get; set; }
        public int SetId { get; set; }
        public int DishId { get; set; }
        public int Count { get; set; }
    }
}
