using System;
using Microsoft.EntityFrameworkCore;
using MvcKitchen.Models;

namespace MvcKitchen.Data
{
    public class MvcKitchenContext : DbContext
    {

        public DbSet<Recipe> Recipe { get; set; }

        public DbSet<Product> Product { get; set; }

        public DbSet<RecipeProduct> RecipeProducts { get; set; }

        public MvcKitchenContext(DbContextOptions<MvcKitchenContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RecipeProduct>().HasKey(rp => new { rp.RecipeId, rp.ProductId });
            modelBuilder.Entity<RecipeProduct>()
               .HasOne(rp => rp.Recipe)
               .WithMany(r => r.RecipeProducts)
               .HasForeignKey(rp => rp.RecipeId);
            modelBuilder.Entity<RecipeProduct>()
                .HasOne(rp => rp.Product)
                .WithMany(p => p.RecipeProducts)
                .HasForeignKey(rp => rp.ProductId);
        }

    }
}
