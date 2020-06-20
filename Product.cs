using System;
using System.Collections.Generic;

namespace MvcKitchen.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Measure { get; set; }

        public List<RecipeProduct> RecipeProducts { get; set; }
    }
}
