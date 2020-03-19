using FoodOrderBusinessLogic.BindingModels;
using FoodOrderBusinessLogic.Interfaces;
using FoodOrderBusinessLogic.ViewModels;
using FoodOrderDatabaseImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FoodOrderDatabaseImplement.Implement
{
    public class DishLogic : IDishLogic
    {
        public void CreateOrUpdate(DishBindingModel model)
        {
            using (var context = new FoodOrderDatabase())
            {
                Dish element = context.Dishes.FirstOrDefault(rec =>
               rec.DishName == model.DishName && rec.Id != model.Id);
                if (element != null)
                {
                    throw new Exception("Уже есть блюдо с таким названием");
                }
                if (model.Id.HasValue)
                {
                    element = context.Dishes.FirstOrDefault(rec => rec.Id ==
                   model.Id);
                    if (element == null)
                    {
                        throw new Exception("Элемент не найден");
                    }
                }
                else
                {
                    element = new Dish();
                    context.Dishes.Add(element);
                }
                element.DishName = model.DishName;
                context.SaveChanges();
            }
        }
        public void Delete(DishBindingModel model)
        {
            using (var context = new FoodOrderDatabase())
            {
                Dish element = context.Dishes.FirstOrDefault(rec => rec.Id ==
               model.Id);
                if (element != null)
                {
                    context.Dishes.Remove(element);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Элемент не найден");
                }
            }
        }
        public List<DishViewModel> Read(DishBindingModel model)
        {
            using (var context = new FoodOrderDatabase())
            {
                return context.Dishes
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
}
