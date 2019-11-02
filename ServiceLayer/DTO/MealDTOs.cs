using DomainModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ServiceLayer.DTO
{
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

        public List<MealContent> MealContents { get; set; }
    }
    public class MealCategoryCreateRequestDTO
    {
        public string Name { get; set; }
        public int Priority { get; set; }
    } 
}
