using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.DTO
{
    public class RestaurantBasicResponseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
    public class MealCategoryResponseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } 
    }
    public class MealTypeResponseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class AreaResponseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class RestaurantCategoryResponseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
