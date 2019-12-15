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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<IngredientWithAmount>>> GetIngredientWithAmountForRecipeId([FromQuery] int recipeId)
        {
            var query = from a in _context.IngredientWithAmount
                join i in _context.Ingredient on a.Ingredient.Id equals i.Id
                where a.RecipeId.Equals(recipeId)
                select new IngredientWithAmount
                {
                    Id = a.Id,
                    Ingredient = i,
                    Amount = a.Amount,
                    RecipeId = a.RecipeId
                };
            var list = await query.ToListAsync();
            return list;
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
