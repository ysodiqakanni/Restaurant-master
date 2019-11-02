using Data.UnitOfWork;
using DomainModel;
using ServiceLayer.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServiceLayer.Implementations
{
    public class MealService : IMealService
    {
        IUnitOfWork uow;
        public MealService(IUnitOfWork _uow)
        {
            uow = _uow;
        }
        public void AddMeal(Meal meal)
        {
            if (meal == null)
                throw new Exception("Meal cannot be null");
            uow.MealRepository.Add(meal);
            uow.Complete();
        }

        public Meal EditMeal(Meal meal)
        {
            if (meal == null)
                throw new ArgumentNullException("Meal cannot be null");
            uow.Complete();
            return meal;
        }

        public List<Meal> GetMeals()
        {
            return uow.MealRepository.GetAll().ToList();
        }
    }
}
