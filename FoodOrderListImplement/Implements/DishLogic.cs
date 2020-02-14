using FoodOrderBusinessLogic.BindingModels;
using FoodOrderBusinessLogic.Interfaces;
using FoodOrderBusinessLogic.ViewModels;
using FoodOrderListImplement.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodOrderListImplement.Implements
{
    public class DishLogic : IDishLogic
    {
        private readonly DataListSingleton source;
        public DishLogic()
        {
            source = DataListSingleton.GetInstance();
        }
        public List<DishViewModel> GetList()
        {
            List<DishViewModel> result = new List<DishViewModel>();
            for (int i = 0; i < source.Dishes.Count; ++i)
            {
                result.Add(new DishViewModel
                {
                    Id = source.Dishes[i].Id,
                    DishName = source.Dishes[i].DishName
                });
            }
            return result;
        }
        public DishViewModel GetElement(int id)
        {
            for (int i = 0; i < source.Dishes.Count; ++i)
            {
                if (source.Dishes[i].Id == id)
                {
                    return new DishViewModel
                    {
                        Id = source.Dishes[i].Id,
                        DishName = source.Dishes[i].DishName
                    };
                }
            }
            throw new Exception("Элемент не найден");
        }
        public void AddElement(DishBindingModel model)
        {
            int maxId = 0;
            for (int i = 0; i < source.Dishes.Count; ++i)
            {
                if (source.Dishes[i].Id > maxId)
                {
                    maxId = source.Dishes[i].Id;
                }
                if (source.Dishes[i].DishName == model.DishName)
                {
                    throw new Exception("Уже есть блюдо с таким названием");
                }
            }
            source.Dishes.Add(new Dish
            {
                Id = maxId + 1,
                DishName = model.DishName
            });
        }
        public void UpdElement(DishBindingModel model)
        {
            int index = -1;
            for (int i = 0; i < source.Dishes.Count; ++i)
            {
                if (source.Dishes[i].Id == model.Id)
                {
                    index = i;
                }
                if (source.Dishes[i].DishName == model.DishName &&
               source.Dishes[i].Id != model.Id)
                {
                    throw new Exception("Уже есть блюдо с таким названием");
                }
            }
            if (index == -1)
            {
                throw new Exception("Элемент не найден");
            }
            source.Dishes[index].DishName = model.DishName;
        }
        public void DelElement(int id)
        {
            for (int i = 0; i < source.Dishes.Count; ++i)
            {
                if (source.Dishes[i].Id == id)
                {
                    source.Dishes.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }
    }
}
