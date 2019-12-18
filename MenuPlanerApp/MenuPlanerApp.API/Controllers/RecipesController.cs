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
    public class RecipesController : ControllerBase
    {
        private readonly MenuPlanerAppAPIContext _context;

        public RecipesController(MenuPlanerAppAPIContext context)
        {
            _context = context;
        }

        // GET: api/Recipes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Recipe>>> GetRecipe()
        {
            return await _context.Recipe
                .Include(a => a.Ingredients)
                .ThenInclude(i => i.Ingredient).ToListAsync();
        }

        // GET: api/Recipes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Recipe>> GetRecipe(int id)
        {
            var recipe = await _context.Recipe.FindAsync(id);

            if (recipe == null) return NotFound();

            return recipe;
        }

        // PUT: api/Recipes/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRecipe(int id, Recipe recipe)
        {
            var existingRecipe = _context.Recipe
                .Where(r => r.Id == recipe.Id)
                .Include(r => r.Ingredients)
                .SingleOrDefault();

            if (existingRecipe != null)
            {
                _context.Entry(existingRecipe).CurrentValues.SetValues(recipe);

                foreach (var existingChild in existingRecipe.Ingredients.ToList()
                    .Where(existingChild => recipe.Ingredients.All(c => c.Id != existingChild.Id)))
                    _context.IngredientWithAmount.Remove(existingChild);

                foreach (var childRecipe in recipe.Ingredients)
                {
                    var existingChild = existingRecipe.Ingredients
                        .SingleOrDefault(i => i.Id == childRecipe.Id);

                    if (existingChild != null)
                    {
                        _context.Entry(existingChild).CurrentValues.SetValues(childRecipe);
                    }
                    else
                    {
                        var newChild = new IngredientWithAmount
                        {
                            Ingredient = childRecipe.Ingredient,
                            Amount = childRecipe.Amount
                        };
                        existingRecipe.Ingredients.Add(newChild);
                    }
                }
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RecipeExists(id))
                    return NotFound();
                throw;
            }

            return NoContent();
        }

        // POST: api/Recipes
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Recipe>> PostRecipe(Recipe recipe)
        {
            var rec = recipe;
            foreach (var ingr in rec.Ingredients)
                ingr.Ingredient = _context.Ingredient.SingleOrDefault(i => i.Id == ingr.Ingredient.Id);
            _context.Recipe.Add(rec);

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRecipe", new {id = recipe.Id}, recipe);
        }

        // DELETE: api/Recipes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Recipe>> DeleteRecipe(int id)
        {
            var recipe = await _context.Recipe.FindAsync(id);
            if (recipe == null) return NotFound();

            var existingRecipe = _context.Recipe
                .Where(r => r.Id == id)
                .Include(r => r.Ingredients)
                .SingleOrDefault();

            if (existingRecipe != null)
            {
                _context.IngredientWithAmount.RemoveRange(existingRecipe.Ingredients);
                _context.Recipe.Remove(existingRecipe);
            }

            await _context.SaveChangesAsync();

            return recipe;
        }

        private bool RecipeExists(int id)
        {
            return _context.Recipe.Any(e => e.Id == id);
        }
    }
}