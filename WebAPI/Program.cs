using Common.Mapping;
using Domain.Entities.Account;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Persistence;
using Serilog;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

var configuration = new ConfigurationBuilder()
  .AddJsonFile("appsettings.json")
  .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json")
  .AddEnvironmentVariables()
  .Build();

var maindbConnectionString = builder.Configuration.GetConnectionString("MainDB");
builder.Services.AddDbContext<BookishDbContext>(options =>
{
    options.UseMySql(maindbConnectionString, ServerVersion.AutoDetect(maindbConnectionString));
});

Log.Logger = new LoggerConfiguration()
                    .ReadFrom.Configuration(configuration)
                    .CreateLogger();

builder.Services.AddControllers(options =>
options.ModelValidatorProviders.Clear())
    .AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Bookish API", Version = "V1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header using the Bearer scheme. r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      Example: 'Bearer 12345abcdef'",
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
        new List<string>() }
    });
});

builder.Host.UseSerilog();
builder.Services.AddMemoryCache();

builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration["jwt_valid_audience"],
        ValidIssuer = builder.Configuration["jwt_valid_issuer"],
        ClockSkew = TimeSpan.Zero,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["jwt_secret"]))
    };
});

builder.Services.AddMiniProfiler(options =>
{
    options.RouteBasePath = "/profiler";
    options.IgnoredPaths.Add("/css");
    options.IgnoredPaths.Add("/js");
    options.IgnoredPaths.Add("/index.html");
}).AddEntityFramework();

builder.Services.AddIdentity<Account, IdentityRole>(
             options =>
             {
                 options.Stores.MaxLengthForKeys = 128;
                 options.Password.RequireDigit = false;
                 options.Password.RequiredLength = 5;
                 options.Password.RequireNonAlphanumeric = false;
                 options.Password.RequireUppercase = false;
                 options.Password.RequireLowercase = false;
             })
             .AddEntityFrameworkStores<BookishDbContext>()
             .AddDefaultTokenProviders();
builder.Services.Configure<DataProtectionTokenProviderOptions>(option =>
 option.TokenLifespan = TimeSpan.FromHours(1));

builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddCors(o =>
{
    o.AddPolicy("AllowAll", builder =>
     builder.AllowAnyOrigin()
     .AllowAnyMethod()
     .AllowAnyHeader());
});


var app = builder.Build();

app.UseForwardedHeaders();

if (app.Environment.IsDevelopment())
{
    app.UseMiniProfiler();
    app.UseDeveloperExceptionPage();
}

app.UseDeveloperExceptionPage();

app.Urls.Add("http://*:5000");

app.UseSwagger();
app.UseSwaggerUI(c => 
c.SwaggerEndpoint("/swagger/v1/swagger.json", "Bookish API")
);

app.UseStaticFiles();
app.UseSerilogRequestLogging();

app.UseCors("AllowAll");


app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

Console.WriteLine("Finished starting");

app.Run();
