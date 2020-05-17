using Kitchen.Models.Dtos.ProductDtos;
using Kitchen.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kitchen.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductService product;
        public ProductController(ProductService product)
        {
            this.product = product;
        }
        [HttpGet]
        public IActionResult Products()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Products(ProductCreateDto productDto)
        {
            this.product.Create(productDto);

            return RedirectToAction("Index", "Home");
        }
    }
}
