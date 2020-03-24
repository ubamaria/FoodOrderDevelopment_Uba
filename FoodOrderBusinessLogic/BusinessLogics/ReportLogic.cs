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
IOrderLogic orderLLogic)
        {
            this.setLogic = setLogic;
            this.dishLogic = dishLogic;
            this.orderLogic = orderLLogic;
        }
        public List<ReportSetOfDishViewModel> GetSetOfDish()
        {
            var dishes = dishLogic.Read(null);
            var sets = setLogic.Read(null);
            var list = new List<ReportSetOfDishViewModel>();
            foreach (var dish in dishes)
            {
                var record = new ReportSetOfDishViewModel
                {
                    DishName = dish.DishName,
                    Sets = new List<Tuple<string, int>>(),
                    TotalCount = 0
                };
                foreach (var set in sets)
                {
                    if (set.SetOfDishes.ContainsKey(dish.Id))
                    {
                        record.Sets.Add(new Tuple<string, int>(set.SetName,
                       set.SetOfDishes[dish.Id].Item2));
                        record.TotalCount +=
                       set.SetOfDishes[dish.Id].Item2;
                    }
                }
                list.Add(record);
            }
            return list;
        }
        /// <summary>
        /// Получение списка заказов за определенный период
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public List<ReportOrdersViewModel> GetOrders(ReportBindingModel model)
        {
            return orderLogic.Read(new OrderBindingModel
            {
                DateFrom = model.DateFrom,
                DateTo = model.DateTo
            })
            .Select(x => new ReportOrdersViewModel
            {
                DateCreate = x.DateCreate,
                SetName = x.SetName,
                Count = x.Count,
                Sum = x.Sum,
                Status = x.Status
            })
           .ToList();
        }
        /// <summary>
        /// Сохранение компонент в файл-Word
        /// </summary>
        /// <param name="model"></param>
        public void SaveDishesToWordFile(ReportBindingModel model)
        {
            SaveToWord.CreateDoc(new WordInfo
            {
                FileName = model.FileName,
                Title = "Список блюд",
                Dishes = dishLogic.Read(null)
            });
        }
        /// <summary>
        /// Сохранение компонент с указаеним продуктов в файл-Excel
        /// </summary>
        /// <param name="model"></param>
        public void SaveSetOfDishToExcelFile(ReportBindingModel model)
        {
            SaveToExcel.CreateDoc(new ExcelInfo
            {
                FileName = model.FileName,
                Title = "Список блюд",
                SetOfDishes = GetSetOfDish()
            });
        }
        /// <summary>
        /// Сохранение заказов в файл-Pdf
        /// </summary>
        /// <param name="model"></param>
        public void SaveOrdersToPdfFile(ReportBindingModel model)
        {
            SaveToPdf.CreateDoc(new PdfInfo
            {
                FileName = model.FileName,
                Title = "Список заказов",
                DateFrom = model.DateFrom.Value,
                DateTo = model.DateTo.Value,
                Orders = GetOrders(model)
            });
        }
    }
}
