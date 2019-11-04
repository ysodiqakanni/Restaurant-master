using DomainModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Contracts
{
    public interface IMealTypeService
    {
        Task<List<MealType>> GetAllMealTypes();
        Task<MealType> AddMealType(MealType mealType);
        Task<MealType> EditMealType(MealType mealType);
        List<MealType> GetAllMealTypesByRestaurantId(int restaurantid);
        MealType GetMealTypeById(int Id);
    }
}
