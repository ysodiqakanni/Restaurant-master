using System;
using Data.RepoInterface;
using DomainModel;

namespace Data.Repository
{
    public class MealTypeRepository : Repository<MealType>, IMealTypeRepository
    {
        public MealTypeRepository(AppDbContext _context) : base(_context)
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
