using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MenuPlanerApp.API.Data;
using MenuPlanerApp.API.Model;

namespace MenuPlanerApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserOptionsController : ControllerBase
    {
        private readonly MenuPlanerAppAPIContext _context;

        public UserOptionsController(MenuPlanerAppAPIContext context)
        {
            _context = context;
        }

        // GET: api/UserOptions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserOptions>>> GetUserOptions()
        {
            return await _context.UserOptions.ToListAsync();
        }

        // GET: api/UserOptions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserOptions>> GetUserOptions(int id)
        {
            var userOptions = await _context.UserOptions.FindAsync(id);

            if (userOptions == null)
            {
                return NotFound();
            }

            return userOptions;
        }

        // PUT: api/UserOptions/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserOptions(int id, UserOptions userOptions)
        {
            if (id != userOptions.Id)
            {
                return BadRequest();
            }

            _context.Entry(userOptions).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserOptionsExists(id))
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

        // POST: api/UserOptions
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<UserOptions>> PostUserOptions(UserOptions userOptions)
        {
            _context.UserOptions.Add(userOptions);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserOptions", new { id = userOptions.Id }, userOptions);
        }

        // DELETE: api/UserOptions/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<UserOptions>> DeleteUserOptions(int id)
        {
            var userOptions = await _context.UserOptions.FindAsync(id);
            if (userOptions == null)
            {
                return NotFound();
            }

            _context.UserOptions.Remove(userOptions);
            await _context.SaveChangesAsync();

            return userOptions;
        }

        private bool UserOptionsExists(int id)
        {
            return _context.UserOptions.Any(e => e.Id == id);
        }
    }
}
