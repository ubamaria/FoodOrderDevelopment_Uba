using FoodOrderListImplement.Models;
using System.Collections.Generic;

namespace FoodOrderListImplement
{
    public class DataListSingleton
    {
        private static DataListSingleton instance;
        public List<Dish> Dishes { get; set; }
        public List<Order> Orders { get; set; }
        public List<Set> Sets { get; set; }
        public List<SetOfDish> SetOfDishes { get; set; }
        //public List<Client> Clients { get; set; }
        private DataListSingleton()
        {
            Dishes = new List<Dish>();
            Orders = new List<Order>();
            Sets = new List<Set>();
            SetOfDishes = new List<SetOfDish>();
        }
        public static DataListSingleton GetInstance()
        {
            if (instance == null)
            {
                instance = new DataListSingleton();
            }
            return instance;
        }
    }
}
