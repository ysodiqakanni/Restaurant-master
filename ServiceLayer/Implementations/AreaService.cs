using Data.UnitOfWork;
using DomainModel;
using ServiceLayer.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Implementations
{
    public class AreaService : IAreaService
    {
        IUnitOfWork uow;
        public AreaService(IUnitOfWork _uow)
        {
            uow = _uow;
        }

        public Area CreateNewArea(Area area)
        {
            if (area == null)
                throw new Exception("Area cannot be null");

            uow.AreaRepository.Add(area);
            uow.Complete();
            return area;
        }

        public List<Area> GetAll()
        {
            var allAreas = uow.AreaRepository.GetAll().ToList(); // uow.AreaRepository.GetAll();
            return allAreas;
        }

        public Area GetAreaById(int areaId)
        {
            return uow.AreaRepository.Get(areaId);
        }

        public async Task<Area> UpdateArea(Area area)
        {
            if (area == null)
                throw new Exception("Area cannot be null");
 
            uow.Complete();
            return area;

        }
    }
}
