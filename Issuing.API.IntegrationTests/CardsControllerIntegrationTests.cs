using Issuing.Domain;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;

namespace Issuing.API.IntegrationTests
{
    public class CardsControllerIntegrationTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public CardsControllerIntegrationTests(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }
        [Fact]
        public async Task Create()
        {
            var response = await _client.PostAsJsonAsync("/api/cards", new Card { Bin = "abcdef", CardNo = "123456789" });
            var x = await response.Content.ReadAsStringAsync();

            var card = JsonConvert.DeserializeObject<Card>(x);
        }

        [Fact]
        public async Task GetCards()
        {
            await _client.PostAsJsonAsync("/api/cards", new Card { Bin = "abcdef", CardNo = "123456789" });
            var response = await _client.GetAsync("/api/cards");
            var x = await response.Content.ReadAsStringAsync();

            var cardList = JsonConvert.DeserializeObject<List<Card>>(x);
        }
    }
}
