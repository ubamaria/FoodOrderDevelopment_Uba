using FoodOrderBusinessLogic.BindingModels;
using FoodOrderBusinessLogic.Interfaces;
using FoodOrderBusinessLogic.ViewModels;
using FoodOrderDatabaseImplement.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FoodOrderDatabaseImplement.Implement
{
    public class SetLogic : ISetLogic
    {
        public void CreateOrUpdate(SetBindingModel model)
        {
            using (var context = new FoodOrderDatabase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        Set element = context.Sets.FirstOrDefault(rec =>
                       rec.SetName == model.SetName && rec.Id != model.Id);
                        if (element != null)
                        {
                            throw new Exception("Уже есть набор с таким названием");
                        }
                        if (model.Id.HasValue)
                        {
                            element = context.Sets.FirstOrDefault(rec => rec.Id ==
                           model.Id);
                            if (element == null)
                            {
                                throw new Exception("Элемент не найден");
                            }
                        }
                        else
                        {
                            element = new Set();
                            context.Sets.Add(element);
                        }
                        element.SetName = model.SetName;
                        element.Price = model.Price;
                        context.SaveChanges();
                        if (model.Id.HasValue)
                        {
                            var setOfDishes = context.SetOfDishes.Where(rec
                           => rec.SetId == model.Id.Value).ToList();
                            // удалили те, которых нет в модели
                            context.SetOfDishes.RemoveRange(setOfDishes.Where(rec =>
                            !model.SetOfDishes.ContainsKey(rec.DishId)).ToList());
                            context.SaveChanges();
                            // обновили количество у существующих записей
                            foreach (var updateDish in setOfDishes)
                            {
                                updateDish.Count =
                               model.SetOfDishes[updateDish.DishId].Item2;

                                model.SetOfDishes.Remove(updateDish.DishId);
                            }
                            context.SaveChanges();
                        }
                        // добавили новые
                        foreach (var pc in model.SetOfDishes)
                        {
                            context.SetOfDishes.Add(new SetOfDish
                            {
                                SetId = element.Id,
                                DishId = pc.Key,
                                Count = pc.Value.Item2
                            });
                            context.SaveChanges();
                        }
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }
        public void Delete(SetBindingModel model)
        {
            using (var context = new FoodOrderDatabase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        // удаяем записи по компонентам при удалении изделия
                        context.SetOfDishes.RemoveRange(context.SetOfDishes.Where(rec =>
                        rec.SetId == model.Id));
                        Set element = context.Sets.FirstOrDefault(rec => rec.Id == model.Id);
                        if (element != null)
                        {
                            context.Sets.Remove(element);
                            context.SaveChanges();
                        }
                        else
                        {
                            throw new Exception("Элемент не найден");
                        }
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }
        public List<SetViewModel> Read(SetBindingModel model)
        {
            using (var context = new FoodOrderDatabase())
            {
                return context.Sets
                .Where(rec => model == null || rec.Id == model.Id)
                .ToList()
               .Select(rec => new SetViewModel
               {
                   Id = rec.Id,
                   SetName = rec.SetName,
                   Price = rec.Price,
                   SetOfDishes = context.SetOfDishes
                .Include(recSD => recSD.Dish)
               .Where(recSD => recSD.SetId == rec.Id)
               .ToDictionary(recSD => recSD.DishId, recSD =>
                (recSD.Dish?.DishName, recSD.Count))
               })
               .ToList();
            }
        }
    }
}
