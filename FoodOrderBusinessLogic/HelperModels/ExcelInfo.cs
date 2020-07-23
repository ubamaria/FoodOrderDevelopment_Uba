using FoodOrderBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FoodOrderBusinessLogic.HelperModels
{
    class ExcelInfo
    {
        public string FileName { get; set; }
        public string Title { get; set; }
        public List<IGrouping<DateTime, OrderViewModel>> Orders { get; set; }
    }
}
