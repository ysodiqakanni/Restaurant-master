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
        public int Id { get; set; }
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
    public class AddRestaurantCategoryViewModel
    {
        public string Name { get; set; }
    }

    public class EditRestaurantCategoryViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
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

        [Display(Name = "Meal image")]
        [DataType(DataType.Upload), Required]
        public IFormFile MealImage { get; set; }

        public List<MealContentViewModel> MealContents { get; set; }
    }
    public class MealContentViewModel
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public int Priority { get; set; }
        public int Type { get; set; }

        [DataType(DataType.Upload), Required]
        public IFormFile ImageFile { get; set; }
    }
}
