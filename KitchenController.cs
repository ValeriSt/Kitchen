using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using MvcKitchen.Data;
using MvcKitchen.Models;
using MvcKitchen.ViewModel;

namespace MvcKitchen.Controllers
{
    public class KitchenController : Controller
    {
        private readonly MvcKitchenContext _context;

        public KitchenController(MvcKitchenContext context)
        {
            _context = context;
        }

        // GET: Kitchen
        public async Task<IActionResult> Index()
        {
            return View(await _context.Recipe.ToListAsync());
        }

        // GET: Kitchen/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recipe = await _context.Recipe
                .FirstOrDefaultAsync(m => m.Id == id);
            if (recipe == null)
            {
                return NotFound();
            }
            _context.Entry(recipe).Collection(rp => rp.RecipeProducts).Load();
            var viewmodel = new KitchenVM
            {
                Recipe = recipe,
                ProductList = new SelectList(_context.Product, "Id", "Title"),
                SelectedProducts = recipe.RecipeProducts.Select(pr => pr.ProductId)
            };

            return View(viewmodel);
        }

        // GET: Kitchen/Create
        public IActionResult Create()
        {
             return View();
        }

        // POST: Kitchen/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Preparation")] Recipe recipe)
        {
            if (ModelState.IsValid)
            {
                _context.Add(recipe);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(recipe);
        }

        // GET: Kitchen/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recipe = await _context.Recipe.FindAsync(id);
            if (recipe == null)
            {
                return NotFound();
            }
            _context.Entry(recipe).Collection(rp => rp.RecipeProducts).Load();
            var viewmodel = new KitchenVM
            {
                Recipe = recipe,
                ProductList = new SelectList(_context.Product, "Id", "Title"),
                SelectedProducts = recipe.RecipeProducts.Select(pr => pr.ProductId)
            };

            return View(viewmodel);
        }

        // POST: Kitchen/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(KitchenVM viewmodel)
        {
            if (viewmodel.Recipe == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Recipe recipe = viewmodel.Recipe;
                    foreach (int element in viewmodel.SelectedProducts) 
                    {
                        var recipeProduct = new RecipeProduct
                        {
                            RecipeId = recipe.Id,
                            ProductId = Convert.ToInt32(element)
                        };
                        if (_context.RecipeProducts.Find(recipeProduct.RecipeId, recipeProduct.ProductId) == null)
                            _context.Add(recipeProduct);
                    }
                    _context.Update(recipe);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RecipeExists(viewmodel.Recipe.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(viewmodel);
        }

        // GET: Kitchen/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recipe = await _context.Recipe
                .FirstOrDefaultAsync(m => m.Id == id);
            if (recipe == null)
            {
                return NotFound();
            }

            return View(recipe);
        }

        // POST: Kitchen/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var recipe = await _context.Recipe.FindAsync(id);
            _context.Recipe.Remove(recipe);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RecipeExists(int id)
        {
            return _context.Recipe.Any(e => e.Id == id);
        }
    }
}
