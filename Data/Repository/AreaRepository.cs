using Data.RepoInterface;
using DomainModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repository
{
    public class AreaRepository : Repository<Area>,  IAreaRepository
    { 
        public AreaRepository(AppDbContext _context) : base(_context)
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
