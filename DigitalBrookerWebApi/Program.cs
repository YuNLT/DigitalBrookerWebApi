using DigitalBroker.Application.Abstracts;
using DigitalBroker.Application.DependencyInjections;
using DigitalBroker.Application.Services;
using DigitalBrokker.Infrastructure.DependencyInjections;
using DigitalBrooker.Domain.Constants;
using DigitalBrookerWebApi.Extensions;
using Microsoft.Extensions.Options;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddInfrastructure(builder.Configuration);//Infrastructure layer dependency injection
builder.Services.AddApplication(builder.Configuration);//application layer dependency injection 
builder.Services.AddIdentityServices(builder.Configuration);// Add Identity services in webapi layer
builder.Services.AddAuthorization();

//smtp services
builder.Services.Configure<SmtpInfo>(builder.Configuration.GetSection("SmtpInfo"));
builder.Services.AddSingleton(resolver =>
    resolver.GetRequiredService<IOptions<SmtpInfo>>().Value);
builder.Services.AddScoped<ISmtpEmailService, SmtpEmailService>();

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddOpenApi();

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
