using System;
using Data.RepoInterface;
using DomainModel;

namespace Data.Repository
{
    public class MealContentRepository : Repository<MealContent>, IMealContentRepository
    {
        public MealContentRepository(AppDbContext _context) : base(_context)
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
