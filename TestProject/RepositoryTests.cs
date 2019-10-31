using Data;
using Data.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using DomainModel;
using TestProject.TestData;

namespace TestProject
{
    public class RepositoryTests : TestBase
    {

        [Fact]
        public void GetAll_Test()
        {
            using (var context = GetSampleData(nameof(GetAll_Test)))
            {
                // Arrange
                var _unitOfWork = new UnitOfWork(context);

                // Act
                var restaurants = _unitOfWork.RestaurantRepository.GetAll();

                // Assert
                Assert.Equal(2, restaurants.Count());
            }
        }

        [Fact]
        public void Add_Test()
        {
            using (var context = GetSampleData(nameof(Add_Test)))
            {
                // Arrange
                var uow = new UnitOfWork(context);

                var dataFactory = new DataFactory();

                // Act
                // add a new restaurant to make 3
                var restaurant = dataFactory.GetRestaurant(3, "new rest", "coding arena", "4974"); 
                uow.RestaurantRepository.Add(restaurant); 
                uow.Complete();
                var allRestaurants = uow.RestaurantRepository.GetAll();

                // Assert
                Assert.Equal("new rest", restaurant.Name); 
                Assert.Equal(3, allRestaurants.Count());
            }
        }
         
    }
}
