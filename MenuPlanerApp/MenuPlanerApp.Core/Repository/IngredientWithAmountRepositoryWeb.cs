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
    public class IngredientWithAmountRepositoryWeb
    {
        private readonly string _httpServerUri = "http://192.168.1.9:5000/api/IngredientWithAmounts/";

        public async Task<List<IngredientWithAmount>> GetAllIngredientsWithAmounts()
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var responseMessage = await httpClient.GetAsync(_httpServerUri);
                if (!responseMessage.IsSuccessStatusCode) return null;

                var jsonResult = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
                var ingredientsWithAmount =
                    JsonConvert.DeserializeObject<IEnumerable<IngredientWithAmount>>(jsonResult);
                return ingredientsWithAmount.ToList();
            }
        }

        public async Task<List<IngredientWithAmount>> GetIngredientWithAmountForRecipeId(int id)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var responseMessage = await httpClient.GetAsync(_httpServerUri + $"?recipeId={id}");
                if (!responseMessage.IsSuccessStatusCode) return null;

                var jsonResult = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
                var ingredientsWithAmount = JsonConvert.DeserializeObject<IEnumerable<IngredientWithAmount>>(jsonResult);
                return ingredientsWithAmount.ToList();
            }
        }

        public async Task PostIngredientWithAmount(IngredientWithAmount newIngredient)
        {
            var serializedIngredient = await Task.Run(() => JsonConvert.SerializeObject(newIngredient));
            var httpContent = new StringContent(serializedIngredient, Encoding.UTF8, "application/json");

            using (var httpClient = new HttpClient())
            {
                var responseMessage = await httpClient.PostAsync(_httpServerUri, httpContent);

                if (responseMessage.Content != null)
                {
                    var responseContent = await responseMessage.Content.ReadAsStringAsync();
                }
            }
        }

        public async Task UpdateIngredient(IngredientWithAmount updatedIngredient)
        {
            var serializedIngredient = await Task.Run(() => JsonConvert.SerializeObject(updatedIngredient));
            var httpContent = new StringContent(serializedIngredient, Encoding.UTF8, "application/json");

            using (var httpClient = new HttpClient())
            {
                var responseMessage = await httpClient.PutAsync(_httpServerUri + updatedIngredient.Id, httpContent);

                if (responseMessage.Content != null)
                {
                    var responseContent = await responseMessage.Content.ReadAsStringAsync();
                }
            }
        }

        public async Task DeleteIngredientWithAmountById(int id)
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