using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainModel
{
    public class Meal:BaseEntity
    {
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int LocalPriority { get; set; }
        public int GeneralPriority { get; set; }

        [ForeignKey("Restaurant")]
        public int? RestaurantId { get; set; }
        public virtual Restaurant Restaurant { get; set; }

        [ForeignKey("MealCategory")]
        public int? MealCategoryId { get; set; }
        public virtual MealCategory MealCategory { get; set; }
         
        [ForeignKey("MealType")]
        public int? MealTypeId { get; set; }
        public virtual MealType MealType { get; set; }

        public virtual List<MealContent> MealContents { get; set; }
    }
}