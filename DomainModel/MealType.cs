using System.ComponentModel.DataAnnotations.Schema;

namespace DomainModel
{
    public class MealType:BaseEntity
    {
        public string Name { get; set; }
        
        [ForeignKey("Restaurant")]
        public int RestaurantId { get; set; }
        public virtual Restaurant Restaurant { get; set; }
    }
}