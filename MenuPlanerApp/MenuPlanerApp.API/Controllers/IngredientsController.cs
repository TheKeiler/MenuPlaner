using MenuPlanerApp.API.Repository;
using Microsoft.AspNetCore.Mvc;

namespace MenuPlanerApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IngredientsController : ControllerBase
    {
        private readonly IngredientsRepository _ingredientsRepository;

        public IngredientsController(IngredientsRepository ingredientsRepository)
        {
            _ingredientsRepository = ingredientsRepository;
        }

        [HttpGet]
        public IActionResult GetAllIngredients()
        {
            return Ok(_ingredientsRepository.GetAllIngredients());
        }

        [HttpGet("{id}")]
        public IActionResult GetIngredientById(int id)
        {
            return Ok(_ingredientsRepository.GetIngredientById(id));
        }
    }
}