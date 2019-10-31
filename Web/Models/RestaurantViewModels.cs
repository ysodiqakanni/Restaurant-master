using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace Web.Models
{
    public class CreateRestaurantViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public Decimal Longitude { get; set; }
        [Required]
        public Decimal Latitude { get; set; }
        [Required]
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
    public class WorkingHourViewModel
    { 
        public string Day { get; set; }
        public string FromTime { get; set; }
        public string ToTime { get; set; }
    }
    public class ImageViewModel
    { 
        public string ImageUrl { get; set; }
        public int ImagePriority { get; set; }
        public IFormFile File { get; set; }
    }

    public class AddMealViewModel
    {
        [Required]
        [Display(Name = "Meal Name")]
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        [Display(Name = "Local Priority")]
        public int LocalPriority { get; set; }
        [Display(Name = "General Priority")]
        public int GeneralPriority { get; set; }

        [Required, Range(1,int.MaxValue, ErrorMessage ="You must select a Restaurant")]
        public int RestaurantId { get; set; }

        [Required, Range(1, int.MaxValue, ErrorMessage = "You must select a Meal category")]
        public int MealCategoryId { get; set; }

        [Required, Range(1, int.MaxValue, ErrorMessage = "You must select a Meal type")]
        public int MealTypeId { get; set; }

         
    }
}
