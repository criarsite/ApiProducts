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
        // Encontrar (Listar todos os produtos)
        [HttpGet]
        public async Task<IEnumerable<Product>> GetAllProduct()
        {
            return await _context.Products.ToArrayAsync();

        }
        // Encontrar produto por ID

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


        // Criar Novo Produto
        [HttpPost]
        public async Task<ActionResult> PostProduct(Product product)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProduct", new { id = product.Id }, product);
        }


        // Atualizar Produto

        [HttpPut("{id}")]
        public async Task<ActionResult> PutProduct(int id, [FromBody] Product product)
        {

            if (id != product.Id)
            {
                return BadRequest();
            }

            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Products.Any(p => p.Id == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }

            }



            return NoContent();

        }
        // Deletar produto por ID
      
      [HttpDelete("{id}")]
      public async Task<ActionResult> DeleteProduct(int id){

        var product = await _context.Products.FindAsync(id);

        if(product == null){
            return NotFound();
        }

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();

        return Ok();
      }
      // Deletar Varios Itens
       [HttpPost]
       [Route("Delete")]

      public async Task<ActionResult> DeleteMultiple([FromQuery]int[] ids)
      {

        var products = new List<Product>();
           foreach (var id in ids)
           {
            var product = await _context.Products.FindAsync(id);

            if(product == null)
            {
                return NotFound();

            }

            products.Add(product);
             
           }
       

        _context.Products.RemoveRange(products);
        await _context.SaveChangesAsync();

        return Ok(products);
      }

    }
}