using System.Net;
using System.Net.Http.Json;
using AmigosConCola.IntegrationTests.FakeData;
using FluentAssertions;
using Xunit.Abstractions;

namespace AmigosConCola.IntegrationTests.VacunacionControllerTests;

public class
    VacunacionController_CreateVacunacion_WithInvalidAnimalId_Tests : IClassFixture<TestWebApplicationFactory<Program>>
{
    private readonly TestWebApplicationFactory<Program> _factory;
    private readonly ITestOutputHelper _output;

    public VacunacionController_CreateVacunacion_WithInvalidAnimalId_Tests(
        TestWebApplicationFactory<Program> factory,
        ITestOutputHelper output)
    {
        _factory = factory;
        _output = output;
    }

    [Fact]
    public async void Test_creating_a_vacunacion_with_invalid_animal_id_returns_not_found()
    {
        // Arrange
        var request = CreateVacunacionRequestFake.Get();
        var client = _factory.CreateClient();

        // Act
        var response = await client.PostAsJsonAsync("/api/animales/1/vacunaciones", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}