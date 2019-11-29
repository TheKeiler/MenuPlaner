using MenuPlanerApp.Core.Repository;
using Microsoft.AspNetCore.Mvc;

namespace MenuPlaner.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IngredientsController : ControllerBase
    {
        private readonly IngredientsRepository _ingredientsRepository;

        public IngredientsController(IngredientsRepository ingredientsRepository)
        {
            _ingredientsRepository = ingredientsRepository;
        }

        [HttpGet]
        public IActionResult GetAllPies()
        {
            return Ok(_ingredientsRepository.GetAllIngredients());
        }

        [HttpGet("{id}")]
        public IActionResult GetPieById(int id)
        {
            return Ok(_ingredientsRepository.GetIngredientById(id));
        }
    }
}