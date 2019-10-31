namespace DomainModel
{
    public class MealContent:BaseEntity
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public int Priority { get; set; }
        public int Type { get; set; }
    }
}