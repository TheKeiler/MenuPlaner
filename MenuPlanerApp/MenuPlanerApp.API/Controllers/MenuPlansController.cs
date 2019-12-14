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
            return await _context.MenuPlan.ToListAsync();
        }

        // GET: api/MenuPlans/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MenuPlan>> GetMenuPlan(int id)
        {
            var menuPlan = await _context.MenuPlan.FindAsync(id);

            if (menuPlan == null)
            {
                return NotFound();
            }

            return menuPlan;
        }

        // PUT: api/MenuPlans/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMenuPlan(int id, MenuPlan menuPlan)
        {
            if (id != menuPlan.Id)
            {
                return BadRequest();
            }

            _context.Entry(menuPlan).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MenuPlanExists(id))
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

        // POST: api/MenuPlans
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<MenuPlan>> PostMenuPlan(MenuPlan menuPlan)
        {
            _context.MenuPlan.Add(menuPlan);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMenuPlan", new { id = menuPlan.Id }, menuPlan);
        }

        // DELETE: api/MenuPlans/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<MenuPlan>> DeleteMenuPlan(int id)
        {
            var menuPlan = await _context.MenuPlan.FindAsync(id);
            if (menuPlan == null)
            {
                return NotFound();
            }

            _context.MenuPlan.Remove(menuPlan);
            await _context.SaveChangesAsync();

            return menuPlan;
        }

        private bool MenuPlanExists(int id)
        {
            return _context.MenuPlan.Any(e => e.Id == id);
        }
    }
}
