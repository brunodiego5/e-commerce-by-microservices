using System.Text;
using System.Text.Json;

namespace LoyaltyProgramClient
{
    public class LoyaltyProgramHttpClient
    {
        private readonly HttpClient _httpClient;

        public LoyaltyProgramHttpClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<HttpResponseMessage> RegisterUser(string name)
        {
            var user = new {name, Settings = new { }};

            return await _httpClient.PostAsync("/users/",
                CreateBody(user));
        }

        private static StringContent CreateBody(object user)
        {
            return new StringContent(
                JsonSerializer.Serialize(user),
                Encoding.UTF8,
                "application/json");
        }

        public async Task<HttpResponseMessage> UpdateUser(dynamic user) =>
            await _httpClient.PutAsync(
                $"/users/{user.Id}",
                CreateBody(user));

        public async Task<HttpResponseMessage> QueryUser(string arg) =>
            await _httpClient.GetAsync($"/users/{int.Parse(arg)}");
    }
}