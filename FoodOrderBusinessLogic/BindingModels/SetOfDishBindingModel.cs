using System;
using System.Collections.Generic;
using System.Text;

namespace FoodOrderBusinessLogic.BindingModels
{
    /// <summary>
    /// Сколько компонента, требуется при изготовлении изделия
    /// </summary>
    public class SetOfDishBindingModel
    {
        public int Id { get; set; }
        public int SetId { get; set; }
        public int DishId { get; set; }
        public int Count { get; set; }

    }
}
