using CardService.Application;
using CardService.Infrastructure;
using CardService.Api.Mappings;
using CardService.Api.Localization;
using CardService.Application.Interfaces;
using CardService.Application.Validators;
using FluentValidation;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Localization;
using System.Globalization;
using Microsoft.Extensions.Localization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Configure Swagger for OpenAPI documentation
builder.Services.AddSwaggerGen(c =>
{
    if (!c.SwaggerGeneratorOptions.SwaggerDocs.ContainsKey("v1")) // Ensure unique SwaggerDoc
    {
        c.SwaggerDoc("v1", new OpenApiInfo
        {
            Title = "Card Service API",
            Version = "v1",
            Description = "API for managing card details and retrieving allowed actions."
        });
    }
    c.EnableAnnotations();
});

// Configure localization using JSON files
builder.Services.AddSingleton<IStringLocalizerFactory, JsonStringLocalizerFactory>();
builder.Services.AddScoped<IStringLocalizer, JsonStringLocalizer>();

// Configure localization
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

// Configure AutoMapper
builder.Services.AddAutoMapper(typeof(CardMappingProfile));

// Register MediatR
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(ICardService).Assembly));

// Register FluentValidation
builder.Services.AddValidatorsFromAssemblyContaining<GetCardDetailsQueryValidator>();

// Register application & infrastructure layers
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices();

var app = builder.Build();

// Configure localization middleware
var supportedCultures = new[] { "en", "pl" };
var localizationOptions = new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture("en"),
    SupportedCultures = supportedCultures.Select(c => new CultureInfo(c)).ToList(),
    SupportedUICultures = supportedCultures.Select(c => new CultureInfo(c)).ToList()
};
app.UseRequestLocalization(localizationOptions);

// Configure Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Card Service API v1"));
}

app.UseAuthorization();

app.MapControllers();

app.Run();