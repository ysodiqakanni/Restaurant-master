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
    public class MealCategoryService : IMealCategoryService
    { 
        IUnitOfWork uow;
        public MealCategoryService(IUnitOfWork _uow)
        {
            uow = _uow;
        }
        public async Task<List<MealCategory>> GetAllMealCategories()
        {
            var results = await uow.MealCategoryRepository.GetAllAsync();
            return results.ToList();
        }
        public async Task<MealCategory> AddMealCategory(MealCategory mealCategory)
        {
            if (mealCategory == null)
                throw new ArgumentNullException("Category cannot be null");

            if (String.IsNullOrEmpty(mealCategory.Name))
                throw new Exception("Category name is required!");

            var exists = uow.MealCategoryRepository.QueryAll()
                .Any(c => String.Compare(mealCategory.Name, c.Name, true) == 0);
            if (exists)
                throw new DuplicateWaitObjectException("Category already exists!");

            return await uow.MealCategoryRepository.AddAsync(mealCategory);
        }
        public async Task<MealCategory> EditMealCategory(MealCategory mealCategory)
        {
            if (mealCategory == null)
                throw new ArgumentNullException("Category cannot be null");

            if (String.IsNullOrEmpty(mealCategory.Name))
                throw new Exception("Category name is required!");

            string oldName = uow.MealCategoryRepository.Get(mealCategory.Id).Name;
            if(String.Compare(mealCategory.Name, oldName, true) != 0)
            {
                // name has changed! check for existence and avoid duplicate
                var exists = uow.MealCategoryRepository.QueryAll()
                    .Any(c => String.Compare(mealCategory.Name, c.Name, true) == 0);
                if (exists)
                    throw new DuplicateWaitObjectException("Category already exists!");
            }

            uow.Complete();
            return mealCategory;
        }

        public async Task<MealCategory> GetMealCategoryById(int id)
        {
            return uow.MealCategoryRepository.Get(id);
        }
    }
}
