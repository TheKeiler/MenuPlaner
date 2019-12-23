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
    public class IngredientsRepositoryWeb
    {
        private const string HttpServerUri = "http://192.168.1.9:5000/api/ingredients/";
        private const string MediaTypeWithQualityHeaderValueText = "application/json";

        public static async Task<List<Ingredient>> GetAllIngredients()
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue(MediaTypeWithQualityHeaderValueText));

                var responseMessage = await httpClient.GetAsync(HttpServerUri);
                if (!responseMessage.IsSuccessStatusCode) return null;

                var jsonResult = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
                var ingredients = JsonConvert.DeserializeObject<IEnumerable<Ingredient>>(jsonResult);
                return ingredients.ToList();
            }
        }

        public static async Task<Ingredient> GetIngredientById(int id)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue(MediaTypeWithQualityHeaderValueText));

                var responseMessage = await httpClient.GetAsync(HttpServerUri + id);
                if (!responseMessage.IsSuccessStatusCode) return null;

                var jsonResult = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
                var ingredients = JsonConvert.DeserializeObject<Ingredient>(jsonResult);
                return ingredients;
            }
        }

        public static async Task PostIngredient(Ingredient newIngredient)
        {
            var serializedIngredient = await Task.Run(() => JsonConvert.SerializeObject(newIngredient));
            var httpContent =
                new StringContent(serializedIngredient, Encoding.UTF8, MediaTypeWithQualityHeaderValueText);

            using (var httpClient = new HttpClient())
            {
                var responseMessage = await httpClient.PostAsync(HttpServerUri, httpContent);

                if (responseMessage.Content != null) await responseMessage.Content.ReadAsStringAsync();
            }
        }

        public static async Task UpdateIngredient(Ingredient updatedIngredient)
        {
            var serializedIngredient = await Task.Run(() => JsonConvert.SerializeObject(updatedIngredient));
            var httpContent =
                new StringContent(serializedIngredient, Encoding.UTF8, MediaTypeWithQualityHeaderValueText);

            using (var httpClient = new HttpClient())
            {
                var responseMessage = await httpClient.PutAsync(HttpServerUri + updatedIngredient.Id, httpContent);

                if (responseMessage.Content != null) await responseMessage.Content.ReadAsStringAsync();
            }
        }

        public static async Task DeleteIngredientById(int id)
        {
            using (var httpClient = new HttpClient())
            {
                var responseMessage = await httpClient.DeleteAsync(HttpServerUri + id);

                if (responseMessage.Content != null) await responseMessage.Content.ReadAsStringAsync();
            }
        }
    }
}