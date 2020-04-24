using FoodOrderBusinessLogic.BindingModels;
using FoodOrderBusinessLogic.HelperModels;
using FoodOrderBusinessLogic.Interfaces;
using FoodOrderBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FoodOrderBusinessLogic.BusinessLogics
{
    public class ReportLogic
    {
        private readonly IDishLogic dishLogic;
        private readonly ISetLogic setLogic;
        private readonly IOrderLogic orderLogic;
        public ReportLogic(ISetLogic setLogic, IDishLogic dishLogic,
IOrderLogic orderLogic)
        {
            this.setLogic = setLogic;
            this.dishLogic = dishLogic;
            this.orderLogic = orderLogic;
        }
        public List<ReportSetOfDishViewModel> GetSetOfDish()
        {
            var Sets = setLogic.Read(null);
            var list = new List<ReportSetOfDishViewModel>();
            foreach (var set in Sets)
            {
                foreach (var sd in set.SetOfDishes)
                {
                        var record = new ReportSetOfDishViewModel
                        {
                            SetName = set.SetName,
                            DishName = sd.Value.Item1,
                            Count = sd.Value.Item2
                        };
                        list.Add(record);
                }
            }
            return list;
        }
        /// <summary>
        /// Получение списка заказов за определенный период
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public List<IGrouping<DateTime, OrderViewModel>> GetOrders(ReportBindingModel model)
        {
            var list = orderLogic
             .Read(new OrderBindingModel
             {
                 DateFrom = model.DateFrom,
                 DateTo = model.DateTo
             })
             .GroupBy(rec => rec.DateCreate.Date)
             .OrderBy(recG => recG.Key)
             .ToList();
            return list;
        }
        /// <summary>
        /// Сохранение компонент в файл-Word
        /// </summary>
        /// <param name="model"></param>
        public void SaveSetsToWordFile(ReportBindingModel model)
        {
            SaveToWord.CreateDoc(new WordInfo
            {
                FileName = model.FileName,
                Title = "Список наборов",
                Sets = setLogic.Read(null)
            });
        }
        /// <summary>
        /// Сохранение в файл-Excel
        /// </summary>
        /// <param name="model"></param>
        public void SaveOrdersToExcelFile(ReportBindingModel model)
        {
            SaveToExcel.CreateDoc(new ExcelInfo
            {
                FileName = model.FileName,
                Title = "Список заказов",
                Orders = GetOrders(model)
            });
        }
        /// <summary>
        /// Сохранение компонент с указаеним продуктов в файл-Pdf
        /// </summary>
        /// <param name="model"></param>
        public void SaveSetOfDishToPdfFile(ReportBindingModel model)
        {
            SaveToPdf.CreateDoc(new PdfInfo
            {
                FileName = model.FileName,
                Title = "Список блюд по наборам",
                SetOfDishes = GetSetOfDish(),
            });
        }
    }
}
