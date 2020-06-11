using FoodOrderBusinessLogic.BindingModels;
using FoodOrderBusinessLogic.Interfaces;
using FoodOrderBusinessLogic.ViewModels;
using FoodOrderFileImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FoodOrderFileImplement.Implements
{
    public class StorageLogic : IStorageLogic
    {
        private readonly FileDataListSingleton source;
        public StorageLogic()
        {
            source = FileDataListSingleton.GetInstance();
        }
        public List<StorageViewModel> GetList()
        {
            return source.Storages.Select(rec => new StorageViewModel
            {
                Id = rec.Id,
                StorageName = rec.StorageName,
                StorageDishes = source.StorageDishes.Where(z => z.StorageId == rec.Id).Select(x => new StorageDishViewModel
                {
                    Id = x.Id,
                    StorageId = x.StorageId,
                    DishId = x.DishId,
                    DishName = source.Dishes.FirstOrDefault(y => y.Id == x.DishId)?.DishName,
                    Count = x.Count
                }).ToList()
            })
                .ToList();
        }
        public StorageViewModel GetElement(int id)
        {
            var elem = source.Storages.FirstOrDefault(x => x.Id == id);
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
                    StorageDishes = source.StorageDishes.Where(z => z.StorageId == elem.Id).Select(x => new StorageDishViewModel
                    {
                        Id = x.Id,
                        StorageId = x.StorageId,
                        DishId = x.DishId,
                        DishName = source.Dishes.FirstOrDefault(y => y.Id == x.DishId)?.DishName,
                        Count = x.Count
                    }).ToList()
                };
            }
        }

        public void AddElement(StorageBindingModel model)
        {

            var elem = source.Storages.FirstOrDefault(x => x.StorageName == model.StorageName);
            if (elem != null)
            {
                throw new Exception("Уже есть склад с таким названием");
            }
            int maxId = source.Storages.Count > 0 ? source.Storages.Max(rec => rec.Id) : 0;
            source.Storages.Add(new Storage
            {
                Id = maxId + 1,
                StorageName = model.StorageName
            });
        }
        public void UpdElement(StorageBindingModel model)
        {
            var elem = source.Storages.FirstOrDefault(x => x.StorageName == model.StorageName && x.Id != model.Id);
            if (elem != null)
            {
                throw new Exception("Уже есть склад с таким названием");
            }
            var elemToUpdate = source.Storages.FirstOrDefault(x => x.Id == model.Id);
            if (elemToUpdate != null)
            {
                elemToUpdate.StorageName = model.StorageName;
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
        public void DelElement(int id)
        {
            var elem = source.Storages.FirstOrDefault(x => x.Id == id);
            if (elem != null)
            {
                source.Storages.Remove(elem);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }

        public void FillStorage(StorageDishBindingModel model)
        {
            var item = source.StorageDishes.FirstOrDefault(x => x.DishId == model.DishId
                    && x.StorageId == model.StorageId);

            if (item != null)
            {
                item.Count += model.Count;
            }
            else
            {
                int maxId = source.StorageDishes.Count > 0 ? source.StorageDishes.Max(rec => rec.Id) : 0;
                source.StorageDishes.Add(new StorageDish
                {
                    Id = maxId + 1,
                    StorageId = model.StorageId,
                    DishId = model.DishId,
                    Count = model.Count
                });
            }
        }

        public bool CheckDishesAvailability(int SetId, int SetsCount)
        {     
            var SetOfDishes = source.SetOfDishes.Where(x => x.SetId == SetId);
            if (SetOfDishes.Count() == 0) 
                return false;
            foreach (var elem in SetOfDishes)
            {
                int count = 0;
                var storageDishes = source.StorageDishes.FindAll(x => x.DishId == elem.DishId);
                count = storageDishes.Sum(x => x.Count);
                if (count < elem.Count * SetsCount)
                    return false;
            }
            return true;
        }

        public void RemoveFromStorage(int SetId, int SetsCount)
        {
            var SetOfDishes = source.SetOfDishes.Where(x => x.SetId == SetId);
            if (SetOfDishes.Count() == 0) return;
            foreach (var elem in SetOfDishes)
            {
                int left = elem.Count * SetsCount;
                var storageDishes = source.StorageDishes.FindAll(x => x.DishId == elem.DishId);
                foreach (var rec in storageDishes)
                {
                    int toRemove = left > rec.Count ? rec.Count : left;
                    rec.Count -= toRemove;
                    left -= toRemove;
                    if (left == 0) break;
                }
            }
            return;
        }
    }
}
