using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace FoodOrderBusinessLogic.ViewModels
{
    public class SetOfDishViewModel
    {
        public int Id { get; set; }
        public int SetId { get; set; }
        public int DishId { get; set; }

        [DisplayName("Блюдо")]
        public string DishName { get; set; }

        [DisplayName("Количество")]
        public int Count { get; set; }
    }
}
