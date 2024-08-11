using AmigosConCola.WebApi.Data.Database;
using AmigosConCola.WebApi.Extensions;
using AmigosConCola.WebApi.Presentation.Converters;
using Keycloak.AuthServices.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.MapType<DateOnly>(() => new OpenApiSchema
    {
        Type = "string",
        Format = "date"
    });
});

config.AddEnvironmentVariables();

builder.Services.AddDbContext<ApplicationDbContext>(x =>
{
    x.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddCors();
builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddLogin(builder.Configuration);

builder.Services.AddKeycloakWebApiAuthentication(builder.Configuration);
builder.Services.AddAuthorization();

builder.Services.AddStorage();

builder.Services.AddAnimals();
builder.Services.AddVacunaciones();
builder.Services.AddDesparasitaciones();
builder.Services.AddAseos();
builder.Services.AddPesos();
builder.Services.AddInventory();

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


app.Urls.Add("http://0.0.0.0:5000");

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program
{
}