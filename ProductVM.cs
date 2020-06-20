using Microsoft.AspNetCore.Mvc.Rendering;
using MvcKitchen.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcKitchen.ViewModel
{
    public class ProductVM
    {
        public Product Product { get; set; }
        public IEnumerable<SelectListItem> RecipeList { get; set; }
        public IEnumerable<int> SelectedRecipes { get; set; }
    }
}
