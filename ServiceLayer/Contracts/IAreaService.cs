using DomainModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Contracts
{
    public interface IAreaService
    {
        List<Area> GetAll();
        Area CreateNewArea(Area area);
        Area GetAreaById(int areaId);
        Task<Area> UpdateArea(Area area);
    }
}
