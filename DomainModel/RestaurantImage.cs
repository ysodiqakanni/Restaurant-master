using System.ComponentModel.DataAnnotations;

namespace DomainModel
{
    public class RestaurantImage:BaseEntity
    {
        public Restaurant Restaurant { get; set; }

        [Required]
        [Display(Name ="Imade Url")]
        public string ImageUrl { get; set; }

        [Required]
        [Display(Name = "Priority")]
        public int ImagePriority { get; set; }
    }
}