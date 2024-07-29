using System.Net.Http.Json;
using AmigosConCola.IntegrationTests.FakeData;
using AmigosConCola.WebApi.Data.Database;
using AmigosConCola.WebApi.Data.Dto;
using AmigosConCola.WebApi.Presentation;
using AmigosConCola.WebApi.Presentation.Responses;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;

namespace AmigosConCola.IntegrationTests.AnimalControllerTests;

public class AnimalControllerNameFilterTests : IClassFixture<TestWebApplicationFactory<Program>>
{
    private readonly TestWebApplicationFactory<Program> _factory;

    public AnimalControllerNameFilterTests(TestWebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async void Test_getting_all_animals_using_the_name_filter_works_for_an_exact_match()
    {
        // Arrange
        using var scope = _factory.Services.CreateScope();
        var scopedServices = scope.ServiceProvider;
        var db = scopedServices.GetRequiredService<ApplicationDbContext>();

        AnimalDto[] animalDtos = [AnimalDtoFake.Get(), AnimalDtoFake.Get()];
        animalDtos[0].Name = "Firulais";
        animalDtos[1].Name = "John";

        await db.Animals.AddRangeAsync(animalDtos);
        await db.SaveChangesAsync();

        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync("/api/animals/?name=Firulais");

        // Assert
        response.EnsureSuccessStatusCode();

        var animals = await response.Content.ReadFromJsonAsync<PaginatedDataResponse<AnimalResponse>>();

        animals?.Data.Count().Should().Be(1);
        animals?.Data.First().Name.Should().Be("Firulais");
    }
}