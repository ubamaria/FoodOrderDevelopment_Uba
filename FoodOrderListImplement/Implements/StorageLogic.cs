using FoodOrderBusinessLogic.BindingModels;
using FoodOrderBusinessLogic.Interfaces;
using FoodOrderBusinessLogic.ViewModels;
using FoodOrderListImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FoodOrderListImplement.Implements
{
    public class StorageLogic : IStorageLogic
    {
        private readonly DataListSingleton source;
        public StorageLogic()
        {
            source = DataListSingleton.GetInstance();
        }
        public List<StorageViewModel> GetList()
        {
            List<StorageViewModel> result = new List<StorageViewModel>();
            for (int i = 0; i < source.Storages.Count; ++i)
            {
                List<StorageDishViewModel> StorageDishes = new
    List<StorageDishViewModel>();
                for (int j = 0; j < source.StorageDishes.Count; ++j)
                {
                    if (source.StorageDishes[j].StorageId == source.Storages[i].Id)
                    {
                        string DishName = string.Empty;
                        for (int k = 0; k < source.Dishes.Count; ++k)
                        {
                            if (source.StorageDishes[j].DishId ==
                           source.Dishes[k].Id)
                            {
                                DishName = source.Dishes[k].DishName;
                                break;
                            }
                        }
                        StorageDishes.Add(new StorageDishViewModel
                        {
                            Id = source.StorageDishes[j].Id,
                            StorageId = source.StorageDishes[j].StorageId,
                            DishId = source.StorageDishes[j].DishId,
                            DishName = DishName,
                            Count = source.StorageDishes[j].Count
                        });
                    }
                }
                result.Add(new StorageViewModel
                {
                    Id = source.Storages[i].Id,
                    StorageName = source.Storages[i].StorageName,
                    StorageDishes = StorageDishes
                });
            }
            return result;
        }
        public StorageViewModel GetElement(int id)
        {
            for (int i = 0; i < source.Storages.Count; ++i)
            {
                List<StorageDishViewModel> StorageDishes = new
    List<StorageDishViewModel>();
                for (int j = 0; j < source.StorageDishes.Count; ++j)
                {
                    if (source.StorageDishes[j].StorageId == source.Storages[i].Id)
                    {
                        string DishName = string.Empty;
                        for (int k = 0; k < source.Dishes.Count; ++k)
                        {
                            if (source.StorageDishes[j].DishId ==
                           source.Dishes[k].Id)
                            {
                                DishName = source.Dishes[k].DishName;
                                break;
                            }
                        }
                        StorageDishes.Add(new StorageDishViewModel
                        {
                            Id = source.StorageDishes[j].Id,
                            StorageId = source.StorageDishes[j].StorageId,
                            DishId = source.StorageDishes[j].DishId,
                            DishName = DishName,
                            Count = source.StorageDishes[j].Count
                        });
                    }
                }
                if (source.Storages[i].Id == id)
                {
                    return new StorageViewModel
                    {
                        Id = source.Storages[i].Id,
                        StorageName = source.Storages[i].StorageName,
                        StorageDishes = StorageDishes
                    };
                }
            }
            throw new Exception("Элемент не найден");
        }
        public void AddElement(StorageBindingModel model)
        {
            int maxId = 0;
            for (int i = 0; i < source.Storages.Count; ++i)
            {
                if (source.Storages[i].Id > maxId)
                {
                    maxId = source.Storages[i].Id;
                }
                if (source.Storages[i].StorageName == model.StorageName)
                {
                    throw new Exception("Уже есть склад с таким названием");
                }
            }
            source.Storages.Add(new Storage
            {
                Id = maxId + 1,
                StorageName = model.StorageName
            });
        }
        public void UpdElement(StorageBindingModel model)
        {
            int index = -1;
            for (int i = 0; i < source.Storages.Count; ++i)
            {
                if (source.Storages[i].Id == model.Id)
                {
                    index = i;
                }
                if (source.Storages[i].StorageName == model.StorageName &&
                source.Storages[i].Id != model.Id)
                {
                    throw new Exception("Уже есть склад с таким названием");
                }
            }
            if (index == -1)
            {
                throw new Exception("Элемент не найден");
            }
            source.Storages[index].StorageName = model.StorageName;

        }
        public void DelElement(int id)
        {
            for (int i = 0; i < source.StorageDishes.Count; ++i)
            {
                if (source.StorageDishes[i].StorageId == id)
                {
                    source.StorageDishes.RemoveAt(i--);
                }
            }
            for (int i = 0; i < source.Storages.Count; ++i)
            {
                if (source.Storages[i].Id == id)
                {
                    source.Storages.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }

        public void FillStorage(StorageDishBindingModel model)
        {
            int index = -1;
            for (int i = 0; i < source.StorageDishes.Count; ++i)
            {
                if (source.StorageDishes[i].DishId == model.DishId
                    && source.StorageDishes[i].StorageId == model.StorageId)
                {
                    index = i;
                    break;
                }
            }
            if (index != -1)
            {
                source.StorageDishes[index].Count =
                    source.StorageDishes[index].Count + model.Count;
            }
            else
            {
                int maxId = 0;
                for (int i = 0; i < source.StorageDishes.Count; ++i)
                {
                    if (source.StorageDishes[i].Id > maxId)
                    {
                        maxId = source.StorageDishes[i].Id;
                    }
                }
                source.StorageDishes.Add(new StorageDish
                {
                    Id = maxId + 1,
                    StorageId = model.StorageId,
                    DishId = model.DishId,
                    Count = model.Count
                });
            }
        }
        public void CheckDishesAvailability(int SetId, int SetsCount)
        {
            var SetOfDishes = source.SetOfDishes.Where(x => x.SetId == SetId);
            if (SetOfDishes.Count() == 0) return;
            foreach (var elem in SetOfDishes)
            {
                int count = 0;
                var storageDishes = source.StorageDishes.FindAll(x => x.DishId == elem.DishId);
                count = storageDishes.Sum(x => x.Count);
                if (count < elem.Count * SetsCount)
                    return;
            }
            return;
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
