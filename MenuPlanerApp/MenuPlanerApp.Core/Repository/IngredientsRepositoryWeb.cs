using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using MenuPlanerApp.Core.Model;
using Newtonsoft.Json;

namespace MenuPlanerApp.Core.Repository
{
    public class IngredientsRepositoryWeb
    {
        public async Task<List<Ingredient>> GetAllIngredients()
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var responseMessage = await httpClient.GetAsync("http://192.168.1.35:5000/api/ingredients");

            if (!responseMessage.IsSuccessStatusCode) return null;

            var jsonResult = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
            var ingredients = JsonConvert.DeserializeObject<IEnumerable<Ingredient>>(jsonResult);
            return ingredients.ToList();
        }

        public async Task<Ingredient> GetIngredientById(int id)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var responseMessage = await httpClient.GetAsync("http://192.168.1.35:5000/api/ingredients/" + id);

            if (!responseMessage.IsSuccessStatusCode) return null;

            var jsonResult = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
            var ingredient = JsonConvert.DeserializeObject<Ingredient>(jsonResult);
            return ingredient;
        }
    }
}