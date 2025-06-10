using Npgsql.EntityFrameworkCore.PostgreSQL;
using Microsoft.AspNetCore.Rewrite;
using LetterBoxd3.Configurations;
using LetterBoxdContext;
using LetterBoxdDomain;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Linq;
using LetterBoxd3.Interfaces;
using LetterBoxd3.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using System;

var builder = WebApplication.CreateBuilder(args);

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddEnvironmentVariables()
    .Build();

builder.Configuration.AddConfiguration(configuration);

builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:3000")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IMovieService, MovieService>();
builder.Services.AddScoped<ICommentService, CommentService>();
builder.Services.AddScoped<IRatingService, RatingService>();

builder.Services.AddDbContext<Context>(options =>
    options.UseNpgsql(connectionString)
);

builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("Jwt"));

var jwtKey = builder.Configuration["Jwt:Key"];
var jwtIssuer = builder.Configuration["Jwt:Issuer"];
var jwtAudience = builder.Configuration["Jwt:Audience"];

if (string.IsNullOrEmpty(jwtKey))
{
    throw new ApplicationException("JWT Key is missing in configuration. Please check your appsettings.json file.");
}

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = !string.IsNullOrEmpty(jwtIssuer),
            ValidateAudience = !string.IsNullOrEmpty(jwtAudience),
            ValidateLifetime = true,
            ValidIssuer = jwtIssuer,
            ValidAudience = jwtAudience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
        };
    });

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "LetterBoxd API",
        Version = "v1",
        Description = "API for movie reviews and ratings",
        Contact = new OpenApiContact { Name = "Your Name", Email = "your.email@example.com" }
    });

    var securityScheme = new OpenApiSecurityScheme
    {
        Name = "JWT Authentication",
        Description = "Enter JWT Bearer token **_only_**",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };

    c.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { securityScheme, Array.Empty<string>() }
    });

    c.OperationFilter<SwaggerDefaultValues>();
});

builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

var app = builder.Build();

app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        context.Response.StatusCode = 500;
        context.Response.ContentType = "application/json";

        var response = new
        {
            error = "An unexpected error occurred. Please try again later."
        };

        await context.Response.WriteAsJsonAsync(response);
    });
});

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        c.RoutePrefix = string.Empty;
    });
}

var rewriteOptions = new RewriteOptions()
    .AddRedirect(@"^$", "login")
    .AddRedirect(@"^index\.html$", "login")
    .AddRewrite(@"^login$", "Index.html", skipRemainingRules: true)
    .AddRewrite(@"^register$", "Register.html", skipRemainingRules: true)
    .AddRewrite(@"^movie$", "Movie.html", skipRemainingRules: true)
    .AddRewrite(@"^movies$", "MoviesPreview.html", skipRemainingRules: true);

app.UseRewriter(rewriteOptions);
app.UseDefaultFiles();
app.UseStaticFiles(new StaticFileOptions
{
    OnPrepareResponse = ctx =>
    {
        ctx.Context.Response.Headers["Cache-Control"] = "no-store, no-cache, must-revalidate, proxy-revalidate";
        ctx.Context.Response.Headers["Pragma"] = "no-cache";
        ctx.Context.Response.Headers["Expires"] = "0";
        ctx.Context.Response.Headers["Surrogate-Control"] = "no-store";
    }
});

app.UseCors("AllowFrontend");

string configDir = Path.Combine(Directory.GetCurrentDirectory(), "Configurations");
string bannedWordsPath = Path.Combine(configDir, "banned_words.txt");

if (!File.Exists(bannedWordsPath))
{
    var bannedWordsContent = Environment.GetEnvironmentVariable("BANNED_WORDS_CONTENT");
    if (!string.IsNullOrEmpty(bannedWordsContent))
    {
        Directory.CreateDirectory(configDir);
        File.WriteAllText(bannedWordsPath, bannedWordsContent);
    }
    else
    {
        Console.WriteLine("Warning: 'BANNED_WORDS_CONTENT' environment variable is not set. Banned words file will not be created.");
    }
}


app.UseAuthentication();
app.UseAuthorization();

app.Use(async (context, next) =>
{

    context.Response.Headers["Cache-Control"] = "no-store, no-cache, must-revalidate, proxy-revalidate";
    context.Response.Headers["Pragma"] = "no-cache";
    context.Response.Headers["Expires"] = "0";
    context.Response.Headers["Surrogate-Control"] = "no-store";

    await next();
});

app.MapControllers();

app.Run();