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
builder.Services.AddRouting(options => options.LowercaseUrls = true);

// Add services to the container
builder.AddLogics();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
    });

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});


using var loggerFactory = LoggerFactory.Create(b => b.AddConsole());
var config = new MapperConfiguration(cfg => {
    cfg.AddProfile<AutoMapperConfig>();
}, loggerFactory);
IMapper mapper = config.CreateMapper();
builder.Services.AddSingleton(mapper);

builder.Services.AddHttpContextAccessor();
// Register Swagger configuration using the extension method
builder.AddSwaggerInfo();

var app = builder.Build();
// 2. Enable middleware (place AFTER app.Build() and BEFORE app.UseAuthorization())
app.UseCors("AllowAll");


// Enable Swagger UI across ALL environments
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    // Relative path to support hosted sub-paths/Docker dynamically
    c.SwaggerEndpoint("v1/swagger.json", "VRMS API V1");
    c.RoutePrefix = "swagger"; // Serves UI at /swagger
});


app.UseAuthorization();
app.MapControllers();

app.Run();
