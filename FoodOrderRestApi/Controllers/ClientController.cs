﻿using FoodOrderBusinessLogic.BindingModels;
using FoodOrderBusinessLogic.Interfaces;
using FoodOrderBusinessLogic.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace FoodOrderRestApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientLogic _logic;
        public ClientController(IClientLogic logic)
        {
            _logic = logic;
        }
        [HttpGet]
        public ClientViewModel Login(string login, string password) => _logic.Read(new ClientBindingModel
        {
            Email = login,
            Password = password
        })?[0];
        [HttpPost]
        public void Register(ClientBindingModel model) => _logic.CreateOrUpdate(model);
        [HttpPost]
        public void UpdateData(ClientBindingModel model) => _logic.CreateOrUpdate(model);
    }
}
