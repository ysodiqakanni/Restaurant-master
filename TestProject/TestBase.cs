using Data;
using DomainModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TestProject.TestData;

namespace TestProject
{
    public class TestBase
    {
        protected AppDbContext GetSampleData(string db)
        {
            var builder = new DbContextOptionsBuilder<AppDbContext>();
            builder.UseInMemoryDatabase(databaseName: db);

            var options = builder.Options;

            var context = new AppDbContext(options);

            // Get sample data and save them for testing
            var restaurants = new List<Restaurant>();
            var dataFactory = new DataFactory();

            restaurants.Add(dataFactory.GetRestaurant(1, "Pier A", "Manhattan", "9839374828"));
            restaurants.Add(dataFactory.GetRestaurant(2, "Abuja", "Jersey", "9839374828"));

            context.Restaurants.AddRange(restaurants);
            context.SaveChanges();

            return context;
        }
    }
}
