using System;
using Data.RepoInterface;
using DomainModel;

namespace Data.Repository
{
    public class MealCategoryRepository:Repository<MealCategory>, IMealCategoryRepository
    {
        public MealCategoryRepository(AppDbContext _context) : base(_context)
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
