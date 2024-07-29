using System.Net.Http.Json;
using AmigosConCola.IntegrationTests.FakeData;
using AmigosConCola.WebApi.Data.Database;
using AmigosConCola.WebApi.Presentation;
using AmigosConCola.WebApi.Presentation.Responses;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
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

        await db.Animals.AddRangeAsync(Enumerable.Range(1, 20).Select(_ => AnimalDtoFake.Get()));
        await db.SaveChangesAsync();

        // Act
        var response = await client.GetAsync($"/api/animals?page={page}&perPage={perPage}");

        // Assert
        response.EnsureSuccessStatusCode();

        var animals = await response.Content.ReadFromJsonAsync<PaginatedDataResponse<AnimalResponse>>();

        animals?.TotalItems.Should().Be(20);
        animals?.TotalPages.Should().Be(2);
        animals?.Data.Should().HaveCount(perPage);

        // Cleanup
        await db.Database.ExecuteSqlRawAsync("delete from animals");
    }
}