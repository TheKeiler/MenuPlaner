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
        private const string MediaTypeWithQualityHeaderValueText = "application/json";
        private const string HttpServerUri = "http://192.168.1.9:5000/api/Recipes/";

        public static async Task<List<Recipe>> GetAllRecipes()
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaTypeWithQualityHeaderValueText));

                var responseMessage = await httpClient.GetAsync(HttpServerUri);
                if (!responseMessage.IsSuccessStatusCode) return null;

                var jsonResult = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
                var recipes = JsonConvert.DeserializeObject<IEnumerable<Recipe>>(jsonResult);
                return recipes.ToList();
            }
        }

        public static async Task<Recipe> PostRecipe(Recipe newRecipe)
        {
            var serializedRecipe = await Task.Run(() => JsonConvert.SerializeObject(newRecipe, Formatting.Indented));
            var httpContent = new StringContent(serializedRecipe, Encoding.UTF8, MediaTypeWithQualityHeaderValueText);

            using (var httpClient = new HttpClient())
            {
                var responseMessage = await httpClient.PostAsync(HttpServerUri, httpContent);

                if (responseMessage.Content == null) return null;
                var responseContent = await responseMessage.Content.ReadAsStringAsync();
                var recipe = JsonConvert.DeserializeObject<Recipe>(responseContent);
                return recipe;
            }
        }

        public static async Task UpdateRecipe(Recipe updatedRecipe)
        {
            var serializedRecipe = await Task.Run(() => JsonConvert.SerializeObject(updatedRecipe));
            var httpContent = new StringContent(serializedRecipe, Encoding.UTF8, MediaTypeWithQualityHeaderValueText);

            using (var httpClient = new HttpClient())
            {
                var responseMessage = await httpClient.PutAsync(HttpServerUri + updatedRecipe.Id, httpContent);

                if (responseMessage.Content != null)
                {
                    var responseContent = await responseMessage.Content.ReadAsStringAsync();
                }
            }
        }

        public static async Task DeleteRecipeById(int id)
        {
            using (var httpClient = new HttpClient())
            {
                var responseMessage = await httpClient.DeleteAsync(HttpServerUri + id);

                if (responseMessage.Content != null)
                {
                    var responseContent = await responseMessage.Content.ReadAsStringAsync();
                }
            }
        }
    }
}