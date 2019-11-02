using DomainModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Contracts
{
    public interface IMealCategoryService
    {
        Task<List<MealCategory>> GetAllMealCategories();
        Task<MealCategory> AddMealCategory(MealCategory mealCategory);
        Task<MealCategory> EditMealCategory(MealCategory mealCategory);
        Task<MealCategory> GetMealCategoryById(int id);
    }
}
