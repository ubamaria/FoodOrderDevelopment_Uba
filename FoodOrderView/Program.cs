using FoodOrderBusinessLogic.BusinessLogics;
using FoodOrderBusinessLogic.Interfaces;
using FoodOrderDatabaseImplement.Implement;
using System;
using System.Windows.Forms;
using Unity;
using Unity.Lifetime;

namespace FoodOrderView
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var container = BuildUnityContainer();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(container.Resolve<FormMain>());
        }
        private static IUnityContainer BuildUnityContainer()
        {
            var currentContainer = new UnityContainer();
            currentContainer.RegisterType<IDishLogic, DishLogic>(new
           HierarchicalLifetimeManager());
            currentContainer.RegisterType<IOrderLogic, OrderLogic>(new
HierarchicalLifetimeManager());
            currentContainer.RegisterType<ISetLogic, SetLogic>(new
           HierarchicalLifetimeManager());
            currentContainer.RegisterType<MainLogic>(new
           HierarchicalLifetimeManager());
            return currentContainer;
        }
    }
}
