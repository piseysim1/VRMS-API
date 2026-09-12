using AutoMapper;
using Microsoft.Extensions.Logging;
using vrms_api.Configs;
using vrms_api.Extensions;

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

var builder = WebApplication.CreateBuilder(args);

// Load settings and environment variables
builder.Configuration
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddEnvironmentVariables();

// Add services to the container
builder.AddLogics();
//builder.Services.AddControllers();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
    });

using var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
var config = new MapperConfiguration(cfg => {
    cfg.AddProfile<AutoMapperConfig>();
}, loggerFactory);
IMapper mapper = config.CreateMapper();
builder.Services.AddSingleton(mapper);


// Register Swagger / OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Enable Swagger UI across ALL environments (Development & Production Docker)
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "VRMS API V1");
    c.RoutePrefix = "swagger"; // Serves UI at http://34.28.193.255:8080/swagger
});

app.UseAuthorization();
app.MapControllers();

app.Run();