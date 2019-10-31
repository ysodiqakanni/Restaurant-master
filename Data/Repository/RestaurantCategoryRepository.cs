using System;
using Data.RepoInterface;
using DomainModel;

namespace Data.Repository
{
    public class RestaurantCategoryRepository : Repository<RestaurantCategory>, IRestaurantCategoryRepository
    {
        public RestaurantCategoryRepository(AppDbContext _context) : base(_context)
        {
        }

        public AppDbContext AppContext
        {
            get
            {
                return Context as AppDbContext;
            }
        }
    }
}
