using Data.UnitOfWork;
using DomainModel;
using ServiceLayer.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace ServiceLayer.Implementations
{
    public class MealTypeService : IMealTypeService
    {
        IUnitOfWork uow;
        public MealTypeService(IUnitOfWork _uow)
        {
            uow = _uow;
        }
        public async Task<MealType> AddMealType(MealType mealType)
        {
            if (mealType == null)
                throw new ArgumentNullException("Meal type cannot be null");

            if (String.IsNullOrEmpty(mealType.Name))
                throw new Exception("Name is required!");

            var exists = uow.MealTypeRepository.QueryAll()
                .Any(c => String.Compare(mealType.Name, c.Name, true) == 0);
            if (exists)
                throw new DuplicateWaitObjectException("Meal Type already exists!");

            return await uow.MealTypeRepository.AddAsync(mealType);
        }

        public async Task<MealType> EditMealType(MealType mealType)
        {
            if (mealType == null)
                throw new ArgumentNullException("Meal type cannot be null");

            if (String.IsNullOrEmpty(mealType.Name))
                throw new Exception("Name is required!");

            string oldName = uow.MealTypeRepository.Get(mealType.Id).Name;
            if (String.Compare(mealType.Name, oldName, true) != 0)
            {
                // name has changed! check for existence and avoid duplicate
                var exists = uow.MealTypeRepository.QueryAll()
                    .Any(c => String.Compare(mealType.Name, c.Name, true) == 0);
                if (exists)
                    throw new DuplicateWaitObjectException("Meal type already exists!");
            }

            uow.Complete();
            return mealType;
        }

        public async Task<List<MealType>> GetAllMealTypes()
        {
            var results = await uow.MealTypeRepository.GetAllAsync();
            return results.ToList();
        }

        public List<MealType> GetAllMealTypesByRestaurantId(int restaurantid)
        {
            return uow.MealTypeRepository.QueryAll()
                .Where(x => x.RestaurantId == restaurantid).ToList();
        }

        public MealType GetMealTypeById(int Id)
        {
            return uow.MealTypeRepository
                .Find(x => x.Id == Id).FirstOrDefault();
        }
    }
}
