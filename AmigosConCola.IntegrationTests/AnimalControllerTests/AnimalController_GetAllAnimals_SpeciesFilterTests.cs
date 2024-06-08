using System.Net;
using System.Net.Http.Json;
using AmigosConCola.IntegrationTests.FakeData;
using AmigosConCola.WebApi.Data.Database;
using AmigosConCola.WebApi.Presentation;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AmigosConCola.IntegrationTests.AnimalControllerTests;

public class AnimalController_GetAllAnimals_SpeciesFilterTests : IClassFixture<TestWebApplicationFactory<Program>>,
    IDisposable
{
    private readonly TestWebApplicationFactory<Program> _factory;

    public AnimalController_GetAllAnimals_SpeciesFilterTests(TestWebApplicationFactory<Program> factory)
    {
        _factory = factory;

        using var scope = _factory.Services.CreateScope();
        var scopedServices = scope.ServiceProvider;
        var db = scopedServices.GetRequiredService<ApplicationDbContext>();

        var dogs = Enumerable.Range(0, 4).Select(_ => AnimalDtoFake.Get(species: "Dog")).ToList();
        var cats = Enumerable.Range(0, 4).Select(_ => AnimalDtoFake.Get(species: "Cat")).ToList();

        db.Animals.AddRange(dogs);
        db.Animals.AddRange(cats);

        db.SaveChanges();
    }

    public void Dispose()
    {
        using var scope = _factory.Services.CreateScope();
        var scopedServices = scope.ServiceProvider;
        var db = scopedServices.GetRequiredService<ApplicationDbContext>();
        db.Database.ExecuteSqlRaw("delete from animals");
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

        var dogs = await dogsResponse.Content.ReadFromJsonAsync<PaginatedDataResponse<AnimalResponse>>();
        var cats = await catsResponse.Content.ReadFromJsonAsync<PaginatedDataResponse<AnimalResponse>>();

        dogs?.TotalItems.Should().Be(8);
        cats?.TotalItems.Should().Be(8);

        dogs?.TotalPages.Should().Be(1);
        cats?.TotalPages.Should().Be(1);

        dogs?.Data.Should().HaveCount(4);
        cats?.Data.Should().HaveCount(4);
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