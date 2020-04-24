using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodOrderBusinessLogic.BindingModels;
using FoodOrderBusinessLogic.BusinessLogics;
using FoodOrderBusinessLogic.Interfaces;
using FoodOrderBusinessLogic.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodOrderRestApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MainController : ControllerBase
    {
        private readonly IOrderLogic _order;
        private readonly ISetLogic _set;
        private readonly MainLogic _main;
        public MainController(IOrderLogic order, ISetLogic set, MainLogic main)
        {
            _order = order;
            _set = set;
            _main = main;
        }
        [HttpGet]
        public List<SetModel> GetSetList() => _set.Read(null)?.Select(rec =>
      Convert(rec)).ToList();
        [HttpGet]
        public SetModel GetSet(int SetId) => Convert(_set.Read(new
       SetBindingModel
        { Id = SetId })?[0]);
        [HttpGet]
        public List<OrderViewModel> GetOrders(int clientId) => _order.Read(new
       OrderBindingModel
        { ClientId = clientId });
        [HttpPost]
        public void CreateOrder(CreateOrderBindingModel model) =>
       _main.CreateOrder(model);
        private SetModel Convert(SetViewModel model)
        {
            if (model == null) return null;
            return new SetModel
            {
                Id = model.Id,
                SetName = model.SetName,
                Price = model.Price
            };
        }
    }
}