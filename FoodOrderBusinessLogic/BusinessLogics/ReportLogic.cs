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
        private readonly IStorageLogic storageLogic;
        public ReportLogic(ISetLogic setLogic, IDishLogic dishLogic,
IOrderLogic orderLogic, IStorageLogic storageLogic)
        {
            this.setLogic = setLogic;
            this.dishLogic = dishLogic;
            this.orderLogic = orderLogic;
            this.storageLogic = storageLogic;
        }
        public List<ReportSetOfDishViewModel> GetSetOfDish()
        {
            var sets = setLogic.Read(null);
            var list = new List<ReportSetOfDishViewModel>();
            foreach (var set in sets)
            {
                foreach (var ds in set.SetOfDishes)
                {
                   
                        var record = new ReportSetOfDishViewModel
                        {
                            SetName = set.SetName,
                            DishName = ds.Value.Item1,
                            Count = ds.Value.Item2
                        };
                        list.Add(record);
                }
            }
            return list;
        }
        public List<ReportStorageDishViewModel> GetStorageFoods()
        {
            var list = new List<ReportStorageDishViewModel>();
            var storages = storageLogic.GetList();
            foreach (var storage in storages)
            {
                foreach (var sd in storage.StorageDishes)
                {
                    var record = new ReportStorageDishViewModel
                    {
                        StorageName = storage.StorageName,
                        DishName = sd.DishName,
                        Count = sd.Count
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
        public void SaveStoragesToWordFile(ReportBindingModel model)
        {
            SaveToWord.CreateDoc(new WordInfo
            {
                FileName = model.FileName,
                Title = "Список складов",
                Storages = storageLogic.GetList()
            });
        }
        public void SaveStorageDishesToExcelFile(ReportBindingModel model)
        {
            SaveToExcel.CreateDoc(new ExcelInfo
            {
                FileName = model.FileName,
                Title = "Список блюд в складах",
                Storages = storageLogic.GetList()
            });
        }
        public void SaveStorageDishesToPdfFile(ReportBindingModel model)
        {
            SaveToPdf.CreateDoc(new PdfInfo
            {
                FileName = model.FileName,
                Title = "Список блюд",
                StorageDishes = GetStorageDishes()
            });
        }
    }
}
