using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MenuPlanerApp.API.Data;
using MenuPlanerApp.API.Model;

namespace MenuPlanerApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngredientWithAmountsController : ControllerBase
    {
        private readonly MenuPlanerAppAPIContext _context;

        public IngredientWithAmountsController(MenuPlanerAppAPIContext context)
        {
            _context = context;
        }

        // GET: api/IngredientWithAmounts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<IngredientWithAmount>>> GetIngredientWithAmount()
        {
            return await _context.IngredientWithAmount.ToListAsync();
        }

        // GET: api/IngredientWithAmounts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IngredientWithAmount>> GetIngredientWithAmount(int id)
        {
            var ingredientWithAmount = await _context.IngredientWithAmount.FindAsync(id);

            if (ingredientWithAmount == null)
            {
                return NotFound();
            }

            return ingredientWithAmount;
        }

        // PUT: api/IngredientWithAmounts/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutIngredientWithAmount(int id, IngredientWithAmount ingredientWithAmount)
        {
            if (id != ingredientWithAmount.Id)
            {
                return BadRequest();
            }

            _context.Entry(ingredientWithAmount).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IngredientWithAmountExists(id))
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

        // POST: api/IngredientWithAmounts
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<IngredientWithAmount>> PostIngredientWithAmount(IngredientWithAmount ingredientWithAmount)
        {
            _context.IngredientWithAmount.Add(ingredientWithAmount);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetIngredientWithAmount", new { id = ingredientWithAmount.Id }, ingredientWithAmount);
        }

        // DELETE: api/IngredientWithAmounts/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<IngredientWithAmount>> DeleteIngredientWithAmount(int id)
        {
            var ingredientWithAmount = await _context.IngredientWithAmount.FindAsync(id);
            if (ingredientWithAmount == null)
            {
                return NotFound();
            }

            _context.IngredientWithAmount.Remove(ingredientWithAmount);
            await _context.SaveChangesAsync();

            return ingredientWithAmount;
        }

        private bool IngredientWithAmountExists(int id)
        {
            return _context.IngredientWithAmount.Any(e => e.Id == id);
        }
    }
}
