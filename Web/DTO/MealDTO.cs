using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.DTO
{
    public class MealDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }

    public class MealTypeDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int RestaurantId { get; set; }
    }
}
