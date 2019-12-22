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
    public class MenuPlansController : ControllerBase
    {
        private readonly MenuPlanerAppAPIContext _context;

        public MenuPlansController(MenuPlanerAppAPIContext context)
        {
            _context = context;
        }

        // GET: api/MenuPlans
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MenuPlan>>> GetMenuPlan()
        {
            return await _context.MenuPlan
                .Include(a => a.RecipesWithAmounts)
                .ThenInclude(r => r.Recipe).ToListAsync();
        }

        // GET: api/MenuPlans/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MenuPlan>> GetMenuPlan(int id)
        {
            var menuPlan = await _context.MenuPlan.FindAsync(id);

            if (menuPlan == null) return NotFound();

            return menuPlan;
        }

        // PUT: api/MenuPlans/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMenuPlan(int id, MenuPlan menuPlan)
        {
            var existingMenu = _context.MenuPlan
                .Where(m => m.Id == menuPlan.Id)
                .Include(a => a.RecipesWithAmounts)
                .SingleOrDefault();

            if (existingMenu != null)
            {
                _context.Entry(existingMenu).CurrentValues.SetValues(menuPlan);

                foreach (var existingChild in existingMenu.RecipesWithAmounts.ToList()
                    .Where(existingChild => menuPlan.RecipesWithAmounts.All(c => c.Id != existingChild.Id)))
                    _context.RecipeWithAmount.Remove(existingChild);

                foreach (var childMenuPlan in menuPlan.RecipesWithAmounts)
                {
                    var existingChild = existingMenu.RecipesWithAmounts
                        .SingleOrDefault(r => r.Id == childMenuPlan.Id);

                    if (existingChild != null)
                    {
                        _context.Entry(existingChild).CurrentValues.SetValues(childMenuPlan);
                    }
                    else
                    {
                        var newChild = new RecipeWithAmount
                        {
                            Recipe = childMenuPlan.Recipe,
                            DayOfWeek = childMenuPlan.DayOfWeek,
                            MealDayTime = childMenuPlan.MealDayTime,
                            NumbersOfMeals = childMenuPlan.NumbersOfMeals
                        };
                        existingMenu.RecipesWithAmounts.Add(newChild);
                    }
                }
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MenuPlanExists(id))
                    return NotFound();
                throw;
            }

            return NoContent();
        }

        // POST: api/MenuPlans
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<MenuPlan>> PostMenuPlan(MenuPlan menuPlan)
        {
            var menuP = menuPlan;
            foreach (var recipeWithAmount in menuP.RecipesWithAmounts)
                recipeWithAmount.Recipe = _context.Recipe
                    .Where(r => r.Id == recipeWithAmount.Recipe.Id)
                    .Include(a => a.Ingredients)
                    .ThenInclude(i => i.Ingredient).FirstOrDefault();

            _context.MenuPlan.Add(menuP);

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMenuPlan", new {id = menuPlan.Id}, menuPlan);
        }

        // DELETE: api/MenuPlans/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<MenuPlan>> DeleteMenuPlan(int id)
        {
            var menuPlan = await _context.MenuPlan.FindAsync(id);
            if (menuPlan == null) return NotFound();

            var existingMenuPlan = _context.MenuPlan
                .Where(r => r.Id == id)
                .Include(r => r.RecipesWithAmounts)
                .SingleOrDefault();

            if (existingMenuPlan != null)
            {
                _context.RecipeWithAmount.RemoveRange(existingMenuPlan.RecipesWithAmounts);
                _context.MenuPlan.Remove(existingMenuPlan);
            }

            await _context.SaveChangesAsync();

            return menuPlan;
        }

        private bool MenuPlanExists(int id)
        {
            return _context.MenuPlan.Any(m => m.Id == id);
        }
    }
}