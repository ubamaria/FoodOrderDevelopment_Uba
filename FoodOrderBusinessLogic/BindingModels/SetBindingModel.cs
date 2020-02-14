using System;
using System.Collections.Generic;
using System.Text;

namespace FoodOrderBusinessLogic.BindingModels
{
    public class SetBindingModel
    {
        /// <summary>
        /// Изделие, изготавливаемое в магазине
        /// </summary>
        public int Id { get; set; }
        public string SetName { get; set; }
        public decimal Price { get; set; }
        public List<SetOfDishBindingModel> SetOfDishes { get; set; }
    }
}
