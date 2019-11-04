using Data.UnitOfWork;
using ServiceLayer.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceLayer.Implementations
{
    public class MealContentService : IMealContentService
    {
        IUnitOfWork uow;
        public MealContentService(IUnitOfWork _uow)
        {
            uow = _uow;
        }
    }
}
