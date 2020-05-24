using System;
using System.Collections.Generic;
using System.Text;

namespace FoodOrderBusinessLogic.BindingModels
{
    public class StorageDishBindingModel
    {
        public int Id { get; set; }
        public int StorageId { get; set; }
        public int DishId { get; set; }
        public int Count { get; set; }
    }
}
