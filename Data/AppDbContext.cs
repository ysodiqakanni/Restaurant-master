using DomainModel;
using Microsoft.EntityFrameworkCore;
namespace Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Area>();
        }

        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<RestaurantCategory> RestaurantCategories { get; set; }
        public DbSet<RestaurantImage> RestaurantImages { get; set; }
        public DbSet<Meal> Meals { get; set; }
        public DbSet<MealCategory> MealCategories { get; set; }
        public DbSet<MealContent> MealContents { get; set; }
        public DbSet<MealType> MealTypes { get; set; }
        public DbSet<WorkingHour> WorkingHours { get; set; }
        public DbSet<Area> Areas { get; set; }

    }
}