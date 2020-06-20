using System;
using System.Collections.Generic;

namespace MvcKitchen.Models
{
    public class Recipe
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Preparation { get; set; }

        public List<RecipeProduct> RecipeProducts { get; set; }
     }
}
