using System;
namespace MvcKitchen.Models
{
    public class RecipeProduct
    {
        public int RecipeId { get; set; }
        public Recipe Recipe { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
