using AmigosConCola.Core.Repositories;
using AmigosConCola.Core.UseCases;
using AmigosConCola.WebApi.Data.Database;
using AmigosConCola.WebApi.Data.Repository;
using AmigosConCola.WebApi.Presentation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

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
builder.Services.AddAutoMapper(typeof(Program));

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

// Desparasitaciones
builder.Services.AddScoped<IDesparasitacionRepository, DesparasitacionRepository>();
builder.Services.AddScoped<CreateDesparasitacionUseCase>();
builder.Services.AddScoped<FindAllDesparasitacionesUseCase>();

// Aseos
builder.Services.AddScoped<IAseosRepository, AseoRepository>();
builder.Services.AddScoped<CreateAseoUseCase>();

// Pesos
builder.Services.AddScoped<IPesosRepository, PesoRepository>();
builder.Services.AddScoped<CreatePesoUseCase>();

builder.Services.AddControllers()
    .AddJsonOptions(x =>
    {
        //
        x.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());
    });

builder.Services.AddDirectoryBrowser();


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

var fileProvider = new PhysicalFileProvider(Path.Combine(builder.Environment.ContentRootPath, "Images"));
var staticRequestPath = "/images";

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = fileProvider,
    RequestPath = staticRequestPath
});

app.UseDirectoryBrowser(new DirectoryBrowserOptions
{
    FileProvider = fileProvider,
    RequestPath = staticRequestPath
});

app.MapControllers();

app.Run();

public partial class Program
{
}