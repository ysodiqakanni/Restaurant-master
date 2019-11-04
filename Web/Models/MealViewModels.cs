using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models
{
    public class CreateMealCategoryViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Priority { get; set; }
    }
    public class MealCategoryViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Priority { get; set; }
    }
}


