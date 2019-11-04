using Data.UnitOfWork;
using DomainModel;
using ServiceLayer.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 
using Microsoft.EntityFrameworkCore; 

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

        public Meal EditMeal(Meal meal, List<MealContent> mealContents)
        {
            if (meal == null)
                throw new ArgumentNullException("Meal cannot be null");

            uow.MealContentRepository.RemoveRange(meal.MealContents);
            if (mealContents != null && mealContents.Any())
            {
                meal.MealContents = mealContents; 
            }
            uow.Complete();
            return meal;
        }

        public Meal GetMealById(int id)
        { 
            var meal = uow.MealRepository.GetAllIncluding(x => x.MealCategory)
                .Include(x => x.MealContents)
                .Include(x => x.MealCategory)
                .Where(x => x.Id == id).FirstOrDefault();

            return meal;
        }

        public List<Meal> GetMeals()
        {
            return uow.MealRepository.GetAll().ToList();
        }
    }
}
