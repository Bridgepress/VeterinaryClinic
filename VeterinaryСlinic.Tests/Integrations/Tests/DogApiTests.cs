using Azure;
using FluentAssertions;
using Newtonsoft.Json;
using System.Net.Http.Json;
using VeterinaryClinic.Core.Exceptions.BadRequest400;
using VeterinaryClinic.Domain.Entities;
using VeterinaryClinic.Domain.Responses;
using VeterinaryClinic.Domain.Responses.Dog;
using static System.Runtime.InteropServices.JavaScript.JSType;

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

        [Fact]
        public async Task CreateDog()
        {
            // arrange
            var dog = new Dog { Id = Guid.NewGuid(), Name = "Bridge", Color = "Red", TailLength = 21, Weight = 100 };
            // act
            var response = await _client.PostAsJsonAsync($"api/Dog", dog);
            var response2 = await _client.GetAsync($"api/Dog/dogs");
            //assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            var content = JsonConvert.DeserializeObject<List<GetAllDogsResponse>>(await response2.Content.ReadAsStringAsync());
            content[2].Name.Should().Be(dog.Name);
        }

        [Fact]
        public async Task TailLengthError()
        {
            // arrange
            var dog = new Dog { Id = Guid.NewGuid(), Name = "Bridge", Color = "Red", TailLength = -21, Weight = 100 };
            // act
            var response = await _client.PostAsJsonAsync($"api/Dog", dog);
            //assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task NameExists()
        {
            // arrange
            var dog = new Dog { Id = Guid.NewGuid(), Name = "Jessy", Color = "Red", TailLength = 21, Weight = 100 };
            // act
            var response = await _client.PostAsJsonAsync($"api/Dog", dog);
            //assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Pagination()
        {
            // arrange
            // act
            var response1 = await _client.GetAsync($"api/Dog/GetAllDogs?PageSize=1&PageNumber=0&SortOrder=true&OrderBy=weight");
            var response2 = await _client.GetAsync($"api/Dog/GetAllDogs?PageSize=1&PageNumber=1&SortOrder=true&OrderBy=weight");
            //assert
            response1.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            response2.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            var content1 = JsonConvert.DeserializeObject<PaginationResponse<GetAllDogsResponse>>(await response1.Content.ReadAsStringAsync());
            var content2 = JsonConvert.DeserializeObject<PaginationResponse<GetAllDogsResponse>>(await response2.Content.ReadAsStringAsync());
            content1.Data.ToList()[0].Name.Should().Be("Jessy"); 
            content2.Data.ToList()[0].Name.Should().Be("Нео");
        }
    }
}
