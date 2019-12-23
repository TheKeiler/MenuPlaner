using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using MenuPlanerApp.Core.Model;
using Newtonsoft.Json;

namespace MenuPlanerApp.Core.Repository
{
    public class UserOptionsRepositoryWeb
    {
        private const string HttpServerUri = "http://192.168.1.9:5000/api/UserOptions/";
        private const string MediaTypeWithQualityHeaderValueText = "application/json";

        public static async Task<UserOptions> GetOptionById(int id)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaTypeWithQualityHeaderValueText));

                var responseMessage = await httpClient.GetAsync(HttpServerUri + id);
                if (!responseMessage.IsSuccessStatusCode) return null;

                var jsonResult = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
                var option = JsonConvert.DeserializeObject<UserOptions>(jsonResult);
                return option;
            }
        }

        public static async Task PostOption(UserOptions newUserOptions)
        {
            var serializedOption = await Task.Run(() => JsonConvert.SerializeObject(newUserOptions));
            var httpContent = new StringContent(serializedOption, Encoding.UTF8, MediaTypeWithQualityHeaderValueText);

            using (var httpClient = new HttpClient())
            {
                var responseMessage = await httpClient.PostAsync(HttpServerUri, httpContent);

                if (responseMessage.Content != null)
                {
                    var responseContent = await responseMessage.Content.ReadAsStringAsync();
                }
            }
        }

        public static async Task UpdateOption(UserOptions updatedUserOptions)
        {
            var serializedOption = await Task.Run(() => JsonConvert.SerializeObject(updatedUserOptions));
            var httpContent = new StringContent(serializedOption, Encoding.UTF8, MediaTypeWithQualityHeaderValueText);

            using (var httpClient = new HttpClient())
            {
                var responseMessage = await httpClient.PutAsync(HttpServerUri + updatedUserOptions.Id, httpContent);

                if (responseMessage.Content != null)
                {
                    var responseContent = await responseMessage.Content.ReadAsStringAsync();
                }
            }
        }
    }
}