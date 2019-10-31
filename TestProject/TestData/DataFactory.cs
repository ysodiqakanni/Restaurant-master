using DomainModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestProject.TestData
{
    public class DataFactory
    {
        public Restaurant GetRestaurant(int id, string name, string address, string phone)
        {
            var restaurant = new Restaurant
            {
                Id = id,
                Name = name,
                PhoneNumber = phone,
                Address = address
            };
            return restaurant;
        }
    }
}
