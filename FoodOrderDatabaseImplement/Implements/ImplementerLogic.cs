using FoodOrderBusinessLogic.BindingModels;
using FoodOrderBusinessLogic.Interfaces;
using FoodOrderBusinessLogic.ViewModels;
using FoodOrderDatabaseImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FoodOrderDatabaseImplement.Implements
{
    public class ImplementerLogic : IImplementerLogic
    {
        public void CreateOrUpdate(ImplementerBindingModel model)
        {
            using (var context = new FoodOrderDatabase())
            {
                Implementer element = model.Id.HasValue ? null : new Implementer();

                if (model.Id.HasValue)
                {
                    element = context.Implementers.FirstOrDefault(rec => rec.Id == model.Id);
                }
                if (model.Id.HasValue)
                {
                    if (element == null)
                    {
                        throw new Exception("Элемент не найден");
                    }

                    CreateModel(model, element);
                }
                else
                {
                    context.Implementers.Add(CreateModel(model, element));
                }
                context.SaveChanges();
            }
        }
        private Implementer CreateModel(ImplementerBindingModel model, Implementer implementer)
        {
            using (var context = new FoodOrderDatabase())
            {
                implementer.ImplementerFIO = model.ImplementerFIO;
                implementer.WorkingTime = model.WorkingTime;
                implementer.PauseTime = model.PauseTime;

                return implementer;
            }
        }
        public void Delete(ImplementerBindingModel model)
        {
            using (var context = new FoodOrderDatabase())
            {
                Implementer element = context.Implementers.FirstOrDefault(rec => rec.Id == model.Id);

                if (element != null)
                {
                    context.Implementers.Remove(element);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Элемент не найден");
                }
            }
        }

        public List<ImplementerViewModel> Read(ImplementerBindingModel model)
        {
            using (var context = new FoodOrderDatabase())
            {
                return context.Implementers
                .Where(
                    rec => model == null
                    || rec.Id == model.Id
                )
                .Select(rec => new ImplementerViewModel
                {
                    Id = rec.Id,
                    ImplementerFIO = rec.ImplementerFIO,
                    WorkingTime = rec.WorkingTime,
                    PauseTime = rec.PauseTime
                })
                .ToList();
            }
        }
    }
}
