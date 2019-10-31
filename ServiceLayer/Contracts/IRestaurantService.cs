using DomainModel;
using System;
using System.Collections.Generic;
using System.Text;

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
    }
}
