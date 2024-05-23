using System.Net.Http.Json;
using AmigosConCola.IntegrationTests.FakeData;
using AmigosConCola.WebApi.Data.Database;
using AmigosConCola.WebApi.Presentation;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;

namespace AmigosConCola.IntegrationTests.AnimalControllerTests;

public class AnimalControllerGetAnimalByIdExistingTests : IClassFixture<TestWebApplicationFactory<Program>>
{
    private readonly TestWebApplicationFactory<Program> _factory;

    public AnimalControllerGetAnimalByIdExistingTests(TestWebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async void Test_get_an_animal_by_its_id_returns_the_animal()
    {
        // Arrange
        using var scope = _factory.Services.CreateScope();
        var scopedServices = scope.ServiceProvider;
        var db = scopedServices.GetRequiredService<ApplicationDbContext>();
        var client = _factory.CreateClient();

        var animal = AnimalDtoFake.Get();
        var animalDto = await db.Animals.AddAsync(animal);
        await db.SaveChangesAsync();

        // Act
        var result = await client.GetAsync($"/api/animals/{animalDto.Entity.Id}");

        // Assert
        result.EnsureSuccessStatusCode();

        var animalResult = await result.Content.ReadFromJsonAsync<AnimalResponse>();
        animalResult.Should().NotBeNull();
        animalResult?.Name.Should().Be(animal.Name);
        animalResult?.Code.Should().Be(animal.Code);
        animalResult?.Species.Should().Be(animal.Species);
    }
}