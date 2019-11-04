using DomainModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceLayer.Contracts
{
    public interface IMealService
    {
        void AddMeal(Meal meal);
        Meal EditMeal(Meal meal, List<MealContent> mealContents);
        List<Meal> GetMeals();
        Meal GetMealById(int id);
        List<Meal> GetMealByCategoryId(int categoryId);
    }
}
