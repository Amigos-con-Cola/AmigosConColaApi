using System.Net.Http.Json;
using AmigosConCola.IntegrationTests.FakeData;
using AmigosConCola.WebApi.Presentation;
using FluentAssertions;

namespace AmigosConCola.IntegrationTests.AnimalControllerTests;

public class AnimalControllerCreateAnimalsTests : IClassFixture<TestWebApplicationFactory<Program>>
{
    private readonly TestWebApplicationFactory<Program> _factory;

    public AnimalControllerCreateAnimalsTests(TestWebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async void Test_creating_an_animal_with_correct_data_works()
    {
        // Arrange
        var client = _factory.CreateClient();
        var payload = CreateAnimalRequestFake.Get();

        // Act
        var response = await client.PostAsJsonAsync("/api/animals", payload);

        // Assert
        response.EnsureSuccessStatusCode();

        var animal = await response.Content.ReadFromJsonAsync<AnimalResponse>();
        animal?.Name.Should().Be(payload.Name);
        animal?.Code.Should().Be(payload.Code);
    }

    [Fact]
    public async void Test_creating_an_animal_with_null_image_returns_success()
    {
        // Arrange
        var client = _factory.CreateClient();
        var payload = CreateAnimalRequestFake.Get();
        payload.ImageUrl = null;

        // Act
        var response = await client.PostAsJsonAsync("/api/animals", payload);

        // Assert
        response.EnsureSuccessStatusCode();

        var animal = await response.Content.ReadFromJsonAsync<AnimalResponse>();
        animal?.ImageUrl.Should().BeNull();
    }
}