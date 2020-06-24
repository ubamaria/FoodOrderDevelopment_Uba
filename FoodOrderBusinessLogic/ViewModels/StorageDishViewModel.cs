using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace FoodOrderBusinessLogic.ViewModels
{
    public class StorageDishViewModel
    {
        public int Id { get; set; }
        public int StorageId { get; set; }
        public int DishId { get; set; }
        [DisplayName("Название блюда")]
        public string DishName { get; set; }
        [DisplayName("Количество")]
        public int Count { get; set; }
    }
}
