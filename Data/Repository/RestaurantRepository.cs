using Data.RepoInterface;
using DomainModel;

namespace Data.Repository
{
    public class RestaurantRepository : Repository<Restaurant>, IRestaurantRepository
    {
        public RestaurantRepository(AppDbContext _context) : base(_context)
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