using FoodOrderBusinessLogic.BindingModels;
using FoodOrderBusinessLogic.Interfaces;
using FoodOrderBusinessLogic.ViewModels;
using FoodOrderListImplement.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodOrderListImplement.Implements
{
    public class SetLogic : ISetLogic
    {
        private readonly DataListSingleton source;
        public SetLogic()
        {
            source = DataListSingleton.GetInstance();
        }

        public void CreateOrUpdate(SetBindingModel model)
        {
            Set tempSet = model.Id.HasValue ? null : new Set { Id = 1 };
            foreach (var set in source.Sets)
            {
                if (set.SetName == model.SetName && set.Id != model.Id)
                {
                    throw new Exception("Уже есть набор с таким названием");
                }
                if (!model.Id.HasValue && set.Id >= tempSet.Id)
                {
                    tempSet.Id = set.Id + 1;
                }
                else if (model.Id.HasValue && set.Id == model.Id)
                {
                    tempSet = set;
                }
            }
            if (model.Id.HasValue)
            {
                if (tempSet == null)
                {
                    throw new Exception("Элемент не найден");
                }
                CreateModel(model, tempSet);
            }
            else
            {
                source.Sets.Add(CreateModel(model, tempSet));
            }
        }
        public void Delete(SetBindingModel model)
        {
            // удаляем записи по компонентам при удалении изделия
            for (int i = 0; i < source.SetOfDishes.Count; ++i)
            {
                if (source.SetOfDishes[i].SetId == model.Id)
                {
                    source.SetOfDishes.RemoveAt(i--);
                }
            }
            for (int i = 0; i < source.Sets.Count; ++i)
            {
                if (source.Sets[i].Id == model.Id)
                {
                    source.Sets.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }
        private Set CreateModel(SetBindingModel model, Set set)
        {
            set.SetName = model.SetName;
            set.Price = model.Price;
            //обновляем существуюущие компоненты и ищем максимальный идентификатор
            int maxSDId = 0;
            for (int i = 0; i < source.SetOfDishes.Count; ++i)
            {
                if (source.SetOfDishes[i].Id > maxSDId)
                {
                    maxSDId = source.SetOfDishes[i].Id;
                }
                if (source.SetOfDishes[i].SetId == set.Id)
                {
                    // если в модели пришла запись компонента с таким id
                    if
                    (model.SetOfDishes.ContainsKey(source.SetOfDishes[i].DishId))
                    {
                        // обновляем количество
                        source.SetOfDishes[i].Count =
                        model.SetOfDishes[source.SetOfDishes[i].DishId].Item2;
                        // из модели убираем эту запись, чтобы остались только не просмотренные
model.SetOfDishes.Remove(source.SetOfDishes[i].DishId);
                    }
                    else
                    {
                        source.SetOfDishes.RemoveAt(i--);
                    }
                }
            }
            // новые записи
            foreach (var sd in model.SetOfDishes)
            {
                source.SetOfDishes.Add(new SetOfDish
                {
                    Id = ++maxSDId,
                    SetId = set.Id,
                    DishId = sd.Key,
                    Count = sd.Value.Item2
                });
            }
            return set;
        }
        public List<SetViewModel> Read(SetBindingModel model)
        {
            List<SetViewModel> result = new List<SetViewModel>();
            foreach (var dish in source.Sets)
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
        private SetViewModel CreateViewModel(Set set)
        {
            // требуется дополнительно получить список компонентов для изделия с названиями и их количество
 Dictionary<int, (string, int)> setOfDishes = new Dictionary<int,
(string, int)>();
            foreach (var sd in source.SetOfDishes)
            {
                if (sd.SetId == set.Id)
                {
                    string dishName = string.Empty;
                    foreach (var dish in source.Dishes)
                    {
                        if (sd.DishId == dish.Id)
                        {
                            dishName = dish.DishName;
                            break;
                        }
                    }
                    setOfDishes.Add(sd.DishId, (dishName, sd.Count));
                }
            }
            return new SetViewModel
            {
                Id = set.Id,
                SetName = set.SetName,
                Price = set.Price,
                SetOfDishes = setOfDishes
            };
        }
    }
}
