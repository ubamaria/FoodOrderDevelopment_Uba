using FoodOrderBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodOrderBusinessLogic.HelperModels
{
    class ExcelInfo
    {
        public string FileName { get; set; }
        public string Title { get; set; }
        public List<ReportSetOfDishViewModel> SetOfDishes { get; set; }
    }
}
