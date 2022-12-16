using genericTest.Models;
using Microsoft.Net.Http.Headers;
using System.Text.Json;

namespace genericTest.Services
{
    internal class WebServiceReader<T> : IWebServiceReader<T>
    {
        private readonly string API_URL = "https://openexchangerates.org/api/latest.json?app_id=";
        private readonly string TOKEN_FILE_PATH = "C:\\Users\\Golan\\source\\repos\\genericTest\\genericTest\\token.txt";

        private readonly IHttpClientFactory _httpClientFactory;

        public WebServiceReader(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        async Task<T> IResourceReader<T>.GetValue()
        {
            string token = "";
            if (File.Exists(TOKEN_FILE_PATH))
            {
                token = File.ReadAllText(TOKEN_FILE_PATH);
            }
            var httpRequestMessage = new HttpRequestMessage(
                HttpMethod.Get,
                $"{API_URL}{token}")
            {
                Headers =
            {
                { HeaderNames.Accept, "application/json" }
            }
            };

            var httpClient = _httpClientFactory.CreateClient();
            var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);

            using (var contentStream =
                await httpResponseMessage.Content.ReadAsStreamAsync())
            {
                return await JsonSerializer.DeserializeAsync<T>(contentStream);
            }

        }
    }
}
