using AmigosConCola.Core.Repositories;
using AmigosConCola.Core.UseCases;
using AmigosConCola.WebApi.Data.Database;
using AmigosConCola.WebApi.Data.Repository;
using AmigosConCola.WebApi.Presentation;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(x =>
{
    x.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddCors();

// Animales
builder.Services.AddScoped<IAnimalRepository, AnimalRepository>();
builder.Services.AddScoped<CreateAnimalUseCase>();
builder.Services.AddScoped<GetAllAnimalsUseCase>();
builder.Services.AddScoped<GetAnimalByIdUseCase>();
builder.Services.AddScoped<CountAllAnimalsUseCase>();

// Vacunaciones
builder.Services.AddScoped<IVacunacionRepository, VacunacionRepository>();
builder.Services.AddScoped<CreateVacunacionUseCase>();
builder.Services.AddScoped<FindAllVacunacionesUseCase>();

builder.Services.AddControllers()
    .AddJsonOptions(x =>
    {
        //
        x.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());
    });


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

app.UseCors(x =>
{
    x
        .AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod();
});

app.MapControllers();

app.Run();

public partial class Program
{
}