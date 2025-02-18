using CardService.Application;
using CardService.Infrastructure;
using CardService.Api.Mappings;
using CardService.Application.Interfaces;
using CardService.Application.Validators;
using FluentValidation;
using MediatR;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

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

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();