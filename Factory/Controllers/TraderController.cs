using Factory.Data.Context;
using Factory.Data.Modules;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Factory.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TraderController : ControllerBase
{
      private readonly AppDBContext dbContext;

      public TraderController(AppDBContext dBContext)
      {
            this.dbContext = dBContext;
      }

      // GET: api/traders
      [HttpGet]
      public async Task<ActionResult<List<Trader>>> GetTraders()
      {
            var traders = await dbContext.Set<Trader>().Include(x => x.Account).ToListAsync();
            return Ok(traders);
      }

      // GET: api/traders/{id}
      [HttpGet("{id}")]
      public async Task<ActionResult<Trader>> GetTrader(int id)
      {
            var trader = await dbContext.Set<Trader>().Include(t => t.Account).FirstOrDefaultAsync(x => x.Id == id);
            if (trader == null)
            {
                  return NotFound();
            }
            return Ok(trader);
      }

      // POST: api/traders
      [HttpPost]
      public async Task<ActionResult<Trader>> AddTrader(Trader trader)
      {
            var newTrader = new Trader()
            {
                  TraderName = trader.TraderName,
                  Account = trader.Account,
            };
            dbContext.Traders.Add(newTrader);
            await dbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetTrader), new { id = newTrader.Id }, newTrader);
      }

      // PUT: api/traders/{id}
      [HttpPut("{id}")]
      public async Task<IActionResult> UpdateTrader(int id, Trader trader)
      {
            if (id != trader.Id)
            {
                  return BadRequest();
            }

            var existingTrader = await dbContext.Set<Trader>().FindAsync(id);
            if (existingTrader == null)
            {
                  return NotFound();
            }

            existingTrader.TraderName = trader.TraderName;
            // Update Account if provided
            if (trader.Account != null)
            {
                  existingTrader.Account = trader.Account;
            }

            await dbContext.SaveChangesAsync();
            return NoContent();
      }

      // DELETE: api/traders/{id}
      [HttpDelete("{id}")]
      public async Task<IActionResult> DeleteTrader(int id)
      {
            var trader = await dbContext.Set<Trader>().FindAsync(id);
            if (trader == null)
            {
                  return NotFound();
            }

            dbContext.Traders.Remove(trader);
            await dbContext.SaveChangesAsync();
            return NoContent();
      }

      // PUT: api/traders/{id}/pay
      [HttpPut("{id}/pay")]
      public async Task<IActionResult> PayForTrader(int id, [FromBody] decimal amount)
      {
            var trader = await dbContext.Set<Trader>().Include(t => t.Account).FirstOrDefaultAsync(x => x.Id == id);
            if (trader == null)
            {
                  return NotFound();
            }
            if (trader.Account != null)
            {
                  trader.Account.Credit += amount;
            }
            await dbContext.SaveChangesAsync();
            return NoContent();
      }
}
