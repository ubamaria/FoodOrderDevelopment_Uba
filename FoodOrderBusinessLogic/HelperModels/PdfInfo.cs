using FoodOrderBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodOrderBusinessLogic.HelperModels
{
    class PdfInfo
    {
        public string FileName { get; set; }
        public string Title { get; set; }
        public List<ReportSetOfDishViewModel> SetOfDishes { get; set; }
        public List<ReportStorageDishViewModel> StorageDishes { get; set; }
    }
}
