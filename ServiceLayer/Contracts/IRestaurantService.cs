using DomainModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Contracts
{
    public interface IRestaurantService
    {
        List<Restaurant> GetAll();
        Restaurant CreateNewRestaurant(Restaurant restaurant);
        List<Restaurant> GetAllWithBasicDetails();
        List<MealType> GetAllMealTypesByRestaurantId(int restaurantid);
        List<Area> GetAreas();
        List<RestaurantCategory> GetRestaurantCategories();
        RestaurantCategory GetRestaurantCategoryById(int resturantId);
        Restaurant GetRestaurantById(int restaurantId);
        Task<RestaurantCategory> UpdateRestaurantCategory(RestaurantCategory restaurantCategory);
        RestaurantCategory CreateNewRestaurantCategory(RestaurantCategory restaurantCategory);
        void DeleteRestaurantCategory(RestaurantCategory restaurantCategory);
    }
}
