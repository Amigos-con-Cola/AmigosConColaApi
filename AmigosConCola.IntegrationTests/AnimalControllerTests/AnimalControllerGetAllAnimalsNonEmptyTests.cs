using System.Net.Http.Json;
using AmigosConCola.IntegrationTests.FakeData;
using AmigosConCola.WebApi.Data.Database;
using AmigosConCola.WebApi.Presentation;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;

namespace AmigosConCola.IntegrationTests.AnimalControllerTests;

public class AnimalControllerGetAllAnimalsNonEmptyTests : IClassFixture<TestWebApplicationFactory<Program>>
{
    private readonly TestWebApplicationFactory<Program> _factory;

    public AnimalControllerGetAllAnimalsNonEmptyTests(TestWebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Theory]
    [InlineData(1, 10)]
    [InlineData(1, 15)]
    public async void Test_getting_all_animals_returns_a_list_of_animals(int page, int perPage)
    {
        // Arrange
        using var scope = _factory.Services.CreateScope();
        var scopedServices = scope.ServiceProvider;
        var db = scopedServices.GetRequiredService<ApplicationDbContext>();

        var client = _factory.CreateClient();
        var animalDtos = Enumerable
            .Range(1, 20)
            .Select(_ => AnimalDtoFake.Get())
            .ToList();

        animalDtos.ForEach(x => db.Animals.Add(x));
        await db.SaveChangesAsync();

        // Act
        var response = await client.GetAsync($"/api/animals?page={page}&perPage={perPage}");

        // Assert
        response.EnsureSuccessStatusCode();

        var animals = await response.Content.ReadFromJsonAsync<IEnumerable<AnimalResponse>>();
        animals.Should().HaveCount(perPage);
    }
}