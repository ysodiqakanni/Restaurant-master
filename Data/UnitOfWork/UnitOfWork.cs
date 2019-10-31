

using Data.RepoInterface;
using Data.Repository;

namespace Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private AppDbContext _context;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            RestaurantRepository = new RestaurantRepository(context);
            MealCategoryRepository = new MealCategoryRepository(context);
            MealTypeRepository = new MealTypeRepository(context);
            MealRepository = new MealRepository(context);
            RestaurantCategoryRepository = new RestaurantCategoryRepository(context);
            AreaRepository = new AreaRepository(context);
        }


        public IRestaurantRepository RestaurantRepository { get; set; }
        public IMealCategoryRepository MealCategoryRepository { get; set; }
        public IMealTypeRepository MealTypeRepository { get; set; }
        public IMealRepository MealRepository { get; set; }
        public IRestaurantCategoryRepository RestaurantCategoryRepository { get; set; }
        public IAreaRepository AreaRepository { get; set; }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}