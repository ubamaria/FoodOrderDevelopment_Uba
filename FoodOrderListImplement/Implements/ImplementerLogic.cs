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
    public class ImplementerLogic : IImplementerLogic
    {
        private readonly DataListSingleton source;
        public ImplementerLogic()
        {
            source = DataListSingleton.GetInstance();
        }
        public void CreateOrUpdate(ImplementerBindingModel model)
        {
                Implementer element = source.Implementers.FirstOrDefault(rec => rec.ImplementerFIO == model.ImplementerFIO && rec.Id != model.Id);
                if (element != null)
                {
                    throw new Exception("Уже есть исполнитель с таким именем");
                }
                if (model.Id.HasValue)
                {
                    element = source.Implementers.FirstOrDefault(rec => rec.Id == model.Id);
                    if (model.Id.HasValue)
                    {
                        if (element == null)
                        {
                            throw new Exception("Элемент не найден");
                        }

                    }
                }
                else
                {
                    element = new Implementer();
                source.Implementers.Add(element);
                }
                element.ImplementerFIO = model.ImplementerFIO;
                element.WorkingTime = model.WorkingTime;
                element.PauseTime = model.PauseTime;
        }

        public void Delete(ImplementerBindingModel model)
        {
            for (int i = 0; i < source.Implementers.Count; ++i)
            {
                if (source.Implementers[i].Id == model.Id)
                {
                    source.Implementers.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }
        public List<ImplementerViewModel> Read(ImplementerBindingModel model)
        {
            List<ImplementerViewModel> result = new List<ImplementerViewModel>();
            foreach (var implementer in source.Implementers)
            {
                if (model != null)
                {
                    if (implementer.Id == model.Id)
                    {
                        result.Add(CreateViewModel(implementer));
                        break;
                    }
                    continue;
                }
                result.Add(CreateViewModel(implementer));
            }
            return result;
        }
        private Implementer CreateModel(ImplementerBindingModel model, Implementer Implementer)
        {
            Implementer.ImplementerFIO = model.ImplementerFIO;
            Implementer.WorkingTime = model.WorkingTime;
            Implementer.PauseTime = model.PauseTime;
            return Implementer;
        }
        private ImplementerViewModel CreateViewModel(Implementer Implementer)
        {
            return new ImplementerViewModel
            {
                Id = Implementer.Id,
                ImplementerFIO = Implementer.ImplementerFIO,
                WorkingTime = Implementer.WorkingTime,
                PauseTime = Implementer.PauseTime
            };
        }
    }
}
