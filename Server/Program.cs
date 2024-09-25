using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ServerLibrary.Data;
using ServerLibrary.Helpers;
using ServerLibrary.Repositories.Contracts;
using ServerLibrary.Repositories.Implementations;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<JwtSection>(builder.Configuration.GetSection("JwtSection"));
var jwtSection = builder.Configuration.GetSection(nameof(JwtSection)).Get<JwtSection>();

builder.Services.AddDbContext<AppDbContext>(options=>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default") ??
        throw new InvalidOperationException("Database not found."));
});

//builder.Services.AddSwaggerGen(c =>
//{
//    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Employee Mgmt Blz", Version = "v1" });

//    // Define the API Security scheme
//    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
//    {
//        In = ParameterLocation.Header,
//        Description = "Enter key",
//        Name = "Authorization",
//        Type = SecuritySchemeType.ApiKey,
//        Scheme = "bearer",
//        BearerFormat = "JWT"
//    });

//    // Add security requirement
//    c.AddSecurityRequirement(new OpenApiSecurityRequirement
//    {
//        {
//            new OpenApiSecurityScheme
//            {
//                Reference = new OpenApiReference
//                {
//                    Type = ReferenceType.SecurityScheme,
//                    Id = "Bearer"
//                }
//            },
//            new string[] {}
//        }
//    });
//});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidateLifetime = true,
        ValidIssuer = jwtSection!.Issuer,
        ValidAudience = jwtSection.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSection.Key!))
    };
});

builder.Services.AddScoped<IUserAccount, UserAccountRepository>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazorWasm",
        builder => builder
        .WithOrigins("http://localhost:5104", "https://localhost:7116")
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials());
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowBlazorWasm");

app.UseAuthentication();
app.UseAuthorization();



app.MapControllers();

app.Run();
