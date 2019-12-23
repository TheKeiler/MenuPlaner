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
    public class MenuPlanRepositoryWeb
    {
        private const string HttpServerUri = "http://192.168.1.9:5000/api/MenuPlans/";
        private const string MediaTypeWithQualityHeaderValueText = "application/json";

        public static async Task<List<MenuPlan>> GetAllMenuPlan()
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue(MediaTypeWithQualityHeaderValueText));

                var responseMessage = await httpClient.GetAsync(HttpServerUri);
                if (!responseMessage.IsSuccessStatusCode) return null;

                var jsonResult = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
                var menuPlans = JsonConvert.DeserializeObject<IEnumerable<MenuPlan>>(jsonResult);
                return menuPlans.ToList();
            }
        }

        public static async Task<MenuPlan> GetMenuPlanById(int id)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue(MediaTypeWithQualityHeaderValueText));

                var responseMessage = await httpClient.GetAsync(HttpServerUri + id);
                if (!responseMessage.IsSuccessStatusCode) return null;

                var jsonResult = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
                var menuPlan = JsonConvert.DeserializeObject<MenuPlan>(jsonResult);
                return menuPlan;
            }
        }

        public static async Task<MenuPlan> PostMenuPlan(MenuPlan newMenuPlan)
        {
            var serializedMenuPlan =
                await Task.Run(() => JsonConvert.SerializeObject(newMenuPlan, Formatting.Indented));
            var httpContent = new StringContent(serializedMenuPlan, Encoding.UTF8, MediaTypeWithQualityHeaderValueText);

            using (var httpClient = new HttpClient())
            {
                var responseMessage = await httpClient.PostAsync(HttpServerUri, httpContent);

                if (responseMessage.Content == null) return null;
                var responseContent = await responseMessage.Content.ReadAsStringAsync();
                var menuPlan = JsonConvert.DeserializeObject<MenuPlan>(responseContent);
                return menuPlan;
            }
        }

        public static async Task UpdateMenuPlan(MenuPlan updatedMenuPlan)
        {
            var serializedMenuPlan = await Task.Run(() => JsonConvert.SerializeObject(updatedMenuPlan));
            var httpContent = new StringContent(serializedMenuPlan, Encoding.UTF8, MediaTypeWithQualityHeaderValueText);

            using (var httpClient = new HttpClient())
            {
                var responseMessage = await httpClient.PutAsync(HttpServerUri + updatedMenuPlan.Id, httpContent);

                if (responseMessage.Content != null)
                {
                    var responseContent = await responseMessage.Content.ReadAsStringAsync();
                }
            }
        }

        public static async Task DeleteMenuPlanById(int id)
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