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
        private readonly string _httpServerUri = "http://192.168.1.9:5000/api/ingredients";

        public async Task<List<Ingredient>> GetAllIngredients()
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var responseMessage = await httpClient.GetAsync(_httpServerUri);
                if (!responseMessage.IsSuccessStatusCode) return null;

                var jsonResult = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
                var ingredients = JsonConvert.DeserializeObject<IEnumerable<Ingredient>>(jsonResult);
                return ingredients.ToList();
            }
        }

        public async Task<Ingredient> GetIngredientById(int id)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var responseMessage = await httpClient.GetAsync(_httpServerUri + id);
                if (!responseMessage.IsSuccessStatusCode) return null;

                var jsonResult = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
                var ingredients = JsonConvert.DeserializeObject<Ingredient>(jsonResult);
                return ingredients;
            }
        }

        public async Task<int> GetCountAllIngredient()
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var responseMessage = await httpClient.GetAsync(_httpServerUri);
                if (!responseMessage.IsSuccessStatusCode) return -1;

                var jsonResult = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
                var ingredients = JsonConvert.DeserializeObject<IEnumerable<Ingredient>>(jsonResult);
                return ingredients.Count();
            }
        }

        public async Task PostIngredient(Ingredient newIngredient)
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

        public async Task UpdateIngredient(Ingredient updatedIngredient)
        {
            var serializedIngredient = await Task.Run(() => JsonConvert.SerializeObject(updatedIngredient));
            var httpContent = new StringContent(serializedIngredient, Encoding.UTF8, "application/json");

            using (var httpClient = new HttpClient())
            {
                var responseMessage = await httpClient.PutAsync(_httpServerUri, httpContent);

                if (responseMessage.Content != null)
                {
                    var responseContent = await responseMessage.Content.ReadAsStringAsync();
                }
            }
        }

        public async Task DeleteIngredientById(int id)
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