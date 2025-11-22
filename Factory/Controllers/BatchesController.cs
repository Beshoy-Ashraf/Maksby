using Factory.Data.Context;
using Factory.Data.Modules;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Factory.Controllers;

[Route("api/[controller]")]
[ApiController]

public class BatchesController : ControllerBase
{
      private readonly AppDBContext dbContext;

      public BatchesController(AppDBContext DBcontext)
      {
            this.dbContext = DBcontext;
      }
      [HttpGet("{id}")]
      public async Task<ActionResult<batch>> GetBatch(int id)
      {
            var Batch = await dbContext.Set<batch>().Include(t => t.Materials).Include(t => t.Product).FirstOrDefaultAsync(x => x.Id == id);
            if (Batch == null)
            {
                  return NotFound();
            }
            return Ok(Batch);
      }
      [HttpPost]
      public async Task<ActionResult<int>> AddBatch([FromBody] AddBatchRequest request)
      {
            var materials = await dbContext.Materials.Where(m => request.MaterialIds.Contains(m.id)).ToListAsync();
            var product = await dbContext.Products.FindAsync(request.ProductId);
            if (product == null)
            {
                  return BadRequest("Product not found");
            }
            var tmp = new batch
            {
                  Materials = materials,
                  Product = product,
                  IsRun = request.IsRun,
            };
            dbContext.Batches.Add(tmp);
            await dbContext.SaveChangesAsync();
            return Ok(tmp.Id);
      }

      [HttpPut("{id}")]
      public async Task<IActionResult> UpdateBatch(int id, [FromBody] AddBatchRequest request)
      {
            var existingBatch = await dbContext.Batches.Include(b => b.Materials).FirstOrDefaultAsync(b => b.Id == id);
            if (existingBatch == null)
            {
                  return NotFound();
            }

            var materials = await dbContext.Materials.Where(m => request.MaterialIds.Contains(m.id)).ToListAsync();
            var product = await dbContext.Products.FindAsync(request.ProductId);
            if (product == null)
            {
                  return BadRequest("Product not found");
            }

            existingBatch.Materials = materials;
            existingBatch.Product = product;
            existingBatch.IsRun = request.IsRun;

            await dbContext.SaveChangesAsync();
            return NoContent();
      }

      [HttpDelete("{id}")]
      public async Task<IActionResult> DeleteBatch(int id)
      {
            var batch = await dbContext.Batches.FindAsync(id);
            if (batch == null)
            {
                  return NotFound();
            }

            dbContext.Batches.Remove(batch);
            await dbContext.SaveChangesAsync();
            return NoContent();
      }
}
