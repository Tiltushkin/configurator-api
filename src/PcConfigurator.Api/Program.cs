using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PcConfigurator.Api.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
var cfg = builder.Configuration;

// EF Core + Postgres
builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseNpgsql(cfg.GetConnectionString("Postgres")));

// Redis Cache
builder.Services.AddStackExchangeRedisCache(opt =>
{
    opt.Configuration = cfg.GetConnectionString("Redis");
    opt.InstanceName = "pcconfig:";
});

// HealthChecks
builder.Services.AddHealthChecks()
    .AddNpgSql(cfg.GetConnectionString("Postgres")!)
    .AddRedis(cfg.GetConnectionString("Redis")!);

// CORS
builder.Services.AddCors(opt =>
{
    var allowed = cfg.GetSection("Cors:AllowedOrigins").Get<string[]>() ?? new string[] {};
    opt.AddPolicy("frontend", policy =>
    {
        policy.WithOrigins(allowed)
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

// JWT Auth
builder.Services.Configure<JwtOptions>(cfg.GetSection("Jwt"));
builder.Services.AddSingleton<IJwtProvider, JwtProvider>();

var jwt = cfg.GetSection("Jwt").Get<JwtOptions>()!;
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = jwt.Issuer,
            ValidateAudience = true,
            ValidAudience = jwt.Audience,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key)),
            ValidateLifetime = true
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin", policy =>
        policy.RequireAssertion(ctx =>
            ctx.User.HasClaim(c => c.Type == "adm" && c.Value == "true") ||
            ctx.User.IsInRole("Admin")));
});

builder.Services.AddControllers()
    .AddJsonOptions(opts =>
    {
        opts.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
        opts.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
    });

// Swagger
if (cfg.GetValue<bool>("Swagger:Enabled"))
{
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo
        {
            Title = "PC Configurator API",
            Version = "v1"
        });
        var securitySchema = new OpenApiSecurityScheme
        {
            Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
            Name = "Authorization",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.Http,
            Scheme = "bearer",
            BearerFormat = "JWT"
        };
        c.AddSecurityDefinition("Bearer", securitySchema);
        c.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                Array.Empty<string>()
            }
        });
    });
}

var app = builder.Build();

// Auto-create database (demo)
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();
}

app.UseCors("frontend");

if (app.Environment.IsDevelopment() && cfg.GetValue<bool>("Swagger:Enabled"))
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// Health endpoints
app.MapHealthChecks("/health");
app.MapGet("/", () => Results.Redirect("/swagger"));

app.Run();
