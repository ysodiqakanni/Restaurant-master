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
            var allRestaurants = GetAllIncluding()
                .ToList(); // uow.RestaurantRepository.GetAll();
            return allRestaurants;
        }

        public List<Restaurant> GetAllWithBasicDetails()
        {
            var all = uow.RestaurantRepository.QueryAll()
                .Select(r => new Restaurant()
                {
                    Id = r.Id,
                    Name = r.Name,
                    Description = r.Description,
                    Meals = r.Meals,
                    MealTypes = r.MealTypes,
                    Address = r.Address,
                    Area = r.Area,
                    Latitude = r.Latitude,
                    Longitude = r.Longitude,
                    Priority = r.Priority,
                    WorkingHours = r.WorkingHours,
                    RestaurantCategory = r.RestaurantCategory,
                    RestaurantImages = r.RestaurantImages,
                    PhoneNumber = r.PhoneNumber,
                    DateCreated = r.DateCreated,
                    DateUpdated = r.DateUpdated

                }).ToList();
            return all;
        }

        public List<RestaurantImage> GetAllImagesByRestaurantId(int restaurantid)
        {
            return uow.RestaurantImageRepository.QueryAll()
                .Where(x => x.Restaurant.Id == restaurantid).ToList();
        }

        public List<WorkingHour> GetWorkingHoursByRestaurantId(int restaurantid)
        {
            return uow.WorkingHourRepository.QueryAll()
                .Where(x => x.Id == restaurantid).ToList();
        }

        public List<MealType> GetAllMealTypesByRestaurantId(int restaurantid)
        {
            return uow.MealTypeRepository.QueryAll()
                .Where(x => x.RestaurantId == restaurantid).ToList();
        }
        public List<MealCategory> GetAreas()
        {
            return uow.MealCategoryRepository.GetAll().ToList();
        }

        // privates
        public IQueryable<Restaurant> GetAllIncluding()
        {
            var results = uow.RestaurantRepository.GetAllIncluding(r => r.RestaurantImages)
                .Include(r => r.WorkingHours)
                .Include(r => r.RestaurantCategory)
                .Include(r => r.Area)
                .Include(r => r.Meals);

            return results;
        }

        List<Area> IRestaurantService.GetAreas()
        {
            return uow.AreaRepository.GetAll().ToList();
        }

        public List<Restaurant> GetRestaurantByAreaId(int areaId)
        {
            return GetAllIncluding().Where(r => r.AreaId == areaId).ToList();
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
            // return uow.RestaurantRepository.GetAllIncluding(r => r.RestaurantImages).Where(r => r.Id == restaurantId).FirstOrDefault();
            //return GetAllIncluding().Where(r => r.Id == restaurantId).FirstOrDefault();
            var all = uow.RestaurantRepository.QueryAll().Where(r => r.Id == restaurantId)
                .Select(r => new Restaurant()
                {
                    Id = r.Id,
                    Name = r.Name,
                    Description = r.Description,
                    Meals = r.Meals,
                    MealTypes = r.MealTypes,
                    Address = r.Address,
                    Area = r.Area,
                    Latitude = r.Latitude,
                    Longitude = r.Longitude,
                    Priority = r.Priority,
                    WorkingHours = r.WorkingHours,
                    RestaurantCategory = r.RestaurantCategory,
                    RestaurantImages = r.RestaurantImages,
                    PhoneNumber = r.PhoneNumber,
                    DateCreated = r.DateCreated,
                    DateUpdated = r.DateUpdated,
                    AreaId = r.AreaId,
                    RestaurantCategoryId = r.RestaurantCategoryId,

                }).FirstOrDefault();
            return all;
        }

        public async Task<Restaurant> UpdateRestaurant(Restaurant restaurant, List<RestaurantImage> images, List<WorkingHour> hours)
        {
            if (restaurant == null)
                throw new Exception("Restaurant cannot be null");

            // delete existing restaurant images and working hours
            uow.RestaurantImageRepository.RemoveRange(restaurant.RestaurantImages);
            uow.WorkingHourRepository.RemoveRange(restaurant.WorkingHours);

            restaurant.RestaurantImages = images;
            restaurant.WorkingHours = hours;


            uow.Complete();
            return restaurant;
        }

        public async Task<RestaurantCategory> UpdateRestaurantCategory(RestaurantCategory restaurantCategory)
        {
            if (restaurantCategory == null)
                throw new Exception("Category cannot be null");


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

        public void DeleteRestaurant(Restaurant restaurant)
        {
            if (restaurant == null)
                throw new Exception("Restaurant cannot be null");
            uow.RestaurantRepository.Remove(restaurant);
            uow.Complete();
        }
    }
}
