using DomainModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceLayer.Contracts
{
    public interface IMealService
    {
        void AddMeal(Meal meal);
        Meal EditMeal(Meal meal);
        List<Meal> GetMeals();
    }
}
