using Factory.Data.Context;
using Factory.Data.Modules;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Factory.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MaterialsController : ControllerBase
{
      private readonly AppDBContext dbContext;

      public MaterialsController(AppDBContext dbContext)
      {
            this.dbContext = dbContext;
      }

      [HttpGet("all")]
      public async Task<ActionResult<IEnumerable<Material>>> GetAllMaterials()
      {
            var materials = await dbContext.Materials.ToListAsync();
            return Ok(materials);
      }

      [HttpGet("{id}")]
      public async Task<ActionResult<Material>> GetMaterial(int id)
      {
            var material = await dbContext.Materials.FindAsync(id);
            if (material == null)
            {
                  return NotFound();
            }
            return Ok(material);
      }

      [HttpPost]
      public async Task<ActionResult<Material>> AddMaterial(Material material)
      {
            var newMaterial = new Material()
            {
                  MaterialName = material.MaterialName,
                  Details = material.Details,
                  PricePerOneKilo = material.PricePerOneKilo,
                  OutgoingMaterial = material.OutgoingMaterial,
                  IncomingMaterial = material.IncomingMaterial,
            };
            dbContext.Materials.Add(newMaterial);
            await dbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetMaterial), new { id = newMaterial.id }, newMaterial);
      }

      [HttpPut("{id}")]
      public async Task<IActionResult> UpdateMaterial(int id, Material material)
      {
            if (id != material.id)
            {
                  return BadRequest();
            }

            var existingMaterial = await dbContext.Materials.FindAsync(id);
            if (existingMaterial == null)
            {
                  return NotFound();
            }

            existingMaterial.MaterialName = material.MaterialName;
            existingMaterial.Details = material.Details;
            existingMaterial.PricePerOneKilo = material.PricePerOneKilo;
            existingMaterial.OutgoingMaterial = material.OutgoingMaterial;
            existingMaterial.IncomingMaterial = material.IncomingMaterial;

            await dbContext.SaveChangesAsync();
            return NoContent();
      }

      [HttpDelete("{id}")]
      public async Task<IActionResult> DeleteMaterial(int id)
      {
            var material = await dbContext.Materials.FindAsync(id);
            if (material == null)
            {
                  return NotFound();
            }

            dbContext.Materials.Remove(material);
            await dbContext.SaveChangesAsync();
            return NoContent();
      }
}
