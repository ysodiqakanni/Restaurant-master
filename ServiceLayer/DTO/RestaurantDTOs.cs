using DomainModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ServiceLayer.DTO
{
    public class RestaurantCreateRequestDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [Display(Name ="Phone Number")]
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


        [Required]
        public int RestaurantCategoryId { get; set; }
        [Required]
        public int AreaId { get; set; }

        public List<RestaurantImage> Images { get; set; }
        public List<WorkingHour> WorkingHours { get; set; }
    }
    public class RestaurantResponseDTO
    {
        public int ID { get; set; }
        public string Name { get; set; } 
        public string PhoneNumber { get; set; } 
        public string Address { get; set; } 
        public Decimal Longitude { get; set; } 
        public Decimal Latitude { get; set; } 
        public string Description { get; set; }
        public int Priority { get; set; } 
        public string Area { get; set; }
        public string Category { get; set; }

        public DateTime DateUpdated { get; set; }
        public DateTime DateCreated { get; set; }
        public List<ImageResponseDTO> Images { get; set; }
        public List<WorkingHourResponseDTO> WorkingHours { get; set; } 
    }
    public class RestaurantCategoryUpdateDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }

    public class RestaurantCategoryCreateDTO
    {
        public string Name { get; set; }
    }

    public class ImageResponseDTO
    {
        public int ID { get; set; }
        public string ImageUrl { get; set; } 
        public int ImagePriority { get; set; }
    }
    public class WorkingHourResponseDTO
    {
        public int ID { get; set; }
        public string Day { get; set; }
        public string FromTime { get; set; }
        public string ToTime { get; set; }
    }
   
    public class MealCreateRequestDTO
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

        [Required]
        public int RestaurantId { get; set; }
        [Required]
        public int MealCategoryId { get; set; }
        [Required]
        public int MealTypeId { get; set; }
    }
}
