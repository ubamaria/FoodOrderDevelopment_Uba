﻿using FoodOrderBusinessLogic.BindingModels;
using FoodOrderBusinessLogic.Enums;
using FoodOrderBusinessLogic.Interfaces;
using FoodOrderBusinessLogic.ViewModels;
using FoodOrderListImplement.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodOrderListImplement.Implements
{
    public class OrderLogic : IOrderLogic
    {
        private readonly DataListSingleton source;
        public OrderLogic()
        {
            source = DataListSingleton.GetInstance();
        }
        public void CreateOrUpdate(OrderBindingModel model)
        {
            Order tempOrder = model.Id.HasValue ? null : new Order
            {
                Id = 1
            };
            foreach (var Order in source.Orders)
            {
                if (!model.Id.HasValue && Order.Id >= tempOrder.Id)
                {
                    tempOrder.Id = Order.Id + 1;
                }
                else if (model.Id.HasValue && Order.Id == model.Id)
                {
                    tempOrder = Order;
                }
            }
            if (model.Id.HasValue)
            {
                if (tempOrder == null)
                {
                    throw new Exception("Элемент не найден");
                }
                CreateModel(model, tempOrder);
            }
            else
            {
                source.Orders.Add(CreateModel(model, tempOrder));
            }
        }
        public void Delete(OrderBindingModel model)
        {
            for (int i = 0; i < source.Orders.Count; ++i)
            {
                if (source.Orders[i].Id == model.Id.Value)
                {
                    source.Orders.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }
        public List<OrderViewModel> Read(OrderBindingModel model)
        {
            List<OrderViewModel> result = new List<OrderViewModel>();
            foreach (var Order in source.Orders)
            {
                if (model != null)
                {
                    if ((model.Id.HasValue && Order.Id == model.Id)
                       || (model.DateFrom.HasValue && model.DateTo.HasValue && Order.DateCreate >= model.DateFrom && Order.DateCreate <= model.DateTo)
                       || (Order.ClientId == model.ClientId)
                       || (model.FreeOrders.HasValue && model.FreeOrders.Value && !Order.ImplementerId.HasValue)
                       || (model.ImplementerId.HasValue && Order.ImplementerId == model.ImplementerId && Order.Status == OrderStatus.Выполняется))
                    {
                        result.Add(CreateViewModel(Order));
                        break;
                    }
                    continue;
                }
                result.Add(CreateViewModel(Order));
            }
            return result;
        }
        private Order CreateModel(OrderBindingModel model, Order Order)
        {
            Set Set = null;
            foreach (Set s in source.Sets)
            {
                if (s.Id == model.SetId)
                {
                    Set = s;
                    break;
                }
            }
            Client client = null;
            foreach (Client c in source.Clients)
            {
                if (c.Id == model.ClientId)
                {
                    client = c;
                    break;
                }
            }
            Implementer implementer = null;
            foreach (Implementer i in source.Implementers)
            {
                if (i.Id == model.ImplementerId)
                {
                    implementer = i;
                    break;
                }
            }
            if (Set == null || client == null || model.ImplementerId.HasValue && implementer == null)
            {
                throw new Exception("Элемент не найден");
            }
            Order.SetId = model.SetId;
            Order.Count = model.Count;
            Order.ClientId = (int)model.ClientId;
            Order.Sum = model.Sum;
            Order.Status = model.Status;
            Order.DateCreate = model.DateCreate;
            Order.DateImplement = model.DateImplement;
            return Order;
        }
        private OrderViewModel CreateViewModel(Order Order)
        {
            Set Set = null;
            foreach (Set set in source.Sets)
            {
                if (set.Id == Order.SetId)
                {
                    Set = set;
                }
            }

            Client client = null;
            foreach (Client c in source.Clients)
            {
                if (c.Id == Order.ClientId)
                {
                    client = c;
                    break;
                }
            }
            Implementer implementer = null;
            foreach (Implementer i in source.Implementers)
            {
                if (i.Id == Order.ImplementerId)
                {
                    implementer = i;
                    break;
                }
            }
            if (Set == null || client == null || Order.ImplementerId.HasValue && implementer == null)
            {
                throw new Exception("Набор не найден");
            }
            return new OrderViewModel
            {
                Id = Order.Id,
                SetId = Order.SetId,
                SetName = Set.SetName,
                ClientId = Order.ClientId,
                Count = Order.Count,
                Sum = Order.Sum,
                Status = Order.Status,
                DateCreate = Order.DateCreate,
                DateImplement = Order.DateImplement
            };
        }
    }
}
