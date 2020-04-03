using FoodOrderBusinessLogic.BindingModels;
using FoodOrderBusinessLogic.Interfaces;
using FoodOrderBusinessLogic.ViewModels;
using FoodOrderDatabaseImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FoodOrderFileImplement.Implements
{
    public class DishLogic : IDishLogic
    {
        private readonly FileDataListSingleton source;
        public DishLogic()
        {
            source = FileDataListSingleton.GetInstance();
        }
        public void CreateOrUpdate(DishBindingModel model)
        {
            Dish element = source.Dishes.FirstOrDefault(rec => rec.DishName
           == model.DishName && rec.Id != model.Id);
            if (element != null)
            {
                throw new Exception("Уже есть блюдо с таким названием");
            }
            if (model.Id.HasValue)
            {
                element = source.Dishes.FirstOrDefault(rec => rec.Id == model.Id);
                if (element == null)
                {
                    throw new Exception("Элемент не найден");
                }
            }
            else
            {
                int maxId = source.Dishes.Count > 0 ? source.Dishes.Max(rec =>
               rec.Id) : 0;
                element = new Dish { Id = maxId + 1 };
                source.Dishes.Add(element);
            }
            element.DishName = model.DishName;
        }
        public void Delete(DishBindingModel model)
        {
            Dish element = source.Dishes.FirstOrDefault(rec => rec.Id ==
           model.Id);
            if (element != null)
            {
                source.Dishes.Remove(element);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
        public List<DishViewModel> Read(DishBindingModel model)
        {
            return source.Dishes
            .Where(rec => model == null || rec.Id == model.Id)
            .Select(rec => new DishViewModel
            {
                Id = rec.Id,
                DishName = rec.DishName
            })
            .ToList();
        }
    }
}
