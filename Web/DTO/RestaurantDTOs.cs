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

    public class RestaurantGetResponseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public Decimal Longitude { get; set; }
        public Decimal Latitude { get; set; }
        public string Description { get; set; }
        public int Priority { get; set; }

        public int RestaurantCategoryId { get; set; }
        public int AreaId { get; set; }

        public string MondayFromHour { get; set; }
        public string MondayFromMinute { get; set; }
        public string MondayToHour { get; set; }
        public string MondayToMinute { get; set; }

        public string TuesdayFromHour { get; set; }
        public string TuesdayFromMinute { get; set; }
        public string TuesdayToHour { get; set; }
        public string TuesdayToMinute { get; set; }

        public string WednesdayFromHour { get; set; }
        public string WednesdayFromMinute { get; set; }
        public string WednesdayToHour { get; set; }
        public string WednesdayToMinute { get; set; }

        public string ThursdayFromHour { get; set; }
        public string ThursdayFromMinute { get; set; }
        public string ThursdayToHour { get; set; }
        public string ThursdayToMinute { get; set; }

        public string FridayFromHour { get; set; }
        public string FridayFromMinute { get; set; }
        public string FridayToHour { get; set; }
        public string FridayToMinute { get; set; }

        public string SaturdayFromHour { get; set; }
        public string SaturdayFromMinute { get; set; }
        public string SaturdayToHour { get; set; }
        public string SaturdayToMinute { get; set; }

        public string SundayFromHour { get; set; }
        public string SundayFromMinute { get; set; }
        public string SundayToHour { get; set; }
        public string SundayToMinute { get; set; }

        public List<ImageViewModel> Images { get; set; }
        public List<WorkingHourViewModel> WorkingHours { get; set; }
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
