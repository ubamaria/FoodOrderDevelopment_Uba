using FoodOrderBusinessLogic.BindingModels;
using FoodOrderBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodOrderBusinessLogic.Interfaces
{
    public interface ISetLogic
    {
        List<SetViewModel> GetList();
        SetViewModel GetElement(int id);
        void AddElement(SetBindingModel model);
        void UpdElement(SetBindingModel model);
        void DelElement(int id);
    }
}
