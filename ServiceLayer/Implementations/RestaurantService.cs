using Data.RepoInterface;
using Data.UnitOfWork;
using DomainModel;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Implementations
{
    public class RestaurantService : IRestaurantService
    {
        IUnitOfWork uow;
        public RestaurantService(IUnitOfWork _uow)
        {
            uow = _uow;
        }

        public Restaurant CreateNewRestaurant(Restaurant restaurant)
        {
            if (restaurant == null)
                throw new Exception("Restaurant cannot be null");

            uow.RestaurantRepository.Add(restaurant);
            uow.Complete();
            return restaurant;
        }

        public List<Restaurant> GetAll()
        {
            var allRestaurants = GetAllIncluding().ToList(); // uow.RestaurantRepository.GetAll();
            return allRestaurants;
        }

        public List<Restaurant> GetAllWithBasicDetails()
        {
            var all = uow.RestaurantRepository.QueryAll()
                .Select(r => new Restaurant()
                {
                    Id = r.Id,
                    Name = r.Name,
                    Address = r.Address
                }).ToList();
            return all;
        }

        public List<MealType> GetAllMealTypesByRestaurantId(int restaurantid)
        {
            return uow.MealTypeRepository.QueryAll()
                .Where(x => x.RestaurantId == restaurantid).ToList();
        }
        public List<MealCategory>GetAreas()
        {
            return uow.MealCategoryRepository.GetAll().ToList();
        }

        // privates
        public IQueryable<Restaurant> GetAllIncluding()
        {
            var results = uow.RestaurantRepository.GetAllIncluding(r => r.RestaurantImages)
                .Include(r => r.WorkingHours)
                .Include(r => r.RestaurantCategory)
                .Include(r => r.Area);

            return results;
        }

        List<Area> IRestaurantService.GetAreas()
        {
            return uow.AreaRepository.GetAll().ToList();
        }

        public List<RestaurantCategory> GetRestaurantCategories()
        {
            return uow.RestaurantCategoryRepository.GetAll().ToList();
        }

        public RestaurantCategory GetRestaurantCategoryById(int resturantId)
        {
           return uow.RestaurantCategoryRepository.Find(r => r.Id == resturantId).FirstOrDefault();
        }

        public Restaurant GetRestaurantById(int restaurantId)
        {
            return uow.RestaurantRepository.Get(restaurantId);
        }

        public async Task<RestaurantCategory> UpdateRestaurantCategory(RestaurantCategory restaurantCategory)
        {
            if(restaurantCategory == null)
                throw new Exception("Category cannot be null");

            //var restaurantGet = uow.RestaurantCategoryRepository.Get(restaurantCategory.Id);
            //if (restaurantGet == null)
            //    throw new Exception("Category Not Found");

            //var updatedRestaurantCategory = uow.RestaurantCategoryRepository.UpdateAsync(restaurantCategory, restaurantCategory.Id);
            uow.Complete();
            return restaurantCategory;
        }

        public RestaurantCategory CreateNewRestaurantCategory(RestaurantCategory restaurantCategory)
        {
            if (restaurantCategory == null)
                throw new Exception("Category cannot be null");

            uow.RestaurantCategoryRepository.Add(restaurantCategory);
            uow.Complete();
            return restaurantCategory;
        }

        public void DeleteRestaurantCategory(RestaurantCategory restaurantCategory)
        {
            if (restaurantCategory == null)
                throw new Exception("Category cannot be null");
            uow.RestaurantCategoryRepository.Remove(restaurantCategory);
            uow.Complete();
        }
    }
}
