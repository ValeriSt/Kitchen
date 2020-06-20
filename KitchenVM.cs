using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using MvcKitchen.Models;

namespace MvcKitchen.ViewModel
{
    public class KitchenVM
    {
        public Recipe Recipe { get; set; }
        public IEnumerable<SelectListItem> ProductList { get; set; }
        public IEnumerable<int> SelectedProducts { get; set; }
    }
}
