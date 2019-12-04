using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MenuPlanerApp.API.Data;
using MenuPlanerApp.API.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MenuPlanerApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeWithAmountsController : ControllerBase
    {
        private readonly MenuPlanerAppAPIContext _context;

        public RecipeWithAmountsController(MenuPlanerAppAPIContext context)
        {
            _context = context;
        }

        // GET: api/RecipeWithAmounts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RecipeWithAmount>>> GetRecipeWithAmount()
        {
            return await _context.RecipeWithAmount.ToListAsync();
        }

        // GET: api/RecipeWithAmounts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RecipeWithAmount>> GetRecipeWithAmount(int id)
        {
            var recipeWithAmount = await _context.RecipeWithAmount.FindAsync(id);

            if (recipeWithAmount == null) return NotFound();

            return recipeWithAmount;
        }

        // PUT: api/RecipeWithAmounts/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRecipeWithAmount(int id, RecipeWithAmount recipeWithAmount)
        {
            if (id != recipeWithAmount.Id) return BadRequest();

            _context.Entry(recipeWithAmount).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RecipeWithAmountExists(id))
                    return NotFound();
                throw;
            }

            return NoContent();
        }

        // POST: api/RecipeWithAmounts
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<RecipeWithAmount>> PostRecipeWithAmount(RecipeWithAmount recipeWithAmount)
        {
            _context.RecipeWithAmount.Add(recipeWithAmount);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRecipeWithAmount", new {id = recipeWithAmount.Id}, recipeWithAmount);
        }

        // DELETE: api/RecipeWithAmounts/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<RecipeWithAmount>> DeleteRecipeWithAmount(int id)
        {
            var recipeWithAmount = await _context.RecipeWithAmount.FindAsync(id);
            if (recipeWithAmount == null) return NotFound();

            _context.RecipeWithAmount.Remove(recipeWithAmount);
            await _context.SaveChangesAsync();

            return recipeWithAmount;
        }

        private bool RecipeWithAmountExists(int id)
        {
            return _context.RecipeWithAmount.Any(e => e.Id == id);
        }
    }
}