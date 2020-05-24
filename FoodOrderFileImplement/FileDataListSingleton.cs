﻿using FoodOrderBusinessLogic.Enums;
using FoodOrderFileImplement.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace FoodOrderFileImplement
{
    public class FileDataListSingleton
    {
        private static FileDataListSingleton instance;
        private readonly string DishFileName = "Dish.xml";
        private readonly string OrderFileName = "Order.xml";
        private readonly string SetFileName = "Set.xml";
        private readonly string SetOfDishFileName = "SetOfDish.xml";
        private readonly string StorageFileName = "Storage.xml";
        private readonly string StorageDishFileName = "StorageDish.xml";

        public List<Dish> Dishes { get; set; }
        public List<Order> Orders { get; set; }
        public List<Set> Sets { get; set; }
        public List<SetOfDish> SetOfDishes { get; set; }
        public List<Storage> Storages { set; get; }
        public List<StorageDish> StorageDishes { set; get; }
        private FileDataListSingleton()
        {
            Dishes = LoadDishes();
            Orders = LoadOrders();
            Sets = LoadSets();
            SetOfDishes = LoadSetOfDishes();
            Storages = LoadStorages();
            StorageDishes = LoadStorageDishes();
        }
        public static FileDataListSingleton GetInstance()
        {
            if (instance == null)
            {
                instance = new FileDataListSingleton();
            }
            return instance;
        }
        ~FileDataListSingleton()
        {
            SaveDishes();
            SaveOrders();
            SaveSets();
            SaveSetOfDishes();
            SaveStorages();
            SaveStorageDishes();
        }
        private List<Dish> LoadDishes()
        {
            var list = new List<Dish>();
            if (File.Exists(DishFileName))
            {
                XDocument xDocument = XDocument.Load(DishFileName);
                var xElements = xDocument.Root.Elements("Dish").ToList();
                foreach (var elem in xElements)
                {
                    list.Add(new Dish
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        DishName = elem.Element("DishName").Value
                    });
                }
            }
            return list;
        }
        private List<Order> LoadOrders()
        {
            var list = new List<Order>();
            if (File.Exists(OrderFileName))
            {
                XDocument xDocument = XDocument.Load(OrderFileName);
                var xElements = xDocument.Root.Elements("Order").ToList();
                foreach (var elem in xElements)
                {
                    list.Add(new Order
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        SetId = Convert.ToInt32(elem.Element("SetId").Value),
                        Count = Convert.ToInt32(elem.Element("Count").Value),
                        Sum = Convert.ToDecimal(elem.Element("Sum").Value),
                        Status = (OrderStatus)Enum.Parse(typeof(OrderStatus),
                   elem.Element("Status").Value),
                        DateCreate =
                   Convert.ToDateTime(elem.Element("DateCreate").Value),
                        DateImplement =
                   string.IsNullOrEmpty(elem.Element("DateImplement").Value) ? (DateTime?)null :
                   Convert.ToDateTime(elem.Element("DateImplement").Value),
                    });
                }
            }
            return list;
        }
        private List<Set> LoadSets()
        {
            var list = new List<Set>();
            if (File.Exists(SetFileName))
            {
                XDocument xDocument = XDocument.Load(SetFileName); var xElements = xDocument.Root.Elements("Set").ToList();
                foreach (var elem in xElements)
                {
                    list.Add(new Set
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        SetName = elem.Element("SetName").Value,
                        Price = Convert.ToDecimal(elem.Element("Price").Value)
                    });
                }
            }
            return list;
        }
        private List<SetOfDish> LoadSetOfDishes()
        {
            var list = new List<SetOfDish>();
            if (File.Exists(SetOfDishFileName))
            {
                XDocument xDocument = XDocument.Load(SetOfDishFileName);
                var xElements = xDocument.Root.Elements("SetOfDish").ToList();
                foreach (var elem in xElements)
                {
                    list.Add(new SetOfDish
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        SetId = Convert.ToInt32(elem.Element("SetId").Value),
                        DishId = Convert.ToInt32(elem.Element("DishId").Value),
                        Count = Convert.ToInt32(elem.Element("Count").Value)
                    });
                }
            }
            return list;
        }
        private List<Storage> LoadStorages()
        {
            var list = new List<Storage>();
            if (File.Exists(StorageFileName))
            {
                XDocument xDocument = XDocument.Load(StorageFileName);
                var xElements = xDocument.Root.Elements("Storage").ToList();
                foreach (var elem in xElements)
                {
                    list.Add(new Storage()
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        StorageName = elem.Element("StorageName").Value.ToString()
                    });
                }
            }
            return list;
        }

        private List<StorageDish> LoadStorageDishes()
        {
            var list = new List<StorageDish>();
            if (File.Exists(StorageDishFileName))
            {
                XDocument xDocument = XDocument.Load(StorageDishFileName);
                var xElements = xDocument.Root.Elements("StorageDish").ToList();
                foreach (var elem in xElements)
                {
                    list.Add(new StorageDish()
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        DishId = Convert.ToInt32(elem.Element("DishId").Value),
                        StorageId = Convert.ToInt32(elem.Element("StorageId").Value),
                        Count = Convert.ToInt32(elem.Element("Count").Value)
                    });
                }
            }
            return list;
        }
        private void SaveDishes()
        {
            if (Dishes != null)
            {
                var xElement = new XElement("Dishes");
                foreach (var dish in Dishes)
                {
                    xElement.Add(new XElement("Dish",
                    new XAttribute("Id", dish.Id),
                    new XElement("DishName", dish.DishName)));
                }
                XDocument xDocument = new XDocument(xElement);
                xDocument.Save(DishFileName);
            }
        }
        private void SaveOrders()
        {
            if (Orders != null)
            {
                var xElement = new XElement("Orders");
                foreach (var order in Orders)
                {
                    xElement.Add(new XElement("Order",
                    new XAttribute("Id", order.Id),
                    new XElement("SetId", order.SetId),
                    new XElement("Count", order.Count),
                    new XElement("Sum", order.Sum),
                    new XElement("Status", order.Status),
                    new XElement("DateCreate", order.DateCreate),
                    new XElement("DateImplement", order.DateImplement)));
                }
                XDocument xDocument = new XDocument(xElement);
                xDocument.Save(OrderFileName);
            }
        }
        private void SaveSets()
        {
            if (Sets != null)
            {
                var xElement = new XElement("Sets");
                foreach (var set in Sets)
                {
                    xElement.Add(new XElement("Set",
                    new XAttribute("Id", set.Id),
                    new XElement("SetName", set.SetName),
                    new XElement("Price", set.Price)));
                }
                XDocument xDocument = new XDocument(xElement);
                xDocument.Save(SetFileName);
            }
        }
        private void SaveSetOfDishes()
        {
            if (SetOfDishes != null)
            {
                var xElement = new XElement("SetOfDishes");
                foreach (var setDish in SetOfDishes)
                {
                    xElement.Add(new XElement("SetOfDish",
                    new XAttribute("Id", setDish.Id),
                    new XElement("SetId", setDish.SetId),
                    new XElement("DishId", setDish.DishId),
                    new XElement("Count", setDish.Count)));
                }
                XDocument xDocument = new XDocument(xElement);
                xDocument.Save(SetOfDishFileName);
            }
        }
        private void SaveStorages()
        {
            if (Storages != null)
            {
                var xElement = new XElement("Storages");
                foreach (var elem in Storages)
                {
                    xElement.Add(new XElement("Storage",
                        new XAttribute("Id", elem.Id),
                        new XElement("StorageName", elem.StorageName)));
                }
                XDocument xDocument = new XDocument(xElement);
                xDocument.Save(StorageFileName);
            }
        }
        private void SaveStorageDishes()
        {
            if (StorageDishes != null)
            {
                var xElement = new XElement("StorageDishes");
                foreach (var elem in StorageDishes)
                {
                    xElement.Add(new XElement("StorageDish",
                        new XAttribute("Id", elem.Id),
                        new XElement("DishId", elem.DishId),
                        new XElement("StorageId", elem.StorageId),
                        new XElement("Count", elem.Count)));
                }
                XDocument xDocument = new XDocument(xElement);
                xDocument.Save(StorageDishFileName);
            }
        }
    }
}
