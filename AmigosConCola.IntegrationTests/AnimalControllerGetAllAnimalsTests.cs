using System.Net.Http.Json;
using AmigosConCola.WebApi.Presentation;
using FluentAssertions;

namespace AmigosConCola.IntegrationTests;

public class AnimalControllerGetAllAnimalsTests : IClassFixture<TestWebApplicationFactory<Program>>
{
    private readonly TestWebApplicationFactory<Program> _factory;

    public AnimalControllerGetAllAnimalsTests(TestWebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async void Test_getting_all_animals_on_an_empty_database_returns_an_empty_list()
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync("/api/animals?page=1&perPage=10");

        // Assert
        response.EnsureSuccessStatusCode();

        var animals = await response.Content.ReadFromJsonAsync<IEnumerable<AnimalResponse>>();
        animals.Should().BeEmpty();
    }

    [Fact]
    public async void Test_getting_all_animals_on_an_empty_database_with_no_parameters_returns_an_empty_list()
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync("/api/animals");

        // Assert
        response.EnsureSuccessStatusCode();

        var animals = await response.Content.ReadFromJsonAsync<IEnumerable<AnimalResponse>>();
        animals.Should().BeEmpty();
    }
}