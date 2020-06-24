using FoodOrderBusinessLogic.BindingModels;
using FoodOrderBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodOrderBusinessLogic.Interfaces
{
    public interface ISetLogic
    {
        List<SetViewModel> Read(SetBindingModel model);
        void CreateOrUpdate(SetBindingModel model);
        void Delete(SetBindingModel model);
    }
}
