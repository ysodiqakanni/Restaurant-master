using DomainModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceLayer.Contracts
{
    public interface IAreaService
    {
        List<Area> GetAll();
        Area CreateNewArea(Area area);
        Area GetAreaById(int areaId);
    }
}
