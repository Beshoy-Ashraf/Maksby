using Factory.Data.Context;
using Factory.Data.Modules;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Factory.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
      private readonly AppDBContext dbContext;

      public ProductController(AppDBContext dbContext)
      {
            this.dbContext = dbContext;
      }

      [HttpGet("all")]
      public async Task<ActionResult<IEnumerable<Product>>> GetAllProducts()
      {
            var products = await dbContext.Products.ToListAsync();
            return Ok(products);
      }

      [HttpGet("{id}")]
      public async Task<ActionResult<Product>> GetProduct(int id)
      {
            var product = await dbContext.Products.FindAsync(id);
            if (product == null)
            {
                  return NotFound();
            }
            return Ok(product);
      }

      [HttpPost]
      public async Task<ActionResult<Product>> AddProduct(Product product)
      {
            var newProduct = new Product()
            {
                  ProductName = product.ProductName,
                  Details = product.Details,
                  PricePerOneKilo = product.PricePerOneKilo,
                  OutgoingProduct = product.OutgoingProduct,
                  IncomingProduct = product.IncomingProduct,
            };
            dbContext.Products.Add(newProduct);
            await dbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetProduct), new { id = newProduct.id }, newProduct);
      }

      [HttpPut("{id}")]
      public async Task<IActionResult> UpdateProduct(int id, Product product)
      {
            if (id != product.id)
            {
                  return BadRequest();
            }

            var existingProduct = await dbContext.Products.FindAsync(id);
            if (existingProduct == null)
            {
                  return NotFound();
            }

            existingProduct.ProductName = product.ProductName;
            existingProduct.Details = product.Details;
            existingProduct.PricePerOneKilo = product.PricePerOneKilo;
            existingProduct.OutgoingProduct = product.OutgoingProduct;
            existingProduct.IncomingProduct = product.IncomingProduct;

            await dbContext.SaveChangesAsync();
            return NoContent();
      }

      [HttpDelete("{id}")]
      public async Task<IActionResult> DeleteProduct(int id)
      {
            var product = await dbContext.Products.FindAsync(id);
            if (product == null)
            {
                  return NotFound();
            }

            dbContext.Products.Remove(product);
            await dbContext.SaveChangesAsync();
            return NoContent();
      }
}
