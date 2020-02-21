using System;
using System.Collections.Generic;
using System.Text;

namespace FoodOrderBusinessLogic.BindingModels
{
    public class CreateOrderBindingModel
    {
        public int SetId { get; set; }
        public int Count { get; set; }
        public decimal Sum { get; set; }
    }
}
