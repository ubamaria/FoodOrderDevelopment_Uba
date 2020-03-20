using System;
using System.Collections.Generic;
using System.Text;

namespace FoodOrderBusinessLogic.ViewModels
{
    public class ReportSetOfDishViewModel
    {
        public string DishName { get; set; }
        public int TotalCount { get; set; }
        public List<Tuple<string, int>> Sets { get; set; }
    }
}
