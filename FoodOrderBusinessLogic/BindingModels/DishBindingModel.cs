using System;
using System.Collections.Generic;
using System.Text;

namespace FoodOrderBusinessLogic.BindingModels
{
    /// <summary>
    /// Вид блюда, выбранное для заказа набора блюд
    /// </summary>
    public class DishBindingModel
    {
        public int Id { get; set; }
        public string DishName { get; set; }

    }
}
