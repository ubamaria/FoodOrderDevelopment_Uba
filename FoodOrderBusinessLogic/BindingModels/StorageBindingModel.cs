using System;
using System.Collections.Generic;
using System.Text;

namespace FoodOrderBusinessLogic.BindingModels
{
    public class StorageBindingModel
    {
        public int Id { get; set; }
        public string StorageName { get; set; }
        public List<StorageDishBindingModel> StorageDishes { get; set; }
    }
}
