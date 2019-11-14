using DomainModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ServiceLayer.DTO
{
    public class MealCreateRequestDTO
    {
        public int Id { get; set; }
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
    public class MealResponseDTO
    {
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int LocalPriority { get; set; }
        public int GeneralPriority { get; set; }


        public virtual List<MealContent> MealContents { get; set; }
    }

    public class MealTypeResponseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int RestaurantId { get; set; }
    }

    public class MealCategoryResponseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Priority { get; set; }
    }
}
