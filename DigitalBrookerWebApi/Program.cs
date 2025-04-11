using DigitalBroker.Application.Abstracts;
using DigitalBroker.Application.Services;
using DigitalBrokker.Infrastructure;
using DigitalBrokker.Infrastructure.Options;
using DigitalBrokker.Infrastructure.Processors;
using DigitalBrokker.Infrastructure.Repositories;
using DigitalBrooker.Domain.Entities;
using DigitalBrooker.Domain.Entities.Request;
using DigitalBrookerWebApi.Handlers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddOpenApi();

//Add Database
builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//Add Identity
builder.Services
    .AddIdentity<User, IdentityRole<Guid>>(Opt =>
    {
        Opt.Password.RequireDigit = true;
        Opt.Password.RequireUppercase= true;
        Opt.Password.RequireNonAlphanumeric = true;
        Opt.Password.RequireLowercase= true;
        Opt.Password.RequiredLength = 8;
        Opt.User.RequireUniqueEmail = true;
    })
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

//Configure Jwt
builder.Services.Configure<Jwt>(builder.Configuration.GetSection(Jwt.jwtkey));
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(option =>
{
    var jwtOptions = builder.Configuration.GetSection(Jwt.jwtkey).Get<Jwt>() ?? throw new ArgumentException(nameof(Jwt));
    option.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtOptions.Issuer,
        ValidAudience = jwtOptions.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Secret))
    };

    //where to find the token. Here is in cookies
    option.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            var token = context.Request.Cookies["access_token"]; //token name 
            if (!string.IsNullOrEmpty(token))
            {
                context.Token = token;
            }
            return Task.CompletedTask;
        }
    };
});
//Add Authorization
builder.Services.AddAuthorization();

//Program services
builder.Services.AddScoped<IAuthTokenProcessor, AuthProcessor>(); //<Iterface, class that implements the interface>
builder.Services.AddScoped<IUserRepository, UerRepository>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddHttpContextAccessor(); //to access the http context in the service layer

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(option=>
    {
        option.Title = "Digital Broker API";
    });
}

app.UseExceptionHandler(_ => { }); //map the exception handler to the pipeline

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

//Map the endpoints

app.MapPost("api/account/register", async (IAccountService accountService, RegisterRequest registerRequest) =>
{
    await accountService.RegisterAsync(registerRequest);
    return Results.Ok();
});

app.MapPost("api/account/login", async (IAccountService accountService, LoginRequest loginRequest) =>
{
    await accountService.LoginAsync(loginRequest);
    return Results.Ok();
});

app.MapPost("api/account/refresh", async (IAccountService accountService, HttpContext httpContext ) =>
{
    var refreshToken = httpContext.Request.Cookies["refresh_token"];
    await accountService.RefreshTokenAsync(refreshToken);
    return Results.Ok();
});

app.MapGet("api/movies", () => Results.Ok(new List<string> { "Matrix" })).RequireAuthorization();

app.MapControllers();

app.Run();
