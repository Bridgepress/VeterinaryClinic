using FluentAssertions;
using Newtonsoft.Json;
using VeterinaryClinic.Domain.Responses.Dog;

namespace VeterinaryСlinic.Tests.Integrations.Tests
{
    public class DogApiTests : IClassFixture<ApplicationFactory>, IAssemblyFixture<DatabaseFixture>
    {
        private readonly HttpClient _client;

        public DogApiTests(ApplicationFactory app)
        {
            _client = app.CreateClient();
        }

        [Fact]
        public async Task GetAll()
        {
            // arrange
            // act
            var response = await _client.GetAsync($"api/Dog/dogs");
            //assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            var content = JsonConvert.DeserializeObject<List<GetAllDogsResponse>>(await response.Content.ReadAsStringAsync());
            content[0].Name.Should().Be("Нео");
            content[1].Name.Should().Be("Jessy");
        }
    }
}
