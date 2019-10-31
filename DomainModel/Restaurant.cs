using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainModel
{
    public class Restaurant : BaseEntity
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public Decimal Longitude { get; set; }
        public Decimal Latitude { get; set; }
        public string Description { get; set; }
        public int Priority { get; set; }
        public List<Meal> Meals { get; set; }

        public virtual List<RestaurantImage> RestaurantImages { get; set; }

        [ForeignKey("RestaurantCategory")]
        public int? RestaurantCategoryId { get; set; }
        public virtual RestaurantCategory RestaurantCategory { get; set; }

        [ForeignKey("Area")]
        public int? AreaId { get; set; }
        public virtual Area Area { get; set; }

        public virtual List<WorkingHour> WorkingHours { get; set; }
        public virtual List<MealType> MealTypes { get; set; }

    }
}