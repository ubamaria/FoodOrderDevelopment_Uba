using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AbstractShopDatabaseImplement.Models
{
    public class SetOfDish
    {
        public int Id { get; set; }

        public int SetId { get; set; }

        public int DishId { get; set; }

        [Required] public int Count { get; set; }

        public virtual Dish Dish { get; set; }

        public virtual Set Set { get; set; }
    }
}
