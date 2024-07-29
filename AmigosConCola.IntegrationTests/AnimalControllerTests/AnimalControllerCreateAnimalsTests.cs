using System.Net.Http.Json;
using AmigosConCola.IntegrationTests.FakeData;
using AmigosConCola.WebApi.Presentation;
using AmigosConCola.WebApi.Presentation.Requests;
using AmigosConCola.WebApi.Presentation.Responses;
using FluentAssertions;
using Xunit.Abstractions;

namespace AmigosConCola.IntegrationTests.AnimalControllerTests;

public class AnimalControllerCreateAnimalsTests : IClassFixture<TestWebApplicationFactory<Program>>
{
    private readonly TestWebApplicationFactory<Program> _factory;
    private readonly ITestOutputHelper _output;

    public AnimalControllerCreateAnimalsTests(TestWebApplicationFactory<Program> factory, ITestOutputHelper output)
    {
        _factory = factory;
        _output = output;
    }

    private HttpContent CreateAnimalRequestToHttpContent(CreateAnimalRequest request)
    {
        var content = new MultipartFormDataContent();

        content.Add(new StringContent(request.Name), "nombre");
        content.Add(new StringContent(request.Age.ToString()), "edad");
        content.Add(new StringContent(request.Gender.ToString()), "genero");
        content.Add(new StringContent(request.Species.ToString()), "especie");
        content.Add(new StringContent(request.Weight.ToString()), "peso");

        if (request.Story is not null)
            content.Add(new StringContent(request.Story), "historia");

        if (request.Location is not null)
            content.Add(new StringContent(request.Location), "ubicacion");

        if (request.Code is not null)
            content.Add(new StringContent(request.Code), "codigo");

        return content;
    }

    [Fact]
    public async void Test_creating_an_animal_with_correct_data_works()
    {
        // Arrange
        var client = _factory.CreateClient();
        var createAnimalRequest = CreateAnimalRequestFake.Get();
        var payload = CreateAnimalRequestToHttpContent(createAnimalRequest);

        // Act
        var response = await client.PostAsync("/api/animals", payload);

        // Assert
        response.EnsureSuccessStatusCode();

        var animal = await response.Content.ReadFromJsonAsync<AnimalResponse>();
        animal?.Name.Should().Be(createAnimalRequest.Name);
        animal?.Code.Should().Be(createAnimalRequest.Code);
    }
}
