using Data;
using Data.UnitOfWork;
using DomainModel;
using ServiceLayer.Contracts;
using ServiceLayer.Implementations;
using System;
using System.Collections.Generic;
using System.Text;
using TestProject.TestData;
using Xunit;

namespace TestProject.UnitTests.Services
{
    public class RestaurantServiceTests : TestBase
    {
        [Fact]
        public void GetAll_ShouldReturnAllRestaurants()
        {
            using (var context = GetSampleData(nameof(GetAll_ShouldReturnAllRestaurants)))
            {
                // Arrange
                var restaurantService = MockRestaurantService(context);

                // Act
                var allRestaurants = restaurantService.GetAll();

                // Assert
                Assert.NotNull(allRestaurants);
                Assert.Equal(2, allRestaurants.Count); 
            }
        }

        [Fact]
        public void CreateNewRestaurant_ShouldCreateNewRestaurantData()
        {
            using (var context = GetSampleData(nameof(CreateNewRestaurant_ShouldCreateNewRestaurantData)))
            {
                // Arrange
                var restaurantService = MockRestaurantService(context); 
                var dataFactory = new DataFactory();
                var restaurant = dataFactory.GetRestaurant(4, "Klint smoothies", "D.C", "7473434398");

                // Act
                restaurantService.CreateNewRestaurant(restaurant); 

                var allRestaurants = restaurantService.GetAll();

                // Assert
                Assert.NotNull(allRestaurants);
                Assert.Equal(3, allRestaurants.Count); 
            }
        }
         

        #region helpers
        private IRestaurantService MockRestaurantService(AppDbContext context)
        {
            var _unitOfWork = new UnitOfWork(context); 

            var restaurantService = new RestaurantService(_unitOfWork);

            return restaurantService;
        }
        #endregion
    }
}
