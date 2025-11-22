using Factory.Data.Context;
using Factory.Data.Modules;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Factory.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SellerController : ControllerBase
{
      private readonly AppDBContext dbContext;

      public SellerController(AppDBContext dBContext)
      {
            this.dbContext = dBContext;
      }

      // GET: api/sellers
      [HttpGet]
      public async Task<ActionResult<List<Seller>>> GetSellers()
      {
            var sellers = await dbContext.Set<Seller>().Include(x => x.Account).ToListAsync();
            return Ok(sellers);
      }

      // GET: api/sellers/{id}
      [HttpGet("{id}")]
      public async Task<ActionResult<Seller>> GetSeller(int id)
      {
            var seller = await dbContext.Set<Seller>().Include(t => t.Account).FirstOrDefaultAsync(x => x.Id == id);
            if (seller == null)
            {
                  return NotFound();
            }
            return Ok(seller);
      }

      // POST: api/sellers
      [HttpPost]
      public async Task<ActionResult<Seller>> AddSeller(Seller seller)
      {
            var newSeller = new Seller()
            {
                  SellerName = seller.SellerName,
                  Account = seller.Account,
            };
            dbContext.Sellers.Add(newSeller);
            await dbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetSeller), new { id = newSeller.Id }, newSeller);
      }

      // PUT: api/sellers/{id}
      [HttpPut("{id}")]
      public async Task<IActionResult> UpdateSeller(int id, Seller seller)
      {
            if (id != seller.Id)
            {
                  return BadRequest();
            }

            var existingSeller = await dbContext.Set<Seller>().FindAsync(id);
            if (existingSeller == null)
            {
                  return NotFound();
            }

            existingSeller.SellerName = seller.SellerName;
            // Update Account if provided
            if (seller.Account != null)
            {
                  existingSeller.Account = seller.Account;
            }

            await dbContext.SaveChangesAsync();
            return NoContent();
      }

      // DELETE: api/sellers/{id}
      [HttpDelete("{id}")]
      public async Task<IActionResult> DeleteSeller(int id)
      {
            var seller = await dbContext.Set<Seller>().FindAsync(id);
            if (seller == null)
            {
                  return NotFound();
            }

            dbContext.Sellers.Remove(seller);
            await dbContext.SaveChangesAsync();
            return NoContent();
      }

      // PUT: api/sellers/{id}/pay التحصيل من العميل
      [HttpPut("{id}/CollectMoney")]
      public async Task<IActionResult> SellerPay(int id, [FromBody] decimal amount)
      {
            var seller = await dbContext.Set<Seller>().Include(t => t.Account).FirstOrDefaultAsync(x => x.Id == id);
            if (seller == null)
            {
                  return NotFound();
            }
            if (seller.Account != null)
            {
                  seller.Account.Credit += amount;
            }
            await dbContext.SaveChangesAsync();
            return NoContent();
      }
}
