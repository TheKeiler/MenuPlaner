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
        private readonly string _httpServerUri = "http://192.168.1.9:5000/api/UserOptions/";

        public async Task<UserOptions> GetOptionById(int id)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var responseMessage = await httpClient.GetAsync(_httpServerUri + id);
                if (!responseMessage.IsSuccessStatusCode) return null;

                var jsonResult = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
                var option = JsonConvert.DeserializeObject<UserOptions>(jsonResult);
                return option;
            }
        }

        public async Task PostOption(UserOptions newUserOptions)
        {
            var serializedOption = await Task.Run(() => JsonConvert.SerializeObject(newUserOptions));
            var httpContent = new StringContent(serializedOption, Encoding.UTF8, "application/json");

            using (var httpClient = new HttpClient())
            {
                var responseMessage = await httpClient.PostAsync(_httpServerUri, httpContent);

                if (responseMessage.Content != null)
                {
                    var responseContent = await responseMessage.Content.ReadAsStringAsync();
                }
            }
        }

        public async Task UpdateOption(UserOptions updatedUserOptions)
        {
            var serializedOption = await Task.Run(() => JsonConvert.SerializeObject(updatedUserOptions));
            var httpContent = new StringContent(serializedOption, Encoding.UTF8, "application/json");

            using (var httpClient = new HttpClient())
            {
                var responseMessage = await httpClient.PutAsync(_httpServerUri + updatedUserOptions.Id, httpContent);

                if (responseMessage.Content != null)
                {
                    var responseContent = await responseMessage.Content.ReadAsStringAsync();
                }
            }
        }
    }
}