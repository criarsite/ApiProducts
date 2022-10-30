using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProductsApi.Data;
using ProductsApi.Models;

namespace ProductsApi.Controllers
{



    [Route("Api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly Contexto _context;
        public ProductsController(Contexto contexto)
        {
            _context = contexto;

            _context.Database.EnsureCreated();
        }

        [HttpGet]
        public async Task< IEnumerable<Product>> GetAllProduct()
        {
            return await _context.Products.ToArrayAsync();

        }


        [HttpGet("{id}")]
        public async Task<ActionResult> GetProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);

        }

  



    }
}