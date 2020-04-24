﻿using FoodOrderBusinessLogic.BindingModels;
using FoodOrderBusinessLogic.Interfaces;
using FoodOrderBusinessLogic.ViewModels;
using FoodOrderDatabaseImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FoodOrderDatabaseImplement.Implements
{
    public class ClientLogic : IClientLogic
    {
        public void CreateOrUpdate(ClientBindingModel model)
        {
            using (var context = new FoodOrderDatabase())
            {
                Client element = context.Clients.FirstOrDefault(rec => rec.Email == model.Email && rec.Id != model.Id);
                if (element != null)
                {
                    throw new Exception("Уже есть клиент с таким логином");
                }
                if (model.Id.HasValue)
                {
                    element = context.Clients.FirstOrDefault(rec => rec.Id == model.Id);
                    if (element == null)
                    {
                        throw new Exception("Элемент не найден");
                    }
                }
                else
                {
                    element = new Client();
                    context.Clients.Add(element);
                }
                element.Email = model.Email;
                element.ClientFIO = model.ClientFIO;
                element.Password = model.Password;
                context.SaveChanges();
            }
        }
        public void Delete(ClientBindingModel model)
        {
            using (var context = new FoodOrderDatabase())
            {
                Client element = context.Clients.FirstOrDefault(rec => rec.Id == model.Id);

                if (element != null)
                {
                    context.Clients.Remove(element);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Элемент не найден");
                }
            }
        }
        public List<ClientViewModel> Read(ClientBindingModel model)
        {
            using (var context = new FoodOrderDatabase())
            {
                return context.Clients
                .Where(
                    rec => model == null
                    || rec.Id == model.Id
                    || rec.Email == model.Email && rec.Password == model.Password
                )
                .Select(rec => new ClientViewModel
                {
                    Id = rec.Id,
                    ClientFIO = rec.ClientFIO,
                    Email = rec.Email,
                    Password = rec.Password
                })
                .ToList();
            }
        }
    }
}
