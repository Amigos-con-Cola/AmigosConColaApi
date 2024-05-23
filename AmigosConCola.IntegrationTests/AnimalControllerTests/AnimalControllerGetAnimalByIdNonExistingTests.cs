using System.Net;
using FluentAssertions;

namespace AmigosConCola.IntegrationTests.AnimalControllerTests;

public class AnimalControllerGetAnimalByIdNonExistingTests : IClassFixture<TestWebApplicationFactory<Program>>
{
    private readonly TestWebApplicationFactory<Program> _factory;

    public AnimalControllerGetAnimalByIdNonExistingTests(TestWebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async void Test_get_an_animal_by_its_id_returns_404_when_it_doesnt_exist()
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var result = await client.GetAsync("/api/animals/1");

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}