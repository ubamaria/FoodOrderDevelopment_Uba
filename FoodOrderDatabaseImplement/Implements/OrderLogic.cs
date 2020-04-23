﻿using FoodOrderBusinessLogic.BindingModels;
using FoodOrderBusinessLogic.Interfaces;
using FoodOrderBusinessLogic.ViewModels;
using FoodOrderDatabaseImplement.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FoodOrderDatabaseImplement.Implement
{
    public class OrderLogic : IOrderLogic
    {
        public void CreateOrUpdate(OrderBindingModel model)
        {
            {
                using (var context = new FoodOrderDatabase())
                {
                    Order element;
                    if (model.Id.HasValue)
                    {
                        element = context.Orders.FirstOrDefault(rec => rec.Id ==
                       model.Id);
                        if (element == null)
                        {
                            throw new Exception("Элемент не найден");
                        }
                    }
                    else
                    {
                        element = new Order();
                        context.Orders.Add(element);
                    }
                    element.SetId = model.SetId == 0 ? element.SetId : model.SetId;
                    element.Count = model.Count;
                    element.Sum = model.Sum;
                    element.Status = model.Status;
                    element.DateCreate = model.DateCreate;
                    element.DateImplement = model.DateImplement;
                    context.SaveChanges();
                }
            }
        }
        public void Delete(OrderBindingModel model)
        {
            using (var context = new FoodOrderDatabase())
            {
                Order element = context.Orders.FirstOrDefault(rec => rec.Id ==
model.Id);
                if (element != null)
                {
                    context.Orders.Remove(element);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Элемент не найден");
                }
            }
        }
        public List<OrderViewModel> Read(OrderBindingModel model)
        {
            //using (var context = new FoodOrderDatabase())
            //{
            //    return context.Orders.Where(rec => model == null || (rec.Id == model.Id && model.Id.HasValue)
            //    || (model.DateFrom.HasValue && model.DateTo.HasValue && rec.DateCreate >= model.DateFrom && rec.DateCreate <= model.DateTo))
            //    .Select(rec => new OrderViewModel
            //    {
            //        Id = rec.Id,
            //        ClientId = rec.ClientId,
            //        SetId = rec.SetId,
            //        ClientFIO = rec.ClientFIO,
            //        SetName = rec.Set.SetName,
            //        Count = rec.Count,
            //        Sum = rec.Sum,
            //        Status = rec.Status,
            //        DateCreate = rec.DateCreate,
            //        DateImplement = rec.DateImplement
            //    })
            //.ToList();
            //}
            return source.Orders.Where(rec => model == null || (rec.Id == model.Id && model.Id.HasValue) || 
            (model.DateFrom.HasValue && model.DateTo.HasValue && rec.DateCreate >= model.DateFrom && rec.DateCreate <= model.DateTo) || 
            (model.ClientId.HasValue && rec.ClientId == model.ClientId))
            .Select(rec => new OrderViewModel
            {
                Id = rec.Id,
                SetId = rec.SetId,
                ClientId = rec.ClientId,
                DateCreate = rec.DateCreate,
                DateImplement = rec.DateImplement,
                Status = rec.Status,
                Count = rec.Count,
                Sum = rec.Sum,
                ClientFIO = source.Clients.FirstOrDefault(recC => recC.Id == rec.ClientId)?.ClientFIO,
                SetName = source.Products.FirstOrDefault(recP => recP.Id == rec.ProductId)?.ProductName,
            })
            .ToList();
        }
    }
}
