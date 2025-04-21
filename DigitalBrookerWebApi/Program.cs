using DigitalBroker.Application.DependencyInjections;
using DigitalBrokker.Infrastructure.DependencyInjections;
using DigitalBrookerWebApi.Extensions;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddInfrastructure(builder.Configuration);//Infrastructure layer dependency injection
builder.Services.AddApplication(builder.Configuration);//application layer dependency injection 
builder.Services.AddIdentityServices(builder.Configuration);// Add Identity services in webapi layer
builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(option=>
    {
        option.Title = "Digital Brooker API";
    });
}

app.UseExceptionHandler(_ => { }); //map the exception handler to the pipeline

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
