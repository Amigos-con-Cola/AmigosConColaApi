using System.Net.Http.Json;
using AmigosConCola.IntegrationTests.FakeData;
using AmigosConCola.WebApi.Data.Database;
using AmigosConCola.WebApi.Presentation;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;

namespace AmigosConCola.IntegrationTests.VacunacionControllerTests;

public class
    VacunacionController_CreateVacunacion_WithValidAnimal_Tests : IClassFixture<TestWebApplicationFactory<Program>>
{
    private readonly TestWebApplicationFactory<Program> _factory;

    public VacunacionController_CreateVacunacion_WithValidAnimal_Tests(TestWebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async void Test_creating_a_vacunacion_with_valid_data_returns_the_vacunacion()
    {
        // Arrange
        using var scope = _factory.Services.CreateScope();
        var scopedServices = scope.ServiceProvider;
        var db = scopedServices.GetRequiredService<ApplicationDbContext>();

        var animal = AnimalDtoFake.Get();
        var animalDto = await db.Animals.AddAsync(animal);
        var animalId = animalDto.Entity.Id;
        await db.SaveChangesAsync();

        var request = CreateVacunacionRequestFake.Get();
        var client = _factory.CreateClient();

        // Act
        var response = await client.PostAsJsonAsync($"/api/animales/{animalId}/vacunaciones", request);

        // Assert
        response.EnsureSuccessStatusCode();

        var vacunacion = await response.Content.ReadFromJsonAsync<VacunacionResponse>();
        vacunacion?.IdAnimal.Should().Be(animalId);
        vacunacion?.Name.Should().Be(request.Name);
        vacunacion?.Date.Should().Be(request.Date);
    }
}