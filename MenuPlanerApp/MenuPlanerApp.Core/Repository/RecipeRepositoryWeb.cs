using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using MenuPlanerApp.Core.Model;
using Newtonsoft.Json;

namespace MenuPlanerApp.Core.Repository
{
    public class RecipeRepositoryWeb
    {
        private readonly string _httpServerUri = "http://192.168.1.9:5000/api/Recipes/";

        public async Task<List<Recipe>> GetAllRecipes()
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var responseMessage = await httpClient.GetAsync(_httpServerUri);
                if (!responseMessage.IsSuccessStatusCode) return null;

                var jsonResult = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
                var recipes = JsonConvert.DeserializeObject<IEnumerable<Recipe>>(jsonResult);
                return recipes.ToList();
            }
        }

        public async Task<Recipe> GetRecipeById(int id)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var responseMessage = await httpClient.GetAsync(_httpServerUri + id);
                if (!responseMessage.IsSuccessStatusCode) return null;

                var jsonResult = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
                var recipe = JsonConvert.DeserializeObject<Recipe>(jsonResult);
                return recipe;
            }
        }

        public async Task PostRecipe(Recipe newRecipe)
        {
            var serializedRecipe = await Task.Run(() => JsonConvert.SerializeObject(newRecipe, Formatting.Indented));
            var httpContent = new StringContent(serializedRecipe, Encoding.UTF8, "application/json");

            using (var httpClient = new HttpClient())
            {
                var responseMessage = await httpClient.PostAsync(_httpServerUri, httpContent);

                if (responseMessage.Content != null)
                {
                    var responseContent = await responseMessage.Content.ReadAsStringAsync();
                }
            }
        }

        public async Task UpdateRecipe(Recipe updatedRecipe)
        {
            var serializedRecipe = await Task.Run(() => JsonConvert.SerializeObject(updatedRecipe));
            var httpContent = new StringContent(serializedRecipe, Encoding.UTF8, "application/json");

            using (var httpClient = new HttpClient())
            {
                var responseMessage = await httpClient.PutAsync(_httpServerUri + updatedRecipe.Id, httpContent);

                if (responseMessage.Content != null)
                {
                    var responseContent = await responseMessage.Content.ReadAsStringAsync();
                }
            }
        }

        public async Task DeleteRecipeById(int id)
        {
            using (var httpClient = new HttpClient())
            {
                var responseMessage = await httpClient.DeleteAsync(_httpServerUri + id);

                if (responseMessage.Content != null)
                {
                    var responseContent = await responseMessage.Content.ReadAsStringAsync();
                }
            }
        }
    }
}