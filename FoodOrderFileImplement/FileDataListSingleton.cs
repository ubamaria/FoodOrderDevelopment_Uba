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
        public List<Dish> Dishes { get; set; }
        public List<Order> Orders { get; set; }
        public List<Set> Sets { get; set; }
        public List<SetOfDish> SetOfDishes { get; set; }
        private FileDataListSingleton()
        {
            Dishes = LoadDishes();
            Orders = LoadOrders();
            Sets = LoadSets();
            SetOfDishes = LoadSetOfDishes();
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
                        ProductId = Convert.ToInt32(elem.Element("ProductId").Value),
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
                XDocument xDocument = XDocument.Load(SetFileName); 
                var xElements = xDocument.Root.Elements("Set").ToList();
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
                        ProductId = Convert.ToInt32(elem.Element("ProductId").Value),
                        ComponentId = Convert.ToInt32(elem.Element("ComponentId").Value),
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
                var xElement = new XElement("Components");
                foreach (var component in Components)
                {
                    xElement.Add(new XElement("Component",
                    new XAttribute("Id", component.Id),
                    new XElement("ComponentName", component.ComponentName)));
                }
                XDocument xDocument = new XDocument(xElement);
                xDocument.Save(ComponentFileName);
            }
        }
    }
}
