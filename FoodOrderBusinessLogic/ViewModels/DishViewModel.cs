using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace FoodOrderBusinessLogic.ViewModels
{
    public class DishViewModel
    {
        public int Id { get; set; }

        [DisplayName("Название блюда")]
        public string DishName { get; set; }
    }
}
