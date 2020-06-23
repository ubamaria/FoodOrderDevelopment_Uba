using FoodOrderBusinessLogic.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodOrderBusinessLogic.ViewModels
{
    public class ReportOrdersViewModel
    {
        public DateTime DateCreate { get; set; }
        public string SetName { get; set; }
        public int Count { get; set; }
        public decimal Sum { get; set; }
        public string Status { get; set; }
    }
}
