using FoodOrderDatabaseImplement.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodOrderDatabaseImplement
{
    public class FoodOrderDatabase : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured == false)
            {
                optionsBuilder.UseSqlServer(
                    @"Data Source=DESKTOP-BUNOAQN\SQLEXPRESS;
Initial Catalog=FoodOrderDatabase;
Integrated Security=True;
MultipleActiveResultSets=True;");
            }
            base.OnConfiguring(optionsBuilder);
        }
        public virtual DbSet<Dish> Dishes { set; get; }
        public virtual DbSet<Set> Sets { set; get; }
        public virtual DbSet<SetOfDish> SetOfDishes { set; get; }
        public virtual DbSet<Order> Orders { set; get; }
        public virtual DbSet<Client> Clients { set; get; }
        public virtual DbSet<Implementer> Implementers { set; get; }
    }
}
