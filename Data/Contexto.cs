using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProductsApi.Models;

namespace ProductsApi.Data
{
    public class Contexto : DbContext  // using Microsoft.EntityFrameworkCore;
    {
        public Contexto(DbContextOptions<Contexto> options) : base(options)
        {

        }
protected override void OnModelCreating(ModelBuilder modelBuilder){

    modelBuilder.Entity<Category>()
    .HasMany(c => c.Products)
    .WithOne(a => a.Category)
    .HasForeignKey(a => a.CategoryId);
    
    modelBuilder.Seed();
}

        public DbSet<Category> Categories {get; set;}  //using ProductsApi.Models;
        public DbSet<Product> Products {get; set;}  
    }
}