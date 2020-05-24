using FoodOrderBusinessLogic.BindingModels;
using FoodOrderBusinessLogic.Interfaces;
using FoodOrderBusinessLogic.ViewModels;
using FoodOrderDatabaseImplement.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FoodOrderDatabaseImplement.Implements
{
    public class StorageLogic : IStorageLogic
    {
        public List<StorageViewModel> GetList()
        {
            using (var context = new FoodOrderDatabase())
            {
                return context.Storages
                .ToList()
               .Select(rec => new StorageViewModel
               {
                   Id = rec.Id,
                   StorageName = rec.StorageName,
                   StorageDishes = context.StorageDishes
                .Include(recSD => recSD.Dish)
               .Where(recSD => recSD.StorageId == rec.Id).
               Select(x => new StorageDishViewModel
               {
                   Id = x.Id,
                   StorageId = x.StorageId,
                  DishId = x.DishId,
                   DishName = context.Dishes.FirstOrDefault(y => y.Id == x.DishId).DishName,
                   Count = x.Count
               })
               .ToList()
               })
            .ToList();
            }
        }
        public StorageViewModel GetElement(int id)
        {
            using (var context = new FoodOrderDatabase())
            {
                var elem = context.Storages.FirstOrDefault(x => x.Id == id);
                if (elem == null)
                {
                    throw new Exception("Элемент не найден");
                }
                else
                {
                    return new StorageViewModel
                    {
                        Id = id,
                        StorageName = elem.StorageName,
                        StorageDishes = context.StorageDishes
                .Include(recSD => recSD.Dish)
               .Where(recSD => recSD.StorageId == elem.Id)
                        .Select(x => new StorageDishViewModel
                        {
                            Id = x.Id,
                            StorageId = x.StorageId,
                            DishId = x.DishId,
                            DishName = context.Dishes.FirstOrDefault(y => y.Id == x.DishId).DishName,
                            Count = x.Count
                        }).ToList()
                    };
                }
            }
        }
        public void AddElement(StorageBindingModel model)
        {
            using (var context = new FoodOrderDatabase())
            {
                var elem = context.Storages.FirstOrDefault(x => x.StorageName == model.StorageName);
                if (elem != null)
                {
                    throw new Exception("Уже есть склад с таким названием");
                }
                var storage = new Storage();
                context.Storages.Add(storage);
                storage.StorageName = model.StorageName;
                context.SaveChanges();
            }
        }
        public void UpdElement(StorageBindingModel model)
        {
            using (var context = new FoodOrderDatabase())
            {
                var elem = context.Storages.FirstOrDefault(x => x.StorageName == model.StorageName && x.Id != model.Id);
                if (elem != null)
                {
                    throw new Exception("Уже есть склад с таким названием");
                }
                var elemToUpdate = context.Storages.FirstOrDefault(x => x.Id == model.Id);
                if (elemToUpdate != null)
                {
                    elemToUpdate.StorageName = model.StorageName;
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Элемент не найден");
                }
            }
        }
        public void DelElement(int id)
        {
            using (var context = new FoodOrderDatabase())
            {
                var elem = context.Storages.FirstOrDefault(x => x.Id == id);
                if (elem != null)
                {
                    context.Storages.Remove(elem);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Элемент не найден");
                }
            }
        }
        public void FillStorage(StorageDishBindingModel model)
        {
            using (var context = new FoodOrderDatabase())
            {
                var item = context.StorageDishes.FirstOrDefault(x => x.DishId == model.DishId
    && x.StorageId == model.StorageId);

                if (item != null)
                {
                    item.Count += model.Count;
                }
                else
                {
                    var elem = new StorageDish();
                    context.StorageDishes.Add(elem);
                    elem.StorageId = model.StorageId;
                    elem.DishId = model.DishId;
                    elem.Count = model.Count;
                }
                context.SaveChanges();
            }
        }
        public void RemoveFromStorage(int SetId, int SetsCount)
        {
            using (var context = new FoodOrderDatabase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var setOfDishes = context.SetOfDishes.Where(x => x.SetId == SetId);
                        if (setOfDishes.Count() == 0) return;
                        foreach (var elem in setOfDishes)
                        {
                            int left = elem.Count * SetsCount;
                            var StorageDishes = context.StorageDishes.Where(x => x.DishId == elem.DishId);
                            int available = StorageDishes.Sum(x => x.Count);
                            if (available < left) throw new Exception("Недостаточно наборов на складе");
                            foreach (var rec in StorageDishes)
                            {
                                int toRemove = left > rec.Count ? rec.Count : left;
                                rec.Count -= toRemove;
                                left -= toRemove;
                                if (left == 0) break;
                            }
                        }
                        context.SaveChanges();
                        transaction.Commit();
                        return;
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }
    }
}
