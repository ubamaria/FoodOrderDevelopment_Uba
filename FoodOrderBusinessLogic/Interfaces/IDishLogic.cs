using FoodOrderBusinessLogic.BindingModels;
using FoodOrderBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodOrderBusinessLogic.Interfaces
{
    public interface IDishLogic
    {
        List<DishViewModel> GetList();
        DishViewModel GetElement(int id);
        void AddElement(DishBindingModel model);
        void UpdElement(DishBindingModel model);
        void DelElement(int id);

    }
}
