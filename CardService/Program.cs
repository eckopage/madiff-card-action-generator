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

// Configure Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "CardService API", Version = "v1" });
});

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
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();