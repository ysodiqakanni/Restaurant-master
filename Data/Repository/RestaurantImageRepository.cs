using System;
using Data.RepoInterface;
using DomainModel;

namespace Data.Repository
{
    public class RestaurantImageRepository : Repository<RestaurantImage>, IRestaurantImageRepository
    {
        public RestaurantImageRepository(AppDbContext _context) : base(_context)
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
