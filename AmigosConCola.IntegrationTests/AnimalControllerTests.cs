using System.Net.Http.Json;
using AmigosConCola.IntegrationTests.FakeData;
using AmigosConCola.WebApi.Presentation;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit.Abstractions;

namespace AmigosConCola.IntegrationTests;

public class AnimalControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;
    private readonly ITestOutputHelper _output;

    public AnimalControllerTests(
        WebApplicationFactory<Program> factory,
        ITestOutputHelper output)
    {
        _factory = factory;
        _output = output;
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
}