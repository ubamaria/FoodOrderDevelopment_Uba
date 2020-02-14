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
        public List<SetViewModel> GetList()
        {
            List<SetViewModel> result = new List<SetViewModel>();
            for (int i = 0; i < source.Sets.Count; ++i)
            {
                // требуется дополнительно получить список компонентов для изделия и их количество
            List<SetOfDishViewModel> setOfDishes = new List<SetOfDishViewModel>();
                for (int j = 0; j < source.SetOfDishes.Count; ++j)
                {
                    if (source.SetOfDishes[j].SetId == source.Sets[i].Id)
                    {
                        string dishName = string.Empty;
                        for (int k = 0; k < source.Dishes.Count; ++k)
                        {
                            if (source.SetOfDishes[j].DishId ==
                           source.Dishes[k].Id)
                            {
                                dishName = source.Dishes[k].DishName;
                                break;
                            }
                        }
                        setOfDishes.Add(new SetOfDishViewModel
                        {
                            Id = source.SetOfDishes[j].Id,
                            SetId = source.SetOfDishes[j].SetId,
                            DishId = source.SetOfDishes[j].DishId,
                            DishName = dishName,
                            Count = source.SetOfDishes[j].Count
                        });
                    }
                }
                result.Add(new SetViewModel
                {
                    Id = source.Sets[i].Id,
                    SetName = source.Sets[i].SetName,
                    Price = source.Sets[i].Price,
                    SetOfDishes = setOfDishes
                });
            }
            return result;
        }
        public SetViewModel GetElement(int id)
        {
            for (int i = 0; i < source.Sets.Count; ++i)
            {
                // требуется дополнительно получить список компонентов для изделия и их количество
            List<SetOfDishViewModel> setOfDishes = new List<SetOfDishViewModel>();
                for (int j = 0; j < source.SetOfDishes.Count; ++j)
                {
                    if (source.SetOfDishes[j].SetId == source.Sets[i].Id)
                    {
                        string dishName = string.Empty;
                        for (int k = 0; k < source.Dishes.Count; ++k)
                        {
                            if (source.SetOfDishes[j].DishId ==
                           source.Dishes[k].Id)
                            {
                                dishName = source.Dishes[k].DishName;
                                break;
                            }
                        }
                        setOfDishes.Add(new SetOfDishViewModel
                        {
                            Id = source.SetOfDishes[j].Id,
                            SetId = source.SetOfDishes[j].SetId,
                            DishId = source.SetOfDishes[j].DishId,
                            DishName = dishName,
                            Count = source.SetOfDishes[j].Count
                        });
                    }
                }
                if (source.Sets[i].Id == id)
                {
                    return new SetViewModel
                    {
                        Id = source.Sets[i].Id,
                        SetName = source.Sets[i].SetName,
                        Price = source.Sets[i].Price,
                        SetOfDishes = setOfDishes
                    };
                }
            }
            throw new Exception("Элемент не найден");
        }
        public void AddElement(SetBindingModel model)
        {
            int maxId = 0;
            for (int i = 0; i < source.Sets.Count; ++i)
            {
                if (source.Sets[i].Id > maxId)
                {
                    maxId = source.Sets[i].Id;
                }
                if (source.Sets[i].SetName == model.SetName)
                {
                    throw new Exception("Уже есть набор с таким названием");
                }
            }
            source.Sets.Add(new Set
            {
                Id = maxId + 1,
                SetName = model.SetName,
                Price = model.Price
            });
            // компоненты для изделия
            int maxSDId = 0;
            for (int i = 0; i < source.SetOfDishes.Count; ++i)
            {
                if (source.SetOfDishes[i].Id > maxSDId)
                {
                    maxSDId = source.SetOfDishes[i].Id;
                }
            }
            // убираем дубли по компонентам
            for (int i = 0; i < model.SetOfDishes.Count; ++i)
            {
                for (int j = 1; j < model.SetOfDishes.Count; ++j)
                {
                    if (model.SetOfDishes[i].DishId ==
                    model.SetOfDishes[j].DishId)
                    {
                        model.SetOfDishes[i].Count +=
                        model.SetOfDishes[j].Count;
                        model.SetOfDishes.RemoveAt(j--);
                    }
                }
            }
            // добавляем компоненты
            for (int i = 0; i < model.SetOfDishes.Count; ++i)
            {
                source.SetOfDishes.Add(new SetOfDish
                {
                    Id = ++maxSDId,
                    SetId = maxId + 1,
                    DishId = model.SetOfDishes[i].DishId,
                    Count = model.SetOfDishes[i].Count
                });
            }
        }
        public void UpdElement(SetBindingModel model)
        {
            int index = -1;
            for (int i = 0; i < source.Sets.Count; ++i)
            {
                if (source.Sets[i].Id == model.Id)
                {
                    index = i;
                }
                if (source.Sets[i].SetName == model.SetName &&
                source.Sets[i].Id != model.Id)
                {
                    throw new Exception("Уже есть набор с таким названием");
                }
            }
            if (index == -1)
            {
                throw new Exception("Элемент не найден");
            }
            source.Sets[index].SetName = model.SetName;
            source.Sets[index].Price = model.Price;
            int maxSDId = 0;
            for (int i = 0; i < source.SetOfDishes.Count; ++i)
            {
                if (source.SetOfDishes[i].Id > maxSDId)
                {
                    maxSDId = source.SetOfDishes[i].Id;
                }
            }
            // обновляем существуюущие компоненты
            for (int i = 0; i < source.SetOfDishes.Count; ++i)
            {
                if (source.SetOfDishes[i].SetId == model.Id)
                {
                    bool flag = true;
                    for (int j = 0; j < model.SetOfDishes.Count; ++j)
                    {
                        // если встретили, то изменяем количество
                        if (source.SetOfDishes[i].Id ==
                        model.SetOfDishes[j].Id)
                        {
                            source.SetOfDishes[i].Count =
                           model.SetOfDishes[j].Count;
                            flag = false;
                            break;
                        }
                    }
                    // если не встретили, то удаляем
                    if (flag)
                    {
                        source.SetOfDishes.RemoveAt(i--);
                    }
                }
            }
            // новые записи
            for (int i = 0; i < model.SetOfDishes.Count; ++i)
            {
                if (model.SetOfDishes[i].Id == 0)
                {
                    // ищем дубли
                    for (int j = 0; j < source.SetOfDishes.Count; ++j)
                    {
                        if (source.SetOfDishes[j].SetId == model.Id &&
                        source.SetOfDishes[j].DishId ==
                       model.SetOfDishes[i].DishId)
                        {
                            source.SetOfDishes[j].Count +=
                           model.SetOfDishes[i].Count; 
                           model.SetOfDishes[i].Id = source.SetOfDishes[j].Id;
                            break;
                        }
                    }
                    // если не нашли дубли, то новая запись
                    if (model.SetOfDishes[i].Id == 0)
                    {
                        source.SetOfDishes.Add(new SetOfDish
                        {
                            Id = ++maxSDId,
                            SetId = model.Id,
                            DishId = model.SetOfDishes[i].DishId,
                            Count = model.SetOfDishes[i].Count
                        });
                    }
                }
            }
        }
        public void DelElement(int id)
        {
            // удаляем записи по компонентам при удалении изделия
            for (int i = 0; i < source.SetOfDishes.Count; ++i)
            {
                if (source.SetOfDishes[i].SetId == id)
                {
                    source.SetOfDishes.RemoveAt(i--);
                }
            }
            for (int i = 0; i < source.Sets.Count; ++i)
            {
                if (source.Sets[i].Id == id)
                {
                    source.Sets.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }
    }
}
