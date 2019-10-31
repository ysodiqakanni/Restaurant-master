using System;
using Data.RepoInterface;
using DomainModel;

namespace Data.Repository
{
    public class WorkingHourRepository : Repository<WorkingHour>, IWorkingHourRepository
    {
        public WorkingHourRepository(AppDbContext _context) : base(_context)
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
