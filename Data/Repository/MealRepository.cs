using System;
using Data.RepoInterface;
using DomainModel;

namespace Data.Repository
{
    public class MealRepository : Repository<Meal>, IMealRepository
    {
        public MealRepository(AppDbContext _context) : base(_context)
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
