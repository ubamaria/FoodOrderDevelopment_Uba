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
        public void CreateOrUpdate(DishBindingModel model)
        {
            Dish tempDish = model.Id.HasValue ? null : new Dish
            {
                Id = 1
            };
            foreach (var dish in source.Dishes)
            {
                if (dish.DishName == model.DishName && dish.Id !=
               model.Id)
                {
                    throw new Exception("Уже есть блюдо с таким названием");
                }
                if (!model.Id.HasValue && dish.Id >= tempDish.Id)
                {
                    tempDish.Id = dish.Id + 1;
                }
                else if (model.Id.HasValue && dish.Id == model.Id)
                {
                    tempDish = dish;
                }
            }
            if (model.Id.HasValue)
            {
                if (tempDish == null)
                {
                    throw new Exception("Элемент не найден");
                }
                CreateModel(model, tempDish);
            }
            else
            {
                source.Dishes.Add(CreateModel(model, tempDish));
            }
        }
        public void Delete(DishBindingModel model)
        {
            for (int i = 0; i < source.Dishes.Count; ++i)
            {
                if (source.Dishes[i].Id == model.Id.Value)
                {
                    source.Dishes.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }
        public List<DishViewModel> Read(DishBindingModel model)
        {
            List<DishViewModel> result = new List<DishViewModel>();
            foreach (var dish in source.Dishes)
            {
                if (model != null)
                {
                    if (dish.Id == model.Id)
                    {
                        result.Add(CreateViewModel(dish));
                        break;
                    }
                    continue;
                }
                result.Add(CreateViewModel(dish));
            }
            return result;
        }
        private Dish CreateModel(DishBindingModel model, Dish dish)
        {
            dish.DishName = model.DishName;
            return dish;
        }
        private DishViewModel CreateViewModel(Dish dish)
        {
            return new DishViewModel
            {
                Id = dish.Id,
                DishName = dish.DishName
            };
        }
    }
}
