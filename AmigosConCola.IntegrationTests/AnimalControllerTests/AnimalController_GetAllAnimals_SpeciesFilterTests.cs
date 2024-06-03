using System.Net;
using System.Net.Http.Json;
using AmigosConCola.IntegrationTests.FakeData;
using AmigosConCola.WebApi.Data.Database;
using AmigosConCola.WebApi.Presentation;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;

namespace AmigosConCola.IntegrationTests.AnimalControllerTests;

public class AnimalController_GetAllAnimals_SpeciesFilterTests : IClassFixture<TestWebApplicationFactory<Program>>
{
    private readonly TestWebApplicationFactory<Program> _factory;

    public AnimalController_GetAllAnimals_SpeciesFilterTests(TestWebApplicationFactory<Program> factory)
    {
        _factory = factory;

        using var scope = _factory.Services.CreateScope();
        var scopedServices = scope.ServiceProvider;
        var db = scopedServices.GetRequiredService<ApplicationDbContext>();

        db.Animals.AddRange(Enumerable.Range(0, 4).Select(_ => AnimalDtoFake.Get(species: "Dog")));
        db.Animals.AddRange(Enumerable.Range(0, 4).Select(_ => AnimalDtoFake.Get(species: "Cat")));
        db.SaveChanges();
    }

    [Fact]
    public async void Test_getting_all_animals_with_species_filter_works()
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var dogsResponse = await client.GetAsync("/api/animals?species=Dog");
        var catsResponse = await client.GetAsync("/api/animals?species=Cat");

        // Assert
        dogsResponse.EnsureSuccessStatusCode();
        catsResponse.EnsureSuccessStatusCode();

        var dogs = await dogsResponse.Content.ReadFromJsonAsync<IEnumerable<AnimalResponse>>();
        var cats = await catsResponse.Content.ReadFromJsonAsync<IEnumerable<AnimalResponse>>();

        dogs.Should().HaveCount(4);
        cats.Should().HaveCount(4);
    }

    [Fact]
    public async void Test_providing_an_invalid_species_filter_returns_bad_request()
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync("/api/animals/?species=Giraffe");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
}