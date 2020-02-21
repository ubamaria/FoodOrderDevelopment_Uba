using FoodOrderBusinessLogic.BindingModels;
using FoodOrderBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodOrderBusinessLogic.Interfaces
{
    public interface IDishLogic
    {
        List<DishViewModel> Read(DishBindingModel model);
        void CreateOrUpdate(DishBindingModel model);
        void Delete(DishBindingModel model);

    }
}
