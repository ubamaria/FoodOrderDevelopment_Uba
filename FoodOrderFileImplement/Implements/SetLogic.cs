using FoodOrderBusinessLogic.BindingModels;
using FoodOrderBusinessLogic.Interfaces;
using FoodOrderBusinessLogic.ViewModels;
using FoodOrderDatabaseImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FoodOrderFileImplement.Implements
{
    public class SetLogic : ISetLogic
    {
        private readonly FileDataListSingleton source;
        public SetLogic()
        {
            source = FileDataListSingleton.GetInstance();
        }
        public void CreateOrUpdate(SetBindingModel model)
        {
            Set element = source.Sets.FirstOrDefault(rec => rec.SetName ==
           model.SetName && rec.Id != model.Id);
            if (element != null)
            {
                throw new Exception("Уже есть набор с таким названием");
            }
            if (model.Id.HasValue)
            {
                element = source.Sets.FirstOrDefault(rec => rec.Id == model.Id);
                if (element == null)
                {
                    throw new Exception("Элемент не найден");
                }
            }
            else
            {
                int maxId = source.Sets.Count > 0 ? source.Sets.Max(rec =>
               rec.Id) : 0;
                element = new Set { Id = maxId + 1 };
                source.Sets.Add(element);
            }
            element.SetName = model.SetName;
            element.Price = model.Price;
            // удалили те, которых нет в модели
            source.SetOfDishes.RemoveAll(rec => rec.SetId == model.Id &&
           !model.SetOfDishes.ContainsKey(rec.SetId));
            // обновили количество у существующих записей
            var updateDishes = source.SetOfDishes.Where(rec => rec.SetId ==
           model.Id && model.SetOfDishes.ContainsKey(rec.DishId));
            foreach (var updateDish in updateDishes)
            {
                updateDish.Count =
               model.SetOfDishes[updateDish.DishId].Item2;
                model.SetOfDishes.Remove(updateDish.DishId);
            }
            // добавили новые
            int maxSDId = source.SetOfDishes.Count > 0 ?
           source.SetOfDishes.Max(rec => rec.Id) : 0; 
           foreach (var sd in model.SetOfDishes)
            {
                source.SetOfDishes.Add(new SetOfDish
                {
                    Id = ++maxSDId,
                    SetId = element.Id,
                   DishId = sd.Key,
                    Count = sd.Value.Item2
                });
            }
        }
        public void Delete(SetBindingModel model)
        {
            // удаяем записи по компонентам при удалении изделия
            source.SetOfDishes.RemoveAll(rec => rec.SetId == model.Id);
            Set element = source.Sets.FirstOrDefault(rec => rec.Id == model.Id);
            if (element != null)
            {
                source.Sets.Remove(element);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
        public List<SetViewModel> Read(SetBindingModel model)
        {
            return source.Sets
            .Where(rec => model == null || rec.Id == model.Id)
            .Select(rec => new SetViewModel
            {
                Id = rec.Id,
                SetName = rec.SetName,
                Price = rec.Price,
                SetOfDishes = source.SetOfDishes
            .Where(recSD => recSD.SetId == rec.Id)
           .ToDictionary(recSD => recSD.DishId, recSD =>
            (source.Dishes.FirstOrDefault(recD => recD.Id ==
           recSD.DishId)?.DishName, recSD.Count))
            })
            .ToList();
        }
    }
}
