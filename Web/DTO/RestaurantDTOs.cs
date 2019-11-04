using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Models;

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
        public int Priority { get; set; }
    }
    public class MealTypeResponseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class ImageResponseDTO
    {
        public int ID { get; set; }
        public int RestaurantId { get; set; }
        public string ImageUrl { get; set; }
        public int ImagePriority { get; set; }
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
