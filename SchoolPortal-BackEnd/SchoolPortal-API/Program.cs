using System;
using System.IO;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SchoolPortal.Shared.Models;
using SchoolPortal_API.Interfaces;
using SchoolPortal_API.Repositories;
using SchoolPortal_API.Services;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Add services to the container.

// Add DbContext
builder.Services.AddDbContext<SchoolPortalContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")),
    ServiceLifetime.Scoped);

// Register repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();

// Register Unit of Work
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Add AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Register services
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ICompanyService, CompanyService>();
builder.Services.AddScoped<ISchoolService, SchoolService>();

// Register AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Configure JWT authentication
var jwtSettings = builder.Configuration.GetSection("Jwt");
var jwtKey = jwtSettings["Key"] ?? throw new InvalidOperationException("JWT Key is not configured");
var key = Encoding.ASCII.GetBytes(jwtKey);

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"]
    };
});

builder.Services.AddControllers();

// Configure Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo 
    { 
        Title = "School Portal API", 
        Version = "v1",
        Description = "API for School Portal Management System",
        Contact = new OpenApiContact
        {
            Name = "School Portal Support",
            Email = "support@schoolportal.com"
        },
        License = new OpenApiLicense
        {
            Name = "Use under LICX",
            Url = new Uri("https://example.com/license")
        }
    });

    // Add XML comments to Swagger
    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
    
    // Add JWT Authentication to Swagger
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    
    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header,
            },
            new List<string>()
        }
    });
});

var app = builder.Build();

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
// Enable middleware to serve generated Swagger as a JSON endpoint
app.UseSwagger(c =>
{
    c.SerializeAsV2 = true;
});

// Enable middleware to serve swagger-ui
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "School Portal API V1");
    c.RoutePrefix = "swagger"; // Serve the Swagger UI at the root /swagger
    c.DocumentTitle = "School Portal API Documentation";
    c.DefaultModelsExpandDepth(-1); // Hide schemas section by default
    c.DisplayRequestDuration();
    c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
    c.EnableDeepLinking();
    c.EnableFilter();
    c.ShowExtensions();
    
    // Add OAuth2 configuration if needed
    // c.OAuthClientId("swagger-ui");
    // c.OAuthClientSecret("swagger-ui-secret");
    // c.OAuthRealm("swagger-ui-realm");
    // c.OAuthAppName("Swagger UI");
});

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
