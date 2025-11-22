using Factory.Data.Context;
using Factory.Data.Modules;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Factory.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ExpensesController : ControllerBase
{
      private readonly AppDBContext dbContext;
      public ExpensesController(AppDBContext dBContext)
      {
            this.dbContext = dBContext;
      }

      [HttpGet("{id}")]
      public async Task<ActionResult<Expenses>> GetExpenses(int id)
      {
            var tmp = dbContext.Set<Expenses>().FirstOrDefault(x => x.Id == id);
            if (tmp == null)
                  return NotFound();
            return Ok(tmp);

      }

      [HttpPost]
      public async Task<ActionResult<Expenses>> AddExpenses(Expenses expenses)
      {
            var tmp = new Expenses
            {
                  Details = expenses.Details,
                  dateTime = expenses.dateTime,
                  Amount = expenses.Amount
            };

            dbContext.Expenses.Add(tmp);
            await dbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetExpenses), new { id = tmp.Id }, tmp);
      }

      [HttpGet]
      public async Task<ActionResult<List<Expenses>>> GetAllExpenses()
      {
            var tmp = await dbContext.Expenses.ToListAsync();
            if (tmp == null)
                  return NotFound();
            return Ok(tmp);
      }

      [HttpDelete("{id}")]
      public async Task<IActionResult> DeleteExpenses(int id)
      {
            var tmp = await dbContext.Set<Expenses>().FindAsync(id);
            if (tmp == null)
                  return NotFound();
            dbContext.Expenses.Remove(tmp);
            await dbContext.SaveChangesAsync();
            return NoContent();

      }

}
