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
    public class IngredientsController : ControllerBase
    {
        private readonly MenuPlanerAppApiContext _context;

        public IngredientsController(MenuPlanerAppApiContext context)
        {
            _context = context;
        }

        // GET: api/Ingredients
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ingredient>>> GetIngredient()
        {
            return await _context.Ingredient.ToListAsync().ConfigureAwait(false);
        }

        // GET: api/Ingredients/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Ingredient>> GetIngredient(int id)
        {
            var ingredient = await _context.Ingredient.FindAsync(id);

            if (ingredient == null) return NotFound();

            return ingredient;
        }

        // PUT: api/Ingredients/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutIngredient(int id, Ingredient ingredient)
        {
            if (id != ingredient.Id) return BadRequest();

            _context.Entry(ingredient).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync().ConfigureAwait(false);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IngredientExists(id))
                    return NotFound();
                throw;
            }

            return NoContent();
        }

        // POST: api/Ingredients
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Ingredient>> PostIngredient(Ingredient ingredient)
        {
            _context.Ingredient.Add(ingredient);
            await _context.SaveChangesAsync().ConfigureAwait(false);

            return CreatedAtAction("GetIngredient", new {id = ingredient.Id}, ingredient);
        }

        // DELETE: api/Ingredients/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Ingredient>> DeleteIngredient(int id)
        {
            var ingredient = await _context.Ingredient.FindAsync(id);
            if (ingredient == null) return NotFound();

            _context.Ingredient.Remove(ingredient);
            await _context.SaveChangesAsync().ConfigureAwait(false);

            return ingredient;
        }

        private bool IngredientExists(int id)
        {
            return _context.Ingredient.Any(e => e.Id == id);
        }
    }
}