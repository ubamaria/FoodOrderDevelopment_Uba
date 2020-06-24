﻿using FoodOrderBusinessLogic.BindingModels;
using FoodOrderBusinessLogic.Enums;
using FoodOrderBusinessLogic.Interfaces;
using FoodOrderBusinessLogic.ViewModels;
using FoodOrderFileImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FoodOrderFileImplement.Implements
{
    public class OrderLogic : IOrderLogic
    {
        private readonly FileDataListSingleton source;
        public OrderLogic()
        {
            source = FileDataListSingleton.GetInstance();
        }
        public void CreateOrUpdate(OrderBindingModel model)
        {
            Order element;
            if (model.Id.HasValue)
            {
                element = source.Orders.FirstOrDefault(rec => rec.Id == model.Id);
                if (element == null)
                {
                    throw new Exception("Элемент не найден");
                }
            }
            else
            {
                int maxId = source.Orders.Count > 0 ? source.Orders.Max(rec =>
               rec.Id) : 0;
                element = new Order { Id = maxId + 1 };
                source.Orders.Add(element);
            }
            element.SetId = model.SetId == 0 ? element.SetId : model.SetId;
            element.ClientId = model.ClientId == null ? element.ClientId : (int)model.ClientId;
            element.Count = model.Count;
            element.Sum = model.Sum;
            element.Status = model.Status;
            element.DateCreate = model.DateCreate;
            element.DateImplement = model.DateImplement;
            element.ImplementerId = model.ImplementerId;
        }
        public void Delete(OrderBindingModel model)
        {
            Order element = source.Orders.FirstOrDefault(rec => rec.Id ==
           model.Id);
            if (element != null)
            {
                source.Orders.Remove(element);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
        public List<OrderViewModel> Read(OrderBindingModel model)
        {
            return source.Orders
            .Where(rec => model == null
            || rec.Id == model.Id || (model.DateFrom.HasValue && model.DateTo.HasValue && rec.DateCreate >= model.DateFrom && rec.DateCreate <= model.DateTo)
        || (model.ClientId.HasValue && rec.ClientId == model.ClientId)
        || (model.FreeOrders.HasValue && model.FreeOrders.Value && !rec.ImplementerId.HasValue)
        || (model.ImplementerId.HasValue && rec.ImplementerId == model.ImplementerId && rec.Status == OrderStatus.Выполняется))
        .Select(rec => new OrderViewModel
        {
            Id = rec.Id,
            SetName = source.Sets.FirstOrDefault(recS => recS.Id == rec.SetId)?.SetName,
            ClientId = rec.ClientId,
            ClientFIO = source.Clients.FirstOrDefault(recC => recC.Id == rec.ClientId)?.ClientFIO,
            ImplementerFIO = source.Implementers.FirstOrDefault(recC => recC.Id == rec.ImplementerId)?.ImplementerFIO,
            Count = rec.Count,
            Sum = rec.Sum,
            Status = rec.Status,
            DateCreate = rec.DateCreate,
            DateImplement = rec.DateImplement
        })
            .ToList();
        }
    }
}
